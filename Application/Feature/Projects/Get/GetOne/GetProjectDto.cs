using Application.Feature.Contracts;
using Application.Feature.Employees.Get.GetAll;

namespace Application.Feature.Projects.Get.GetOne
{
    public record GetProjectDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? CustomerCompanyName { get; set; }
        public string? PerformingCompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public Guid? ProjectManagerId { get; set; }

        public List<EmployeeProfileDto>? ProjectEmployees { get; set; } = new();
        public List<CustomTaskDto>? Tasks { get; set; } = new();
    }
}
