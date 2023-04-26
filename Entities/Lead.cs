namespace Db1HealthPanelBack.Entities
{
    public sealed class Lead
    {
        public Guid Id { get; set; }
        public ICollection<LeadProject>? LeadProjects { get; set; }
        public string? Name { get; set; }
        public Boolean? InTraining { get; set; }
    }
}