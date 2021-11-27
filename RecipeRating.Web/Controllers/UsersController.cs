using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeRating.DAL;
using RecipeRating.Model;
using RecipeRating.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeRating.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly RecipeRatingDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UsersController(RecipeRatingDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            List<UsersRolesModel> usersWithRoles = new List<UsersRolesModel>();
            var users = await _context.Users.ToListAsync();
            foreach(var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userWithRoles = new UsersRolesModel
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    RoleName = userRoles[0],
                    Id = user.Id
                };
                usersWithRoles.Add(userWithRoles);
            }
            
            return View(usersWithRoles);
        }

        public async Task<IActionResult> ChangeRole(string id, UsersRolesModel model)
        {
            try
            {
                var user = _context.AppUsers.Find(model.Id);
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRoleAsync(user, userRoles[0]);
                await _userManager.AddToRoleAsync(user, model.RoleName);
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

    }

}
