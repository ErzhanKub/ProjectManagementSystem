using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CustomerCompanyName { get; set; }
        public string PerformingCompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Priority Priority { get; set; }

        public Guid ProjectManagerId { get; set; }
        public Employee ProjectManager { get; set; }

        public List<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    }

}
