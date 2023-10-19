using Domain.Entities;
using Domain.Repositories;

namespace Infrastucture.Repositories
{
    internal class ProjectRepo : IProjectRepository
    {
        public Task CreateAsync(Project entity)
        {
            throw new NotImplementedException();
        }

        public Task<Guid[]> DeleteByIdAsync(params Guid[] id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Project>> FilterByStartDateRangeAsync(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Project>> SortByField(string fieldName)
        {
            throw new NotImplementedException();
        }

        public void Update(Project entity)
        {
            throw new NotImplementedException();
        }
    }
}
