using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class AnswerQuestionRequest
    {
        [Required]
        public Guid? QuestionId { get; set; }
        
        [Required]
        public string? Value { get; set; }
    }
}