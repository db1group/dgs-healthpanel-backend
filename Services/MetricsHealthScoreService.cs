using Db1HealthPanelBack.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Db1HealthPanelBack.Services
{
    public class MetricsHealthScoreService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MetricsHealthScoreService> _logger;

        public MetricsHealthScoreService(IConfiguration configuration, ILogger<MetricsHealthScoreService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<decimal> GetMetricsHealthScore(Project project)
        {
            if (project.SonarName.IsNullOrEmpty() && project.SonarProjectKeys.IsNullOrEmpty())
                return 0;

            var urlRequest = await DefineProjectUrlRequest(project);

            var client = new HttpClient();
            HttpResponseMessage response;

            try
            {
                response = await client.GetAsync(urlRequest);

                _logger.LogInformation("URL: {0}", urlRequest);
                _logger.LogInformation("RESPONSE: {0}" + response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

            if (!response.IsSuccessStatusCode)
                return 0;

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var healthScore = await JsonSerializer.DeserializeAsync<HealthScore>(jsonResponse);

            return healthScore is not null ? healthScore.Value!.Value : 0;
        }

        private Task<string> DefineProjectUrlRequest(Project project)
        {
            var url = _configuration["SonarMetricsHealthScore:url"];
            return Task.FromResult($"{url}/?projectKeyName={project.SonarName}&projectKeys={project.SonarProjectKeys}&sonarUserToken={project.SonarToken}&sonarUrl={project.SonarUrl}");
        }

        internal class HealthScore
        {
            public decimal? Value { get; set; }
        }
    }
}