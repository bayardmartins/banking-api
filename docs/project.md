## Definition
In this document you find explanation about the decision making process, and some other broad questions around the whole project.

### Architecture
The chosen architecture was a Clean Architecture model, based on Steve Smith's lecture at dotNET Conf 2021. The main reason is the possibility to adapt, maintain and test.

### Database, Login and Password
The chosen login method is very simple just to simulate some kind of authentication process. The same apply to the Database. GDPR and security issues were ignored for the sake os simplicity.

### Validation and Entities
The entities in the project have just a few properties and the validation in Controllers and Services are very shallow for the sake os simplicity. 

### Inactivate or Delete
This is a tricky question, I don't have enough information to decide between one over the other in this cenary so I implemented both, in a real life project I would show the project manager the advantages and disavantages of both approaches.  

### Next steps
The next steps of the project, or "what would I do if I had more time?":  
Redis for cache, Load Balancer and Messaging Service;  
Real properties and validation;  
Integration tests;  
Kibana Logger and New Relic monitoring;

### Before running the project
This project was made in Linux with vscode, I didn't have the opportunity to test in Visual Studio, maybe the .sln doesn't work. If that's the case open the folder without the solution, the structions above must be enough even if your not use to run c# without Visual Studio.  
First create a .env file at src/Banking.Web/ and add the property SECRET_KEY, the value can be something like fedaf7d8863b48e197b9287d492b708e, or another hash.  
Then lets build the project, the first time may take a while.

### Caution:
The project uses docker-compose to the database container, if you don't use docker you can change to any database you have access, just change the connection string at appsettings.json.  
The project uses PostgreSQL, it's also possible to use other SQL, changing the setup in src/Banking.Infrastructure/StartupSetup.cs. But this was not tested, it's highly recommended to use PostgreSQL and Docker.
  
### To run the project:  
  
$cd src/Banking.Web  
$dotnet build  
Now it's time to start the Database  
$cd ..  
$cd Banking.Infrastructure  
$docker-compose build  
$docker-compose up  
$cd ..  
$cd Banking.Web  
$dotnet ef migrations add Start --project ../Banking.Infrastructure  
$dotnet ef database update  
And finally running the project at developer mode  
$dotnet run  
  
Swagger is running at localhost:7265/swagger/index.html  
Database browser solution running at //localhost:5050/ (credentials at docker-compose)  
  
### Using Swagger in Development Mode
First, at Login section: register a new user, then log in with registred credentials.  
Copy the token returned by the login endpoint and add it to Authorize button following the shown instructions.  
Now you must be ready to use any Swagger endpoint. Follow the instructions in /docs/API/ to the proper body json and query data.
If you prefer use Postman, there's a collection available in /docs/Collection. (you need to add the bearer token manually in all requests)

tip: If you want to skip the authentication process during development tests, you can delete the [Authorize] at BaseController and TransactionController 