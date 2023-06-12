using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class ColumnRequest
    {
        [Required]
        public string? Title { get; set; }
        
        [Required]
        public decimal? Weight { get; set; }

        [Required]
        public ICollection<QuestionRequest>? Questions { get; set; }

        [Required]
        public int? Order { get; set; }
    }
}