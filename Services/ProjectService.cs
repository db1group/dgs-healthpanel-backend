using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Models.Responses.Errors;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Db1HealthPanelBack.Services
{
    public class ProjectService
    {
        private readonly ContextConfig _contextConfig;

        public ProjectService(ContextConfig contextConfig)
        {
            _contextConfig = contextConfig;
        }

        public async Task<IEnumerable<ProjectResponse>> GetAllProjects()
        {
            var projects = await _contextConfig.Projects
                            .Include(p => p.CostCenter)
                            .Include(prop => prop.LeadProjects!)
                            .ThenInclude(prop => prop.Lead)
                            .ToListAsync();

            return projects.Adapt<ICollection<ProjectResponse>>();
        }
        public async Task<IActionResult> FindProject(Guid id)
        {
            var project = await _contextConfig.Projects
                            .Include(p => p.CostCenter)
                            .Include(prop => prop.LeadProjects!)
                            .ThenInclude(prop => prop.Lead)
                            .FirstOrDefaultAsync(property => property.Id == id);

            if (project is null) return new ErrorResponse("Project Not Found");

            return project.Adapt<ProjectResponse>();
        }

        public async Task<IActionResult> ImproveProject(Guid id, ProjectRequest project)
        {
            var projectResult = await _contextConfig.Projects.FirstOrDefaultAsync(property => property.Id == id);

            if (projectResult is null) return new ErrorResponse("Project Not Found");

            projectResult.Name = project.Name;

            _contextConfig.Update(projectResult);
            await _contextConfig.SaveChangesAsync();

            return projectResult.Adapt<ProjectResponse>();
        }

        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await _contextConfig.Projects.FirstOrDefaultAsync(property => property.Id == id);

            if (project is null) return new ErrorResponse("Project Not Found");

            _contextConfig.Remove(project);
            await _contextConfig.SaveChangesAsync();

            return new ObjectResult(null) { StatusCode = 204 };
        }

        public async Task<IActionResult> CreateProject(ProjectRequest project)
        {
            var projectEntity = project.Adapt<Project>();

            await _contextConfig.AddAsync(projectEntity);
            await _contextConfig.SaveChangesAsync();

            var result = projectEntity.Adapt<ProjectResponse>();

            result.SetStatusCode(201);

            return result;
        }
    }
}