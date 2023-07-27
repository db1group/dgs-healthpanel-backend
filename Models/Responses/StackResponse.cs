namespace Db1HealthPanelBack.Models.Responses;

public class StackResponse
{
    public string StackId { get; set; }
    public string StackName { get; set; }

    public StackResponse(string stackId, string stackName)
    {
        StackId = stackId;
        StackName = stackName;
    }
}