namespace Db1HealthPanelBack.Entities;

public class ProjectResponder
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsLead { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
}