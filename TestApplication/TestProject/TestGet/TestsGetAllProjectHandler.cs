using Application.Projects.Get;
using Domain.Entities;
using Domain.Repositories;

namespace TestApplication.TestProject.TestGet
{
    public class TestsGetAllProjectHandler
    {
        private Mock<IProjectRepository> projectRepositoryMock;
        private GetAllProjectHandler _handler;

        public TestsGetAllProjectHandler()
        {
            projectRepositoryMock = new Mock<IProjectRepository>();
            _handler = new GetAllProjectHandler(projectRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsAllProjects()
        {
            //Arrange
            var projects = new List<Project>
            {
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Project 1",
                    CustomerCompanyName= "Microsoft",
                    PerformingCompanyName= "Microsoft",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Priority= 0,
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name ="Project 2",
                    CustomerCompanyName= "Microsoft",
                    PerformingCompanyName= "Microsoft",
                    StartDate= DateTime.Now,
                    EndDate= DateTime.Now,
                    Priority= 2,
                },
            };

            var command = new GetAllProjectRequest();

            projectRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(projects);

            //Act
            var result = await _handler.Handle(command, default);

            //Assrt
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(projects.Select(p => p.Adapt<ProjectDto>()));
        }

        [Fact]
        public async Task Handle_NoProjects_ReturnsEmptyList()
        {
            //Arrange
            var command = new GetAllProjectRequest();
            projectRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync((List<Project>)null!);

            //Act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsSuccess!.Should().BeTrue();
            result.Value.Should().BeNullOrEmpty();
        }
    }
}
