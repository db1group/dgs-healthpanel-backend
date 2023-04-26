using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class AnswerPillarRequest
    {
        [Required]
        public Guid? PillarId { get; set; }
        
        [Required]
        public string? AdditionalData { get; set; }
    }
}