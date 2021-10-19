using SharedModels;
using System;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MedicalLaboratory20.WebAPI.JWT
{
    public interface IJwtAuthManager
    {
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokens { get; }
        JwtAuthResult GenerateTokens(string login, Claim[] claims, DateTime now);
        JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);
        void RemoveExpiredRefreshTokens(DateTime now);
        void RemoveRefreshTokenByLogin(string username);
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
    }
}
