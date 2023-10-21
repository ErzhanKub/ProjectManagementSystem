using Domain.Entities;
using Domain.Repositories;

namespace Infrastucture.Repositories
{
    internal class CustomTaskRepository : ICustomTaskRepository
    {
        public Task CreateAsync(CustomTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<Guid[]> DeleteByIdAsync(params Guid[] id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomTask>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomTask> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(CustomTask entity)
        {
            throw new NotImplementedException();
        }
    }
}
