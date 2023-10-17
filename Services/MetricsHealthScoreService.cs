using Db1HealthPanelBack.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Db1HealthPanelBack.Services
{
    public class MetricsHealthScoreService
    {
        private readonly IConfiguration _configuration;

        public MetricsHealthScoreService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<decimal> GetMetricsHealthScore(Project project)
        {
            if (project.SonarName.IsNullOrEmpty())
                return 0;

            var urlRequest = await DefineProjectUrlRequest(project);

            var client = new HttpClient();
            HealthScore healthScore;
            HttpResponseMessage response;

            try
            {
                response = await client.GetAsync(urlRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

            if (!response.IsSuccessStatusCode)
                return 0;

            healthScore = await response.Content.ReadAsAsync<HealthScore>();
            return healthScore is not null ? healthScore.Value.Value : 0;
        }

        private Task<string> DefineProjectUrlRequest(Project project)
        {
            var url = _configuration["SonarMetricsHealthScore:url"];
            const string endpoint = "projectKeyName";
            return Task.FromResult($"{url}/?{endpoint}={project.SonarName}&sonarUserToken={project.SonarToken}&sonarUrl={project.SonarUrl}");
        }

        internal class HealthScore
        {
            public decimal? Value { get; set; }
        }
    }
}