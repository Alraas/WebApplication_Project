using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Project.Models
{
    public class Order_details
    {
        public int ID { get; set; }
        public int Order_ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public Decimal? Price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }



    }
}
