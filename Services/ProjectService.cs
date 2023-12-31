using System.Net;
using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Infra.Http;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Models.Responses.Errors;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Db1HealthPanelBack.Services
{
    public class ProjectService
    {
        private readonly ContextConfig _contextConfig;
        private readonly SonarHttpService _sonarHttpService;

        public ProjectService(ContextConfig contextConfig, SonarHttpService sonarHttpService)
        {
            _contextConfig = contextConfig;
            _sonarHttpService = sonarHttpService;
        }

        public async Task<IEnumerable<ProjectResponse>> GetAllProjects()
        {
            var projects = await _contextConfig.Projects
                            .Include(p => p.CostCenter)
                            .Include(prop => prop.LeadProjects!)
                            .ThenInclude(prop => prop.Lead)
                            .Include(prop => prop.CostCenter)
                            .OrderBy(pro => pro.Name)
                            .ToListAsync();

            foreach (var project in projects)
            {
                project.SonarToken = "";
            }

            return projects.Adapt<ICollection<ProjectResponse>>();
        }
        public async Task<IActionResult> FindProject(Guid id)
        {
            var project = await _contextConfig.Projects
                            .Include(p => p.CostCenter)
                            .Include(prop => prop.LeadProjects!)
                            .ThenInclude(prop => prop.Lead)
                            .Include(prop => prop.CostCenter)
                            .FirstOrDefaultAsync(property => property.Id == id);

            if (project is null) return new ErrorResponse("Project Not Found");

            project.SonarToken = string.Empty;

            return project.Adapt<ProjectResponse>();
        }

        public async Task<IActionResult> ImproveProject(Guid id, ProjectRequest project)
        {
            var projectResult = await _contextConfig.Projects
                                    .Include(prop => prop.LeadProjects)
                                    .FirstOrDefaultAsync(property => property.Id == id);

            if (projectResult is null) return new ErrorResponse("Project Not Found");

            var costCenter = await _contextConfig.CostCenters.FirstAsync(prop => prop.Id == project.CostCenter!.Id);

            if (costCenter is null) return new ErrorResponse("Cost Center Not Found");

            var sonarTokenValidationResult = await SonarTokenValidation(project.SonarUrl!, project.SonarToken!, projectResult);

            if (sonarTokenValidationResult is not null) return sonarTokenValidationResult;

            var isDuplicate = _contextConfig.Projects.Where(proj => proj.Name == project.Name).Any();

            if (project.Name != projectResult.Name && isDuplicate)
            {
                var resultError = projectResult.Adapt<ProjectResponse>();
                resultError.SetStatusCode(409);
                return resultError;
            }

            projectResult.Name = project.Name;
            projectResult.CostCenter = costCenter;
            projectResult.SonarName = project.SonarName;
            projectResult.SonarUrl = project.SonarUrl;
            projectResult.SonarProjectKeys = project.SonarProjectKeys;

            if (!project.SonarToken.IsNullOrEmpty())
            {
                projectResult.SonarToken = project.SonarToken;
            }

            AddLeadsToProject(project.LeadProjects, projectResult);

            _contextConfig.Update(projectResult);
            await _contextConfig.SaveChangesAsync();

            projectResult.SonarToken = string.Empty;

            return projectResult.Adapt<ProjectResponse>();
        }

        private static void AddLeadsToProject(ICollection<LeadProjectRequest>? leadsProject, Project projectResult)
        {
            if (leadsProject is null)
            {
                projectResult.LeadProjects = new List<LeadProject>();

                return;
            }

            projectResult.LeadProjects = projectResult.LeadProjects?
                    .Where(ld => leadsProject!.Any(lds => lds.ProjectId == ld.ProjectId))
                    .ToList();

            var leadsToAdd = leadsProject!
                .Where(ld => !projectResult.LeadProjects!.Any(lds => lds.ProjectId == ld.ProjectId))
                .Adapt<List<LeadProject>>();

            leadsToAdd.ForEach(pta => projectResult.LeadProjects!.Add(pta));
        }

        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await _contextConfig.Projects.FirstOrDefaultAsync(property => property.Id == id);

            if (project is null) return new ErrorResponse("Project Not Found");

            _contextConfig.Remove(project);
            await _contextConfig.SaveChangesAsync();

            return new ObjectResult(null) { StatusCode = 204 };
        }

        public async Task<IActionResult> CreateProject(ProjectRequest createProject)
        {
            var costCenter = await _contextConfig.CostCenters
                .FirstOrDefaultAsync(prop => prop.Id == createProject.CostCenter!.Id);

            if (costCenter is null)
                return new ErrorResponse("Cost Center Not Found");

            var projectEntity = createProject.Adapt<Project>();
            projectEntity.CostCenter = costCenter;

            var sonarTokenValidationResult = await SonarTokenValidation(createProject.SonarUrl!, createProject.SonarToken!, projectEntity);

            if (sonarTokenValidationResult is not null) return sonarTokenValidationResult;

            var isDuplicate = _contextConfig.Projects.Where(project => project.Name == createProject.Name).Any();

            if (isDuplicate)
            {
                var resultError = projectEntity.Adapt<ProjectResponse>();
                resultError.SetStatusCode(409);
                return resultError;
            }

            await _contextConfig.AddAsync(projectEntity);
            await _contextConfig.SaveChangesAsync();

            var result = projectEntity.Adapt<ProjectResponse>();

            result.SetStatusCode(201);

            return result;
        }

        public async Task<IActionResult> DisableProjectStacks(Guid id, ProjectStackRemovalRequest removalRequest)
        {
            var stacks = await _contextConfig.StackProjects
                .Where(sp => sp.Active
                     && sp.ProjectId == id
                     && removalRequest.StacksId!.Contains(sp.StackId!))
                .ToListAsync();

            stacks.ForEach(stack => stack.Active = false);

            _contextConfig.UpdateRange(stacks);

            await _contextConfig.SaveChangesAsync();

            return new ObjectResult(null) { StatusCode = (int)HttpStatusCode.NoContent };
        }

        private async Task<IActionResult?> SonarTokenValidation(string sonarUrl, string sonarToken, Project project)
        {
            if (!string.IsNullOrEmpty(sonarToken) || !string.IsNullOrEmpty(sonarUrl))
            {
                var sonarTokenValidationResult = await _sonarHttpService.SonarTokenValidation(sonarUrl!, sonarToken!);
                var valid = sonarTokenValidationResult!.Valid;

                if (!valid)
                {
                    var resultError = project.Adapt<ProjectResponse>();
                    resultError.SetStatusCode(400);
                    return resultError;
                };
            }
            return null;
        }
    }
}