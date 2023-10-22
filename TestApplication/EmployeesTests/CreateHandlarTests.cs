using Application.Feature.Employees.Create;
using Domain.Enums;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Tests.Feature.Employees.Create
{
    public class CreateEmployeeCommandTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateEmployeeCommandTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateEmployee()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                Employee = new CreateEmployeeDto
                {
                    Firstname = "Test",
                    Lastname = "User",
                    Patronymic = "qwerty",
                    Email = "test.user@example.com",
                    Password = "password123",
                    Role = Role.Employee,
                }
            };
            var handler = new CreateEmployeeHandlar(_employeeRepositoryMock.Object, _unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _employeeRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Employee>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveCommitAsync(), Times.Once);
        }
    }
}
