using System;
using Xunit;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System.Collections.Generic;

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
        public void CanAddAccount()
        {
            _mockProviderService
               .Given("The account 'test' does not exist")
               .UponReceiving("An add account request for 'test'")
               .With(
                   new ProviderServiceRequest
                   {
                       Method = HttpVerb.Post,
                       Path = "/accounts/",
                       Query = new
                       {
                           accountName = "test"
                       }
                   }
               )
               .WillRespondWith(
                   new ProviderServiceResponse
                   {
                       Status = 201,
                       Body = new
                       {
                           accountShortName = "test"
                       }
                   }
               );
        }
    }
}
