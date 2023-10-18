public class Employee
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }

    public List<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
}