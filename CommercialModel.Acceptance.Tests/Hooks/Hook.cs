using System;
using TechTalk.SpecFlow;
using CommercialModelApi;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Threading;

namespace CommercialModel.Acceptance.Tests.Hooks
{
    [Binding]
    public class Hooks
    {
        private static CancellationTokenSource _cancelToken = new CancellationTokenSource();

        [BeforeTestRun]
        public static void StartApi()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webbuilder =>
                {
                    webbuilder.UseStartup<Startup>();
                })
                .Build().RunAsync(_cancelToken.Token);
        }

        [AfterTestRun]
        public static void StopApi()
        {
            _cancelToken.Cancel();
        }

    }
}
