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

        public Task DeleteByIdAsync(params Guid[] Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Project entity)
        {
            throw new NotImplementedException();
        }
    }
}
