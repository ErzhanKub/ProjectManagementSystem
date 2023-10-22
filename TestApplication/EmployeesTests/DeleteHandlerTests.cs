using Application.Feature.Employees.Delete;
using Application.Shared;
using Domain.Repositories;

namespace Application.Tests.Feature.Employees.Delete
{
    public class DeleteEmployeeByIdCommandTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteEmployeeByIdCommandTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldDeleteEmployee()
        {
            // Arrange
            var command = new DeleteEmployeeByIdsCommand
            {
                Id = new Guid[] { Guid.NewGuid() }
            };
            var handler = new DeleteEmployeeHandler(_employeeRepositoryMock.Object, _unitOfWorkMock.Object);

            _employeeRepositoryMock.Setup(x => x.DeleteByIdAsync(It.IsAny<Guid[]>()))
                .ReturnsAsync(command.Id);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(command.Id);
            _employeeRepositoryMock.Verify(x => x.DeleteByIdAsync(It.IsAny<Guid[]>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveCommitAsync(), Times.Once);
        }
    }
}
