using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string ImageName { get; set; }


        [NotMapped]
        [DisplayName("Uplaod Image")]
        public IFormFile ImageFile { get; set; }


        public int Stock { get; set; }
        //public int CategorieID { get; set; }

        public virtual ICollection<Order_details> Order_Details { get; set; }
        public Categories Categorie { get; set; }
    }
}
