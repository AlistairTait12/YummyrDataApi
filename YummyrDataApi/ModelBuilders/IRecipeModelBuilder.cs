using YummyrDataApi.Models;

namespace YummyrDataApi.ModelBuilders
{
    public interface IRecipeModelBuilder
    {
        RecipeModel Build(
            Recipe recipe,
            IEnumerable<IngredientQuantity> ingredientQuantities,
            IEnumerable<Ingredient> ingredients,
            IEnumerable<RecipeStep> recipeSteps);
    }
}