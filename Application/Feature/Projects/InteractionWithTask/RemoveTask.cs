namespace Application.Feature.Projects.InteractionWithTask
{
    public record RemoveTaskToProjectCommand : IRequest<Result>
    {
        public Guid ProjectId { get; init; }
        public Guid TaskId { get; init; }
    }

    public class RemoveTaskToProjectValidator : AbstractValidator<RemoveTaskToProjectCommand>
    {
        public RemoveTaskToProjectValidator()
        {
            RuleFor(t => t.TaskId).NotEmpty();
            RuleFor(p => p.ProjectId).NotEmpty();
        }
    }

    public class RemoveTaskToProjectHandler : IRequestHandler<RemoveTaskToProjectCommand, Result>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTaskToProjectHandler(IProjectRepository projectRepository, ITaskRepository customTaskRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _taskRepository = customTaskRepository ?? throw new ArgumentNullException(nameof(customTaskRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(RemoveTaskToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            var task = await _taskRepository.GetByIdAsync(request.TaskId);

            if (project == null || task == null)
                return Result.Fail("Project or task not found");


            task.ProjectId = Guid.Empty;
            task.Project = default;

            project.Tasks!.Remove(task);

            _taskRepository.Update(task);
            _projectRepository.Update(project);
            await _unitOfWork.SaveCommitAsync();

            return Result.Ok();
        }
    }
}
