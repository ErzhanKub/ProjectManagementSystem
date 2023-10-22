using Application.Feature.Projects.Get.GetAll.Contracts;

namespace Application.Feature.Projects.Get.GetAll
{
    public record GetProjectsByDateRequest : IRequest<Result<IEnumerable<ProjectDto>>>
    {
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
    }

    public class GetProjectByDateValidator : AbstractValidator<GetProjectsByDateRequest>
    {
        public GetProjectByDateValidator()
        {
            RuleFor(s => s.StartDate).NotNull();
            RuleFor(e => e.EndDate).NotNull();
        }
    }

    internal class GetprojectsByDateHandler : IRequestHandler<GetProjectsByDateRequest, Result<IEnumerable<ProjectDto>>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetprojectsByDateHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Result<IEnumerable<ProjectDto>>> Handle(GetProjectsByDateRequest request, CancellationToken cancellationToken)
        {

            var projects = await _projectRepository.GetAllAsync();

            var filteredProjects = projects.Where(p => p.StartDate >= request.StartDate && p.EndDate <= request.EndDate).ToList();

            if (!filteredProjects.Any())
                return Result.Fail<IEnumerable<ProjectDto>>("No projects found in the given date range");

            var projectDtos = filteredProjects.Select(p => new ProjectDto
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
