# Food-T Order App 🍕
Project using  tool build in support from [ASP.Net](https://dotnet.microsoft.com/en-us/apps/aspnet)
### Requirements
+ Windows O.S 10+
+ Visual Studio 2.19+
+ SQL Server 2.08+
+ .NET SDKs 4.5+
+ .NET Framwork 4.7.2+
### Setup
1. Install the required applications
2. Open google download (IIS) Express by Microsoft
3. Clone the repository
4. Set up a database connection to the project
5. Configure the connectionStrings file system to complete the installation for the project
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
``````
### Version
 + Initial release
## Author
Trung-Van Nguyen ([@trungnv61](https://www.instagram.com/trungnv61/)) - [ASP.Net](https://dotnet.microsoft.com/en-us/apps/aspnet)
