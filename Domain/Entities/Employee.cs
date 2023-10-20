using Domain.Enums;

namespace Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Employee;

        public List<Project>? ManagedProjects { get; set; } = new();
        public List<Project>? MemberProjects { get; set; } = new();
        public List<CustomTask>? AuthoredTasks { get; set; } = new();
        public List<CustomTask>? AssignedTasks { get; set; } = new();

    }
}