using Db1HealthPanelBack.Infra.Shared.Extensions;

namespace Db1HealthPanelBack.Infra.Http.HttpResponses;

public class SonarStack
{
    public Component Component { get; set; }
}

public class Component
{
    public List<Measure>? Measures { get; set; }
}

public class Measure
{
    public string? Metric { get; set; }
    public string? Value { get; set; }

    public List<string> Language
    {
        get
        {
            if (Value is null)
                return new List<string>();

            var measureLanguages = Value.Deserialize<List<MeasureLanguage>>();

            return measureLanguages is not null && measureLanguages.Any()
                ? measureLanguages.Select(s => s.Language).ToList()
                : new List<string>();
        }
    }
}

public class MeasureLanguage
{
    public string Language { get; set; }
}