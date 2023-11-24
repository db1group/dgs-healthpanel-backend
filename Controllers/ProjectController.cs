using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
            => _projectService = projectService;

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<ProjectResponse>>(StatusCodes.Status200OK)]
        public async Task<IEnumerable<ProjectResponse>> GetAllProjects()
            => await _projectService.GetAllProjects();

        /// <summary>
        /// Find a project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType<ProjectResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
            => await _projectService.FindProject(id);

        /// <summary>
        /// Edit a project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType<ProjectResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProjectRequest project)
            => await _projectService.ImproveProject(id, project);

        /// <summary>
        /// Delete a project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
            => await _projectService.DeleteProject(id);

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="project"></param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /project
        ///     {
        ///         "name": "Project Name",
        ///         "sonarName": "Sonar Name",
        ///         "sonarUrl": "Sonar Url",
        ///         "sonarToken": "Sonar Token",
        ///         "sonarProjectKeys": "Sonar Project Keys",
        ///         "leadProjects": [{ leadId: "Lead Id" }],
        ///         "costCenter": { costCenterId: "Cost Center Id" }
        ///     }
        ///     
        /// </remarks>
        /// <returns>A newly created project</returns>
        [HttpPost]
        [ProducesResponseType<ProjectResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] ProjectRequest project)
            => await _projectService.CreateProject(project);

        /// <summary>
        /// Disable a stack from a project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="removalRequest"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}/disable-stacks")]
        [ProducesResponseType<ProjectResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> DisableProjectStacks([FromRoute] Guid id, [FromBody] ProjectStackRemovalRequest removalRequest)
            => await _projectService.DisableProjectStacks(id, removalRequest);
    }
}
