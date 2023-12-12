using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests;

public class GenerateKeyRequest
{
    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    public Guid LeadId { get; set; }
}

