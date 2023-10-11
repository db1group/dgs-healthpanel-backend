using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProjectResponderController : ControllerBase
{
    private readonly ProjectResponderService _service;

    public ProjectResponderController(ProjectResponderService service)
        => _service = service;

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProjectResponderRequest projectResponder)
        => await _service.Create(projectResponder);
    
    [HttpGet("project/{projectId:guid}")]
    public async Task<IEnumerable<ProjectResponderResponse>> BetByProject(Guid projectId)
        => await _service.GetByProject(projectId);
    
    [HttpGet("email/{email}")]
    public async Task<IEnumerable<ProjectResponderResponse>> BetByProject(string email)
        => await _service.GetByEmail(email);
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
        => await _service.GetById(id);
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
        => await _service.Delete(id);
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromBody] ProjectResponderRequest projectResponder)
        => await _service.Update(id, projectResponder);
}