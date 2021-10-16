using System.Security.Claims;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using DataModels.Interfaces;
using System.Threading.Tasks;
using MedicalLaboratory20.WebAPI.Helpers;
using System.Collections.Generic;

namespace MedicalLaboratory20.WebAPI.Controllers
{
    [Route("auth")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unit;

        public AccountController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token(string login, string password)
        {
            var identity = await GetIdentity(login, password);

            if (identity is null)
            {
                return BadRequest("Invalid login/password");
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthorizationOptions.ISSUER,
                audience: AuthorizationOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.AddSeconds(AuthorizationOptions.LIFETIME),
                signingCredentials: new SigningCredentials(AuthorizationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                accessToken = encodedJwt,
                username = identity.FindFirst("Name").Value.ToString(),
                role = identity.FindFirst("Role").Value.ToString()
            };
            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            var user = await _unit.Users.GetUserAsync(login, password);

            if (user is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim("Name", user.Name),
                    new Claim("Role", user.Role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Identity", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
