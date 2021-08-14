using System;
using System.Collections.Generic;
using System.IO;
using CommercialModelApi.Model;

namespace CommercialModelApi.Data
{
    internal class FileBasedAccountRepository : IAccountRepository
    {
        private readonly static string _baseFolder =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "CommercialModel");

        public IEnumerable<Account> ListAccounts()
        {
            var accountFiles = Directory.GetFiles(_baseFolder);
            foreach (var accountFile in accountFiles)
            {
                yield return new Account
                {
                    AccountShortName = Path.GetFileNameWithoutExtension(accountFile),
                };
            }
        }

        public void AddAccount(Account account)
        {
            var accountFile = Path.Combine(_baseFolder, account.AccountShortName);
            if (File.Exists(accountFile))
            {
                throw new Exception("Account already exists!");
            }
            File.Create(accountFile);
        }

        public void DeleteAllAccounts()
        {
            var accountFiles = Directory.GetFiles(_baseFolder);
            foreach (var accountFile in accountFiles)
            {
                File.Delete(accountFile);
            }
        }
    }
}