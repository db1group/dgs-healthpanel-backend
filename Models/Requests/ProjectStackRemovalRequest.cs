using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests;

public class ProjectStackRemovalRequest
{
    [Required]
    [MinLength(1)]
    public ICollection<string>? StacksId { get; set; }
}