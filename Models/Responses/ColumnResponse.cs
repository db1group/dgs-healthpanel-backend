namespace Db1HealthPanelBack.Models.Responses
{
    public class ColumnResponse
    {
        public string? Title { get; set; }
        public int Weight { get; set; }
        public ICollection<QuestionResponse>? Questions { get; set; }        
    }
}