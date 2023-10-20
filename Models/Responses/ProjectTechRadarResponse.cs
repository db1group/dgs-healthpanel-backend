namespace Db1HealthPanelBack.Models.Responses;

public class ProjectTechRadarResponse
{
    public Guid ProjectId { get; set; }
    public List<TechRadarResponse>? TechRadarResponses { get; set; }
    public AdherenceResponse? AdherenceResponse { get; set; }
}