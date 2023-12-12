using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Db1HealthPanelBack.Services
{
    public class MetricsHealthScoreService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MetricsHealthScoreService> _logger;
        private readonly ContextConfig _contextConfig;

        public MetricsHealthScoreService(IConfiguration configuration, ILogger<MetricsHealthScoreService> logger, ContextConfig contextConfig)
        {
            _configuration = configuration;
            _logger = logger;
            _contextConfig = contextConfig;
        }

        public async Task<decimal> GetMetricsHealthScore(Project project)
        {
            if (project.SonarName.IsNullOrEmpty() && project.SonarProjectKeys.IsNullOrEmpty())
                return 0;

            var evaluation = await _contextConfig.Evaluations.FirstOrDefaultAsync(prop => prop.ProjectId == project!.Id
                                                                                          && prop.Date.Month == DateTime.Now.Month
                                                                                          && prop.Date.Year == DateTime.Now.Year);

            if (evaluation is not null && evaluation.MetricsHealthScore != 0) return evaluation.MetricsHealthScore;

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
            var healthScore = await JsonSerializer.DeserializeAsync<HealthScore>(jsonResponse, _jsonOptions);

            return healthScore!.Value is not null ? healthScore.Value!.Value : 0;
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

        private readonly JsonSerializerOptions _jsonOptions =
            new() { PropertyNameCaseInsensitive = true };
    }
}