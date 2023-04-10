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


#Use Cases

1. Add - Addition of two or more numeric operands: https://localhost:7107/Add
   
   This operation receives an object of type AddArguments with an Addends property to indicate the number of addends to be added. It returns an object of type AddResult containing a Sum property with the result of the addition.

2. Sub - Subtraction of two or more numeric operands: https://localhost:7107/Sub

      This operation receives an object of type SubtractArguments with Minuend and Subtrahend properties to indicate the numbers to be subtracted. It returns an object of type SubtractResult containing a Difference property with the result of the subtraction.

3. Mult: Multiply of two or more numeric operands https://localhost:7107/Mult
    
    This operation receives an object of type MultiplyArguments with an Factors property to indicate the number of addends to be multiplied. It returns an object of type MultiplyResult containing a Product property with the result of the multiplication.

4. Div - Division of two or more numeric operands: https://localhost:7107/Div

    This operation receives an object of type DivisionArguments with Dividend and Divisor properties to indicate the numbers to be divided. It returns an object of type DivisionResult containing a Quotient and Remainder properties with the result of the division.

5. Sqrt - Square root of a numeric operand: https://localhost:7107/Sqrt

    This operation receives an object of type SquareRootArguments with Number properties to indicate the numbers to compute the squre root. It returns an object of type SquareRootResult containing aSquareRoot property with the result of the division.

6. Sqrt - Square root of a numeric operand: https://localhost:7107/Query?id=

    In this case the track id identifier must be indicated to retrieve the operations performed in that session.
