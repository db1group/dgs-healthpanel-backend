namespace Db1HealthPanelBack.Entities
{
    public sealed class Question
    {
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
        public string? Description { get; set; }
        public bool Value { get; set; }
    }
}