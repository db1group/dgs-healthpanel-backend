using System.Text.Json.Serialization;

namespace Db1HealthPanelBack.Infra.Http.HttpResponses;

public class SonarProject
{
    public Paging? Paging { get; set; }

    [JsonPropertyName("components")]
    public List<Project>? Projects { get; set; }   
}

public class Project
{
    public string? Key { get; set; }
    public string? Name { get; set; }        
}

public class Paging
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
}