namespace Application.Projects.Get
{
    public record GetAllProjectRequest : IRequest<Result<IEnumerable<ProjectDto>>> { }

    public class GetAllProjectHandler : IRequestHandler<GetAllProjectRequest, Result<IEnumerable<ProjectDto>>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<Result<IEnumerable<ProjectDto>>> Handle(GetAllProjectRequest request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllAsync().ConfigureAwait(false);

            if (projects == null)
                return new Result<IEnumerable<ProjectDto>>();

            var response = projects.Select(project => new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                CustomerCompanyName = project.CustomerCompanyName,
                PerformingCompanyName = project.PerformingCompanyName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
            }).ToList();

            return Result.Ok((IEnumerable<ProjectDto>)response);
        }
    }
}
