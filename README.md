# ProjectManagementSystem

ProjectManagementSystem - это веб-сервис для работы с проектами, сотрудниками и задачами.

ProjectManagementSystem is a web service for working with projects, employees and tasks.

# Используемые технологии / Technologies used

1) .NET 7.0
2) ASP.NET Core Web API
3) MS SQL Server

# Библиотеки / Libraries

**Application layer**

• FluentResults - Version="3.15.2"

• FluentValidation.AspNetCore - Version="11.3.0"

• Mapster - Version="7.4.0"

• MediatR - Version="12.1.1"

• Microsoft.IdentityModel.Tokens - Version="6.24.0"

• System.IdentityModel.Tokens.Jwt - Version="6.24.0"

• System.Linq.Dynamic.Core - Version="1.3.5"

• ProjectReference Include = \Domain\Domain.csproj

**Infrastructure layer**

• Microsoft.EntityFrameworkCore - Version="7.0.12"

• Microsoft.EntityFrameworkCore.SqlServer - Version="7.0.12"

• Microsoft.EntityFrameworkCore.Tools - Version="7.0.12"

• ProjectReference Include = \Application\Application.csproj

**WebApi(Presentation) layer**

• Microsoft.AspNetCore.Authentication.JwtBearer - Version="7.0.12"

• Microsoft.AspNetCore.OpenApi - Version="7.0.12"

• Microsoft.EntityFrameworkCore.Design - Version="7.0.12"

• Newtonsoft.Json - Version="13.0.3"

• Serilog.AspNetCore - Version="7.0.0"

• Serilog.Sinks.Console - Version="4.1.0"

• Serilog.Sinks.File - Version="5.0.0"

• Swashbuckle.AspNetCore.Swagger - Version="6.5.0"

• Swashbuckle.AspNetCore.SwaggerGen - Version="6.5.0"

• Swashbuckle.AspNetCore.SwaggerUI - Version="6.5.0"

• ProjectReference = \Application\Application.csproj; \Infrastucture\Infrastucture.csproj

**Tests layer (ApplicationTests)**

• FluentAssertions - Version="6.12.0"

• Microsoft.NET.Test.Sdk - Version="17.7.2"

• Moq - Version="4.20.69"

• xunit - Version="2.5.3"

• xunit.runner.visualstudio - Version="2.5.3"

• ProjectReference = \Application\Application.csproj; \Domain\Domain.csproj

# Бизнес логика / Business logic

**AuthController**

1) [Post] /api/Auth/login - authorization with parameters: email, password
2) [Post] /api/Auth/create - employee registration with parameters: fistname, lastname, patronymic, email, password, role

**EmployeeController**

1) [Get] /api/Employee - returns a list of employees
2) [Delete] /api/Employee - accepts an array of ids of the employees to be deleted and returns the ids of the deleted employees
3) [Get] /api/Employee/{id} - accepts employee id and returns his data
4) [Patch] /api/Employee/update - endpoint for updating employee data
5) [Patch] /appi/Employee/addToProject - adds an employee to the project, parameters: project id, employee id
6) [Patch] /api/Employee/removeFromProject - removes an employee from the project, parameters: project id, employee id
7) [Patch] /api/Employee/appoint - appoints an employee as a project manager, parameters: project id, employee id

**ProjectController**

1) [Get] /api/Project - returns a list of projects
2) [Delete] /api/Project - accepts an array of projects to be deleted and returns the id of the deleted projects
3) [Get] /api/Project/{id} - accepts the project id and returns its data
4) [Post] /api/Project/create - creates a project, parameters: name, customerCompanyName, performingCompanyName, startDate, endDate, priority
5) [Patch] /api/Project/update - endpoint for updating project data
6) [Get] /api/Project/{field} - accepts a field (ex: name | customerCompanyName) and returns a list of matching projects
7) [Post] /api/Project/date - returns projects corresponding to the date, parameters: startDate, endDate

**TaskController**

1) [Post] /api/Task/create - creates a task, parameters: name, comment, status, priority, projectId, authorId
2) [Delete] /api/Task - deletes employee tasks, parameters: employeeId, tasksId[array]
3) [Get] /api/Task - returns a list of tasks
4) [Get] /api/Task/{id} - accepts the task id and returns its data
5) [Patch] /api/Task/update - endpoint for updating task data
