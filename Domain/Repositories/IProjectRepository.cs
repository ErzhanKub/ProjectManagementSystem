using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> FilterByStartDateRangeAsync(DateTime start, DateTime end);
        Task<IEnumerable<Project>> SortByField(string fieldName);
    }
}
