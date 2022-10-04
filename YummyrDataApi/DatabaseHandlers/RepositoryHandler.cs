using System.Threading.Tasks.Dataflow;
using YummyrDataApi.Models;
using YummyrDataApi.UnitOfWork;

namespace YummyrDataApi.DatabaseHandlers
{
    public class RepositoryHandler : IRecipeDatabaseWriter
    {
        private readonly IUnitOfWork _unitOfWork;

        public RepositoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void WriteRecipeData(RecipeModel postModel)
        {
            var recipeId = WriteRecipe(postModel.Recipe);
            WriteUniqueIngredients(postModel.IngredientQuantityModels.Select(model => model.Ingredient));
            WriteIngredientQuantities(postModel.IngredientQuantityModels, recipeId);
            WriteRecipeSteps(postModel.RecipeSteps, recipeId);

            _unitOfWork.Complete();
        }

        private int WriteRecipe(Recipe recipe)
        {
            _unitOfWork.Recipes.Add(recipe);

            return recipe.Id;
        }

        private void WriteUniqueIngredients(IEnumerable<Ingredient> ingredients)
        {
            var ingredientsNotInDb = ingredients
                .Where(ingredient => !_unitOfWork.Ingredients.GetAll()
                    .Select(dbIngredient => dbIngredient.Name)
                    .Contains(ingredient.Name))
                    .ToList();

            ingredientsNotInDb.ForEach(ingredient => _unitOfWork.Ingredients.Add(ingredient));
        }

        private void WriteIngredientQuantities(IEnumerable<IngredientQuantityModel> ingredientQuantityModels, int recipeId)
        {
            foreach (var model in ingredientQuantityModels)
            {
                var quantity = model.IngredientQuantity;
                quantity.RecipeId = recipeId;
                quantity.IngredientId = _unitOfWork.Ingredients.GetAll().FirstOrDefault(ingredient => ingredient.Name == model.Ingredient.Name).Id;

                _unitOfWork.IngredientQuantities.Add(quantity);
            }
        }

        private void WriteRecipeSteps(IEnumerable<RecipeStep> recipeSteps, int recipeId)
        {
            foreach (var step in recipeSteps)
            {
                step.RecipeId = recipeId;
                _unitOfWork.RecipeSteps.Add(step);
            }
        }
    }
}
