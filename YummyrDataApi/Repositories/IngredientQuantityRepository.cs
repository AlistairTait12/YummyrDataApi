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
            return YummyrContext.IngredientQuantities
                .Where(ingredientQuantity => ingredientQuantity.RecipeId == recipeId)
                .ToList();
        }

        public YummyrContext? YummyrContext => Context as YummyrContext;
    }
}
