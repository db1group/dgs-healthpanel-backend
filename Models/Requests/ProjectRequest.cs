using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class ProjectRequest
    {
        [Required]
        public string? Name { get; set; }

        public string? SonarName { get; set; }
        
        public string? SonarUrl { get; set; }
        
        public string? SonarToken { get; set; }
        
        public ICollection<LeadProjectRequest>? LeadProjects { get; set; }

        [Required]
        public CostCenterRequest? CostCenter { get; set; }
    }
}