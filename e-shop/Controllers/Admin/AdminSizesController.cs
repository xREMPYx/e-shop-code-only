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
    public class AdminSizesController : BaseController
    {
        private readonly MyContext _context;

        public AdminSizesController(MyContext context)
        {
            _context = context;
        }

        // GET: AdminSizes
        public async Task<IActionResult> Index()
        {
            var sizes = await _context.Sizes.ToListAsync();

            sizes.Sort((a, b) => a.Size_Number.CompareTo(b.Size_Number));

            return View(sizes);
        }

        // GET: AdminSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sizes == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // GET: AdminSizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Size_Number")] Size size)
        {
            if (true)
            {
                _context.Add(size);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(size);
        }

        // GET: AdminSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sizes == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        // POST: AdminSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Size_Number")] Size size)
        {
            if (id != size.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(size);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeExists(size.Id))
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
            return View(size);
        }

        // GET: AdminSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sizes == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // POST: AdminSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sizes == null)
            {
                return Problem("Entity set 'MyContext.Size'  is null.");
            }
            var size = await _context.Sizes.FindAsync(id);
            if (size != null)
            {
                _context.Sizes.Remove(size);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SizeExists(int id)
        {
            return _context.Sizes.Any(e => e.Id == id);
        }
    }
}
