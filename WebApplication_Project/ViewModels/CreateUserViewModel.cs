using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Project.ViewModels
{
    public class CreateUserViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Dafault_Shpping_Address { get; set; }


    }
}
