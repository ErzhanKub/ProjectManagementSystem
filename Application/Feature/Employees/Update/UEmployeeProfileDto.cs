using Domain.Enums;

namespace Application.Feature.Employees.Update
{
    public record UEmployeeProfileDto
    {
        public Guid Id { get; init; }
        public required string Firstname { get; init; }
        public required string Lastname { get; init; }
        public string? Patronymic { get; init; }
        public required string Email { get; init; }
        public required string PasswordHash { get; init; }
        public Role Role { get; init; }
    }
}
