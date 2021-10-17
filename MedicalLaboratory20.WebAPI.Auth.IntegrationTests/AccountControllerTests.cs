using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MedicalLaboratory20.WebAPI.Controllers;
using MedicalLaboratory20.WebAPI.JWT;
using MedicalLaboratory20.WebAPI.Models;
using MedicalLaboratory20.WebAPI.Models.Requests;
using MedicalLaboratory20.WebAPI.Models.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MedicalLaboratory20.WebAPI.Auth.IntegrationTests
{
    [TestClass]
    public class AccountControllerTests
    {
        private readonly TestHostFixture _testHostFixture = new TestHostFixture();
        private HttpClient _httpClient;
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void SetUp()
        {
            _httpClient = _testHostFixture.Client;
            _serviceProvider = _testHostFixture.ServiceProvider;
        }

        [TestMethod]
        public async Task ShouldExpect401WhenLoginWithInvalidCredentials()
        {
            var credentials = new LoginRequest
            {
                Login = "admin",
                Password = "invalidPassword"
            };
            var response = await _httpClient.PostAsync("auth/login",
                new StringContent(JsonSerializer.Serialize(credentials), Encoding.UTF8, MediaTypeNames.Application.Json));
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task ShouldReturnCorrectResponseForSuccessLogin()
        {
            var credentials = new LoginRequest
            {
                Login = "chacking0",
                Password = "4tzqHdkqzo4"
            };
            var loginResponse = await _httpClient.PostAsync("auth/login",
                new StringContent(JsonSerializer.Serialize(credentials), Encoding.UTF8, MediaTypeNames.Application.Json));
            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
            var loginResponseContent = await loginResponse.Content.ReadAsStringAsync();
            var loginResult = JsonSerializer.Deserialize<LoginResult>(loginResponseContent);
            Assert.AreEqual(credentials.Login, loginResult.Login);
            Assert.AreEqual("Лаборант", loginResult.Role);
            Assert.IsFalse(string.IsNullOrWhiteSpace(loginResult.AccessToken));
            Assert.IsFalse(string.IsNullOrWhiteSpace(loginResult.RefreshToken));

            var jwtAuthManager = _serviceProvider.GetRequiredService<IJwtAuthManager>();
            var (principal, jwtSecurityToken) = jwtAuthManager.DecodeJwtToken(loginResult.AccessToken);
            Assert.AreEqual(credentials.Login, principal.FindFirst("Login").Value);
            Assert.AreEqual("Лаборант", principal.FindFirst("Role").Value);
            Assert.IsNotNull(jwtSecurityToken);
        }

        [TestMethod]
        public async Task ShouldBeAbleToLogout()
        {
            var credentials = new LoginRequest
            {
                Login = "chacking0",
                Password = "4tzqHdkqzo4"
            };
            var loginResponse = await _httpClient.PostAsync("auth/login",
                new StringContent(JsonSerializer.Serialize(credentials), Encoding.UTF8, MediaTypeNames.Application.Json));
            var loginResponseContent = await loginResponse.Content.ReadAsStringAsync();
            var loginResult = JsonSerializer.Deserialize<LoginResult>(loginResponseContent);

            var jwtAuthManager = _serviceProvider.GetRequiredService<IJwtAuthManager>();
            Assert.IsTrue(jwtAuthManager.UsersRefreshTokens.ContainsKey(loginResult.RefreshToken));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, loginResult.AccessToken);
            var logoutResponse = await _httpClient.PostAsync("auth/logout", null);
            Assert.AreEqual(HttpStatusCode.OK, logoutResponse.StatusCode);
            Assert.IsFalse(jwtAuthManager.UsersRefreshTokens.ContainsKey(loginResult.RefreshToken));
        }

        [TestMethod]
        public async Task ShouldCorrectlyRefreshToken()
        {
            const string login = "chacking0";
            var claims = new[]
            {
                new Claim("Login", login),
                new Claim("Role", "Лаборант")
            };
            var jwtAuthManager = _serviceProvider.GetRequiredService<IJwtAuthManager>();
            var jwtResult = jwtAuthManager.GenerateTokens(login, claims, DateTime.Now.AddMinutes(-1));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtResult.AccessToken);
            var refreshRequest = new RefreshTokenRequest
            {
                RefreshToken = jwtResult.RefreshToken.Token
            };
            var response = await _httpClient.PostAsync("auth/refresh-token",
                new StringContent(JsonSerializer.Serialize(refreshRequest), Encoding.UTF8, MediaTypeNames.Application.Json));
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LoginResult>(responseContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var refreshToken2 = jwtAuthManager.UsersRefreshTokens.GetValueOrDefault(result.RefreshToken);
            Assert.AreEqual(refreshToken2.Token, result.RefreshToken);
            Assert.AreNotEqual(refreshToken2.Token, jwtResult.RefreshToken.Token);
            Assert.AreNotEqual(jwtResult.AccessToken, result.AccessToken);
        }

        [TestMethod]
        public async Task ShouldNotAllowToRefreshTokenWhenRefreshTokenIsExpired()
        {
            const string login = "chacking0";
            var claims = new[]
            {
                new Claim("Login", login),
                new Claim("Role", "Лаборант")
            };
            var jwtAuthManager = _serviceProvider.GetRequiredService<IJwtAuthManager>();
            var jwtTokenConfig = _serviceProvider.GetRequiredService<JwtTokenConfig>();
            var jwtResult1 = jwtAuthManager.GenerateTokens(login, claims, DateTime.Now.AddMinutes(-jwtTokenConfig.RefreshTokenLifetime - 1));
            var jwtResult2 = jwtAuthManager.GenerateTokens(login, claims, DateTime.Now.AddMinutes(-1));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtResult2.AccessToken); // valid JWT token
            var refreshRequest = new RefreshTokenRequest
            {
                RefreshToken = jwtResult1.RefreshToken.Token
            };
            var response = await _httpClient.PostAsync("auth/refresh-token",
                new StringContent(JsonSerializer.Serialize(refreshRequest), Encoding.UTF8, MediaTypeNames.Application.Json)); // expired Refresh token
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("Invalid token", responseContent);
        }
    }
}
