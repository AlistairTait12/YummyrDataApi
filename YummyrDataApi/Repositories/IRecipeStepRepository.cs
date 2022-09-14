using YummyrDataApi.Models;

namespace YummyrDataApi.Repositories
{
    public interface IRecipeStepRepository : IRepository<RecipeStep>
    {
        IEnumerable<RecipeStep> GetRecipeStepsForRecipe(int recipeId);
    }
}
