using Db1HealthPanelBack.Infra.Http;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Newtonsoft.Json;

namespace Db1HealthPanelBack.Services
{
    public class TechRadarService
    {
        private readonly TechRadarHttpService _techRadarHttpService;
        private readonly StackService _stackService;

        public TechRadarService(StackService stackService, TechRadarHttpService techRadarHttpService)
        {
            _stackService = stackService;
            _techRadarHttpService = techRadarHttpService;
        }

        public async Task<List<ProjectTechRadarResponse>?> FetchTechRadarComparison(List<Guid>? projectIds)
        {
            var techRadarContent = await _techRadarHttpService.GetTechOpinion();

            if (techRadarContent is null) return null;

            var stackData = await _stackService.GetStacks(projectIds: projectIds, listOnlyActive: true);
            var techComparison = GetTechComparisons(stackData, techRadarContent);

            return techComparison;
        }

        public List<ProjectTechRadarResponse> GetTechComparisons(List<ProjectStacksResponse> stackData, string techRadar)
        {
            var originalTechRadarData = JsonConvert.DeserializeObject<TechRadarRequest>(techRadar);
            List<ProjectTechRadarResponse> projectTechRadarResponses = [];

            foreach (var project in stackData)
            {
                var response = CompareTechs(originalTechRadarData!, project);
                var projectTechRadarResponse = new ProjectTechRadarResponse
                {
                    ProjectId = project.ProjectId,
                    TechRadarResponses = response,
                    AdherenceResponse = CalculateAdherence(response)
                };
                projectTechRadarResponses.Add(projectTechRadarResponse);
            }

            return projectTechRadarResponses;
        }

        private List<TechRadarResponse> CompareTechs(TechRadarRequest techRadarData, ProjectStacksResponse projectStackData)
        {
            var radarStack = techRadarData.Items!;

            var projectStack = projectStackData.Stacks;

            return projectStack
                .Select(e => new TechRadarResponse()
                {
                    Ring = radarStack.FirstOrDefault(r => CompareName(e, r))?.Ring ?? null,
                    Title = e.StackName
                })
                .ToList();
        }

        private bool CompareName(StackResponse e, ProjectTechRadarRequest r)
        {
            var equalsByName = string.Equals(r.Name!, e.StackId, StringComparison.CurrentCultureIgnoreCase);
            var equalsByTitle = string.Equals(r.Title!, e.StackName, StringComparison.CurrentCultureIgnoreCase);

            return equalsByName || equalsByTitle;
        }

        private AdherenceResponse CalculateAdherence(List<TechRadarResponse> techRadarResponse)
        {
            var adopt = CalculateByRing(techRadarResponse, Ring.Adopt);
            var avoid = CalculateByRing(techRadarResponse, Ring.Avoid);
            var assess = CalculateByRing(techRadarResponse, Ring.Assess);
            var experiment = CalculateByRing(techRadarResponse, Ring.Experiment);
            var unspecified = CalculateByRing(techRadarResponse, null);

            return new AdherenceResponse(
                adopt,
                avoid,
                assess,
                experiment,
                unspecified);
        }

        private string CalculateByRing(List<TechRadarResponse> techRadarResponse, Ring? ring)
        {
            var totalStacks = Convert.ToDecimal(techRadarResponse.Count);

            var ringCount = techRadarResponse
                .Count(e => ring.HasValue ?
                    e.Ring?.Equals(ring.ToString(), StringComparison.OrdinalIgnoreCase) ?? false :
                    e.Ring is null);

            return (ringCount / totalStacks).ToString("#0.##%");
        }
    }
}
