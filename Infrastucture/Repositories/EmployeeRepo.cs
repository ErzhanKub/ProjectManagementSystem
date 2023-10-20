using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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

        public async Task<Employee> CheckUserCredentialsAsync(string email, string password)
        {
            var employee = await _appDbContext.Employees.SingleOrDefaultAsync(e => e.Email == email).ConfigureAwait(false);
            if (employee is null || await HashPasswordAsync(password).ConfigureAwait(false) != employee.PasswordHash)
                return default;
            return employee;
        }
        public async Task CreateAsync(Employee employee)
        {
            employee.PasswordHash = await HashPasswordAsync(employee.PasswordHash);
            await _appDbContext.Employees.AddAsync(employee);
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

        public async Task<string> HashPasswordAsync(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return await Task.FromResult(hash).ConfigureAwait(false);
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
