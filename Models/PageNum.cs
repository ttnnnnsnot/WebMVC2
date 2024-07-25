using System.Reflection.Metadata.Ecma335;

namespace WebMVC2.Models
{
    public class PageNum
    {
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }
        public int ItemSize { get; set; }
        public int PageNumSize { get; set; } = 8;
        public int TotalPages => (int)Math.Ceiling((decimal)TotalRecords / ItemSize);
        public string UrlControllerName { get; set; } = string.Empty;
        public string UrlActionName { get; set; } = string.Empty;
        public string UrlParam { get; set; } = string.Empty;

    }
}
