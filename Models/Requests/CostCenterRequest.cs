using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class CostCenterRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}