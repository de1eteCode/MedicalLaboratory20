using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using MedicalLaboratory20.DesktopApp.Services.ApiServices;
using MedicalLaboratory20.DesktopApp.Model.POCO;
using RestSharp;
using System.Net;

namespace MedicalLaboratory20.DesktopApp.Services
{
    internal class ClientService
    {
        #region Singleton
        private static object _lock = new object();
        private static ClientService? _userService;
        public static ClientService GetInstance()
        {
            if (_userService is null)
            {
                lock (_lock)
                {
                    if (_userService is null)
                    {
                        _userService = new ClientService();
                    }
                }
            }
            return _userService;
        }
        #endregion

        private bool _isLogin = false;

        private ConfigurationService _configurationService;
        private RestClient _restClient;

        private ClientService()
        {
            _configurationService = ConfigurationService.GetInstance();
            _restClient = new RestClient(_configurationService.HttpServerAddress);

        }

        public event Action SignIn;
        public event Action SignOut;
        public event Action<string> Error;
        public event Action<Exception> Exception;

        public bool IsLogin => _isLogin;
        public User User { get; private set; }

        public void Auth(string login, string password)
        {
            var authResponse = new AuthorizateService(_restClient).Authorizate(login, password);
            if (authResponse.StatusCode == HttpStatusCode.OK)
            {
                User = JsonSerializer.Deserialize<User>(authResponse.Content);
                SignIn?.Invoke();
            }
            else if (authResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                Error?.Invoke(authResponse.Content.ToString());
            }
            else
            {
                Exception?.Invoke(authResponse.ErrorException);
            }
        }
    }
}
