# ten-pin-bowling

The project is build on
- ASP.Net Core 3.1

The APIs are build using Feature pattern where all the feature are divided into their own feature class. This helps in seperating the logic and avoid clutering the classes and funtionalities.

## What's Inside?
This project includes:

- API to calculate the frame progress scores and indicate if the game has completed
- Unit test cases for the API

### Packages.

This project uses the following key nuget packages
- FluentValidation
  - An easy way to validate request before request hits controller.
- MediatR
 - Framework for writing API funtionality where the focus is on the core business logic, instead of how it will be called.
- Swashbuckle.AspNetCore
 - For Swagger