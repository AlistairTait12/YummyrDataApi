using System.Linq;
using YummyrDataApi.Models;

namespace YummyrDataApi.ModelBuilders
{
    public class RecipeModelBuilder : IRecipeModelBuilder
    {
        public RecipeModelBuilder()
        {
        }

        public RecipeModel Build(
            Recipe recipe,
            IEnumerable<IngredientQuantity> ingredientQuantities,
            IEnumerable<Ingredient> ingredients,
            IEnumerable<RecipeStep> recipeSteps)
        {
            var ingredientQuantityModels = GetIngredientQuantityModels(ingredientQuantities, ingredients);

            return new RecipeModel
            {
                Recipe = recipe,
                IngredientQuantityModels = ingredientQuantityModels,
                RecipeSteps = recipeSteps.OrderBy(step => step.StepOrder).ToList()
            };
        }

        private List<IngredientQuantityModel> GetIngredientQuantityModels(IEnumerable<IngredientQuantity> ingredientQuantities, IEnumerable<Ingredient> ingredients)
        {
            return ingredientQuantities.Select(ingredientQuantity =>
                new IngredientQuantityModel
                {
                    IngredientQuantity = ingredientQuantity,
                    Ingredient = ingredients
                        .Where(ingredient => ingredientQuantity.IngredientId == ingredient.Id)
                        .FirstOrDefault()
                }).ToList();
        }
    }
}
