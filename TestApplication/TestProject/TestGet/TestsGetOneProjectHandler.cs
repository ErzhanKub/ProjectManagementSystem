using Application.Projects.Get;
using Domain.Entities;
using Domain.Repositories;

namespace TestApplication.TestProject.TestGet
{
    public class TestsGetOneProjectHandler
    {
        private Mock<IProjectRepository> _projectRepositoryMock;
        private GetOneProjectHandler _hander;

        public TestsGetOneProjectHandler()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _hander = new GetOneProjectHandler(_projectRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsProject()
        {
            //Arrange
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Project 1",
                CustomerCompanyName = "Academy",
                PerformingCompanyName = "Academy",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Priority = 10
            };

            var command = new GetOneProjectByIdRequest { Id = project.Id };
            _projectRepositoryMock.Setup(r => r.GetByIdAsync(project.Id)).ReturnsAsync(project);

            //Act
            var result = await _hander.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeTrue(); 
            result.Value.Should().BeEquivalentTo(project.Adapt<FullProjectDto>());
        }

        [Fact]
        public async Task Handle_ProjectNotFound_ReturnsFailResult()
        {
            //Arrange 
            var command = new GetOneProjectByIdRequest { Id = Guid.NewGuid() };
            _projectRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync((Project)null!);

            //Act
            var result = await _hander.Handle(command, default);

            //Assert
            result.IsFailed.Should().BeTrue();
        }
    }
}
