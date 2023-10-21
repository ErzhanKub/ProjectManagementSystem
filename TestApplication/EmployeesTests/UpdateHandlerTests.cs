using Application.Feature.Employees.Update;
using Domain.Enums;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Tests.Feature.Employees.Update
{
    public class UpdateEmployeeByIdCommandTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateEmployeeByIdCommandTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldUpdateEmployee()
        {
            // Arrange
            var command = new UpdateEmployeeByIdCommand
            {
                EmployeeDto = new UEmployeeProfileDto
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Test",
                    Lastname = "User",
                    Email = "test.user@example.com",
                    PasswordHash = "password123",
                    Role = Role.Employee,
                }
            };
            var handler = new UpdateEmployeeHandler(_employeeRepositoryMock.Object, _unitOfWorkMock.Object);

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Employee { Id = command.EmployeeDto.Id });

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(command.EmployeeDto, options => options.ExcludingMissingMembers());
            _employeeRepositoryMock.Verify(x => x.Update(It.IsAny<Employee>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveCommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnFailure()
        {
            // Arrange
            var command = new UpdateEmployeeByIdCommand
            {
                EmployeeDto = new UEmployeeProfileDto
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Test",
                    Lastname = "User",
                    Email = "test.user@example.com",
                    PasswordHash = "password123",
                    Role = Role.Employee,
                }
            };
            var handler = new UpdateEmployeeHandler(_employeeRepositoryMock.Object, _unitOfWorkMock.Object);

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Employee)null!);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            _employeeRepositoryMock.Verify(x => x.Update(It.IsAny<Employee>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveCommitAsync(), Times.Never);
        }

    }
}
