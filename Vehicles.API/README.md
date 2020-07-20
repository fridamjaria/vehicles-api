
## DataBase

Install MongoDB Community Edition:
https://docs.mongodb.com/manual/administration/install-community/

[Optional] Create `.mongod/` to store runtime files created by running `mongod`.
`.mongod/` has been added to `.gitignore` file.

Connect to MongoDB on default port `27017`

```
mongod --dbpath <vehicles_project_directory_path>/.mongod/

```

## Packages
Ensure you have the following NuGet packages installed:
- Akka
- MongoDB.Driver
- Newtonsoft.JSON
