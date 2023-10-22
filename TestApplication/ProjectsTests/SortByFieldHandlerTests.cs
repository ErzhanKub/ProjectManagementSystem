using Application.Feature.Projects.Get.SortByField;
using Domain.Entities;
using Domain.Repositories;

namespace ApplicationTests.ProjectsTests
{
    public class SortByFieldHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;

        public SortByFieldHandlerTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsOk()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project { Id = Guid.NewGuid(), Name = "Project1" },
                new Project { Id = Guid.NewGuid(), Name = "Project2" },
                new Project { Id = Guid.NewGuid(), Name = "Project3" }
            };
            _projectRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(projects);

            var command = new SortProjectByFieldRequest { FieldName = "Name" };
            var handler = new SortProjectByFieldHandler(_projectRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(projects.Count);
            result.Value.Select(dto => dto.Id).Should().Equal(projects.OrderBy(p => p.Name).Select(p => p.Id));
        }
    }
}