namespace Application.Feature.Projects.Create
{
    public record CreateProjectCommand : IRequest<Result<CreateProjectDto>>
    {
        public CreateProjectDto? Project { get; init; }
    }

    public class CreateProjectValidetor : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidetor()
        {
            RuleFor(c => c.Project).NotNull();
            When(c => c.Project != null,
                () =>
                {
                    RuleFor(c => c.Project!.Name).NotEmpty().Length(1, 150);
                    RuleFor(c => c.Project!.CustomerCompanyName).NotEmpty().Length(1, 200);
                    RuleFor(c => c.Project!.PerformingCompanyName).NotEmpty().Length(1, 200);
                    RuleFor(c => c.Project!.Priority).ExclusiveBetween(1, 100).GreaterThanOrEqualTo(1);
                });
        }
    }

    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Result<CreateProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateProjectDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.Adapt<Project>();

            project.Id = Guid.NewGuid();
            project.Name = request.Project!.Name;
            project.CustomerCompanyName = request.Project.CustomerCompanyName;
            project.PerformingCompanyName = request.Project.PerformingCompanyName;
            project.Priority = request.Project.Priority;
            project.StartDate = request.Project.StartDate;
            project.EndDate = request.Project.EndDate;


            await _projectRepository.CreateAsync(project).ConfigureAwait(false);
            await _unitOfWork.SaveCommitAsync();
            return Result.Ok(request.Project);
        }
    }
}
