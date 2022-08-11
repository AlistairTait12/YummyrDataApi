using Microsoft.EntityFrameworkCore;
using YummyrDataApi.enums;

namespace YummyrDataApi.Models
{
    public class YummyrContext : DbContext
    {
        public YummyrContext(DbContextOptions<YummyrContext> options)
            :base(options)
        {
        }

        public DbSet<Ingredient>? Ingredients { get; set; } = null;

        public DbSet<Recipe>? Recipies { get; set; } = null;

        public DbSet<RecipeStep>? RecipeSteps { get; set; } = null;

        public DbSet<IngredientQuantity> IngredientQuantities { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientQuantity>()
                .Property(e => e.UnitOfMeasure)
                .HasConversion(
                    v => v.ToString(),
                    v => (UnitOfMeasure)Enum.Parse(typeof(UnitOfMeasure), v));
        }
    }
}
