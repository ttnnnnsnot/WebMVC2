using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebMVC2.Models
{
    public class ResultData
    {
        public ResultMessage resultMessage { get; set; } = new ResultMessage();

        [JsonPropertyName("data")]
        public List<JsonElement> Data { get; set; } = new List<JsonElement>();
    }

    public class ResultMessage
    {
        public bool Msg { get; set; } = false;
        public string MsgText { get; set; } = string.Empty;
    }
}
