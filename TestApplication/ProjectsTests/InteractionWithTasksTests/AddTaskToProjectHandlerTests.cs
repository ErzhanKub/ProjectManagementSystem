using Application.Feature.Projects.InteractionWithTask;
using Domain.Repositories;
using Application.Shared;
using Application.Feature.Employees.InteractionWithProject;
using Domain.Entities;

namespace TestApplication.TestProject.InteractionWithTask
{
    public class AddTaskToProjectHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<ICustomTaskRepository> _customTaskRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public AddTaskToProjectHandlerTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _customTaskRepositoryMock = new Mock<ICustomTaskRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsOk()
        {
            // Arrange
            var project = new Project { Id = Guid.NewGuid() };
            var task = new CustomTask { Id = Guid.NewGuid() };

            _projectRepositoryMock.Setup(p => p.GetByIdAsync(project.Id)).ReturnsAsync(project);
            _customTaskRepositoryMock.Setup(t => t.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var handler = new AddTaskToProjectHandler(_projectRepositoryMock.Object, _customTaskRepositoryMock.Object, _unitOfWorkMock.Object);
            var command = new AddTaskToProjectCommand { ProjectId = project.Id, TaskId = task.Id };
            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            project.Tasks.Should().Contain(task);
        }
    }
}