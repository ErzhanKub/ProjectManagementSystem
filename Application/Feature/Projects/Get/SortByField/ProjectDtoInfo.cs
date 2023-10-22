namespace Application.Feature.Projects.Get.SortByField
{
    public record ProjectDtoInfo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? CustomerCompanyName { get; set; }
        public string? PerformingCompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
    }
}
