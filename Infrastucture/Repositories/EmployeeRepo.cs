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

        public Task DeleteByIdAsync(params Guid[] Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
