using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebMVC2.Services
{
    public static class GlobalJsonSerializerOptions
    {
        public static JsonSerializerOptions Default { get; } = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }
}
