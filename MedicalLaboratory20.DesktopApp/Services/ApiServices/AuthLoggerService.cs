using RestSharp;
using System.Threading.Tasks;

namespace MedicalLaboratory20.DesktopApp.Services.ApiServices
{
    internal class AuthLoggerService : ApiService
    {
        public AuthLoggerService(IRestClient client) : base(client) { }

        public async Task<IRestResponse> GetDataLog(string token)
        {
            IRestRequest request = new RestRequest($"{Cfg.HttpServerAddress}/auth/log", Method.GET, DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await Client.ExecuteAsync(request);
            return response;
        }
    }
}
