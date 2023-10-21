using Application.Feature.Employees.Login;
using Domain.Enums;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace Application.Tests.Feature.Employees.Login
{
    public class LoginRequestTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public LoginRequestTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _configurationMock = new Mock<IConfiguration>();
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnToken()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "test.user@example.com",
                Password = "password123",
            };
            var handler = new LoginHandler(_configurationMock.Object, _employeeRepositoryMock.Object);

            _employeeRepositoryMock.Setup(x => x.CheckUserCredentialsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Employee { Email = request.Email, Role = Role.Employee });
            _configurationMock.Setup(x => x["Jwt"]).Returns("qwertyuiopasdfgh");


            // Act
            var result = await handler.Handle(request, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNullOrEmpty();
            _employeeRepositoryMock.Verify(x => x.CheckUserCredentialsAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
