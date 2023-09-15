using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Newtonsoft.Json;

namespace Db1HealthPanelBack.Services
{
    public class TechRadarService
    {

        public List<ProjectTechRadarResponse> GetTechComparisons(List<ProjectStacksResponse> stackData, string techRadar)
        {
            var originalTechRadarData = JsonConvert.DeserializeObject<TechRadarRequest>(techRadar);
            List<ProjectTechRadarResponse> projectTechRadarResponses = new List<ProjectTechRadarResponse>();
            foreach (var project in stackData)
            {
                var projectTechRadarResponse = new ProjectTechRadarResponse
                {
                    ProjectId = project.ProjectId,
                    TechRadarResponses = CompareTechs(originalTechRadarData, project)
                };
                projectTechRadarResponses.Add(projectTechRadarResponse);
            }
            
            return projectTechRadarResponses;
        }
        
        private List<TechRadarResponse> CompareTechs(TechRadarRequest techRadarData, ProjectStacksResponse projectStackData)
        {
            var radarStack = techRadarData.Items!;
            var projectStack = projectStackData.Stacks;
            var upperCaseProjectStackNames = projectStack.Select(element => element.StackName.ToUpper()).ToList();
            List<TechRadarResponse> comparisons = new List<TechRadarResponse>();
            foreach (var tech in radarStack)
            {
                var comparison = new TechRadarResponse
                {
                    Ring = tech.Ring,
                    Title = tech.Title,
                    IsPresent = upperCaseProjectStackNames.Contains(tech.Name!.ToUpper())
                };
                
                comparisons.Add(comparison);
            }

            return comparisons;


        }
    }
}
