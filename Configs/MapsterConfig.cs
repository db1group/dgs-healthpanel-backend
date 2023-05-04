using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Mapster;

namespace Db1HealthPanelBack.Configs
{
    public static class MapsterConfig
    {
        public static void AddMapster(this IServiceCollection services)
        {
            TypeAdapterConfig<QuestionRequest, Question>
                .NewConfig()
                .Map(target => target.Value, intent => intent.Value == "DONE");

            TypeAdapterConfig<Question, QuestionResponse>
                .NewConfig()
                .Map(target => target.Value, intent => intent.Value ? "DONE" : "PENDING");

            TypeAdapterConfig<ProjectResponse, Project>
                .NewConfig()
                .TwoWays()
                .PreserveReference(true);

            TypeAdapterConfig<LeadProjectResponse, LeadProject>
                .NewConfig()
                .TwoWays()
                .PreserveReference(true);
        }
    }
}