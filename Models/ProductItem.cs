﻿namespace WebMVC2.Models
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Num {  get; set; }
    }
}
