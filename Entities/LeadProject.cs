namespace Db1HealthPanelBack.Entities
{
    public sealed class LeadProject
    {
        public Guid LeadId { get; set; }
        public Guid ProjectId { get; set; }

        public Project? Project { get; set; }

        public Lead? Lead { get; set; }
    }
}