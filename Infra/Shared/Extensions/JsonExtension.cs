using System.Text.Json;

namespace Db1HealthPanelBack.Infra.Shared.Extensions;

public static class JsonExtension
{
    public static async Task<T?> Deserialize<T>(this Stream stream) where T : class
        => await JsonSerializer.DeserializeAsync<T>(stream, GetOptions());

    public static T? Deserialize<T>(this string json) where T : class
        => JsonSerializer.Deserialize<T>(json, GetOptions());
    
    private static JsonSerializerOptions GetOptions() 
        => new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
}