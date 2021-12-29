using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Project.Models
{
    public class Cart
    {
        public int ID { get; set; }


        public int Quantity { get; set; }
        public Decimal Total { get; set; }

        [ForeignKey("Customer_ID")]
        public Customer Customer { get; set; }
        public int Customer_ID { get; set; }
    }
}
