This project does Pact.io based validation to test the contract.

I tried using TestServer for integration testing as described in:
https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0
That failed, because it only allows the system to be reached via a custom client created from the WebApplicationFactory.

So then I though we could just start the API and test against that, but I would not get code coverage. So I am starting up a server using the same config as the API project, but with additional middleware to handle the provider states. See TestStartup for the appended middleware and look at the CliConsumerPactTests file for the starting of the Host just like in the Api project. This allows for Coverlet to easily collect test coverage (which you can do by using the VSCode task to run the api pact tests).
