using Application.Feature.Projects.Get.GetAll.Contracts;
using System.Linq.Dynamic.Core;

namespace Application.Feature.Projects.Get.GetAll
{
    public record SortProjectsByFieldRequest : IRequest<Result<IEnumerable<ProjectDto>>>
    {
        public string? FieldName { get; init; }
    }

    public class SortProjectByFieldValdator : AbstractValidator<SortProjectsByFieldRequest>
    {
        public SortProjectByFieldValdator() { }
    }

    public class SortProjectByFieldHandler : IRequestHandler<SortProjectsByFieldRequest, Result<IEnumerable<ProjectDto>>>
    {
        private readonly IProjectRepository _projectRepository;

        public SortProjectByFieldHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<Result<IEnumerable<ProjectDto>>> Handle(SortProjectsByFieldRequest request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllAsync();

            var propertyExists = typeof(Project).GetProperties().Any(p => p.Name == request.FieldName);
            if (!propertyExists)
                return Result.Fail<IEnumerable<ProjectDto>>($"Property '{request.FieldName}' not found");

            var sortedProjects = projects.AsQueryable()
                .OrderBy(request.FieldName!)
                .ToList();

            var projectDtos = sortedProjects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                CustomerCompanyName = p.CustomerCompanyName,
                PerformingCompanyName = p.PerformingCompanyName,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Priority = p.Priority,
            }).ToList();

            return Result.Ok<IEnumerable<ProjectDto>>(projectDtos);
        }
    }
}
