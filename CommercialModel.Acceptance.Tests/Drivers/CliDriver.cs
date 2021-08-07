using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;

namespace CommercialModel.Acceptance.Tests.Drivers
{
    public class CliDriver
    {
        public (int, string) AddAccount(string accountName) {
            ProcessStartInfo startInfo = new ProcessStartInfo() { 
                FileName = "/usr/bin/dotnet", 
                Arguments = $"run --project /home/wsimons/projects/commercial-model/CommercialModelCli/CommercialModelCli.csproj add-account {accountName}",
                RedirectStandardError = true,
            }; 
            var process = Process.Start(startInfo);
            while (!process.HasExited) {
                // wait
            }
            if (process.ExitCode != 0) {
                return(process.ExitCode, process.StandardError.ReadToEnd());
            }
            return (0, "");
        }

        public IEnumerable<string> ListAccounts() {
            ProcessStartInfo startInfo = new ProcessStartInfo() { 
                FileName = "/usr/bin/dotnet", 
                Arguments = $"run --project /home/wsimons/projects/commercial-model/CommercialModelCli/CommercialModelCli.csproj list-accounts",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            using (var process = Process.Start(startInfo))
            {
                var standardOutput = new StringBuilder();
                while (!process.HasExited)
                {
                    standardOutput.Append(process.StandardOutput.ReadToEnd());
                }
                if (process.ExitCode != 0) {
                    throw new Exception("Error executing command: " + process.StandardError.ReadToEnd());
                }
                standardOutput.Append(process.StandardOutput.ReadToEnd());

                var captured = standardOutput.ToString();
                return captured.Split("\n");
            }
        }

        public void CleanModel() {
            ProcessStartInfo startInfo = new ProcessStartInfo() { 
                FileName = "/usr/bin/dotnet", 
                Arguments = $"run --project /home/wsimons/projects/commercial-model/CommercialModelCli/CommercialModelCli.csproj clean-model",
                RedirectStandardError = true,
            }; 
            var process = Process.Start(startInfo);
            while (!process.HasExited) {
                // wait
            }
            if (process.ExitCode != 0) {
                throw new Exception("Error executing command: " + process.StandardError.ReadToEnd());
            }
        }

    }
}
