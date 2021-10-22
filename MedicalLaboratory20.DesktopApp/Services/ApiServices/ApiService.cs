using RestSharp;

namespace MedicalLaboratory20.DesktopApp.Services.ApiServices
{
    abstract class ApiService
    {
        protected IRestClient Client;
        protected ConfigurationService Cfg;

        protected ApiService(IRestClient client)
        {
            Client = client;
            Cfg = ConfigurationService.GetInstance();
        }
    }
}
