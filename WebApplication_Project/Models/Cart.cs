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

        public Nullable<Guid> CustomerId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Total { get; set; }

        [ForeignKey("Customer_ID")]
        public Customer Customer { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public Nullable<int> ProductId { get; set; }

    }
}
