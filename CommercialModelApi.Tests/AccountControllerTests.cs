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
        [Fact]
        public void ValidateListAccounts()
        {
            var logger = new Mock<ILogger<AccountManagementController>>();
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository
                .Setup(x => x.ListAccounts())
                .Returns(
                    new Account[] {
                        new Account { AccountShortName = "test-account"}
                    });
            var controller = new AccountManagementController(logger.Object, accountRepository.Object);
            var accounts = controller.Get();
            Assert.Equal("test-account", accounts.First().AccountShortName);
        }
    }
}
