using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YummyrDataApi.ModelBuilders;
using YummyrDataApi.Models;
using YummyrDataApi.Repositories;
using YummyrDataApi.UnitOfWork;

namespace YummyrDataApi.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeModelBuilder _recipeModelBuilder;
        private readonly IUnitOfWork _unitOfWork;

        public RecipeController(
            IRecipeModelBuilder recipeModelBuilder,
            IUnitOfWork unitOfWork)
        {
            _recipeModelBuilder = recipeModelBuilder;
            _unitOfWork = unitOfWork;
        }

        // GET: api/recipes
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetRecipes()
        {
            if (_unitOfWork.Recipes.GetAllRecipes() is null)
            {
                return NotFound();
            }

            return _unitOfWork.Recipes.GetAllRecipes().ToList();
        }

        [HttpGet("{id}")]
        // GET: api/recipes/5
        public ActionResult<RecipeModel> GetRecipe(int id)
        {
            var recipe = _unitOfWork.Recipes.GetRecipe(id);

            if (recipe is null)
            {
                return NotFound();
            }

            var ingredientQuantities = _unitOfWork.IngredientQuantities
                .GetIngredientQuantitiesForRecipe(id);
            var recipeSteps = _unitOfWork.RecipeSteps
                .GetRecipeStepsForRecipe(id);
            var ingredients = _unitOfWork.Ingredients
                .GetIngredientsForIngredientQuantities(ingredientQuantities);

            return _recipeModelBuilder.Build(recipe, ingredientQuantities, ingredients, recipeSteps);
        }
    }
}
