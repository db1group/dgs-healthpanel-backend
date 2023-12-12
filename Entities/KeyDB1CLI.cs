namespace Db1HealthPanelBack.Entities;

public class KeyDB1CLI
{
    public Guid Key { get; set; }
    public Guid ProjectId { get; set; }
    public Guid LeadId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

