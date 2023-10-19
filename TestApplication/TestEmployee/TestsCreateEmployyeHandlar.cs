using Domain.Repositories;
using Application.Shared;
using Application.Employees;
using Application.Contracts;
using Domain.Entities;

namespace TestApplication.TestEmployee
{
    public class CreateEmployyeHandlarTests
    {

        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateEmployeeHandlar _handlar;

        public CreateEmployyeHandlarTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handlar = new CreateEmployeeHandlar(_employeeRepositoryMock.Object,
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_ValidCommand_CreateEmploee()
        {
            // Arange 
            var command = new CreateEmployeeCommand
            {
                EmployeeDto = new EmployeeDto
                {
                    Firstname = "Erzhan",
                    Lastname = "Kub",
                    Email = "avazov.erjan@gmail.com",
                }
            };

            // Act
            var result = await _handlar.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Firstname.Should().Be(command.EmployeeDto.Firstname);
            result.Value.Lastname.Should().Be(command.EmployeeDto.Lastname);
            result.Value.Email.Should().Be(command.EmployeeDto.Email);

            _employeeRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Employee>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveCommitAsync(), Times.Once);
        }
    }
}