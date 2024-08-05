namespace WebMVC2.Models
{
    public class OrderInfo
    {
        public string OrderId { get; set; } = string.Empty;
        public int PayPrice { get; set; }
        public string StatusType { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
    }
}
