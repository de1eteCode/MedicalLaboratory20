using MedicalLaboratory20.DesktopApp.Services;
using MedicalLaboratory20.DesktopApp.Models.POCO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalLaboratory20.DesktopApp.Services.ApiServices;
using System.Net;
using System.Text.Json;

namespace MedicalLaboratory20.DesktopApp.Models
{
    class Client
    {
        #region Singleton

        private static readonly object _syncObj = new();
        private static Client _authorization;
        public static Client GetInstance()
        {
            if (_authorization is null)
            {
                lock (_syncObj)
                {
                    if (_authorization is null)
                    {
                        _authorization = new Client();
                    }
                }
            }
            return _authorization;
        }

        #endregion

        private Client()
        {
            _configurationService = ConfigurationService.GetInstance();
            _restClient = new RestClient(_configurationService.HttpServerAddress);
        }

        private readonly ConfigurationService _configurationService;
        private readonly RestClient _restClient;

        public event Action<string> Error;

        public User User { get; private set; }
        
        public async Task<bool> Auth(string login, string password)
        {
            var authService = new AuthorizateService(_restClient);
            var authResponse = await authService.Authorizate(login, password);
            if (authResponse.StatusCode == HttpStatusCode.OK)
            {
                User = JsonSerializer.Deserialize<User>(authResponse.Content);
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
