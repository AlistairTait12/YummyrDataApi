using Microsoft.EntityFrameworkCore;

namespace YummyrDataApi.Models
{
    public class YummyrContext : DbContext
    {
        public YummyrContext(DbContextOptions<YummyrContext> options)
            :base(options)
        {
        }

        public DbSet<Ingredient>? Ingredients { get; set; } = null;
        public DbSet<DietaryInfo>? DietaryInfo { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DietaryInfo>()
                .Property(e => e.DietaryValue)
                .HasConversion(
                    v => v.ToString(),
                    v => (DietaryValue)Enum.Parse(typeof(DietaryValue), v));
        }
    }
}
