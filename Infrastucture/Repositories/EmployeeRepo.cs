using Domain.Entities;
using Domain.Repositories;

namespace Infrastucture.Repositories
{
    internal class EmployeeRepo : IEmployeeRepository
    {
        public Task CreateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<Guid[]> DeleteByIdAsync(params Guid[] id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
