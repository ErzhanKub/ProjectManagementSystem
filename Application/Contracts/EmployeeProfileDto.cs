using Domain.Enums;

namespace Application.Contracts
{
    public record EmployeeProfileDto
    {
        public Guid Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public string Patronymic { get; set; } = string.Empty;
        public required string Email { get; set; }
        public Role Role { get; set; }
    }
}
