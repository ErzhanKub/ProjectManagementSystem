using Domain.Enums;

namespace Application.Feature.Tasks.Get.GetOne
{
    public record FullTaskDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? Comment { get; init; }
        public StatusTask Status { get; init; }
        public int Priority { get; init; }
        public Guid? ProjectId { get; init; }
        public Guid AuthorId { get; init; }
        public Guid? ExecutorId { get; init; }
    }
}
