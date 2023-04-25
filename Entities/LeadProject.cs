namespace Db1HealthPanelBack.Entities
{
    public class LeadProject
    {
        public Guid LeadId { get; set; }
        public Guid ProjectId { get; set; }

        public Project? Project { get; set; }

        public Lead? Lead { get; set; }
    }
}