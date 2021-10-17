using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace MedicalLaboratory20.WebAPI.Auth.IntegrationTests
{
    public class TestHostFixture : IDisposable
    {
        public HttpClient Client { get; set; }
        public IServiceProvider ServiceProvider { get; }

        public TestHostFixture()
        {
            var builder = Program.CreateHostBuilder(null)
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseEnvironment("Test");
                });
            var host = builder.Start();
            ServiceProvider = host.Services;
            Client = host.GetTestClient();
            Console.WriteLine("TEST host started");
        }

        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}