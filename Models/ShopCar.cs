namespace WebMVC2.Models
{
    public class ShopCar
    {
        public List<ProductItem> productItem { get; set; } = new List<ProductItem>();
        public int Sum { get { return productItem.Count; }}
    }
}
