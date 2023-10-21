using Application.Feature.Employees.InteractionWithProject;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;

namespace TestApplication.TestEmployee.InteractionWithProject
{
    public class AddToProjectHandlerTests
    {

        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public AddToProjectHandlerTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_ShouldAddEmployeeToProject_WhenCommandIsValid()
        {
            // Arrange
            var employee = new Employee { Id = Guid.NewGuid() };
            var project = new Project { Id = Guid.NewGuid() };

            _employeeRepositoryMock.Setup(r => r.GetByIdAsync(employee.Id)).ReturnsAsync(employee);
            _projectRepositoryMock.Setup(r => r.GetByIdAsync(project.Id)).ReturnsAsync(project);

            var handler = new AddEmployeeToProjectHandler(_employeeRepositoryMock.Object, _projectRepositoryMock.Object, _unitOfWorkMock.Object);
            var command = new AddEmployeeToProjectCommand { EmployeeId = employee.Id, ProjectId = project.Id };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            project.ProjectEmployees.Should().Contain(employee);
        }

    }
}
