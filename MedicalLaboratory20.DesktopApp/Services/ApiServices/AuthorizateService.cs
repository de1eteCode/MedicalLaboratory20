using RestSharp;
using System.Threading.Tasks;

namespace MedicalLaboratory20.DesktopApp.Services.ApiServices
{
    sealed class AuthorizateService : ApiService
    {
        public AuthorizateService(IRestClient client) : base(client) { }

        public async Task<IRestResponse> Authorizate(string login, string password)
        {
            var cfg = ConfigurationService.GetInstance();
            IRestRequest request = new RestRequest($"{cfg.HttpServerAddress}/auth/login", Method.POST, DataFormat.Json)
                .AddJsonBody(new { login, password });

            var response = await _client.ExecuteAsync(request);
            return response;
        }
    }
}
