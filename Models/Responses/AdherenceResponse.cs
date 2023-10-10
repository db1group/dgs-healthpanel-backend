namespace Db1HealthPanelBack.Models.Responses;

public class AdherenceResponse
{
    public string AdoptPercentage { get; set; }
    public string AvoidPercentage { get; set; }
    public string AssessPercentage { get; set; }
    public string ExperimentPercentage { get; set; }
    public string UnspecifiedPercentage { get; set; }

    public AdherenceResponse(
        string adoptPercentage,
        string avoidPercentage,
        string assessPercentage,
        string experimentPercentage,
        string unspecifiedPercentage)
    {
        AdoptPercentage = adoptPercentage;
        AvoidPercentage = avoidPercentage;
        AssessPercentage = assessPercentage;
        ExperimentPercentage = experimentPercentage;
        UnspecifiedPercentage = unspecifiedPercentage;
    }
}