using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests;

public class ProjectResponderRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public bool IsLead { get; set; }
    
    [Required]
    public Guid ProjectId { get; set; }
}