using YummyrDataApi.Models;

namespace YummyrDataApi.Repositories
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(YummyrContext context)
            : base(context)
        {
        }

        public Recipe GetRecipe(int id) => Get(id);
    }
}
