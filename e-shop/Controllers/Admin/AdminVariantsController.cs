using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1;
using e_shop.Models;
using System.Collections;

namespace e_shop.Controllers.Admin
{
    [Secured]
    public class AdminVariantsController : BaseController
    {
        private readonly MyContext _context;

        public AdminVariantsController(MyContext context)
        {
            _context = context;
        }

        // GET: AdminVariants1/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Variants == null)
            {
                return NotFound();
            }

            var variant = _context.Variants
                .FirstOrDefault(m => m.Id == id);

            if (variant == null)
            {
                return NotFound();
            }

            return View(variant);
        }

        // GET: AdminParameters1/Create
        public IActionResult Create(int? productId)
        {
            if (productId == null || _context.Variants == null || _context.Products.Where(p => p.Id == productId).Count() == 0)
            {
                return Problem("Product does not exists!");
            }

            this.ViewBag.Colors = _context.Colors.ToList();
            this.ViewBag.Sizes = _context.Sizes.ToList();

            this.ViewBag.ProductId = productId;

            return View();
        }

        // POST: AdminParameters1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Variant variant, int productId)
        {
            if (true)
            {
                Product product = _context.Products.Find(productId);

                variant.Product = product;

                try
                {
                    int colorId = Convert.ToInt32(Request.Form["colorId"]);
                    int sizeId = Convert.ToInt32(Request.Form["sizeId"]);

                    Color color = _context.Colors.Find(colorId);
                    Size size = _context.Sizes.Find(sizeId);

                    variant.Color = color;
                    variant.Size = size;
                }
                catch
                {

                }

                _context.Variants.Add(variant);
                _context.SaveChanges();

                return RedirectToAction("Edit", "AdminProducts", new { id = productId });
            }

            return View(variant);
        }

        // GET: AdminParameters1/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Variants == null)
            {
                return NotFound();
            }

            var variant = _context.Variants.Find(id);

            if (variant == null)
            {
                return NotFound();
            }

            this.ViewBag.Colors = _context.Colors.ToList();
            this.ViewBag.Sizes = _context.Sizes.ToList();

            this.ViewBag.ProductId = variant.Product.Id;

            return View(variant);
        }

        // POST: AdminParameters1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Variant variant, int productId)
        {
            if (id != variant.Id)
            {
                return NotFound();
            }

            try
            {
                int colorId = Convert.ToInt32(Request.Form["colorId"]);
                int sizeId = Convert.ToInt32(Request.Form["sizeId"]);

                Color color = _context.Colors.Find(colorId);
                Size size = _context.Sizes.Find(sizeId);

                variant.Color = color;
                variant.Size = size;
            }
            catch
            {

            }

            if (true)
            {
                try
                {                    
                    _context.Variants.Update(variant);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariantExists(variant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Edit", "AdminProducts", new { id = productId });
            }

            this.ViewBag.Colors = _context.Colors.ToList();
            this.ViewBag.Sizes = _context.Sizes.ToList();

            this.ViewBag.ProductId = productId;

            return View(variant);
        }

        // GET: AdminParameters1/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Variants == null)
            {
                return NotFound();
            }

            var variant = _context.Variants
                .FirstOrDefault(m => m.Id == id);
            if (variant == null)
            {
                return NotFound();
            }

            this.ViewBag.ProductId = variant.Product.Id;

            return View(variant);
        }

        // POST: AdminParameters1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Variants == null)
            {
                return Problem("Entity set 'MyContext.Parameter'  is null.");
            }
            var variant = _context.Variants.Find(id);

            int productId = variant.Product.Id;

            if (variant != null)
            {
                _context.Variants.Remove(variant);
            }

            _context.SaveChanges();

            return RedirectToAction("Edit", "AdminProducts", new { id = productId });
        }

        private bool VariantExists(int id)
        {
            return _context.Variants.Any(e => e.Id == id);
        }
    }
}
