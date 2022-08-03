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
    }
}
