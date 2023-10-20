namespace Application.Projects
{
    public record CreateProjectCommand : IRequest<Result<ProjectDto>>
    {
        public ProjectDto? ProjectDto { get; init; }
    }

    public class CreateProjectValidetor : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidetor()
        {
            RuleFor(c => c.ProjectDto).NotNull();
            When(c => c.ProjectDto != null,
                () =>
                {
                    RuleFor(c => c.ProjectDto!.Name).NotEmpty().Length(1, 150);
                    RuleFor(c => c.ProjectDto!.CustomerCompanyName).NotEmpty().Length(1, 200);
                    RuleFor(c => c.ProjectDto!.PerformingCompanyName).NotEmpty().Length(1, 200);
                    //RuleFor(c => c.ProjectDto!.Priority).IsInEnum();
                    //RuleFor(c => c.ProjectDto!.StartDate).LessThan(DateOnly.);
                });
        }
    }

    internal class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Result<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProjectDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.Adapt<Project>();
            await _projectRepository.CreateAsync(project).ConfigureAwait(false);
            await _unitOfWork.SaveCommitAsync();
            return Result.Ok(project.Adapt<ProjectDto>());
        }
    }
}
