using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class Product : ICosmosDbItem
    {

        public Product(string id, string name, string genre, double price, string shopURL, string image, string associatedPartner)
        {
            id = id;
            Name = name;
            Genre = genre;
            PartitionKey = genre;
            Price = price;
            ShopURL = shopURL;
            Image = image;
            AssociatedPartner = associatedPartner;
        }

        public string id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public string ShopURL { get; set; }
        public string Image { get; set; }
        public string AssociatedPartner { get; set; }
        public string PartitionKey { get; set; }
    }
}
