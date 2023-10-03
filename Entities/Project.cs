namespace Db1HealthPanelBack.Entities
{
    public sealed class Project
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<LeadProject>? LeadProjects { get; set; }
        public ICollection<Evaluation>? Evaluations { get; set; }
        public uint? QuantityDevs { get; set; }
        public string? AdditionalData { get; set; }
        public Guid CostCenterId {get;set;}
        public CostCenter? CostCenter {get;set;}
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public ICollection<StackProject>? StackProjects { get; set; }
        public string? MetricsCollectorProjectName { get; set; }
    }
}