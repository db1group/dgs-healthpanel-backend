using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class PillarRequest
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? BackgroundColor { get; set; }

        [Required]
        public ICollection<ColumnRequest>? Columns { get; set; }

        [Required]
        public int? Order { get; set; }

        [Required]
        public decimal? Weight { get; set; }
    }
}