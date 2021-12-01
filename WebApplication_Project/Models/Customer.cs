using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Project.Models
{
    public class Customer
    {
        public int ID { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }

        public string Biling_Address { get; set; }

        public string Dafault_Shpping_Address { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; }




    }
}
