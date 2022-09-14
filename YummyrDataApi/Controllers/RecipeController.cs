using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YummyrDataApi.ModelBuilders;
using YummyrDataApi.Models;

namespace YummyrDataApi.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly YummyrContext _context;
        private readonly RecipeModelBuilder _recipeModelBuilder;

        public RecipeController(YummyrContext context)
        {
            _context = context;
            _recipeModelBuilder = new RecipeModelBuilder();
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            if (_context.Recipes is null)
            {
                return NotFound();
            }

            return await _context.Recipes.ToListAsync();
        }

        [HttpGet("{id}")]
        // GET: api/recipes/5
        public async Task<ActionResult<RecipeModel>> GetRecipe(int id)
        {
            // TODO: Get the Recipe model from a dedicated repository
            var recipe = _context.Recipes
                .Where(dbRecipe => dbRecipe.Id == id).FirstOrDefault();

            if (recipe is null)
            {
                return NotFound();
            }

            // TODO: IngredientQuantity repository, so we can implement a method of getting
            // ingredient quanities linked to a certain recipe
            var ingredientQuantities = _context.IngredientQuantities
                .Where(dbQuantity => dbQuantity.RecipeId == id)
                .ToList();

            // TODO: Same with a RecipeStepRepository
            var recipeSteps = _context.RecipeSteps
                .Where(dbStep => dbStep.RecipeId == id)
                .ToList();

            // TODO: GetIngredientsForRecipe(int id)
            var ingredients = _context.Ingredients
                .Where(dbIngredient => 
                ingredientQuantities
                .Select(quantity => quantity.IngredientId)
                .ToList()
                .Contains(dbIngredient.Id))
                .ToList();

            return _recipeModelBuilder.Build(recipe, ingredientQuantities, ingredients, recipeSteps);
        }
    }
}
