using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Project.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display (Name ="Name")]

        public string Name { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public int Stock { get; set; }
        [ForeignKey ("CategorieID")]

        public ICollection<Order_details> Order_Details { get; set; }
        public Categories Categorie { get; set; }


    }
}
