using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class Product
    {
        public Product()
        {

        }

        public Product(string name)
        {
            Name = name;
        }

        public Product(string id, string name, string genre, double price, string shopURL, string image, string associatedPartner)
        {
            ID = id;
            Name = name;
            Genre = genre;
            Price = price;
            ShopURL = shopURL;
            Image = image;
            AssociatedPartner = associatedPartner;
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public string ShopURL { get; set; }
        public string Image { get; set; }
        public string AssociatedPartner { get; set; }

    }
}
