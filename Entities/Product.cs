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

        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ShopURL { get; set; }
        public string Image { get; set; }
        public Partner AssociatedPartner { get; set; }
    }
}
