using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Project.Models
{
    public class Order
    {
        public int ID { get; set; }

        public int Customer_ID { get; set; }
        public int Amount { get; set; }
        public string Shipping_Address { get; set; }
        public string Order_Address { get; set; }

        public string Order_Email { get; set; }

        public DateTime Order_Date { get; set; }

        public string Order_Status { get; set; }
        public ICollection<Order_details> Order_Details { get; set; }
        public Customer Customer { get; set; }


    }
}
