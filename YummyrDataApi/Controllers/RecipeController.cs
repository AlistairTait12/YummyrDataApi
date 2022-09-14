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
        private readonly YummyrContext _context;
        private readonly RecipeModelBuilder _recipeModelBuilder;
        private readonly RecipeRepository _recipeRepository;
        private readonly RecipeStepRepository _recipeStepRepository;
        private readonly IngredientQuantityRepository _ingredientQuantityRepository;
        private readonly IngredientRepository _ingredientRepository;

        public RecipeController(YummyrContext context)
        {
            _context = context;
            _recipeModelBuilder = new RecipeModelBuilder();

            _recipeRepository = new RecipeRepository(_context);
            _recipeStepRepository = new RecipeStepRepository(_context);
            _ingredientQuantityRepository = new IngredientQuantityRepository(_context);
            _ingredientRepository = new IngredientRepository(_context);
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            if (_recipeRepository.GetAllRecipes() is null)
            {
                return NotFound();
            }

            return _recipeRepository.GetAllRecipes().ToList();
        }

        [HttpGet("{id}")]
        // GET: api/recipes/5
        public async Task<ActionResult<RecipeModel>> GetRecipe(int id)
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
