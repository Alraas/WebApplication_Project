using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_Project.Data;
using WebApplication_Project.Models;

namespace WebApplication_Project.Controllers
{

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }
        //[Authorize]
        // GET: Products
        public async Task<IActionResult> Index()
        {
            var users = HttpContext.User.Claims.ToList();
            if (users.Count() !=0)
            {
                var RoleName = users[3].Value;
                if (RoleName == "Member")
                {
                    ViewBag.IsCheck = false;
                }
                else
                {
                    ViewBag.IsCheck = true;
                }
            }
          
            var list = _context.Product.ToList();
            return View(list);
        }

        // GET: ShowSearchForm
        [AllowAnonymous]

        public async Task<IActionResult> ShowSearchForm()
        {
            return View("ShowSearchForm");
        }
        // GET: ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.Product.Where(x => x.Description.Contains(SearchPhrase) || x.Name.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Products/Details/5
        [AllowAnonymous]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Create([Bind("ID,Name,Price,Description,ImageFile,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);
                product.ImageName = fileName = fileName + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }


                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]


        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,Description,Image,Stock")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            var users = HttpContext.User.Claims.ToList();
            var cusid = users[0].Value;
            Cart cart = new Cart();
            cart.Quantity = 1;
            cart.ProductId = id;
            cart.CustomerId = Guid.Parse(cusid);
            cart.Total = 10;
            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();


            var list = _context.Cart.Where(a => a.CustomerId == Guid.Parse(cusid)).ToList().Count();
            string counter = "SessionCount";


            int newCounter = list;
           

            
            HttpContext.Session.SetInt32(counter, newCounter);

            return RedirectToAction(nameof(Index));
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ID == id);
        }
    }
}
