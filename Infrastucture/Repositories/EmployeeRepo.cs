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
        public async Task<Employee> CheckUserCredentialsAsync(string email, string password)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email).ConfigureAwait(false);
            if (employee is null)
                throw new ArgumentNullException("Employee email not found");

            var hashedPassword = HashPassword(password);
            if (hashedPassword != employee.PasswordHash)
                throw new ArgumentNullException("Incorrect password");

            return employee;
        }

        public async Task CreateAsync(Employee employee)
        {
            employee.PasswordHash = HashPassword(employee.PasswordHash);
            await _appDbContext.Employees.AddAsync(employee);
        }

        public Task<Guid[]> DeleteRangeAsync(params Guid[] id)
        {
            var employeesToDelete = _appDbContext.Employees.Where(e => id.Contains(e.Id));
            _appDbContext.Employees.RemoveRange(employeesToDelete);
            return Task.FromResult(id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _appDbContext.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee is null)
                throw new ArgumentNullException("Employee not found");
            return employee;
        }

        public string HashPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hash;
        }

        public void Update(Employee entity)
        {
            _appDbContext.Employees.Update(entity);
        }
    }
}
