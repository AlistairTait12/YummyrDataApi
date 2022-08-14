using System.Linq;
using YummyrDataApi.Models;

namespace YummyrDataApi.ModelBuilders
{
    public class RecipeModelBuilder
    {
        public RecipeModelBuilder()
        {
        }

        public RecipeModel Build(
            Recipe recipe, 
            List<IngredientQuantity> ingredientQuantities,
            List<Ingredient> ingredients,
            List<RecipeStep> recipeSteps)
        {
            var ingredientQuantityModels = GetIngredientQuantityModels(ingredientQuantities, ingredients);

            return new RecipeModel
            {
                Recipe = recipe,
                IngredientQuantityModels = ingredientQuantityModels,
                RecipeSteps = recipeSteps.OrderBy(step => step.StepOrder).ToList()
            };
        }

        private List<IngredientQuantityModel> GetIngredientQuantityModels(List<IngredientQuantity> ingredientQuantities, List<Ingredient> ingredients)
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
