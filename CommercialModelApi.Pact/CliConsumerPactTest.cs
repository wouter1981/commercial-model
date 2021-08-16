using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace CommercialModelApi.Pact
{
    public class CliConsumerPactTests
    {
        private readonly ITestOutputHelper _output;
        private readonly Task _hostTask;
        public CliConsumerPactTests(ITestOutputHelper output)
        {
            _output = output;
            _hostTask = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webbuilder =>
                {
                    webbuilder.UseStartup<TestStartup>();
                })
                .Build().RunAsync();

        }

        [Fact]
        public void TestPact()
        {
            var pactVerifier = new PactVerifier(new PactVerifierConfig
            {
                Outputters = new IOutput[] {
                    new XUnitOutput(_output)
                }
            });

            pactVerifier
                .ProviderState("https://localhost:9001/provider-states")
                .ServiceProvider("Commercial Model API", "https://localhost:9001")
                .HonoursPactWith("Commercial Model CLI")
                .PactUri("../../../../CommercialModelCli.Pact/pacts/commercial_model_cli-commercial_model_api.json")
                .Verify();
        }
    }
}