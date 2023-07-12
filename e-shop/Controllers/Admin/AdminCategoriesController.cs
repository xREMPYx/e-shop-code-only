using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_shop.Models;

namespace e_shop.Controllers.Admin
{
    [Secured]
    public class AdminCategoriesController : BaseController
    {
        private readonly MyContext _context;

        public AdminCategoriesController(MyContext context)
        {
            _context = context;
        }

        // GET: AdminCategories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            
            categories = categories.Where(c => c.Parent == null).ToList();

            List<Category> cats = new List<Category>();

            categories.ForEach(c => Add(c));

            void Add(Category c)
            {
                cats.Add(c);

                foreach (Category category in c.SubCategories)
                {
                    Add(category);
                }
            }

            return View(cats);
        }

        // GET: AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: AdminCategories/Create
        public IActionResult Create()
        {
            List<Category> categories = _context.Categories.ToList();

            this.ViewBag.Categories = categories;

            return View();
        }

        // POST: AdminCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (true)
            {
                try
                {
                    int parentId = Convert.ToInt32(Request.Form["parentId"]);

                    Category c = _context.Categories.Find(parentId);

                    category.Parent = c;
                }
                catch (Exception)
                {
                    
                }

                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            List<Category> categories = _context.Categories
                .Where(c => c.Id != id)
                .ToList();

            this.ViewBag.Categories = categories;

            return View(category);
        }

        // POST: AdminCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    try
                    {
                        int parentId = Convert.ToInt32(Request.Form["parentId"]);

                        Category c = _context.Categories.Find(parentId);

                        category.Parent = c;
                    }
                    catch
                    {

                    }

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'MyContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int? id)
        {
            if(id == null)
            {
                return false;
            }

            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
