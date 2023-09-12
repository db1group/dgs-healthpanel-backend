using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class LeadRequest
    {
        public ICollection<LeadProjectRequest>? LeadProjects { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }

        [Required]
        public Boolean? InTraining { get; set; }
    }
}