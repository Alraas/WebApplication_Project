using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Project.Models
{
    public class Categories
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
