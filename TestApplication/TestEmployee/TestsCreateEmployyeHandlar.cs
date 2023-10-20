using Application.Employees;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;

public class CreateEmployeeHandlerTests
{
    private Mock<IEmployeeRepository> _employeeRepositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private CreateEmployeeHandlar _handler;

    public CreateEmployeeHandlerTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateEmployeeHandlar(_employeeRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_CreatesEmployeeAndReturnsOkResult()
    {
        // Arrange
        var employeeDto = new EmployeeProfileDto
        {
            Firstname = "Test",
            Lastname = "User",
            Patronymic = "tet",
            Email = "test.user@example.com",
            PasswordHash = "123",
            Role = Domain.Enums.Role.Employee,
        };
        var command = new CreateEmployeeCommand { EmployeeDto = employeeDto };

        _employeeRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveCommitAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(employeeDto, options => options.Excluding(e => e.Id));
        _employeeRepositoryMock.Verify(r => r.CreateAsync(It.Is<Employee>(e =>
            e.Firstname == employeeDto.Firstname &&
            e.Lastname == employeeDto.Lastname &&
            e.Email == employeeDto.Email)), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveCommitAsync(), Times.Once);
    }
}
