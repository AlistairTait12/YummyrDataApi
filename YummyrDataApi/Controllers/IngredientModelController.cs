using Microsoft.AspNetCore.Mvc;
using YummyrDataApi.Models;

namespace YummyrDataApi.Controllers
{
    [Route("api/ingredients")]
    public class IngredientModelController : ControllerBase
    {
        private readonly YummyrContext _context;

        public IngredientModelController(YummyrContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients/5
        [HttpGet]
        public async Task<ActionResult<IngredientModel>> GetIngredient(int id)
        {
            if (_context.Ingredients is null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FindAsync(id);
            var dietaryInfo = _context.DietaryInfo.Where(info => info.IngredientId == id);

            return new IngredientModel
            {
                IngredientName = ingredient.Name,
                DietaryValues = dietaryInfo.Select(info => info.DietaryValue).ToList()
            };
        }
    }
}
