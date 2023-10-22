using Application.Feature.Contracts;

namespace Application.Feature.Tasks.Create
{
    public record CreateTaskCommand : IRequest<Result<ResponseTaskDto>>
    {
        public Guid EmployeeId { get; init; }
        public TaskDto? Task { get; init; }
    }

    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(e => e.EmployeeId).NotEmpty();
            RuleFor(e => e.Task).NotNull();

            When(e => e.Task != null,
                () =>
                {
                    RuleFor(t => t.Task!.Name).NotEmpty().Length(1, 100);
                    RuleFor(t => t.Task!.Comment).Length(0, 500);
                    RuleFor(t => t.Task!.Priority).ExclusiveBetween(1, 100);
                });
        }
    }

    internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Result<ResponseTaskDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskHandler(ITaskRepository taskRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<ResponseTaskDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
                return Result.Fail<ResponseTaskDto>("Employee not found");

            var task = new CustomTask
            {
                Id = Guid.NewGuid(),
                Name = request.Task!.Name,
                Comment = request.Task.Comment!,
                Status = request.Task.Status,
                Priority = request.Task.Priority,
                AuthorId = employee.Id,
                Author = employee,
            };

            employee.AuthoredTasks!.Add(task);
            _employeeRepository.Update(employee);
            await _taskRepository.CreateAsync(task);

            await _unitOfWork.SaveCommitAsync();

            var response = task.Adapt<ResponseTaskDto>();
            return Result.Ok(response);
        }
    }
}
