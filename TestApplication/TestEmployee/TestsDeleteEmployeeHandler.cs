using Application.Employees;
using Domain.Repositories;

namespace TestApplication.TestEmployee
{
    public class TestsDeleteEmployeeHandler
    {
        [Fact]
        public async Task Handle_ReturnsEmployeeId_WhenEmployeesExist()
        {
            // Arrange
            var emplRepoMock = new Mock<IEmployeeRepository>();
            var emplId = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            emplRepoMock.Setup(repo => repo.DeleteByIdAsync(emplId)).ReturnsAsync(emplId);

            var handler = new DeleteEmployeeHandler(emplRepoMock.Object);

            // Act
            var result = await handler.Handle(new DeleteEmployeeByIdCommand { Id = emplId }, default);

            // Assect
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(emplId);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenEmployeesDoNotExist()
        {
            // Arrange
            var emplRepoMock = new Mock<IEmployeeRepository>();
            var emplId = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            emplRepoMock.Setup(repo => repo.DeleteByIdAsync(emplId)).ReturnsAsync((Guid[])null!);

            var handler = new DeleteEmployeeHandler(emplRepoMock.Object);

            // Act
            var result = await handler.Handle(new DeleteEmployeeByIdCommand { Id = emplId }, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }
    }
}
