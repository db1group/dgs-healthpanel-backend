namespace Db1HealthPanelBack.Models.Requests
{
    public class ProjectRequest
    {
        public string? Name { get; set; }
        public ICollection<LeadProjectRequest>? LeadProjects { get; set; }
        public string? CostCenter { get; set; }
    }
}