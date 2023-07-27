using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses;

public class ProjectStacksResponse
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }
    public ICollection<StackResponse> Stacks { get; set; }

    public ProjectStacksResponse(Guid projectId, string projectName, ICollection<StackResponse> stacks)
    {
        ProjectId = projectId;
        ProjectName = projectName;
        Stacks = stacks;
    }
}