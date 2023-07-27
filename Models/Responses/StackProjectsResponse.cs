namespace Db1HealthPanelBack.Models.Responses;

public class StackProjectsResponse
{
    public string StackId { get; set; }
    public string StackName { get; set; }
    public ICollection<ProjectStackResponse> Projects { get; set; }

    public StackProjectsResponse(string stackId, string stackName, ICollection<ProjectStackResponse> projects)
    {
        StackId = stackId;
        StackName = stackName;
        Projects = projects;
    }
}

public class ProjectStackResponse
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }

    public ProjectStackResponse(Guid projectId, string projectName)
    {
        ProjectId = projectId;
        ProjectName = projectName;
    }
}
