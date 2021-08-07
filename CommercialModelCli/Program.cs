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
                var accounts = await client.AccountManagementAsync();
                foreach(var account in accounts) {
                    Console.WriteLine(account.AccountShortName);
                }
            }

            if (args.Length == 1 && args[0] == "clean-model") {
                // pass
            }
        }
    }
}
