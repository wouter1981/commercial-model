using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using CommercialModel.Acceptance.Tests.Drivers;

namespace CommercialModel.Acceptance.Tests.Steps
{
    [Binding]
    public sealed class AccountStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CliDriver _driver;

        public AccountStepDefinitions(
            ScenarioContext scenarioContext,
            CliDriver driver)
        {
            _scenarioContext = scenarioContext;
            _driver = driver;
        }

        [When("a user adds the account '(.*)'")]
        public void WhenAnAccountIsAdded(object accountName)
        {
            (var errorCode, var errorMessage) = _driver.AddAccount(accountName as string);
            _scenarioContext["lastExitCode"] = errorCode;
            _scenarioContext["lastErrorMessage"] = errorMessage;
        }

        [Then("the commercial model has the account '(.*)'")]
        public void ThenTheAccountShouldExist(object accountName)
        {
            _driver.ListAccounts().Should().Contain(accountName as string);
        }
        [Then("adding the account fails")]
        public void ThenAddingTheAccountFails()
        {
            ((int)_scenarioContext["lastExitCode"]).Should().NotBe(0);
        }

        [Given(@"the commercial model has been cleaned up")]
        public void GivenTheCommercialModelHasBeenCleanedUp()
        {
            _driver.CleanModel();
        }
    }
}
