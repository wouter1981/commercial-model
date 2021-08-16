using System;
using Xunit;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System.Collections.Generic;
using System.IO;

namespace CommercialModelCli.Pact
{
    public class AccountTests : IClassFixture<ConsumerPactClassFixture>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public AccountTests(ConsumerPactClassFixture fixture)
        {
            _mockProviderService = fixture.MockProviderService;
            _mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
            _mockProviderServiceBaseUri = fixture.MockProviderServiceBaseUri;
        }

        [Fact]
        public async void CanAddAccount()
        {
            _mockProviderService
               .Given("The account 'test' does not exist")
               .UponReceiving("An add account request for 'test'")
               .With(
                   new ProviderServiceRequest
                   {
                       Method = HttpVerb.Post,
                       Path = "/Accounts",
                       Query = new
                       {
                           accountName = "test"
                       },
                       Headers = new Dictionary<string, object>
                        {
                            { "Accept", "text/plain" }
                        }
                   }
               )
               .WillRespondWith(
                   new ProviderServiceResponse
                   {
                       Headers = new Dictionary<string, object> {
                            { "Content-Type", "application/json; charset=utf-8" }
                        },
                       Status = 201,
                       Body = new
                       {
                           accountShortName = "test"
                       }
                   }
               );

            var output = new StringWriter();
            var error = new StringWriter();
            var orgOutput = Console.Out;
            var orgError = Console.Error;
            Console.SetOut(output);
            Console.SetError(error);
            try
            {
                await CommercialModelCli.Program.Main(new[] { "accounts", "add", "test", "--base-url", _mockProviderServiceBaseUri });
                var capturedOutput = output.ToString();
                Assert.Equal("Account added: test\n", capturedOutput);
                _mockProviderService.VerifyInteractions(); // Verify actions as defined in the mock were called once
            }
            finally
            {
                Console.SetOut(orgOutput);
                Console.SetError(orgError);
            }
        }
    }
}
