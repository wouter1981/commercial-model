# Installing tools
https://docs.specflow.org/projects/specflow/en/latest/Installation/Installation.html

Check out the recommended plugins and .NET templates.

```
# install templates
dotnet new -i SpecFlow.Templates.DotNet

# add feature
dotnet new specflow-feature
```
## Certs
To make it work in WSL you need linux to trust the dev certificate, that's a bit of work :-)
https://www.fearofoblivion.com/setting-up-asp-net-dev-certs-for-wsl

Generated certificates are added to the solution root folder:
- localhost.conf
- localhost.crt
- localhost.key

# Run tests
```dotnet test CommercialModel.Acceptance.Tests```

# Generate documentation
From root solution folder:
`~/.dotnet/tools/livingdoc feature-folder CommercialModel.Acceptance.Tests/ -t CommercialModel.Acceptance.Tests/bin/Debug/net5.0/TestExecution.json`