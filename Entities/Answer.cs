namespace Db1HealthPanelBack.Entities
{
    public sealed class Answer
    {
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public bool Value { get; set; }
    }
}