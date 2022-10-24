using System.ComponentModel.DataAnnotations;

namespace GifterWebApplication.Models.Products
{
    public class ProductInsertViewModel
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string ShopURL { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string AssociatedPartner { get; set; }
    }
}
