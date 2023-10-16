using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
            => _projectService = projectService;

        [HttpGet("")]
        public async Task<IEnumerable<ProjectResponse>> GetAllProjects()
            => await _projectService.GetAllProjects();

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
            => await _projectService.FindProject(id);

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProjectRequest project)
            => await _projectService.ImproveProject(id, project);

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
            => await _projectService.DeleteProject(id);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectRequest project)
            => await _projectService.CreateProject(project);
        
        [HttpPut("{id:guid}/disable-stacks")]
        public async Task<IActionResult> DisableProjectStacks([FromRoute] Guid id, [FromBody] ProjectStackRemovalRequest removalRequest)
            => await _projectService.DisableProjectStacks(id, removalRequest);
    }
}
