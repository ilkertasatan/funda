#funda .NET Assignment
This is an assignment that I've developed for funda .NET Software Engineer position.

#Technologies
Aside from .NET 5, numerous technologies I used within this solution including:

- NET 5 and Clean Architecture
- Web API
- CQRS with MediatR
- Testing with XUnit, Moq and FluentAssertions

##Architecture
I used Clean Architecture in this project and the project consists of the following layers;

- API Layer
- Application Layer
- Domain Layer
- Infrastructure Layer
- Tests

###API Layer
This layer is the outermost layer. By having this layer, we're showing the world what functionalities our API has. It has no logic. This layer interacts with the application layer by using Inputs which in our case Commands and Queries.

###Application Layer
This layer basically accepts Inputs coming from the API layer and generates Output based on which Command or Query has been executed.

###Domain Layer
This is the hearth of the application. It has our entities and abstractions.

###Infrastructure Layer
This layer interacts with the external components such as MongoDB and external API calls. In our case, this layer communicates with OmDB.

###Tests
This project also has Unit Tests, Integration Tests and End To End tests.

####API Interfaces
Swagger documentation has been added to the project to see endpoints, request and responses. You can also test the API via Swagger or any other rest client tools. Please use this link `http://localhost:5000/swagger/index.html` to reach Swagger document.

##How To Build
All project was dockerized. You need to run build-all.ps1 PowerShell script in the root directory of the project.

After open any terminal, go to the project directory and type in the command below.

`./build-all.ps1`

In case you might get a security error when running the PowerShell script, you should run the following;

`Set-ExecutionPolicy RemoteSigned`

Afterward, you should see the project started to be built.

##Conclusion
I would like to thank you for giving me this chance to showcase what I can do. I hope that you will like my solution when you review it.