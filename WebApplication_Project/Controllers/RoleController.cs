using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Project.Areas.Identity.Data;
using WebApplication_Project.ViewModels;

namespace WebApplication_Project.Controllers
{
    public class RoleController : Controller
    {
        private UserManager<CustomUser> _userManager;

        public RoleController(UserManager<CustomUser> userManager)
        {
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            RolesListViewModel ViewModel = new RolesListViewModel()
            {
                users = _userManager.Users.ToList()
            };
            //var roles = _userManager.Roles.ToList();
            return View(ViewModel);
        }

        public IActionResult Details(string id)
        {
            CustomUser user= _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user != null)
            {
                UserDetailsViewModel viewModel = new UserDetailsViewModel()
                {
                    ID = user.Id,
                    Name = user.Name,
                    Adress = user.Adress
                };
                return View(viewModel);
            }
            else
            {
                RolesListViewModel vm = new RolesListViewModel()
                {
                    users = _userManager.Users.ToList()

                };
                return View("Index", vm);
            }
        }

        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                CustomUser user = new CustomUser
                {
                    Name = createUserViewModel.Name,
                    Email = createUserViewModel.Email,
                    UserName = createUserViewModel.Email,
                    Adress = createUserViewModel.Adress
                };
                IdentityResult result = await _userManager.CreateAsync(user, createUserViewModel.Password);
                if (result.Succeeded)
                
                    return RedirectToAction("Index");
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                

            }

            return View(createUserViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(String id)
        {
            CustomUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)

                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }


            }
            else
                ModelState.AddModelError("", "User not found");
            return View("Index", _userManager.Users.ToList());



        }




        //[HttpPost]
        //public async Task<IActionResult> Create(IdentityRole role)
        //{
        //    //await _userManager.CreateAsync(role);
        //    //return RedirectToAction("Index");
        //}
    }
}
