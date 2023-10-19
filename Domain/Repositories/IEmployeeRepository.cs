using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task AddEmployeeToProjectAsync(Guid projectId, Guid employeeId);
        Task RemoveEmployeeFromProjectAsync(Guid projectId, Guid employeeId);
    }
}
