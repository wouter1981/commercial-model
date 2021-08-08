using System;
using Xunit;
using CommercialModelApi.Controllers;
using CommercialModelApi.Data;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CommercialModelApi.Model;
using System.Linq;

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
            // No exception is good
            Assert.True(result);
        }

        [Fact]
        public void AddAccountThatExists()
        {
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository
                .SetupSequence(x => x.AddAccount("test-account"))
                .Pass()
                .Throws(new Exception("Account already exists"));
            var controller = new AccountsController(_logger.Object, accountRepository.Object);
            var result = controller.AddAccount("test-account");
            result = controller.AddAccount("test-account");
            Assert.False(result);
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
