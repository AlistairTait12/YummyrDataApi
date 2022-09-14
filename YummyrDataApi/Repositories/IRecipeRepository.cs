using YummyrDataApi.Models;

namespace YummyrDataApi.Repositories
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Recipe GetRecipe(int id);
    }
}
