using MedicalLaboratory20.DesktopApp.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalLaboratory20.DesktopApp.Services.ApiServices;
using System.Net;
using System.Text.Json;
using SharedModels;

namespace MedicalLaboratory20.DesktopApp.Models
{
    internal class Client
    {
        #region Singleton

        private static readonly object SyncObj = new();
        private static Client _client;
        public static Client GetInstance()
        {
            if (_client is null)
            {
                lock (SyncObj)
                {
                    if (_client is null)
                    {
                        _client = new Client();
                    }
                }
            }
            return _client;
        }

        #endregion

        private Client()
        {
            _configurationService = ConfigurationService.GetInstance();
            _restClient = new RestClient(_configurationService.HttpServerAddress);
        }

        private readonly ConfigurationService _configurationService;
        private readonly RestClient _restClient;

        public event Action<string>? Error;

        public RestClient RestClient => _restClient;
        public LoginResult? User { get; private set; }
        
        public async Task<bool> Auth(string login, string password)
        {
            var authService = new AuthorizateService(_restClient);
            var authResponse = await authService.Authorizate(login, password);
            if (authResponse.StatusCode == HttpStatusCode.OK)
            {
                User = JsonSerializer.Deserialize<LoginResult>(authResponse.Content);
                return true;
            }
            else if (authResponse.StatusCode == HttpStatusCode.BadRequest ||
                     authResponse.StatusCode == (HttpStatusCode)401)
            {
                Error?.Invoke("Неверный логин/пароль");
            }
            else
            {
                Error?.Invoke(authResponse.ErrorMessage);
            }
            return false;
        }
    }
}
