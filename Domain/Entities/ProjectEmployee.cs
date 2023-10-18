using Domain.Entities;

public class ProjectEmployee
{
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
}