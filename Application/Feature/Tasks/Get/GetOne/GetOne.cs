namespace Application.Feature.Tasks.Get.GetOne
{
    public record GetOneTaskRequest : IRequest<Result<FullTaskDto>>
    {
        public Guid TaskId { get; init; }
    }

    public class GetOneTaskValidator : AbstractValidator<GetOneTaskRequest>
    {
        public GetOneTaskValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty();
        }
    }

    internal class GetOneTaskHander : IRequestHandler<GetOneTaskRequest, Result<FullTaskDto>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetOneTaskHander(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Result<FullTaskDto>> Handle(GetOneTaskRequest request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.TaskId);
            if (task == null)
                return Result.Fail<FullTaskDto>("Task not found");

            return Result.Ok(task.Adapt<FullTaskDto>());
        }
    }
}
