namespace Db1HealthPanelBack.Models.Requests
{
    public class SonarMetricRequest
    {
        public string? Key { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Domain { get; set; }
        public string? Type { get; set; }
    }
}