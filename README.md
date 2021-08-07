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

Generated certificates are added to the solution root folder:
- localhost.conf
- localhost.crt
- localhost.key
- localhost.pfx

Add the PFX as trusted root certificate
Windows: Install certificate, choose system store, manually select 'Trusted Root Certificate Authorities' for the location
Linux (Ubuntu): copy the crt file to /usr/local/share/ca-certificates and run sudo ca-update-certificates

If you are developing with WSL, do both.

Unfortunately I have not found a way yet to reference the certificate without using the full path from the API project. Make sure to adjust the path in CommercialModelApi/appsettings.Development.json

# Run tests
```dotnet test CommercialModel.Acceptance.Tests```

# Generate documentation
From root solution folder:
`~/.dotnet/tools/livingdoc feature-folder CommercialModel.Acceptance.Tests/ -t CommercialModel.Acceptance.Tests/bin/Debug/net5.0/TestExecution.json`