using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeRating.DAL;
using RecipeRating.Model;
using RecipeRating.Web.Models;
using RecipeRating.Web.Services;

namespace RecipeRating.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly RecipeRatingDbContext _context;
        private readonly IYtHttpService _ytService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public RecipesController(RecipeRatingDbContext context, IYtHttpService ytService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _ytService = ytService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            var recipeRatingDbContext = _context.Recipes.Include(r => r.Dish).Include(r => r.ProviderAccount).Include(r => r.Ratings).Include(r => r.User);
            return View(await recipeRatingDbContext.ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Dish)
                .Include(r => r.ProviderAccount)
                .Include(r => r.User)
                .Include(r => r.Ratings).ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ProviderAccountId"] = new SelectList(_context.Set<ProviderAccount>(), "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ThumbnailUrl,Url,DishId,ProviderAccountId, AccountId, AccountName, AccountImageUrl")] RecipeInputModel recipe)
        {
            if (ModelState.IsValid)
            {
                var newRecipe = new Recipe
                {
                    Name = recipe.Name,
                    ThumbnailUrl = recipe.ThumbnailUrl,
                    Url = recipe.Url,
                    DishId = recipe.DishId,
                    UserId = _userManager.GetUserId(User)
                };
                
                if (recipe.ProviderAccountId == null)
                {
                    newRecipe.ProviderAccount = new ProviderAccount
                    {
                        ProviderId = 1,
                        AccountId = recipe.AccountId,
                        Name = recipe.AccountName,
                        ImageUrl = recipe.AccountImageUrl
                    };
                    /*_context.ProviderAccounts.Add(newAccount);
                    newRecipe.ProviderAccountId = newAccount.Id;*/
                }
                else
                {
                    newRecipe.ProviderAccountId = recipe.ProviderAccountId.Value;
                }
                _context.Recipes.Add(newRecipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            /*ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "ImageUrl", recipe.DishId);*/
            /*ViewData["ProviderAccountId"] = new SelectList(_context.Set<ProviderAccount>(), "Id", "Id", recipe.ProviderAccountId);*/
            /*ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", recipe.UserId);*/
            return View(recipe);
        }

        [HttpGet]
        public async Task<ActionResult> CreateAjax(string keyword)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            //var token = info.AuthenticationTokens.Where(a => a.Name == "access_token").First().Value;
            var model = _ytService.GetSearchResults(keyword);
            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetVideoAjax(string id)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            //var token = info.AuthenticationTokens.Where(a => a.Name == "access_token").First().Value;
            var model = _ytService.GetVideo(id);
            
            var dto = new VideoAutocompleteModel
            {
                Url = model.items[0].id,
                Name = model.items[0].snippet.title,
                AccountName = model.items[0].snippet.channelTitle,
                ThumbnailUrl = model.items[0].snippet.thumbnails.medium.url,
                AccountId = model.items[0].snippet.channelId
            };
            var provider = _context.ProviderAccounts.FirstOrDefault(p => p.AccountId == model.items[0].snippet.channelId);
            if (provider != null)
            {
                dto.ProviderAccountId = provider.Id.ToString();
            }
            return Ok(dto);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "ImageUrl", recipe.DishId);
            ViewData["ProviderAccountId"] = new SelectList(_context.Set<ProviderAccount>(), "Id", "Id", recipe.ProviderAccountId);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", recipe.UserId);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ThumbnailUrl,Url,DishId,ProviderAccountId,UserId")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
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
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "ImageUrl", recipe.DishId);
            ViewData["ProviderAccountId"] = new SelectList(_context.Set<ProviderAccount>(), "Id", "Id", recipe.ProviderAccountId);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", recipe.UserId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Dish)
                .Include(r => r.ProviderAccount)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Rate(int? recipeId, Rating rating)
        {
            rating.RecipeId = recipeId.Value;
            rating.CreatedAt = DateTime.Now;
            rating.UserId = _userManager.GetUserId(User);
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = recipeId});
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
