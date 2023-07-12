using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1;
using e_shop.Models;
using e_shop.Utils;

namespace e_shop.Controllers.Admin
{
    [Secured]
    public class AdminPhotosController : BaseController
    {
        private readonly MyContext _context;

        public AdminPhotosController(MyContext context)
        {
            _context = context;
        }

        // GET: AdminPhotos1/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Photos == null)
            {
                return NotFound();
            }

            var photo = _context.Photos
                .FirstOrDefault(m => m.Id == id);

            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // GET: AdminPhotos1/Create
        public IActionResult Create(int? productId)
        {
            if (productId == null || _context.Photos == null || _context.Products.Where(p => p.Id == productId).Count() == 0)
            {
                return Problem("Product does not exists!");
            }

            this.ViewBag.ProductId = productId;

            return View();
        }

        // POST: AdminPhotos1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Photo photo, int productId, IFormFile formFile)
        {
            if (true)
            {
                Product product = _context.Products.Find(productId);

                PhotoService service = new PhotoService(formFile);                

                photo.Product = product;
                photo.Name = service.GetFileName();

                _context.Photos.Add(photo);
                _context.SaveChanges();
                service.Save();

                return RedirectToAction("Edit", "AdminProducts", new { id = productId });
            }

            return View(photo);
        }

        // GET: AdminPhotos1/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Photos == null)
            {
                return NotFound();
            }

            var photo = _context.Photos.Find(id);

            if (photo == null)
            {
                return NotFound();
            }

            this.ViewBag.ProductId = photo.Product.Id;

            return View(photo);
        }

        // POST: AdminPhotos1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Photo photo, int id, int productId, IFormFile formFile)
        {
            if (id != photo.Id)
            {
                return NotFound();
            }
            
            PhotoService service = new PhotoService(formFile);

            if (true)
            {
                try
                {
                    service.Update(photo);
                    _context.Photos.Update(photo);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photo.Id))
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
        }

        // GET: AdminPhotos1/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Photos == null)
            {
                return NotFound();
            }

            var photo = _context.Photos
                .FirstOrDefault(m => m.Id == id);
            if (photo == null)
            {
                return NotFound();
            }

            this.ViewBag.ProductId = photo.Product.Id;

            return View(photo);
        }

        // POST: AdminPhotos1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Photos == null)
            {
                return Problem("Entity set 'MyContext.Parameter'  is null.");
            }
            var photo = _context.Photos.Find(id);

            int productId = photo.Product.Id;

            if (photo != null)
            {
                _context.Photos.Remove(photo);
                PhotoService.Remove(photo);
            }

            _context.SaveChanges();

            return RedirectToAction("Edit", "AdminProducts", new { id = productId });
        }

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}
