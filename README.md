# Food-T Order App 🍕
Project using  tool build in support from [ASP.Net](https://dotnet.microsoft.com/en-us/apps/aspnet)
### Requirements
+ Visual Studio 2.19+
+ SQL Server 2.08+
### Setup
1. Install requirements
2. Clone the repository
3. Config system app
### Develop
#### Tech stack
+ C#
+ Javascript
+ Booststrap
+ SQL Server
+ Entity framework for autocomplet generate database mapping from database
### Understand parser
The parser has 2 steps:
1. Using references for add sub model from Model to WebFood. Main file will 
append all key and value and set get this value each feature when building.

2. Load file connectionStrings and change `parameters` in this file and `name` 
will be matching suitable name with ADO.NET Entity Data Model 

``````
<connectionStrings>
   <add name="FoodOnlineDbContext" connectionString="data source=Server_name;initial catalog=Database_name;user id=sa;password=You_password;
    MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>

