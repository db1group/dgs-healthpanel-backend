﻿using System.Text;
using Db1HealthPanelBack.Infra.Http.HttpResponses;
using Db1HealthPanelBack.Models.Responses;
using Microsoft.IdentityModel.Tokens;

namespace Db1HealthPanelBack.Infra.Http;

public class SonarHttpService
{
    private readonly HttpService _httpService;
    private readonly IConfiguration _configuration;

    public SonarHttpService(HttpService httpService, IConfiguration configuration)
    {
        _httpService = httpService;
        _configuration = configuration;
    }

    public async Task<List<string?>> GetSonarProjectNames(string projectName)
    {
        var request = BuildRequest(route: "api/projects/search", queryString: $"q={projectName}");

        var project = await _httpService.Get<SonarProject>(request);

        return project is null
            ? new List<string?>()
            : project.Projects?.Select(p => p.Key).ToList() ?? new List<string?>();
    }

    public async Task<List<string?>> GetProjectStacks(string projectKey)
    {
        var request = BuildRequest(
            route: "api/measures/component",
            queryString: $"component={projectKey}&metricKeys=quality_profiles"
        );

        var stack = await _httpService.Get<SonarStack>(request);

        var listOfStacks = new List<string?>();
        foreach (var measure in stack?.Component?.Measures ?? new List<Measure>())
        {
            listOfStacks.AddRange(measure.Language);
        }

        return listOfStacks.Distinct().ToList();
    }

    public async Task<SonarTokenValidationResponse?> SonarTokenValidation(string uri, string sonarToken)
    {
        var url = _configuration["Sonar:url"];
        var sonarTokenConverted = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{sonarToken}:"));
        RequestData request = new()
        {
            Uri = $"{(uri.IsNullOrEmpty() ? $"{url}" : $"{uri}")}/api/authentication/validate",
            Headers = new() { { $"Authorization", $"Basic {(sonarToken.IsNullOrEmpty() ? "" : $"{sonarTokenConverted}")}" } },
        };

        return await _httpService.Get<SonarTokenValidationResponse>(request);
    }

    private RequestData BuildRequest(string route, string queryString)
        => new()
        {
            Uri = $"{GetSonarUrl()}/{route}?{queryString}",
            Headers = GetHeaders()
        };

    private Dictionary<string, string> GetHeaders()
        => new()
        {
            { "Authorization", $"Bearer {GetSonarToken()}" }
        };

    private string? GetSonarToken()
        => _configuration.GetSection("Sonar").GetValue<string>("Token");

    private string GetSonarUrl()
        => _configuration.GetSection("Sonar").GetValue<string>("url") ?? string.Empty;
}