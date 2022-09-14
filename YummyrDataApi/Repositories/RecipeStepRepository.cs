using YummyrDataApi.Models;

namespace YummyrDataApi.Repositories
{
    public class RecipeStepRepository : Repository<RecipeStep>, IRecipeStepRepository
    {
        public RecipeStepRepository(YummyrContext context)
            : base(context)
        {
        }

        public IEnumerable<RecipeStep> GetRecipeStepsForRecipe(int recipeId)
        {
            return Find(recipeStep => recipeStep.RecipeId == recipeId);
        }
    }
}
