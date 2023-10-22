using Domain.Enums;

namespace Application.Feature.Employees.Create
{
    public record CreateEmployeeDto
    {
        public Guid Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public string? Patronymic { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Role Role { get; set; }
    }
}
