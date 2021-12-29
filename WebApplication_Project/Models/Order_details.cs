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

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }



    }
}
