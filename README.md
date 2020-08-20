# Samhammer.Configuration.Childs

## Usage

#### How to add this to your project:
- reference this package to your project: https://www.nuget.org/packages/Samhammer.Configuration.Childs/

To add it to ASP.NET Core configuration simply place .EnableChildSubstitutions() last. Make sure it is always called after all other configurations are added, else it won't behave properly!

```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
   Host.CreateDefaultBuilder(args)
      .ConfigureAppConfiguration((ctx, builder) =>
      {
          // if you have any additional configuration place it before
          var customer = Environment.GetEnvironmentVariable("CUSTOMER");
          builder.EnableChildSubstitutions(customer);
      });
```

Set enviroment variable 'Customer' to control which child substitutions are loaded in the configuration.
For local development you can do set in launchsettings.json.

```json
   "environmentVariables": {
     "ASPNETCORE_ENVIRONMENT": "Development",
     "CUSTOMER": "customer1"
   }
```

#### When to use it

If you want to deploy multiple production systems (e.g. different customers) and you don´t want to add additional appsettings.json files or use enviroment variables.

#### How to use it

Example where the entry ConnectionString has some default value and some substitutions values for different customers.

```json
{
    "ConnectionString ": "blabla&catalog=myapp",
    "customer1:ConnectionString ": "blabla&catalog=myapp-customer1",
    "customer2:ConnectionString ": "blabla&catalog=myapp-customer2",
}
```

```csharp
var value = configuration["ConnectionString"];
```

This will return 'blabla&catalog=myapp-customer1'.

## Contribute

#### How to publish package
- set package version in Samhammer.Configuration.Childs.csproj
- add information to changelog
- create git tag
- dotnet pack -c Release
- nuget push .\bin\Release\Samhammer.Configuration.Childs.*.nupkg NUGET_API_KEY -src https://api.nuget.org/v3/index.json
- (optional) nuget setapikey NUGET_API_KEY -source https://api.nuget.org/v3/index.json
