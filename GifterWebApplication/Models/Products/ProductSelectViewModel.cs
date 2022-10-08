using System.ComponentModel.DataAnnotations;

namespace GifterWebApplication.Models.Products
{
    public class ProductSelectViewModel
    {
        public string id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public double Price { get; set; }

        public int Clicks { get; set; }

        public string ShopURL { get; set; }

        public string Image { get; set; }
    }
}
