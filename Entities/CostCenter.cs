namespace Db1HealthPanelBack.Entities
{
    public sealed class CostCenter
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Project>? Projects { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}