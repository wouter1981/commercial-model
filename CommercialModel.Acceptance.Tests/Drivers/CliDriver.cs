using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace CommercialModel.Acceptance.Tests.Drivers
{
    public class CliDriver
    {
        private static readonly string _commercialCliPath;

        static CliDriver()
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var commercialCliProjectPath = Path.Combine(
                Path.GetDirectoryName(assemblyPath),
                "../../../../CommercialModelCli/CommercialModelCli.csproj");

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "/usr/bin/dotnet",
                Arguments = $"build {commercialCliProjectPath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            using (var process = Process.Start(startInfo))
            {
                while (!process.HasExited)
                {
                }
                if (process.ExitCode != 0)
                {
                    throw new Exception(process.StandardError.ReadToEnd());
                }
            }

            _commercialCliPath = Path.Combine(
                Path.GetDirectoryName(assemblyPath),
                "../../../../CommercialModelCli/bin/Debug/net5.0/CommercialModelCli");
        }

        public (int, string) AddAccount(string accountName)
        {
            return Execute($"accounts add {accountName}");
        }

        public IEnumerable<string> ListAccounts()
        {
            (int exitCode, string output) = Execute("accounts list");
            if (exitCode == 0)
                return output.Split("\n");
            else
                throw new Exception("Error while executing command: " + output);
        }

        public void CleanModel()
        {
            (var exitCode, var output) = Execute("accounts delete-all");
            if (exitCode != 0)
            {
                throw new Exception("Error executing command: " + output);
            }
        }
        private static (int, string) Execute(string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = _commercialCliPath,
                Arguments = args,
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
                if (process.ExitCode != 0)
                {
                    return (process.ExitCode, process.StandardError.ReadToEnd());
                }
                standardOutput.Append(process.StandardOutput.ReadToEnd());

                var captured = standardOutput.ToString();
                return (0, captured);
            }

        }
    }
}