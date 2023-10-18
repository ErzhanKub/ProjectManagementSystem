namespace Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public List<Project> Projects { get; set; } = new();
    }
}
