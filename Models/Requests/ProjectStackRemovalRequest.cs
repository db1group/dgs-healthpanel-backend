using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests;

public class ProjectStackRemovalRequest
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public ICollection<string> StacksId { get; set; }
}