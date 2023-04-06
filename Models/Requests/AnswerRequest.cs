using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class AnswerRequest
    {
        [Required]
        public string? Project { get; set; }
        [Required]
        public ICollection<AnswerQuestionRequest>? Questions { get; set; }
    }
}