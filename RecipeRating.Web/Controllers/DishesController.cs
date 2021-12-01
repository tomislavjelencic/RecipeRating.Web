using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeRating.DAL;
using RecipeRating.Model;
using RecipeRating.Web.Helpers;
using RecipeRating.Web.Models;

namespace RecipeRating.Web.Controllers
{
    public class DishesController : Controller
    {
        private readonly RecipeRatingDbContext _context;

        public DishesController(RecipeRatingDbContext context)
        {
            _context = context;
        }

        // GET: Dishes
        public async Task<IActionResult> Index(DishesFilterModel filter)
        {          
            if (filter.searchString != null || filter.categoryFilter != null)
            {
                filter.pageNumber = 1;
            }
            else
            {
                filter.searchString = filter.currentFilter;
            }

            ViewData["CurrentFilter"] = filter.searchString;
            ViewData["CurrentCategoryFilter"] = filter.categoryFilter;

            var dishes = from s in _context.Dishes
                             select s;
            if (!String.IsNullOrEmpty(filter.searchString))
            {
                dishes = dishes.Include(d =>d.Category).Where(s => s.Name.Contains(filter.searchString));
            }
            if (filter.categoryFilter != null)
            {
                dishes = dishes.Include(d => d.Category).Where(s => s.CategoryId == filter.categoryFilter.Value);
            }

            int pageSize = 12;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", filter.categoryFilter);
            //ViewBag.CurrentFilter = filter.searchString;
            return View(await PaginatedList<Dish>.CreateAsync(dishes.Include(d => d.Category).AsNoTracking(), filter.pageNumber ?? 1, pageSize));

            /*var recipeRatingDbContext = _context.Dishes.Include(d => d.Category);
            return View(await recipeRatingDbContext.ToListAsync());*/
        }

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUrl,CategoryId")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", dish.CategoryId);
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", dish.CategoryId);
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl,CategoryId")] Dish dish)
        {
            if (id != dish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", dish.CategoryId);
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }
    }
}
