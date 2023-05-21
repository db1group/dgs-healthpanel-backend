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

            TypeAdapterConfig<Project, ProjectResponse>
                .NewConfig()
                .PreserveReference(true);

            TypeAdapterConfig<LeadProjectResponse, LeadProject>
                .NewConfig()
                .TwoWays()
                .PreserveReference(true);

            TypeAdapterConfig<Evaluation, EvaluationResponse>
                .NewConfig()
                .Map(target => target.ProjectName, intent => intent.Project != null ? intent.Project.Name : "")
                .Map(target => target.CostCenterName, intent => intent.Project!.CostCenter != null ? intent.Project.CostCenter.Name : "")
                .Map(target => target.CostCenterId, intent => intent.Project!.CostCenter != null ? intent.Project.CostCenter.Id : Guid.Empty);
        }
    }
}