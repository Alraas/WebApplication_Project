using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Project.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        public string Stock { get; set; }

        public ICollection<Order_details> Order_Details { get; set; }


    }
}
