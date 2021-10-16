using DataModels.Entities;
using DataModels.Interfaces.IUnitOfWorks;
using MedicalLaboratory20.WebAPI.Helpers;
using MedicalLaboratory20.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalLaboratory20.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private IUnitOfAuthUser _unitOfAuthUser;

        public AuthController(IOptions<AuthOptions> authOptions, IUnitOfAuthUser unitOfAuthUser)
        {
            _authOptions = authOptions;
            _unitOfAuthUser = unitOfAuthUser;
        }

        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> Token([FromBody] Account accRequest)
        {
            var user = await AuthenticationUser(accRequest.Login, accRequest.Password);

            if (user is not null)
            {
                var accessToken = GenerateJWT(user);
                return Ok(new 
                { 
                    accessToken = accessToken,
                    username = user.Name,
                    role = user.Role.Name
                });
            }

            return Unauthorized();
        }

        private async Task<User> AuthenticationUser(string login, string password)
        {
            return await _unitOfAuthUser.Users.GetUserAsync(login, password);
        }

        private string GenerateJWT(User user)
        {
            var authParams = _authOptions.Value;

            var secretKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("name", user.Name),
                new Claim("role", user.Role.Name)
            };

            var accessToken = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: System.DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }
    }
}
