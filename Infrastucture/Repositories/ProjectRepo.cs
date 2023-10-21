using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repositories
{
    internal class ProjectRepo : IProjectRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProjectRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateAsync(Project entity)
        {
            await _appDbContext.Projects.AddAsync(entity);
        }

        public Task<Guid[]> DeleteByIdAsync(params Guid[] id)
        {
            var projectToDelete = _appDbContext.Projects.Where(p => id.Contains(p.Id));
            _appDbContext.Projects.RemoveRange(projectToDelete);
            return Task.FromResult(id);
        }

        public Task<IEnumerable<Project>> FilterByStartDateRangeAsync(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> GetAllAsync()
        {
            return _appDbContext.Projects.AsNoTracking().ToListAsync();
        }

        public async Task<Project> GetByIdAsync(Guid id)
        {
            var project = await _appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project is null)
                throw new ArgumentNullException("Project not found");
            return project;
        }

        public Task<IEnumerable<Project>> SortByField(string fieldName)
        {
            throw new NotImplementedException();
        }

        public void Update(Project entity)
        {
            _appDbContext.Projects.Update(entity);
        }
    }
}
