using System.ComponentModel.DataAnnotations;

namespace GifterWebApplication.Models.Products
{
    public class ProductDeleteViewModel
    {
        [Required]
        public string id { get; set; }
    }
}
