# VS Code
This project contains run and launch configurations for VSCode and has been built with the extensions:
- C#
- .NET Core Test Explorer
- Snippets and Syntax Highlight for Gherkin (Cucumber)

# Installing tools
https://docs.specflow.org/projects/specflow/en/latest/Installation/Installation.html

Check out the recommended plugins and .NET templates.
```
# install templates
dotnet new -i SpecFlow.Templates.DotNet

# add feature
dotnet new specflow-feature
```
# Setup Dev Cert
To make it work in WSL you need linux to trust the dev certificate, that's a bit of work :-)

Generated certificates are added to the solution root folder:
- localhost.conf
- localhost.crt
- localhost.key
- localhost.pfx

Add the PFX as trusted root certificate
Windows: Install certificate, choose system store, manually select 'Trusted Root Certificate Authorities' for the location
Linux (Ubuntu): copy the crt file to /usr/local/share/ca-certificates and run sudo ca-update-certificates

If you are developing with WSL, do both of the above. Mac is probably similar to Linux, but great if someone can help documenting that.

TODO: Unfortunately I have not found a way yet to reference the certificate without using the full path from the API project. Make sure to adjust the path in CommercialModelApi/appsettings.Development.json

# Run tests
Make sure to run the webapi project to be able to execute acceptance tests.

Run all:
```dotnet test```

Run unit:
```dotnet test CommercialModelApi.Tests```

Run acceptance:
```dotnet test CommercialModel.Acceptance.Tests```

# Update API definition in Cli project
If you update the API, you need to copy the updated spec (downloadable via SwaggerUI after starting the API project) to the `CommercialModelCli/CommercialModelApi.json` file. 

The project file has a reference that will automatically update the library code on every build. So after updating the file make sure to run a build before updating the code.