using Application.Feature.Projects.Update;
using Domain.Repositories;
using Application.Shared;
using Domain.Entities;

namespace TestApplication.TestProject
{
    public class UpdateProjectHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateProjectHandlerTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsOk()
        {
            // Arrange
            var command = new UpdateProjectByIdCommand
            {
                ProjectDto = new UProjectDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    CustomerCompanyName = "Test",
                    PerformingCompanyName = "Test",
                    Priority = 10
                }
            };
            var handler = new UpdateProjectHandler(_projectRepositoryMock.Object, _unitOfWorkMock.Object);
            _projectRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Project { Id = command.ProjectDto.Id });

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

    }
}