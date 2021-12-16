using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Project.Areas.Identity.Data;

namespace WebApplication_Project.ViewModels
{
    public class RolesListViewModel
    {
        public List<CustomUser> users { get; set; }
    }
}
