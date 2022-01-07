using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Project.Areas.Identity.Data;
using WebApplication_Project.Data;

namespace WebApplication_Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            CreateRoles(serviceProvider).Wait();
        }

        //private async Task CreateRoles(IServiceProvider serviceProvider)
        //{
        //    RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        //    IdentityResult roleResult;

        //    bool roleCheck = await roleManager.RoleExistsAsync("user");

        //    if (!roleCheck)
        //    {
        //        roleResult = await roleManager.CreateAsync(new IdentityRole("user"));
        //    }

        //     roleCheck = await roleManager.RoleExistsAsync("Admin");

        //    if (!roleCheck)
        //    {
        //        roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
        //    }

        //    context.SaveChanges();

        ////Assign Admin to Main User
        //CustomUser user = context.Users.FirstOrDefault(u => u.Email == "Admin@Admin.com");
        //if (user != null)
        //{
        //    DbSet<IdentityUserRole<string>> roles = context.UserRoles;
        //    IdentityRole adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
        //    if (adminRole != null)
        //    {
        //        if (!roles.Any(ur => ur.UserId == user.Id && ur.RoleId == adminRole.Id))
        //        {
        //            roles.Add(new IdentityUserRole<string>() { UserId = user.Id, RoleId = adminRole.Id });
        //            context.SaveChanges();
        //        }
        //    }
        //}
        //}


        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<CustomUser>>();
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    //Create the roles and seed them into the database
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Assign Admin to Main User
            CustomUser user = context.Users.FirstOrDefault(u => u.Email == "Admin@Admin.com");
            if (user != null)
            {
                DbSet<IdentityUserRole<string>> roles = context.UserRoles;
                IdentityRole adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
                if (adminRole != null)
                {
                    if (!roles.Any(ur => ur.UserId == user.Id && ur.RoleId == adminRole.Id ))
                    {
                        roles.Add(new IdentityUserRole<string>() { UserId = user.Id, RoleId = adminRole.Id });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
