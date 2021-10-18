using MedicalLaboratory20.DesktopApp.Services;
using MedicalLaboratory20.DesktopApp.Model.POCO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalLaboratory20.DesktopApp.Services.ApiServices;
using System.Net;
using System.Text.Json;

namespace MedicalLaboratory20.DesktopApp.Model
{
    class Authorization
    {
        #region Singleton

        private static object _syncObj = new object();
        private static Authorization _authorization;
        public static Authorization GetInstance()
        {
            if (_authorization is null)
            {
                lock (_syncObj)
                {
                    if (_authorization is null)
                    {
                        _authorization = new Authorization();
                    }
                }
            }
            return _authorization;
        }

        #endregion

        private bool _isLogin = false;

        private Authorization()
        {
            _configurationService = ConfigurationService.GetInstance();
            _restClient = new RestClient(_configurationService.HttpServerAddress);
        }

        private ConfigurationService _configurationService;
        private RestClient _restClient;

        public event Action SignIn;
        public event Action SignOut;
        public event Action<string> Error;

        public User User { get; private set; }
        public bool IsLogin => _isLogin;

        public async Task<bool> Auth(string login, string password)
        {
            var authService = new AuthorizateService(_restClient);
            var authResponse = await authService.Authorizate(login, password);
            if (authResponse.StatusCode == HttpStatusCode.OK)
            {
                User = JsonSerializer.Deserialize<User>(authResponse.Content);
                SignIn?.Invoke();
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
