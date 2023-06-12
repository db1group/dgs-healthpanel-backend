namespace Db1HealthPanelBack.Entities
{
    public sealed class AnswerQuestion
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
        public string? Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}