# Better Plan Endpoints


## Run Via CLI

ℹ Go to appsettings.Development.json and put the conenction string in the SqlConnection="<CONNECTION_STRING>"
> User ID=< user >;Password=< password >;Server=< server >;Port=< port >;Database=< db_name >;Integrated Security=true;Pooling=true;

Insde the root folder run this command
- ``dotnet restore``
- ``dotnet run --project .\src\Better.Api\Better.Api.csproj``

Go to https://localhost:<<port>>/swagger/index.html

## Run Via Docker

ℹ Go to Dockerfile and put the conenction string in the ConnectionStrings__SqlConnection="<CONNECTION_STRING>"
> User ID=< user >;Password=< password >;Server=< server >;Port=< port >;Database=< db_name >;Integrated Security=true;Pooling=true;

Insde the root folder run this command

- ``docker build . -t betterapi:0.1``
- ``docker run -p 5000:80 betterapi:0.1``

Go to http://localhost:<<port>>/swagger/index.html


## Use API
The API was published in Azure and you can use the endoints here
https://www.postman.com/alfonsovgs/workspace/better-api/api/

ℹ Set the environment as "Production"

Also import the swagger.json (docs\swagger\swagger.json) and use this endpoint to test
https://betterapi.azurewebsites.net