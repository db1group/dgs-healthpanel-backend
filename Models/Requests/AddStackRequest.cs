namespace Db1HealthPanelBack.Models.Requests;

public class AddStackRequest
{
    public Guid ProjectId { get; set; }
    public string StackId { get; set; }
}