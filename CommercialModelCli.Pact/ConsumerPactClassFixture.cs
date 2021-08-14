using System;
using Xunit;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace CommercialModelCli.Pact
{
    public class ConsumerPactClassFixture : IDisposable
    {
        private const string CONSUMER_NAME = "Commercial Model CLI";
        private const string PRODUCER_NAME = "Commercial Model API";
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort { get { return 9222; } }
        public string MockProviderServiceBaseUri { get { return String.Format("http://localhost:{0}", MockServerPort); } }

        public ConsumerPactClassFixture()
        {
            // Using Spec version 2.0.0 more details at https://goo.gl/UrBSRc
            var pactConfig = new PactConfig
            {
                SpecificationVersion = "2.0.0",
            };

            PactBuilder = new PactBuilder(pactConfig);

            PactBuilder.ServiceConsumer(CONSUMER_NAME)
                       .HasPactWith(PRODUCER_NAME);

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // This will save the pact file once finished.
                    PactBuilder.Build();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}