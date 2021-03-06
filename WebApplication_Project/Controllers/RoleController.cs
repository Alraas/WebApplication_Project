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
    //[Authorize(Roles = "Administrator")]

    public class RoleController : Controller
    {
        private UserManager<CustomUser> _userManager;
        private RoleManager<IdentityRole> roleManager;

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
                    Dafault_Shpping_Address = user.Dafault_Shpping_Address,
                    FirstName = user.FirstName
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
                    FirstName = createUserViewModel.Firstname,
                    Email = createUserViewModel.Email,
                    UserName = createUserViewModel.Email,
                    Dafault_Shpping_Address = createUserViewModel.Dafault_Shpping_Address
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

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }



        //[HttpPost]
        //public async Task<IActionResult> Create(IdentityRole role)
        //{
        //    //await _userManager.CreateAsync(role);
        //    //return RedirectToAction("Index");
        //}
    }
}
