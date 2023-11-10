using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers;

[Authorize(AuthenticationSchemes = "JwtBearer")]
[ApiController]
[Route("[controller]")]
public class StackController : ControllerBase
{
    private readonly StackService _stackService;

    public StackController(StackService stackService)
        => _stackService = stackService;

    [HttpPatch("projects/populate-all")]
    public async Task<IActionResult> PopulateAllProjects([FromQuery] Guid? projectId)
    {
        await _stackService.PopulateAllProjects(projectId);
        return Ok();
    }

    [HttpGet("all-languages")]
    public async Task<IEnumerable<StackResponse>> GetAllStacks()
        => await _stackService.GetAll();

    [HttpGet("projects")]
    public async Task<IEnumerable<ProjectStacksResponse>> GetByProject(
        [FromQuery] List<Guid>? projectId,
        [FromQuery] bool listOnlyActive = true)
        => await _stackService.GetStacks(projectId, listOnlyActive);

    [HttpGet("languages")]
    public async Task<IEnumerable<StackProjectsResponse>> GetByLanguage(
        [FromQuery] List<string>? languageId,
        [FromQuery] bool listOnlyActive = true)
        => await _stackService.GetStacks(languageId, listOnlyActive);

    [HttpPost("add/stack")]
    public async Task<IActionResult> AddStacks(
        [FromBody] AddStackRequest request)
        => await _stackService.AddStacks(request);
}