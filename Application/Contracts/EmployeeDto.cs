using Domain.Entities;

namespace Application.Contracts
{
    public record EmployeeDto
    {
        public Guid Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public List<Project> Projects { get; set; } = new();
    }
}
