using Domain.Enums;

namespace Application.Feature.Employees.Get.GetAll
{
    public record EmployeeProfileDto
    {
        public Guid Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Patronymic { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
    }
}
