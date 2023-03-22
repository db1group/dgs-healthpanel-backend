namespace Db1HealthPanelBack.Models.Requests
{
    public class ColumnRequest
    {
        public string? Title { get; set; }
        public int Weight { get; set; }
        public ICollection<QuestionRequest>? Questions { get; set; }
    }
}