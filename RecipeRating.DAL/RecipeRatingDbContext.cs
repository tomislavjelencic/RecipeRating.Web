using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeRating.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeRating.DAL
{
    public class RecipeRatingDbContext : IdentityDbContext<AppUser>
    {
            public RecipeRatingDbContext()
            {
            }

            public RecipeRatingDbContext(DbContextOptions<RecipeRatingDbContext> options)
                : base(options)
            {
            }
            public DbSet<AppUser> AppUsers { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Dish> Dishes { get; set; }
            public DbSet<Provider> Providers { get; set; }
            public DbSet<Rating> Ratings { get; set; }
            public DbSet<Recipe> Recipes { get; set; }
            public DbSet<Model.RecipeRating> RecipeRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
