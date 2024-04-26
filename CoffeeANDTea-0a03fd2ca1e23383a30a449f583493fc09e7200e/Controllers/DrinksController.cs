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
    public class DrinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DrinksController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Drinks
        public async Task<IActionResult> Index(int? catageryNum)
        {
            var applicationDbContext =await _context.Drinks
                .Include(d => d.DrinkCategories).ToListAsync();
            if (catageryNum != null)
            {
                applicationDbContext = applicationDbContext
                    .Where(x => x.DrinkCategoriesId == catageryNum).ToList();
            }
            return View("Index",  applicationDbContext);
        }

        // GET: Drinks
        public async Task<IActionResult> IndexTea()
        {
            var applicationDbContext = _context.Drinks
                .Include(d => d.DrinkCategories)
                .Where(d => d.DrinkCategoriesId == 1);
            return View("Index",await applicationDbContext.ToListAsync());
        }

        // GET: Drinks
        public async Task<IActionResult> IndexCoffee()
        {
            var applicationDbContext = _context.Drinks.
                Include(d => d.DrinkCategories).
                Where(d => d.DrinkCategoriesId == 2); 
            return View("Index",await applicationDbContext.ToListAsync());
        }

        // GET: Drinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .Include(d => d.DrinkCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // GET: Drinks/Create
        public IActionResult Create()
        {
            ViewData["DrinkCategoriesId"] = new SelectList(_context.DrinkCategories, "Id", "Name");
            return View();
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DrinkCategoriesId,Coffein,Bio,Image,Description,Price,DateUpdate")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                drink.DateUpdate = DateTime.Now;
                _context.Add(drink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrinkCategoriesId"] = new SelectList(_context.DrinkCategories, "Id", "Name", drink.DrinkCategoriesId);
            return View(drink);
        }

        // GET: Drinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }
            ViewData["DrinkCategoriesId"] = new SelectList(_context.DrinkCategories, "Id", "Name", drink.DrinkCategoriesId);
            return View(drink);
        }

        // POST: Drinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DrinkCategoriesId,Coffein,Bio,Image,Description,Price,DateUpdate")] Drink drink)
        {
            if (id != drink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkExists(drink.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        drink.DateUpdate = DateTime.Now;
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrinkCategoriesId"] = new SelectList(_context.DrinkCategories, "Id", "Name", drink.DrinkCategoriesId);
            return View(drink);
        }

        // GET: Drinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .Include(d => d.DrinkCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            if (drink != null)
            {
                _context.Drinks.Remove(drink);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.Id == id);
        }
    }
}
