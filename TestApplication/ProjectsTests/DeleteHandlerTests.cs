using Application.Feature.Projects.Delete;
using Domain.Repositories;
using Application.Shared;

namespace TestApplication.TestProject
{
    public class DeleteProjectHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteProjectHandlerTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsOk()
        {
            // Arrange
            var command = new DeleteProjectByIdsCommand { Id = new Guid[] { Guid.NewGuid() } };
            var handler = new DeleteProjectHandler(_projectRepositoryMock.Object, _unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

    }
}