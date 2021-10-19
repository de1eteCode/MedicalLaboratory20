using RestSharp;

namespace MedicalLaboratory20.DesktopApp.Services.ApiServices
{
    abstract class ApiService
    {
        protected IRestClient _client;
        protected ConfigurationService _cfg;

        public ApiService(IRestClient client)
        {
            _client = client;
            _cfg = ConfigurationService.GetInstance();
        }
    }
}
