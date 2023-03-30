# CalculatorService
Calculator Service capable of some basic arithmetic operations, like add, subtract, square, etc. along with a history
service keeping track of requests sharing a common an identifier.

# Requirements and packages

Project implemented in C#, consists of three projects:

* ASP.NET Core Web API project with Framework .NET 6.0
* xUnit testing project
* Console application

Necessary Packages for the application and its tests: 

Test Project:
* Microsoft.NET.Test.Sdk
* Moq
* xunit
* xunit.runner.visualstud

Api Rest:
* Serilog
* Swashbuckle.AspNetCore
* Swashbuckle.AspNetCore.SwaggerGen

# How to run the application

The application is ready to be tested with Swagger. The other option is through the console application project