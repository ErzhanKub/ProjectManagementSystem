namespace Application.Contracts
{
    public record ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CustomerCompanyName { get; set; } = string.Empty;
        public string PerformingCompanyName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }


        public Guid? ProjectManagerId { get; set; }
        public Employee? ProjectManager { get; set; }

        public List<Employee>? ProjectEmployees { get; set; } = new();
        public List<CustomTask>? Tasks { get; set; } = new();
    }
}
