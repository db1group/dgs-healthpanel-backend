using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class AnswerRequest
    {
        [Required]
        public Guid Project { get; set; }

        [Required]
        public bool? IsRetroactive { get; set; }

        [Required]
        public ICollection<AnswerQuestionRequest>? Questions { get; set; }

        [Required]
        public ICollection<AnswerPillarRequest>? Pillars { get; set; }
    }
}