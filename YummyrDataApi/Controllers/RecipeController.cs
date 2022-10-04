using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YummyrDataApi.DatabaseHandlers;
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
        private readonly IRepositoryHandler _repositoryHandler;

        public RecipeController(
            IRecipeModelBuilder recipeModelBuilder,
            IUnitOfWork unitOfWork,
            IRepositoryHandler repositoryHandler)
        {
            _recipeModelBuilder = recipeModelBuilder;
            _unitOfWork = unitOfWork;
            _repositoryHandler = repositoryHandler;
        }

        // GET: api/recipes
        [HttpGet]
        public IActionResult GetRecipes() =>
            _unitOfWork.Recipes.GetAllRecipes == null
            ? NotFound()
            : new OkObjectResult(_unitOfWork.Recipes.GetAllRecipes().ToList());

        // GET: api/recipes/5
        [HttpGet("{id}")]
        public IActionResult GetRecipe(int id)
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

            var recipeModel = _recipeModelBuilder.Build(recipe, ingredientQuantities, ingredients, recipeSteps);

            return new OkObjectResult(recipeModel);
        }

        [HttpPost]
        public void PostRecipe(RecipeModel recipeModel)
        {
            _repositoryHandler.WriteRecipeData(recipeModel);
        }
    }
}
