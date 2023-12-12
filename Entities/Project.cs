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
        public Guid CostCenterId { get; set; }
        public CostCenter? CostCenter { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public string? SonarName { get; set; }
        public string? SonarUrl { get; set; }
        public string? SonarToken { get; set; }
        public string? SonarProjectKeys { get; set; }
        public bool UseDB1CLI { get; set; } = false;
        public ICollection<StackProject>? StackProjects { get; set; }
        public ICollection<ProjectResponder>? ProjectResponders { get; set; }
    }
}
