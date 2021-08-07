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
        void Temp(string[] args)
        {
            if (!Directory.Exists(_baseFolder)) {
                Directory.CreateDirectory(_baseFolder);
            }
            if (args.Length == 2 && args[0] == "add-account") {
                var accountFile = Path.Combine(_baseFolder, args[1]);
                if (File.Exists(accountFile)) {
                    Console.Error.WriteLine("Account already exists.");
                    Environment.Exit(1);
                }
                File.Create(accountFile);
            } else if (args.Length == 1 && args[0] == "list-accounts") {
                
            } else if (args.Length == 1 && args[0] == "clean-model") {
                var accountFiles = Directory.GetFiles(_baseFolder);
                foreach (var accountFile in accountFiles)
                {
                    File.Delete(accountFile);
                }
                Console.WriteLine("Model cleaned up");
            } else {
                Console.Error.WriteLine("Unknown command.");
                Environment.Exit(1);
            }
        }
        
        public IEnumerable<Account> ListAccounts()
        {
            var accountFiles = Directory.GetFiles(_baseFolder);
            foreach (var accountFile in accountFiles)
            {
                yield return new Account {
                    AccountShortName = Path.GetFileNameWithoutExtension(accountFile),
                };
            }
        }
    }
}