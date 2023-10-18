using Domain.Entities;
using Domain.Enums;

namespace Application.Contracts
{
    public record ProjectDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Employee> Employees { get; set; } = new();
        public required string CustomerCompanyName { get; set; }
        public required string PerformingCompanyName { get; set; }
        public required Employee ProjectManager { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Priority Priority { get; set; }
    }
}
