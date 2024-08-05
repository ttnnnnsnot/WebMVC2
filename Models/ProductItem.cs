namespace WebMVC2.Models
{
    public class ProductItemBase
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
    } 
    public class ProductItem : ProductItemBase
    {
        public string Description { get; set; } = string.Empty;
    }
    public class ProductItemForOrder : ProductItemBase
    {
        public string OrderId { get; set; } = string.Empty;
    }


}
