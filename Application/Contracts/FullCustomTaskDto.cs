using Domain.Enums;

namespace Application.Contracts
{
    public class FullCustomTaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public StatusTask Status { get; set; }
        public int Priority { get; set; }

        public Guid? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public Guid AuthorId { get; set; }
        public virtual Employee Author { get; set; }

        public Guid? ExecutorId { get; set; }
        public virtual Employee Executor { get; set; }
    }
}
