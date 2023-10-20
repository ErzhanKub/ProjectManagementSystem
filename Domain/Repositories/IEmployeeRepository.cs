using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        string HashPassword(string password);
        Task<Employee> CheckUserCredentialsAsync(string email, string password);
    }
}
