using Application.Feature.Employees.Get.GetOne;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Tests.Feature.Employees.Get.GetOne
{
    public class GetOneEmployeeHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;

        public GetOneEmployeeHandlerTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnEmployee()
        {
            // Arrange
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Firstname = "Erzhan",
                Lastname = "Kubanchbek",
                Patronymic = "Ava",
                Email = "avazov.erjan@gmail.com",
                Role = Domain.Enums.Role.Director,
                AuthoredTasks = new List<CustomTask>
            {
                new CustomTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Some name",
                    Comment = "Some comment",
                    ExecutorId = Guid.NewGuid(),
                    Priority = 10,
                    Status = Domain.Enums.StatusTask.ToDo,
                    ProjectId = Guid.NewGuid(),
                },
            },
                AssignedTasks = new List<CustomTask>
            {
                 new CustomTask
                 {
                    Id = Guid.NewGuid(),
                    Name = "Some name",
                    Comment = "Some comment",
                    AuthorId = Guid.NewGuid(),
                    Priority = 5,
                    Status = Domain.Enums.StatusTask.InProgress,
                 },
            },
                ManagedProjects = new List<Project>
            {
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Some Project Name",
                    CustomerCompanyName = "Bers",
                    PerformingCompanyName = "It Academy",
                    StartDate= DateTime.Parse("11.10.2023"),
                    EndDate =  DateTime.Parse("11.10.2023"),
                    Priority = 10,
                    ProjectEmployees = new List<Employee>()
                    {
                        new Employee
                        {
                            Id = Guid.NewGuid(),
                            Firstname = "Somename employee",
                            Lastname = "SomeLastname",
                            Patronymic = "SomePatr",
                            Email = "some@mail.com",
                            Role = Domain.Enums.Role.Employee,
                        }
                    },
                },
            },
            };
            var handler = new GetOneEmployeeHandler(_employeeRepositoryMock.Object);

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(employee);

            // Act
            var result = await handler.Handle(new GetOneEmployeeRequest { Id = employee.Id }, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(employee, options => options.ExcludingMissingMembers());
            _employeeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldReturnFailure()
        {
            // Arrange
            var handler = new GetOneEmployeeHandler(_employeeRepositoryMock.Object);
            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Employee)null!);

            // Act
            var result = await handler.Handle(new GetOneEmployeeRequest { Id = Guid.NewGuid() }, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            _employeeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
        }

    }
}
