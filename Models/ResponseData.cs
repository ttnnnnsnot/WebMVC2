﻿using System.Text.Json;

namespace WebMVC2.Models
{
    public class ResponseData
    {
        public string ProcedureName { get; set; } = string.Empty;
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
        public string TableTypeName { get; set; } = string.Empty;
        public List<Dictionary<string, string>> TableData { get; set; } = new List<Dictionary<string, string>>();
    }
}
