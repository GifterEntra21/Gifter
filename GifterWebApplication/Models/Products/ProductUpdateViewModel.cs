using System.ComponentModel.DataAnnotations;

namespace GifterWebApplication.Models.Products
{
    public class ProductUpdateViewModel
    {

        [Required]
        public string id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public string ShopURL { get; set; }
        public string Image { get; set; }
        public string AssociatedPartner { get; set; }

    }
}
