using Domain.Enums;

namespace Application.Feature.Contracts
{
    public class TaskDto
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public string? Comment { get; init; }
        public StatusTask Status { get; init; }
        public int Priority { get; init; }
        public Guid? ProjectId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
