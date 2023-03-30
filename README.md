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

# How to run and test the application

The application is ready to be tested with Swagger. The other option is through CalculatorService.Client project. 

Each of the Controller requests are made via POST. Example of a POST request with curl to /Add

```sh
curl -X 'POST' \
  'https://localhost:7107/Add' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "addends": [
    1,2,3
  ]
}'
```

The server returns the entity generated in each case. For the example of the sum:

```sh
{
  "sum": 6
}
```

If a 'TrackingId’ was provided, the server should store this request’s details inside it’s journal, indexed by the given Id.  The above example would read as follows:
```sh
curl -X 'POST' \
  'https://localhost:7107/Add' \
  -H 'accept: text/plain' \
  -H 'x-evi-tracking-id: 1' \
  -H 'Content-Type: application/json' \
  -d '{
  "addends": [
    1,2,3
  ]
}'
```

Anyone can test the API either from Swagger, as it is prepared from debugging, or from the CalculatorService.Client project. If you are going to test from the console application, you have to open 2 Visual Studio, one for the Service and one for the client.
