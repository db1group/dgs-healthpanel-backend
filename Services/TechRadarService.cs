using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;

namespace Db1HealthPanelBack.Services
{
    public class TechRadarService
    {
        public static List<TechRadarResponse> Split(TechRadarRequest techRadar)
        {
            var groupedTechsByQuadrant = techRadar.Items.GroupBy(tech => tech.Quadrant);

            List<TechRadarResponse> splitTechs = new List<TechRadarResponse>();
            foreach (var group in groupedTechsByQuadrant)
            {
                var splitTech = new TechRadarResponse
                {
                    Quadrant = group.Key,
                    Techs = group.ToList()
                };
                splitTechs.Add(splitTech);
            }

            return splitTechs;
        }
    }
}
