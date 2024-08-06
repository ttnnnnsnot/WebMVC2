using System.ComponentModel.DataAnnotations.Schema;

namespace WebMVC2.Models
{
    public class ProductItemBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Num { get; set; }
    } 
    public class ProductItem
    {

        [Column(Order = 1)]
        public int Id { get; set; }

        [Column(Order = 2)]
        public string Name { get; set; } = string.Empty;

        [Column(Order = 3)]
        public int Price { get; set; }

        [Column(Order = 4)]
        public string Description { get; set; } = string.Empty;

        [Column(Order = 5)]
        public int Num { get; set; }
    }
    public class ProductItemForOrder : ProductItemBase
    {
        [Column(Order = 6)]
        public string OrderId { get; set; } = string.Empty;
    }


}
