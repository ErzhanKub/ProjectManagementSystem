using Application.Feature.Contracts;
using Domain.Enums;

namespace Application.Feature.Employees.Get.GetOne
{
    public class EmployeeDto
    {
        public Guid Id { get; init; }
        public string? Firstname { get; init; }
        public string? Lastname { get; init; }
        public string? Patronymic { get; init; }
        public string? Email { get; init; }
        public Role Role { get; init; }

        public List<CustomTaskDto>? AuthoredTasks { get; set; }
        public List<CustomTaskDto>? AssignedTasks { get; set; }
    }
}
