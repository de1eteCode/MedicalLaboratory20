using RestSharp;

namespace MedicalLaboratory20.DesktopApp.Services.ApiServices
{
    sealed class AuthorizateService : ApiService
    {
        public AuthorizateService(IRestClient client) : base(client) { }

        public IRestResponse Authorizate(string login, string password)
        {
            var cfg = ConfigurationService.GetInstance();
            IRestRequest request = new RestRequest($"{cfg.HttpServerAddress}/auth/token", Method.POST, DataFormat.Json)
                .AddJsonBody(new { login, password });

            var response = _client.Execute(request);
            return response;
        }
    }
}
