using Application.Feature.Employees.Get.GetAll;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace TestApplication.TestEmployee.TestGet
{
    public class GetAllEmployeeHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _repositoryMock;

        public GetAllEmployeeHandlerTests()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnAllEmployees()
        {
            //Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Test",
                    Lastname = "User",
                    Patronymic = "testpat",
                    Email = "test.user@example.com",
                    PasswordHash = "qeqrqwr",
                    AssignedTasks = new List<CustomTask>
                    {
                        new CustomTask
                        {
                            Name = "Test",
                            Comment= "Test",
                            Priority= 10,
                            Status = StatusTask.Done,
                            Project = new Project(),
                            ExecutorId= Guid.NewGuid(),
                        }
                    },
                    AuthoredTasks = new List<CustomTask>(),
                    ManagedProjects = new List<Project>(),
                    MemberProjects= new List<Project>(),
                    Role = Role.Employee
                },
                new Employee
                { Id = Guid.NewGuid(),
                    Firstname = "Erzhan",
                    Lastname = "Kub",
                    Email = "john.doe@example.com",
                    Role = Role.ProjectManager
                }
            };
            var handler = new GetAllEmployeeHandler(_repositoryMock.Object);
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(employees);

            //Act
            var result = await handler.Handle(new GetAllEmployeeRequest(), default);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(employees, opts => opts.ExcludingMissingMembers());
            _repositoryMock.Verify(x => x.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldReturnFailure()
        {
            // Arrange
            var handler = new GetAllEmployeeHandler(_repositoryMock.Object);
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync((List<Employee>)null!);

            // Act
            var result = await handler.Handle(new GetAllEmployeeRequest(), default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeNull();
            _repositoryMock.Verify(x => x.GetAllAsync(), Times.Once());
        }

    }
}