
## DataBase

Install MongoDB Community Edition:
https://docs.mongodb.com/manual/administration/install-community/

[Optional] Create `.mongod/` in Vehicles directory to store runtime files created by running `mongod`.
`.mongod/` has been added to `.gitignore` file.

Connect to MongoDB on default port `27017`

```
mongod --dbpath <directory_path>

```
The port can be changed in `Vehicles.API/appsettings.json`

## Environment variables

For Macos/Linux using Visual Studio, you need to launch Visual Studio from command line
to have access to bash/zsh/etc environment variables, discussed in detail
here https://docs.microsoft.com/en-us/dotnet/api/system.environment.getenvironmentvariable?view=netcore-3.1

## Packages
Ensure you have the following NuGet packages installed:
- Akka
- MongoDB.Driver
- RestSharp
- Newtonsoft.JSON
