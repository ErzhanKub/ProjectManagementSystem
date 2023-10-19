using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastucture.Repositories
{
    internal class EmployeeRepo : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task AddEmployeeToProjectAsync(Guid projectId, Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Employee entity)
        {
            await _appDbContext.Employees.AddAsync(entity);
        }

        public Task<Guid[]> DeleteByIdAsync(params Guid[] id)
        {
            var employeesToDelete = _appDbContext.Employees.Where(e => id.Contains(e.Id));
            _appDbContext.Employees.RemoveRange(employeesToDelete);
            return Task.FromResult(id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _appDbContext.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task RemoveEmployeeFromProjectAsync(Guid projectId, Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee entity)
        {
            _appDbContext.Employees.Update(entity);
        }
    }
}
