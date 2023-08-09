namespace Db1HealthPanelBack.Infra.Http;

public class RequestData
{
    public string? Uri { get; set; }
    public Dictionary<string, string>? Headers { get; set; }
}