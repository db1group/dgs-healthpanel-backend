using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests;

public class GenerateKeyRequest
{
    [Required]
    public Guid ProjectId { get; set; }
}

