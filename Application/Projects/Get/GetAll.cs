namespace Application.Projects.Get
{
    public record GetAllProjectRequest : IRequest<Result<IEnumerable<ProjectDto>>> { }

    internal class GetAllProjectHandler : IRequestHandler<GetAllProjectRequest, Result<IEnumerable<ProjectDto>>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<Result<IEnumerable<ProjectDto>>> Handle(GetAllProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAllAsync().ConfigureAwait(false);
            if (project == null)
                return new Result<IEnumerable<ProjectDto>>();
            return Result.Ok(project.Select(project => project.Adapt<ProjectDto>()));
        }
    }
}
