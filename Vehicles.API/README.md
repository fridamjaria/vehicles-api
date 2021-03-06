﻿# Vehicles API

Api to store vehicles and the lines that they are operating on using WhereIsMyTransport API to get lines data.
The API is currently limited to buses for the Algoa Company Bus agency lines.
It can be extended to include other vehicle modes and lines from multiple agencies. 

## Packages
Ensure you have the following NuGet packages installed:
* Akka
* MongoDB.Driver
* RestSharp
* Newtonsoft.JSON

## DataBase

Install [MongoDB Community Edition](https://docs.mongodb.com/manual/administration/install-community/)

**[Optional]** Create `.mongod/` in Vehicles directory to store runtime files created by running `mongod`.
`.mongod/` has been added to `.gitignore` file.

Connect to MongoDB on default port `27017`

```
mongod --dbpath <directory_path>

```
The port can be changed in `Vehicles.API/appsettings.json`

## Environment variables

Add the following environment variables to your bashrc/zshrc/etc files:
```
ENV["WHERE_IS_MY_TRANSPORT_CLIENT_ID"]
ENV["WHERE_IS_MY_TRANSPORT_CLIENT_SECRET"]
```
You can get the values from the [WhereIsMyTransport Developer Portal](https://developer.whereismytransport.com/clients).

If using Visual Studio for Macos/Linux, you need to launch Visual Studio from command line
to have access to bash/zsh/etc environment variables, as discussed
[here](https://docs.microsoft.com/en-us/dotnet/api/system.environment.getenvironmentvariable?view=netcore-3.1)


## Limitations of API

Due to time constraints the following elements are missing from the API:
* Ensure all busses that are not assigned to a line due to some error/broken code,
  are assigned to a line periodically and asynchronously (maybe through the Scheduler).
  Currently a bus can be created in the system without being linked to a bus line.
* Terminate Busses after a specific amount of time has passed since they were created
* Error handling
* Input and model validations
* Conversion of model fields to camelCase in response payload
* API Authentication
* API Documentation
* Return correct status codes to clients for create, update and delete actions
* Logging/Monitoring
* Resolving DeadLettered Messages
