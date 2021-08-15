using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CommercialModelCli
{
    /// <summary>
    /// Thin adapter to the auto generated class to make the functions more descriptive and add the
    /// option to move to another way of implementing the interface.
    /// </summary>
    public class Client
    {
        private readonly CommercialModelApiClient _client;
        public Client(string baseUrl = "https://localhost:5001/")
        {
            _client = new CommercialModelApiClient(baseUrl, new System.Net.Http.HttpClient());
        }

        public async Task<IEnumerable<Account>> ListAccounts()
        {
            return await _client.AccountsAllAsync();
        }

        public async Task<bool> DeleteAllAccounts()
        {
            return await _client.Accounts2Async();
        }

        public async Task<Account> AddAccount(string accountName)
        {
            return await _client.AccountsAsync(accountName);
        }
    }
}
