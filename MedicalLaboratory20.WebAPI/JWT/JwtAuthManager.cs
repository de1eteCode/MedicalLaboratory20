using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using MedicalLaboratory20.WebAPI.Models;
using SharedModels;

namespace MedicalLaboratory20.WebAPI.JWT
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly ConcurrentDictionary<string, RefreshToken> _usersRefreshTokens;
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly byte[] _secret;

        public JwtAuthManager(JwtTokenConfig jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            _secret = Encoding.ASCII.GetBytes(_jwtTokenConfig.Secret);
        }

        public IImmutableDictionary<string, RefreshToken> UsersRefreshTokens => _usersRefreshTokens.ToImmutableDictionary();

        /// <summary>
        /// Очистка истекших токенов
        /// </summary>
        /// <param name="now">Время</param>
        public void RemoveExpiredRefreshTokens(DateTime now)
        {
            var expiredTokens = _usersRefreshTokens.Where(x => x.Value.ExpireAt < now).ToList();
            foreach(var token in expiredTokens)
            {
                _usersRefreshTokens.TryRemove(token.Key, out _);
            }
        }

        /// <summary>
        /// Очистка токена по логину пользователя
        /// </summary>
        /// <param name="login"></param>
        public void RemoveRefreshTokenByLogin(string login)
        {
            var refreshTokens = _usersRefreshTokens.Where(x => x.Value.Login == login).ToList();
            foreach (var token in refreshTokens)
            {
                _usersRefreshTokens.TryRemove(token.Key, out _);
            }
        }

        /// <summary>
        /// Генерация токенов
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="claims">Разрешения</param>
        /// <param name="now">Время</param>
        /// <returns>Пара из accessToken и refreshToken</returns>
        public JwtAuthResult GenerateTokens(string login, Claim[] claims, DateTime now)
        {
            var jwtToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                _jwtTokenConfig.Audience,
                claims,
                expires: now.AddMinutes(_jwtTokenConfig.AccessTokenLifetime),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken()
            {
                Login = login,
                Token = GenerateRefreshToken(),
                ExpireAt = now.AddMinutes(_jwtTokenConfig.RefreshTokenLifetime)
            };
            _usersRefreshTokens.AddOrUpdate(refreshToken.Token, refreshToken, (_, _) => refreshToken);

            return new JwtAuthResult()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        /// <summary>
        /// Обновление токена
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <param name="accessToken">Access token</param>
        /// <param name="now">Время</param>
        /// <returns>Новая пара access|refresh</returns>
        public JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now)
        {
            var (principal, jwtToken) = DecodeJwtToken(accessToken);
            if (jwtToken is null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var login = principal.FindFirst("Login")?.Value;
            if (!_usersRefreshTokens.TryGetValue(refreshToken, out var existingRefreshTokens))
            {
                throw new SecurityTokenException("Invalid token");
            }
            if (existingRefreshTokens.Login != login || existingRefreshTokens.ExpireAt < now)
            {
                throw new SecurityTokenException("Invalid token");
            }

            return GenerateTokens(login, principal.Claims.ToArray(), now);
        }

        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new SecurityTokenException("Invalid token");
            }
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token,
                    new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtTokenConfig.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_secret),
                        ValidateAudience = true,
                        ValidAudience = _jwtTokenConfig.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    },
                    out var validateToken);
            return (principal, validateToken as JwtSecurityToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
