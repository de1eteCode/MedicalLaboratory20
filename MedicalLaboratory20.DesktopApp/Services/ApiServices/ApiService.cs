using RestSharp;

namespace MedicalLaboratory20.DesktopApp.Services.ApiServices
{
    abstract class ApiService
    {
        protected IRestClient _client;

        public ApiService(IRestClient client)
        {
            _client = client;
        }
    }
}
