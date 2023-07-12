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
    public class AdminParametersController : BaseController
    {
        private readonly MyContext _context;

        public AdminParametersController(MyContext context)
        {
            _context = context;
        }

        // GET: AdminParameters1/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Parameters == null)
            {
                return NotFound();
            }

            var parameter = _context.Parameters
                .FirstOrDefault(m => m.Id == id);

            if (parameter == null)
            {
                return NotFound();
            }

            return View(parameter);
        }

        // GET: AdminParameters1/Create
        public IActionResult Create(int? productId)
        {            
            if (productId == null || _context.Parameters == null || _context.Products.Where(p => p.Id == productId).Count() == 0)
            {
                return Problem("Product does not exists!");
            }

            this.ViewBag.ProductId = productId;

            return View();
        }

        // POST: AdminParameters1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Parameter parameter, int productId)
        {
            if (true)
            {
                Product product = _context.Products.Find(productId);

                parameter.Product = product;

                _context.Parameters.Add(parameter);
                _context.SaveChanges();

                return RedirectToAction("Edit", "AdminProducts", new { id = productId });
            }

            return View(parameter);
        }

        // GET: AdminParameters1/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Parameters == null)
            {
                return NotFound();
            }

            var parameter = _context.Parameters.Find(id);

            if (parameter == null)
            {
                return NotFound();
            }

            this.ViewBag.ProductId = parameter.Product.Id;

            return View(parameter);
        }

        // POST: AdminParameters1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Value,Title")] Parameter parameter, int productId)
        {
            if (id != parameter.Id)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(parameter);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParameterExists(parameter.Id))
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

            return View(parameter);
        }

        // GET: AdminParameters1/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Parameters == null)
            {
                return NotFound();
            }

            var parameter = _context.Parameters
                .FirstOrDefault(m => m.Id == id);
            if (parameter == null)
            {
                return NotFound();
            }

            this.ViewBag.ProductId = parameter.Product.Id;

            return View(parameter);
        }

        // POST: AdminParameters1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Parameters == null)
            {
                return Problem("Entity set 'MyContext.Parameter'  is null.");
            }
            var parameter = _context.Parameters.Find(id);

            int productId = parameter.Product.Id;

            if (parameter != null)
            {
                _context.Parameters.Remove(parameter);
            }

            _context.SaveChanges();

            return RedirectToAction("Edit", "AdminProducts", new { id = productId });
        }

        private bool ParameterExists(int id)
        {
            return _context.Parameters.Any(e => e.Id == id);
        }
    }
}
