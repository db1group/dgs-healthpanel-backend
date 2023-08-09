namespace Db1HealthPanelBack.Models.Requests;

public class ProjectStackRequest
{
    public Guid ProjectId { get; set; }
    public ICollection<string>? StacksId { get; set; }
}