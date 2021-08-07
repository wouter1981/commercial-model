using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommercialModelApi.Model;
using CommercialModelApi.Data;

namespace CommercialModelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountRepository _accountRepository;

        public AccountsController(
            ILogger<AccountsController> logger,
            IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// List all accounts
        /// </summary>
        /// <returns>A list of accounts</returns>
        [HttpGet]
        public IEnumerable<Account> ListAccounts()
        {
            _logger.LogDebug("List accounts called");
            return _accountRepository.ListAccounts();
        }

        /// <summary>
        /// Add an account
        /// </summary>
        /// <param name="accountName">The account name to add</param>
        /// <returns>Success or failure</returns>
        [HttpPut]
        public bool AddAccount(string accountName)
        {
            _logger.LogDebug("Add account called with {accountName}", accountName);
            try
            {
                _accountRepository.AddAccount(accountName);
                return true;
            } catch {
                return false;
            } 
        }

        /// <summary>
        /// CAREFUL: Deletes all accounts
        /// </summary>
        /// <returns>Success or failure</returns>
        [HttpDelete]
        public bool DeleteAllAccounts()
        {
            _logger.LogDebug("Delete all accounts");
            try {
                _accountRepository.DeleteAllAccounts();
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Get one account
        /// </summary>
        /// <param name="accountName">The name of the account to get</param>
        /// <returns>The account if found</returns>
        [HttpGet("{accountName}")]
        public Account GetAccount(string accountName)
        {
            return new Account {
                AccountShortName = "test"
            };
        }
    }
}
