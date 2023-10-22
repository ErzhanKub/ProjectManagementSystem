using Application.Feature.Contracts;

namespace Application.Feature.Tasks.Get.GetAll
{
    public record GetAllTasksRequest : IRequest<Result<IEnumerable<TaskDto>>> { }

    public class GetAllTaskValidator : AbstractValidator<GetAllTasksRequest>
    {
        public GetAllTaskValidator() { }
    }

    internal class GetAllTasksHandler : IRequestHandler<GetAllTasksRequest, Result<IEnumerable<TaskDto>>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetAllTasksHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Result<IEnumerable<TaskDto>>> Handle(GetAllTasksRequest request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllAsync();

            if (tasks == null)
                return new Result<IEnumerable<TaskDto>>();

            var response = tasks.Select(task => new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Comment = task.Comment,
                AuthorId = task.AuthorId,
                Priority = task.Priority,
                ProjectId = task.ProjectId,
                Status = task.Status,
            }).ToList();

            return Result.Ok((IEnumerable<TaskDto>)response);
        }
    }
}
