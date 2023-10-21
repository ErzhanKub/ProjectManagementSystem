namespace Application.Feature.Projects.Create
{
    public class CreateProjectDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string CustomerCompanyName { get; set; }
        public required string PerformingCompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
    }

}