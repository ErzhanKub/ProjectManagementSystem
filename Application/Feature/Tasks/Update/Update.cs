using Application.Feature.Tasks.Get.GetOne;

namespace Application.Feature.Tasks.Update
{
    public record UpdateTaskCommand : IRequest<Result<FullTaskDto>>
    {
        public Guid EmployeeId { get; init; }
        public FullTaskDto? TaskDto { get; init; }
    }

    public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty();
            RuleFor(x => x.TaskDto).NotNull();

            When(x => x.TaskDto != null,
                () =>
                {
                    RuleFor(x => x.TaskDto!.Id).NotEmpty();
                    RuleFor(x => x.TaskDto!.Name).NotEmpty().Length(1, 200);
                    RuleFor(x => x.TaskDto!.Priority).NotEmpty().InclusiveBetween(1, 100);
                });
        }
    }

    internal class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Result<FullTaskDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskHandler(ITaskRepository taskRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<FullTaskDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
                return Result.Fail<FullTaskDto>("Employee not found");

            var task = employee.AuthoredTasks?.FirstOrDefault(t => t.Id == request.TaskDto!.Id);
            if (task == null)
                return Result.Fail<FullTaskDto>("Task not found");

            task.Name = request.TaskDto!.Name!;
            task.Priority = request.TaskDto.Priority;
            task.ExecutorId = request.TaskDto.ExecutorId;
            task.Comment = request.TaskDto!.Comment!;
            task.Status = request.TaskDto.Status;
            task.ProjectId = request.TaskDto.ProjectId;

            _taskRepository.Update(task);
            await _unitOfWork.SaveCommitAsync();

            return Result.Ok(request.TaskDto);
        }
    }
}
