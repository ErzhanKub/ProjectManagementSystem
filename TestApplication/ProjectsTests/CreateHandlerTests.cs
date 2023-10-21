using Application.Feature.Projects.Create;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;

namespace TestApplication.TestProject
{
    public class CreateHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateHandlerTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateProject()
        {
            // Arrange
            var command = new CreateProjectCommand
            {
                Project = new CreateProjectDto
                {
                    Name = "Test",
                    CustomerCompanyName = "Test Company",
                    PerformingCompanyName = "Test Company",
                    Priority = 1,
                }
            };
            var handler = new CreateProjectHandler(_projectRepositoryMock.Object, _unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(command.Project, options => options.ExcludingMissingMembers());
            _projectRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Project>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveCommitAsync(), Times.Once);
        }

    }
}
