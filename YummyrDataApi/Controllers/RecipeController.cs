using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YummyrDataApi.ModelBuilders;
using YummyrDataApi.Models;
using YummyrDataApi.Repositories;

namespace YummyrDataApi.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeModelBuilder _recipeModelBuilder;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeStepRepository _recipeStepRepository;
        private readonly IIngredientQuantityRepository _ingredientQuantityRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeController(
            IRecipeModelBuilder recipeModelBuilder,
            IRecipeRepository recipeRepository,
            IRecipeStepRepository recipeStepRepository,
            IIngredientQuantityRepository ingredientQuantityRepository,
            IIngredientRepository ingredientRepository)
        {
            _recipeModelBuilder = recipeModelBuilder;

            _recipeRepository = recipeRepository;
            _recipeStepRepository = recipeStepRepository;
            _ingredientQuantityRepository = ingredientQuantityRepository;
            _ingredientRepository = ingredientRepository;
        }

        // GET: api/recipes
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetRecipes()
        {
            if (_recipeRepository.GetAllRecipes() is null)
            {
                return NotFound();
            }

            return _recipeRepository.GetAllRecipes().ToList();
        }

        [HttpGet("{id}")]
        // GET: api/recipes/5
        public ActionResult<RecipeModel> GetRecipe(int id)
        {
            var recipe = _recipeRepository.GetRecipe(id);

            if (recipe is null)
            {
                return NotFound();
            }

            var ingredientQuantities = _ingredientQuantityRepository
                .GetIngredientQuantitiesForRecipe(id);
            var recipeSteps = _recipeStepRepository
                .GetRecipeStepsForRecipe(id);
            var ingredients = _ingredientRepository
                .GetIngredientsForIngredientQuantities(ingredientQuantities);

            return _recipeModelBuilder.Build(recipe, ingredientQuantities, ingredients, recipeSteps);
        }
    }
}
