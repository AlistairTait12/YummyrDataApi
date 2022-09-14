using YummyrDataApi.Models;

namespace YummyrDataApi.Repositories
{
    public class IngredientQuantityRepository : Repository<IngredientQuantity>, IIngredientQuantityRepository
    {
        public IngredientQuantityRepository(YummyrContext context)
            : base(context)
        {
        }

        public IEnumerable<IngredientQuantity> GetIngredientQuantitiesForRecipe(int recipeId)
        {
            return Find(ingredientQuantity => ingredientQuantity.RecipeId == recipeId).ToList();
        }
    }
}
