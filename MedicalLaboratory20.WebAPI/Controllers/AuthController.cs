using DataModels.Entities;
using DataModels.Interfaces.IUnitOfWorks;
using MedicalLaboratory20.WebAPI.JWT;
using MedicalLaboratory20.WebAPI.Models.Requests;
using MedicalLaboratory20.WebAPI.Models.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalLaboratory20.WebAPI.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUnitOfAuthUser _unitOfAuthUser;
        private IJwtAuthManager _jwtAuthManager;

        public AuthController(IUnitOfAuthUser unitOfAuthUser, IJwtAuthManager jwtAuthManager)
        {
            _unitOfAuthUser = unitOfAuthUser;
            _jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (ModelState.IsValid is false)
            {
                return BadRequest();
            }

            var user = _unitOfAuthUser.Users.GetUser(loginRequest.Login, loginRequest.Password);

            if (user is null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim("Login", user.Login),
                new Claim("Name", user.Name),
                new Claim("Role", user.Role.Name),
                new Claim("RoleId", user.RoleId.ToString())
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(user.Login, claims, DateTime.Now);

            return Ok(new LoginResult()
            {
                Login = user.Login,
                Name = user.Name,
                Role = user.Role.Name,
                RoleId = user.RoleId.ToString(),
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.Token
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var login = User.FindFirst("Login")?.Value;
            _jwtAuthManager.RemoveRefreshTokenByLogin(login);
            return Ok();
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var login = User.FindFirst("Login")?.Value;

                if (string.IsNullOrEmpty(login))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);

                return Ok(new LoginResult()
                {
                    Login = login,
                    Role = User.FindFirst("Role")?.Value ?? string.Empty,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.Token
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetCurrentUser()
        {
            return Ok(new LoginResult()
            {
                Login = User.FindFirst("Login")?.Value,
                Role = User.FindFirst("Role")?.Value,
                RoleId = User.FindFirst("RoleId")?.Value,
                Name = User.FindFirst("Name").Value
            });
        }
    }
}
