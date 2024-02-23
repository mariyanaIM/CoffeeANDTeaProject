using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeeANDTea.Data;

namespace CoffeeANDTea.Controllers
{
    public class DrinkCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DrinkCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DrinkCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.DrinkCategories.ToListAsync());
        }

        // GET: DrinkCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkCategory = await _context.DrinkCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drinkCategory == null)
            {
                return NotFound();
            }

            return View(drinkCategory);
        }

        // GET: DrinkCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DrinkCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DateUpdate")] DrinkCategory drinkCategory)
        {
            drinkCategory.DateUpdate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View(drinkCategory);
            }
            _context.DrinkCategories.Add(drinkCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: DrinkCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkCategory = await _context.DrinkCategories.FindAsync(id);
            if (drinkCategory == null)
            {
                return NotFound();
            }
            return View(drinkCategory);
        }

        // POST: DrinkCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateUpdate")] DrinkCategory drinkCategory)
        {
            if (id != drinkCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drinkCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkCategoryExists(drinkCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        drinkCategory.DateUpdate = DateTime.Now;
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(drinkCategory);
        }

        // GET: DrinkCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkCategory = await _context.DrinkCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drinkCategory == null)
            {
                return NotFound();
            }

            return View(drinkCategory);
        }

        // POST: DrinkCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drinkCategory = await _context.DrinkCategories.FindAsync(id);
            if (drinkCategory != null)
            {
                _context.DrinkCategories.Remove(drinkCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkCategoryExists(int id)
        {
            return _context.DrinkCategories.Any(e => e.Id == id);
        }
    }
}
