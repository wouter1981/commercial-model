using System;
using System.IO;
using System.Threading.Tasks;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace CommercialModelCli
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand();
            rootCommand.AddGlobalOption(
                new Option("--base-url", "The base URL for the system", typeof(string), () => { return "https://localhost:5001/"; }));
            var accountCommand = new Command("accounts", "Manage the accounts in the system");
            var addAccountCommand = new Command("add", "Add a new account") {
                new Argument("accountName")
            };
            addAccountCommand.Handler = CommandHandler.Create<string, string>(AddAccount);
            accountCommand.AddCommand(addAccountCommand);

            var deleteAllAccountsCommand = new Command("delete-all", "Remove all accounts");
            deleteAllAccountsCommand.Handler = CommandHandler.Create<string>(DeleteAllAccounts);
            accountCommand.AddCommand(deleteAllAccountsCommand);

            var listAccountsCommand = new Command("list", "List accounts");
            listAccountsCommand.Handler = CommandHandler.Create<string>(ListAccounts);
            accountCommand.AddCommand(listAccountsCommand);
            rootCommand.AddCommand(accountCommand);

            return await rootCommand.InvokeAsync(args);
        }

        private static async Task<int> ListAccounts(string baseUrl)
        {
            var client = new Client(baseUrl);
            var accounts = await client.ListAccounts();
            foreach (var account in accounts)
            {
                Console.WriteLine(account.AccountShortName);
            }
            return 0;
        }

        private static async Task<int> DeleteAllAccounts(string baseUrl)
        {
            var client = new Client(baseUrl);
            var success = await client.DeleteAllAccounts();
            if (success)
            {
                Console.WriteLine("All accounts have been removed.");
                return 0;
            }
            else
            {
                Console.WriteLine("Problem removing accounts...");
                return 1;
            }
        }

        private static async Task<int> AddAccount(string baseUrl, string accountName)
        {
            var client = new Client(baseUrl);
            try
            {
                var account = await client.AddAccount(accountName);
                Console.WriteLine("Account added: " + account.AccountShortName);
                return 0;
            }
            catch (ApiException<ProblemDetails> err)
            {
                Console.WriteLine(err.Result.Detail);
                return 1;
            }
        }
    }
}
