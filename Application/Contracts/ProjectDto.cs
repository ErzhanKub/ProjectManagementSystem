﻿namespace Application.Contracts
{
    public class ProjectDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string CustomerCompanyName { get; set; } = string.Empty;
        public string PerformingCompanyName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
    }
}
