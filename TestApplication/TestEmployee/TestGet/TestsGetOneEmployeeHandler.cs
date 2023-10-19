
using Application.Contracts;
using Application.Employees.Get;
using Domain.Entities;
using Domain.Repositories;
using Mapster;

public class GetOneEmployeeHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsEmployee_WhenEmployeeExists()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        var employeeId = Guid.NewGuid();
        var employee = new Employee
        {
            Id = employeeId,
            Firstname = "Erzhan",
            Lastname = "Kubanchbek",
            Patronymic = "Ava",
            Email = "avazov.erjan@gmail.com",
            Role = Domain.Enums.Role.Director,
            AuthoredTasks = new List<CustomTask>
            {
                new CustomTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Some name",
                    Comment = "Some comment",
                    AuthorId = employeeId,
                    ExecutorId = Guid.NewGuid(),
                    Priority = 10,
                    Status = Domain.Enums.StatusTask.ToDo,
                    ProjectId = Guid.NewGuid(),
                },
            },
            AssignedTasks = new List<CustomTask>
            {
                 new CustomTask
                 {
                    Id = Guid.NewGuid(),
                    Name = "Some name",
                    Comment = "Some comment",
                    AuthorId = Guid.NewGuid(),
                    ExecutorId = employeeId,
                    Priority = 5,
                    Status = Domain.Enums.StatusTask.InProgress,
                 },
            },
            ManagedProjects = new List<Project>
            {
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Some Project Name",
                    CustomerCompanyName = "Bers",
                    PerformingCompanyName = "It Academy",
                    StartDate= DateTime.Now,
                    EndDate =  DateTime.Parse("11.10.2023"),
                    Priority = 10,
                    ProjectManagerId = employeeId,
                    ProjectEmployees = new List<Employee>() 
                    {
                        new Employee
                        {
                            Id = Guid.NewGuid(),
                            Firstname = "Somename employee",
                            Lastname = "SomeLastname",
                            Patronymic = "SomePatr",
                            Email = "some@mail.com",
                            Role = Domain.Enums.Role.Employee,
                        }
                    },
                },
            },
        };

        employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employeeId)).ReturnsAsync(employee);

        var handler = new GetOneEmployeeHandler(employeeRepositoryMock.Object);

        // Act
        var result = await handler.Handle(new GetOneEmployeeRequest { Id = employeeId }, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(employee.Adapt<FullEmployeeDto>());
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenEmployeeDoesNotExist()
    {
        // Arrange
        var employeeRepoMock = new Mock<IEmployeeRepository>();
        var employeeId = Guid.NewGuid();
        employeeRepoMock.Setup(repo => repo.GetByIdAsync(employeeId)).ReturnsAsync((Employee)null!);

        var handler = new GetOneEmployeeHandler(employeeRepoMock.Object);

        // Act
        var result = await handler.Handle(new GetOneEmployeeRequest { Id = employeeId }, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}
