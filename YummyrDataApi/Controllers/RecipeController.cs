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
            var recipe = _context.Recipes
                .Where(dbRecipe => dbRecipe.Id == id).FirstOrDefault();

            var ingredientQuantities = _context.IngredientQuantities
                .Where(dbQuantity => dbQuantity.RecipeId == id)
                .ToList();

            var recipeSteps = _context.RecipeSteps
                .Where(dbStep => dbStep.RecipeId == id)
                .ToList();

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
