namespace Db1HealthPanelBack.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<LeadProject>? LeadProjects { get; set; }
    }
}