namespace Application.Feature.Projects.Get.GetOne
{
    public record GetOneProjectByIdRequest : IRequest<Result<GetProjectDto>>
    {
        public Guid Id { get; init; }
    }

    public class GetOneProjectValidator : AbstractValidator<GetOneProjectByIdRequest>
    {
        public GetOneProjectValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class GetOneProjectHandler : IRequestHandler<GetOneProjectByIdRequest, Result<GetProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetOneProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<Result<GetProjectDto>> Handle(GetOneProjectByIdRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
            if (project == null)
                return Result.Fail<GetProjectDto>("Project not found");

            return Result.Ok(project.Adapt<GetProjectDto>());
        }
    }
}
