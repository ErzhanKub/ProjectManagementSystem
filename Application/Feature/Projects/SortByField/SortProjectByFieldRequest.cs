using System.Linq.Dynamic.Core;
namespace Application.Feature.Projects.SortByField
{
    public class SortProjectByFieldRequest : IRequest<Result<IEnumerable<ProjectDtoInfo>>>
    {
        public string? FieldName { get; init; }
    }

    public class SortProjectByFieldValdator : AbstractValidator<SortProjectByFieldRequest>
    {
        public SortProjectByFieldValdator() { }
    }

    public class SortProjectByFieldHandler : IRequestHandler<SortProjectByFieldRequest, Result<IEnumerable<ProjectDtoInfo>>>
    {
        private readonly IProjectRepository _projectRepository;

        public SortProjectByFieldHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Result<IEnumerable<ProjectDtoInfo>>> Handle(SortProjectByFieldRequest request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllAsync();

            var propertyExists = typeof(Project).GetProperties().Any(p => p.Name == request.FieldName);
            if (!propertyExists)
                return Result.Fail<IEnumerable<ProjectDtoInfo>>($"Property '{request.FieldName}' not found");

            var sortedProjects = projects.AsQueryable()
                .OrderBy(request.FieldName!)
                .ToList();

            var projectDtos = sortedProjects.Select(p => new ProjectDtoInfo
            {
                Id = p.Id,
                Name = p.Name,
                CustomerCompanyName = p.CustomerCompanyName,
                PerformingCompanyName = p.PerformingCompanyName,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Priority = p.Priority,
            }).ToList();

            return Result.Ok<IEnumerable<ProjectDtoInfo>>(projectDtos);
        }
    }
}
