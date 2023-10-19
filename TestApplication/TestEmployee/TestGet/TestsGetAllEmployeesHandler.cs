
using Application.Employees.Get;
using Domain.Entities;
using Domain.Repositories;

public class GetAllEmployeeHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllEmployees()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        var employees = new List<Employee>
            {
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Erzhan",
                    Lastname = "Kubanchbek",
                    Patronymic = "Ava",
                    Email = "avazov.erjan@gmail.com",
                    Role = Domain.Enums.Role.Director,
                },
                 new Employee
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Mark",
                    Lastname = "Melnik",
                    Patronymic = "Test",
                    Email = "mark@gmail.com",
                    Role = Domain.Enums.Role.ProjectManager,
                },
            };
        employeeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employees);

        var handler = new GetAllEmployeeHandler(employeeRepositoryMock.Object);

        // Act
        var result = await handler.Handle(new GetAllEmployeeRequest(), default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
    }
}
