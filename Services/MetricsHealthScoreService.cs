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
            if (project.SonarName.IsNullOrEmpty() && project.SonarProjectKeys.IsNullOrEmpty())
                return 0;

            var urlRequest = await DefineProjectUrlRequest(project);

            var client = new HttpClient();
            HttpResponseMessage response;

            try
            {
                response = await client.GetAsync(urlRequest);
                Console.WriteLine("URL: " + urlRequest);
                Console.WriteLine("RESPONSE: " + response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

            if (!response.IsSuccessStatusCode)
                return 0;

            var healthScore = await response.Content.ReadAsAsync<HealthScore>();
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