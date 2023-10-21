using Application.Feature.Employees.InteractionWithProject;
using Domain.Repositories;
using Application.Shared;
using Domain.Entities;

namespace TestApplication.TestEmployee
{
    public class AppointEmployeeHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public AppointEmployeeHandlerTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsOk()
        {
            // Arrange
            var employee = new Employee { Id = Guid.NewGuid() };
            var project = new Project { Id = Guid.NewGuid() };

            _employeeRepositoryMock.Setup(e => e.GetByIdAsync(employee.Id)).ReturnsAsync(employee);
            _projectRepositoryMock.Setup(p => p.GetByIdAsync(project.Id)).ReturnsAsync(project);

            var handler = new AppointEmployeeHandler(_projectRepositoryMock.Object, _employeeRepositoryMock.Object, _unitOfWorkMock.Object);
            var command = new AppointEmployeeCommand { EmployeeId = employee.Id, ProjectId = project.Id };
            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}