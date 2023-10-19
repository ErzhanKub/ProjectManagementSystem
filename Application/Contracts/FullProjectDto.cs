namespace Application.Contracts
{
    public record FullProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CustomerCompanyName { get; set; } = string.Empty;
        public string PerformingCompanyName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }


        public Guid? ProjectManagerId { get; set; }
        public EmployeeProfileDto? ProjectManager { get; set; }

        public List<EmployeeProfileDto>? ProjectEmployees { get; set; } = new();
        public List<CustomTaskDto>? Tasks { get; set; } = new();
    }
}
