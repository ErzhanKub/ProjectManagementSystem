namespace Application.Feature.Tasks.Delete
{
    public record DeleteTaskByIdsCommand : IRequest<Result<Guid[]>>
    {
        public Guid EmployeeId { get; init; }
        public Guid[]? TasksId { get; init; }
    }

    public class DeleteTaskByIdsValidator : AbstractValidator<DeleteTaskByIdsCommand>
    {
        public DeleteTaskByIdsValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty();
            RuleFor(x => x.TasksId).NotEmpty();
        }
    }

    internal class DeleteTaskByIdsHandler : IRequestHandler<DeleteTaskByIdsCommand, Result<Guid[]>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskByIdsHandler(ITaskRepository taskRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<Guid[]>> Handle(DeleteTaskByIdsCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
                return Result.Fail<Guid[]>("Employee not found");

            var tasksToDelete = employee.AuthoredTasks?.Where(t => request.TasksId!.Contains(t.Id)).ToList();
            if (tasksToDelete == null || !tasksToDelete.Any())
                return Result.Fail<Guid[]>("Tasks not found");

            var deletedTaskIds = tasksToDelete.Select(t => t.Id).ToArray();

            await _taskRepository.DeleteRangeAsync(deletedTaskIds);
            await _unitOfWork.SaveCommitAsync();

            return Result.Ok(deletedTaskIds);
        }
    }
}
