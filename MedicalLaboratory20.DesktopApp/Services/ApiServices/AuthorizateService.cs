using RestSharp;

namespace MedicalLaboratory20.DesktopApp.Services.ApiServices
{
    sealed class AuthorizateService : ApiService
    {
        public AuthorizateService(IRestClient client) : base(client) { }

        public IRestResponse Authorizate(string login, string password)
        {
            var cfg = ConfigurationService.GetInstance();
            IRestRequest request = new RestRequest($"{cfg.HttpServerAddress}/auth/token", Method.POST, DataFormat.Json);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("login", login);
            request.AddParameter("password", password);

            var response = _client.Execute(request);
            return response;
        }
    }
}
