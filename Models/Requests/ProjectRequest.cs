using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class ProjectRequest
    {
        [Required]
        public string? Name { get; set; }
        public ICollection<LeadProjectRequest>? LeadProjects { get; set; }

        [Required]
        public CostCenterRequest? CostCenter { get; set; }
    }
}