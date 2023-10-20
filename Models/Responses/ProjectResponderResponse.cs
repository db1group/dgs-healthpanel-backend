using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses;

public class ProjectResponderResponse : IActionResult
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public bool IsLead { get; set; }
    public Guid ProjectId { get; set; }
    
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var result = new ObjectResult(this);

        await result.ExecuteResultAsync(context);
    }
}