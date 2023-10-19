using Domain.Enums;

namespace Application.Contracts
{
    public class CustomTaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public StatusTask Status { get; set; }
        public int Priority { get; set; }
    }
}
