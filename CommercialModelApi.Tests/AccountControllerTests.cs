using System;
using Xunit;
using CommercialModelApi.Controllers;
using CommercialModelApi.Data;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CommercialModelApi.Model;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CommercialModelApi.Tests
{
    public class AccountControllerTests
    {
        private Mock<ILogger<AccountsController>> _logger;
        public AccountControllerTests()
        {
            _logger = new Mock<ILogger<AccountsController>>();
        }

        [Fact]
        public void ListAccounts()
        {
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository
                .Setup(x => x.ListAccounts())
                .Returns(
                    new Account[] {
                        new Account { AccountShortName = "test-account"}
                    });
            var controller = new AccountsController(_logger.Object, accountRepository.Object);
            var accounts = controller.ListAccounts();
            Assert.Equal("test-account", accounts.First().AccountShortName);
        }

        [Fact]
        public void AddAccount()
        {
            var accountRepository = new Mock<IAccountRepository>();
            var controller = new AccountsController(_logger.Object, accountRepository.Object);
            var result = controller.AddAccount("test-account");
            Assert.True(result.Result is CreatedAtActionResult);

            var expectedResult = (CreatedAtActionResult)result.Result;
            Assert.Equal("test-account", ((Account)expectedResult.Value).AccountShortName);
        }

        [Fact]
        public void AddAccountThatExists()
        {
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository
                .SetupSequence(x => x.AddAccount(It.Is<Account>(y => y.AccountShortName == "test-account")))
                .Pass()
                .Throws(new Exception("Account already exists"));
            var controller = new AccountsController(_logger.Object, accountRepository.Object);
            var result = controller.AddAccount("test-account");
            result = controller.AddAccount("test-account");
            Assert.True(result.Result is BadRequestObjectResult);
            var badRequestResult = (BadRequestObjectResult)result.Result;
            var problemDetails = (ProblemDetails)badRequestResult.Value;
            Assert.Equal(problemDetails.Detail, "Account already exists");
        }

        [Fact]
        public void GetAccount()
        {
            var accountRepository = new Mock<IAccountRepository>();
            var controller = new AccountsController(_logger.Object, accountRepository.Object);
            var account = controller.GetAccount("test-account");
            Assert.Equal("test", account.AccountShortName);
        }

        [Fact]
        public void DeleteAllAccounts()
        {
            var accountRepository = new Mock<IAccountRepository>();
            var controller = new AccountsController(_logger.Object, accountRepository.Object);
            var result = controller.DeleteAllAccounts();
            // No exception is good
            Assert.True(result);
        }

        [Fact]
        public void DeleteAllAccountsFails()
        {
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(x => x.DeleteAllAccounts()).Throws(new Exception("Something is wrong"));
            var controller = new AccountsController(_logger.Object, accountRepository.Object);
            var result = controller.DeleteAllAccounts();
            // No exception is good
            Assert.False(result);
        }
    }
}
