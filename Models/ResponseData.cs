namespace WebMVC2.Models
{
    public class ResponseData
    {
        public string ProcedureName { get; set; } = string.Empty;
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
    }
}
