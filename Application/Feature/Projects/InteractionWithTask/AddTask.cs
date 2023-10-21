namespace Application.Feature.Projects.InteractionWithTask
{
    public class AddTaskToProjectCommand : IRequest<Result>
    {
        public Guid ProjectId { get; init; }
        public Guid TaskId { get; init; }
    }

    public class AddTaskToProjectValidator : AbstractValidator<AddTaskToProjectCommand>
    {
        public AddTaskToProjectValidator()
        {
            RuleFor(t => t.TaskId).NotEmpty();
            RuleFor(p => p.ProjectId).NotEmpty();
        }
    }

    public class AddTaskToProjectHandler : IRequestHandler<AddTaskToProjectCommand, Result>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICustomTaskRepository _customTaskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTaskToProjectHandler(IProjectRepository projectRepository, ICustomTaskRepository customTaskRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _customTaskRepository = customTaskRepository ?? throw new ArgumentNullException(nameof(customTaskRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(AddTaskToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            var task = await _customTaskRepository.GetByIdAsync(request.TaskId);

            if (project == null || task == null)
                return Result.Fail("Project or task not found");

            project.Tasks!.Add(task);

            _projectRepository.Update(project);
            await _unitOfWork.SaveCommitAsync();

            return Result.Ok();
        }
    }
}
