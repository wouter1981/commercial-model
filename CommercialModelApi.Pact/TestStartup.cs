using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CommercialModelApi.Pact
{
    public class TestStartup : CommercialModelApi.Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
            app.UseMiddleware<ProviderStatesMiddleware>(); // Handles provider state changes at provider-states
        }
    }
}