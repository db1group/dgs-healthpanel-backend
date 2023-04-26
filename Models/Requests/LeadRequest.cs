using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class LeadRequest
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public Boolean? InTraining { get; set; }
    }
}