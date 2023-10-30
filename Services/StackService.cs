using System.Net;
using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Infra.Http;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Models.Responses.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Db1HealthPanelBack.Services;

public class StackService
{
    private readonly ContextConfig _contextConfig;
    private readonly SonarHttpService _sonarHttpService;

    public StackService(ContextConfig contextConfig, SonarHttpService sonarHttpService)
    {
        _contextConfig = contextConfig;
        _sonarHttpService = sonarHttpService;
    }

    public async Task PopulateAllProjects([FromQuery] Guid? projectId)
    {
        var projectQuery = _contextConfig.Projects.AsQueryable();

        if ((projectId != null) && projectId != Guid.Empty)
            projectQuery = projectQuery.Where(p => p.Id == projectId);

        var projectsToPopulate = await projectQuery.Include(p => p.StackProjects).ToListAsync();                
        
        foreach (var project in projectsToPopulate)
        {
            await PopulateStacks(project);
        }
    }

    public async Task<List<StackResponse>> GetAll()
        => await _contextConfig.Stacks
            .AsQueryable()
            .Select(s => new StackResponse(s.Id!, s.Name!, true))
            .ToListAsync();

    public async Task<List<ProjectStacksResponse>> GetStacks(List<Guid>? projectIds, bool listOnlyActive)
    {
        var listOfProjects = new List<ProjectStacksResponse>();

        var projects = await GetProjects(projectIds);
        
        foreach (var project in projects)
        {
            var projectStacks = project.StackProjects?
                .Where(sp => !listOnlyActive || sp.Active)
                .Select(s => new StackResponse(s.StackId!, s.Stack?.Name ?? string.Empty, s.Active))
                .ToList() ?? new List<StackResponse>();

            listOfProjects.Add(new ProjectStacksResponse(project.Id, project.Name ?? string.Empty, projectStacks));
        }

        return listOfProjects;
    }

    public async Task<List<StackProjectsResponse>> GetStacks(List<string>? languageIds, bool listOnlyActive)
    {
        var listOfStacks = new List<StackProjectsResponse>();
        var stacks = await GetStacksWithProject(languageIds);
        
        foreach (var stack in stacks)
        {
            var projects = stack.StackProjects!
                .Where(sp => !listOnlyActive || sp.Active)
                .Select(s => new ProjectStackResponse(s.Project!.Id, s.Project!.Name ?? string.Empty, s.Active))
                .ToList();
            listOfStacks.Add(new StackProjectsResponse(stack.Id!, stack.Name!, projects));
        }

        return listOfStacks;
    }

    private async Task<List<Stack>> GetStacksWithProject(List<string>? languageIds)
    {
        return await _contextConfig.Stacks
            .Include(s => s.StackProjects)!
            .ThenInclude(sp => sp.Project)
            .Where(s => (languageIds == null || !languageIds.Any()) || languageIds.Contains(s.Id!))
            .ToListAsync();
    }

    private async Task<List<Project>> GetProjects(List<Guid>? projectIds)
    {
        var query =  _contextConfig.Projects
            .Include(p => p.StackProjects)!
            .ThenInclude(s => s.Stack)
            .AsQueryable();

        if (projectIds is not null && projectIds.Any())
            query = query.Where(p => projectIds.Contains(p.Id));

        return await query.ToListAsync();
    }

    private async Task PopulateStacks(Project project)
    {
        var projectKeys = await _sonarHttpService.GetSonarProjectNames(project.SonarName ?? string.Empty);
        if (!projectKeys.Any()) return;

        var listOfStacks = new List<string>();
        foreach (var projectKey in projectKeys)
        {
            var stacks = await _sonarHttpService.GetProjectStacks(projectKey!);
            listOfStacks.AddRange(stacks!);
        }

        if (!listOfStacks.Any()) return;

        foreach (var stack in listOfStacks.Distinct().ToList())
        {
            if (project.StackProjects!.Any(ps => ps.StackId == stack)) continue;

            var stackEntity = new StackProject
            {
                ProjectId = project.Id,
                StackId = stack,
                Active = true
            };
            await _contextConfig.AddAsync(stackEntity);
        }

        await _contextConfig.SaveChangesAsync();
    }
    
    public async Task<IActionResult> AddStacks(AddStackRequest request)
    {
        if (request.StackId.IsNullOrEmpty()) return new ErrorResponse("The StacksId field is required");

        var stacks = await _contextConfig.StackProjects
            .Where(sp => sp.ProjectId == request.ProjectId)
            .FirstOrDefaultAsync(proj => proj.StackId == request.StackId);
        
        if (stacks is not null && stacks.Active) return new ErrorResponse("Stack already exists");

        await AddStack(request, stacks!);
        
        return new ObjectResult(null) { StatusCode = (int) HttpStatusCode.NoContent };
    }

    private async Task AddStack(AddStackRequest request, StackProject stack)
    {
        if (stack is null)
        {
            var newStack = new StackProject
            {
                ProjectId = request.ProjectId,
                StackId = request.StackId,
                Active = true
            };

            await _contextConfig.AddAsync(newStack);
        } else
        {
            stack.Active = true;
            _contextConfig.Update(stack);
        }

        await _contextConfig.SaveChangesAsync();
    }
}