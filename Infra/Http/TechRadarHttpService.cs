namespace Db1HealthPanelBack.Infra.Http
{
    public class TechRadarHttpService
    {
        private readonly HttpClient _httpClient;

        public TechRadarHttpService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["QualityTechBase:TechRadar:Url"]!);
        }

        public async Task<string?> GetTechOpinion()
        {
            var response = await _httpClient.GetAsync("db1-opinion.json");

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync();
        }
    }
}
