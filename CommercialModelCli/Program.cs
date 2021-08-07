using System;
using System.IO;
using System.Threading.Tasks;

namespace CommercialModelCli
{
    class Program
    {
        private readonly static string _baseUrl = "https://localhost:5001/";
        static async Task Main(string[] args)
        {
            var client = new CommercialModelApiClient(_baseUrl, new System.Net.Http.HttpClient());

            if (args.Length == 1 && args[0] == "list-accounts") {
                var accounts = await client.AccountsAllAsync();
                foreach(var account in accounts) {
                    Console.WriteLine(account.AccountShortName);
                }
                Environment.Exit(0);
            }

            if (args.Length == 1 && args[0] == "clean-model") {
                if (await client.Accounts2Async()) {
                    Environment.Exit(0);
                }
                Environment.Exit(1);
            }

            if (args.Length == 2 && args[0] == "add-account") {
                if (await client.AccountsAsync(args[1])) {
                    Environment.Exit(0);
                }
                Environment.Exit(1);
            }
        }
    }
}
