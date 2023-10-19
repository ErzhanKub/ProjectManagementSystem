using Application.Employees;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;

namespace TestApplication.TestEmployee
{
    public class TestsUpdateEmployeeProfileHandler
    {
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private UpdateEmployeeProfileHandler _handler;

        public TestsUpdateEmployeeProfileHandler()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateEmployeeProfileHandler(_employeeRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_UpdatesEmployeeAndReturnsOkResult()
        {
            //Arrange
            var employeeDto = new EmployeeProfileDto
            {
                Id = Guid.NewGuid(),
                Firstname = "Test",
                Lastname = "Test",
                Patronymic = "User",
                Email = "test@mail.com",
                Role = Domain.Enums.Role.Employee
            };

            var command = new UpdateEmployeeProfileByIdCommand
            {
                EmployeeDto = employeeDto,
            };

            var employee = new Employee
            {
                Id = employeeDto.Id
            };

            _employeeRepositoryMock.Setup(r => r.GetByIdAsync(employeeDto.Id)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(r => r.Update(It.IsAny<Employee>()));
            _unitOfWorkMock.Setup(u => u.SaveCommitAsync()).Returns(Task.CompletedTask);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(employeeDto);

            _employeeRepositoryMock.Verify(r => r.Update(It.Is<Employee>(e =>
            e.Firstname == employeeDto.Firstname &&
            e.Lastname == employeeDto.Lastname &&
            e.Patronymic == employeeDto.Patronymic &&
            e.Email == employeeDto.Email &&
            e.Role == employeeDto.Role
            )));

            _unitOfWorkMock.Verify(u => u.SaveCommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_EmployeeNotFound_ReturnsFailResult()
        {
            //Arrange
            var command = new UpdateEmployeeProfileByIdCommand
            {
                EmployeeDto = new EmployeeProfileDto
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Test",
                    Lastname = "Test",
                    Patronymic = "User",
                    Email = "test@mail.com",
                    Role = Domain.Enums.Role.Employee
                }
            };

            _employeeRepositoryMock.Setup(r => r.GetByIdAsync(command.EmployeeDto.Id)).ReturnsAsync((Employee)null!);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeFalse();
        }
    }
}
