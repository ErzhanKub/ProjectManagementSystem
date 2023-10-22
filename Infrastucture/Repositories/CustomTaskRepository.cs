using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repositories
{
    public class CustomTaskRepository : ITaskRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomTaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateAsync(CustomTask entity)
        {
            await _appDbContext.Tasks.AddAsync(entity);
        }

        public Task<Guid[]> DeleteByIdAsync(params Guid[] id)
        {
            var taskToDelete = _appDbContext.Tasks.Where(t => id.Contains(t.Id));
            _appDbContext.Tasks.RemoveRange(taskToDelete);
            return Task.FromResult(id);
        }

        public Task<List<CustomTask>> GetAllAsync()
        {
            return _appDbContext.Tasks.AsNoTracking().ToListAsync();
        }

        public async Task<CustomTask> GetByIdAsync(Guid id)
        {
            var task = await _appDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task not found\nTaskRepository");
            return task;
        }

        public void Update(CustomTask entity)
        {
            _appDbContext.Tasks.Update(entity);
        }
    }
}
