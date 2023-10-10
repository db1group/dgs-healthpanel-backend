namespace Db1HealthPanelBack.Models.Responses;

public class StackResponse
{
    public string StackId { get; set; }
    public string StackName { get; set; }
    public bool Active { get; set; }

    public StackResponse(string stackId, string stackName, bool active)
    {
        StackId = stackId;
        StackName = stackName;
        Active = active;
    }
}