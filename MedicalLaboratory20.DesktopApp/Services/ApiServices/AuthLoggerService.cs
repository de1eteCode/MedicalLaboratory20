using RestSharp;
using System.Threading.Tasks;

namespace MedicalLaboratory20.DesktopApp.Services.ApiServices
{
    internal class AuthLoggerService : ApiService
    {
        public AuthLoggerService(IRestClient client) : base(client) { }

        public async Task<IRestResponse> GetDataLog()
        {
            IRestRequest request = new RestRequest($"{_cfg.HttpServerAddress}/auth/log", Method.GET, DataFormat.Json);
            var response = await _client.ExecuteAsync(request);
            return response;
        }
    }
}
