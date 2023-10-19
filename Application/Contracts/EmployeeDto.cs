﻿using Domain.Enums;

namespace Application.Contracts
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public string Patronymic { get; set; } = string.Empty;
        public required string Email { get; set; }
        public Role Role { get; set; }

        public List<ProjectDto>? Projects { get; set; }
        public List<CustomTaskDto>? AuthoredTasks { get; set; }
        public List<CustomTaskDto>? AssignedTasks { get; set; }
    }
}
