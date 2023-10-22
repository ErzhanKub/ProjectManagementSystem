namespace Application.Feature.Projects.Update
{
    public record UpdateProjectByIdCommand : IRequest<Result<UProjectDto>>
    {
        public UProjectDto? ProjectDto { get; init; }
    }

    public class UpdateProjectValidator : AbstractValidator<UpdateProjectByIdCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(c => c.ProjectDto).NotNull();
            When(c => c.ProjectDto != null,
                () =>
                {
                    RuleFor(c => c.ProjectDto!.Name).NotEmpty().Length(1, 150);
                    RuleFor(c => c.ProjectDto!.CustomerCompanyName).NotEmpty().Length(1, 200);
                    RuleFor(c => c.ProjectDto!.PerformingCompanyName).NotEmpty().Length(1, 200);
                    RuleFor(c => c.ProjectDto!.Priority).ExclusiveBetween(1, 100).GreaterThanOrEqualTo(1);
                });
        }
    }

    public class UpdateProjectHandler : IRequestHandler<UpdateProjectByIdCommand, Result<UProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<UProjectDto>> Handle(UpdateProjectByIdCommand request, CancellationToken cancellationToken)
        {

            var project = await _projectRepository.GetByIdAsync(request.ProjectDto!.Id);

            if (project is null)
                return Result.Fail<UProjectDto>("Project not found");

            project = request.ProjectDto.Adapt(project);

            _projectRepository.Update(project);
            await _unitOfWork.SaveCommitAsync();

            return Result.Ok(request.ProjectDto);
        }
    }
}
