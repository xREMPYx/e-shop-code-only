using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1;
using e_shop.Models;

namespace e_shop.Controllers.Admin
{
    [Secured]
    public class AdminProductsController : BaseController
    {
        private readonly MyContext _context;

        public AdminProductsController(MyContext context)
        {
            _context = context;
        }

        // GET: AdminProducts1
        public IActionResult Index()
        {
              return View(_context.Products.ToList());
        }

        // GET: AdminProducts1/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = _context.Products
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: AdminProducts1/Create
        public IActionResult Create()
        {
            ICollection<Category> categories = this._context.Categories.ToList();

            this.ViewBag.Categories = categories;

            return View();
        }

        // POST: AdminProducts1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,Full_Description")] Product product)
        {
            if (true)
            {
                try
                {
                    int categoryId = Convert.ToInt32(Request.Form["categoryId"]);

                    Category category = _context.Categories.Find(categoryId);

                    product.Category = category;
                }
                catch
                {

                }

                _context.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: AdminProducts1/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            ICollection<Category> categories = this._context.Categories.ToList();

            this.ViewBag.Categories = categories;

            return View(product);
        }

        // POST: AdminProducts1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Description,Full_Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    try
                    {
                        int categoryId = Convert.ToInt32(Request.Form["categoryId"]);

                        Category category = _context.Categories.Find(categoryId);

                        product.Category = category;
                    }
                    catch
                    {

                    }

                    _context.Update(product);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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

        // GET: AdminProducts1/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = _context.Products
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: AdminProducts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'MyContext.Products'  is null.");
            }
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.Id == id);
        }
    }
}