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
    public class AccountManagementController : ControllerBase
    {
        private readonly ILogger<AccountManagementController> _logger;
        private readonly IAccountRepository _accountRepository;

        public AccountManagementController(
            ILogger<AccountManagementController> logger,
            IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            _logger.LogDebug("List accounts called");
            return _accountRepository.ListAccounts();
        }
    }
}
