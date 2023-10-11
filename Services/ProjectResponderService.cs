using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Infra.Shared;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Models.Responses.Errors;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Db1HealthPanelBack.Services;

public class ProjectResponderService
{
    private readonly ContextConfig _contextConfig;

    public ProjectResponderService(ContextConfig contextConfig)
    {
        _contextConfig = contextConfig;
    }

    public async Task<IActionResult> Create(ProjectResponderRequest request)
    {
        if (!await ProjectExists(request.ProjectId)) return new ErrorResponse(ErrorMessage.ProjectNotFound);
        
        if (await ResponderExists(request)) return new ErrorResponse(ErrorMessage.ResponderAlreadyRegistered);

        var projectResponder = new ProjectResponder()
        {
            Name = request.Name,
            Email = request.Email,
            ProjectId = request.ProjectId,
            IsLead = request.IsLead
        };

        await _contextConfig.AddAsync(projectResponder);
        await _contextConfig.SaveChangesAsync();

        return projectResponder.Adapt<ProjectResponderResponse>();
    }

    public async Task<IEnumerable<ProjectResponderResponse>> GetByProject(Guid projectId)
        => (await _contextConfig.ProjectResponders
            .Where(p => p.ProjectId == projectId)
            .ToListAsync())
            .Adapt<IEnumerable<ProjectResponderResponse>>();
    
    public async Task<IEnumerable<ProjectResponderResponse>> GetByEmail(string email)
        => (await _contextConfig.ProjectResponders
                .Where(p => p.Email == email)
                .ToListAsync())
            .Adapt<IEnumerable<ProjectResponderResponse>>();

    public async Task<IActionResult> GetById(Guid id)
    {
        var projectResponder = await _contextConfig.ProjectResponders.FirstOrDefaultAsync(p => p.Id == id);
        if (projectResponder is null) return new ErrorResponse(ErrorMessage.ResponderNotFound);
        
        return projectResponder.Adapt<ProjectResponderResponse>();    
    }
    
    public async Task<IActionResult> Delete(Guid id)
    {
        var projectResponder = await _contextConfig.ProjectResponders.FirstOrDefaultAsync(p => p.Id == id);
        if (projectResponder is null) return new ErrorResponse(ErrorMessage.ResponderNotFound);

        _contextConfig.Remove(projectResponder);
        await _contextConfig.SaveChangesAsync();
        
        return projectResponder.Adapt<ProjectResponderResponse>();    
    }
    
    public async Task<IActionResult> Update(Guid id, ProjectResponderRequest request)
    {
        var projectResponder = await _contextConfig.ProjectResponders.FirstOrDefaultAsync(p => p.Id == id);
        if (projectResponder is null) return new ErrorResponse(ErrorMessage.ResponderNotFound);

        projectResponder.Email = request.Email;
        projectResponder.Name = request.Name;
        projectResponder.ProjectId = request.ProjectId;
        projectResponder.IsLead = request.IsLead;
        
        _contextConfig.Update(projectResponder);
        await _contextConfig.SaveChangesAsync();
        
        return projectResponder.Adapt<ProjectResponderResponse>();    
    }
    
    private async Task<bool> ProjectExists(Guid projectId)
        => await _contextConfig.Projects.AnyAsync(p => p.Id == projectId);
    
    private async Task<bool> ResponderExists(ProjectResponderRequest request)
        => await _contextConfig.ProjectResponders
            .AnyAsync(p => p.Email == request.Email &&
                           p.ProjectId == request.ProjectId);
}