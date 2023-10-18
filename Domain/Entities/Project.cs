using Domain.Enums;

namespace Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Employee> Employees { get; set; } = new();
        public required string CustomerCompanyName { get; set; }
        public required string PerformingCompanyName { get; set; }
        public required Employee Supervisor { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public Priority Priority { get; set; }
    }
}
