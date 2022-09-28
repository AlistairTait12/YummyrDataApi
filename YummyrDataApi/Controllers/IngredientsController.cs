using Microsoft.AspNetCore.Mvc;
using YummyrDataApi.Models;
using YummyrDataApi.UnitOfWork;

namespace YummyrDataApi.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngredientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Ingredients
        [HttpGet]
        public IActionResult GetIngredients()
        {
            if (_unitOfWork.Ingredients is null)
            {
                return NotFound();
            }

            return new OkObjectResult(_unitOfWork.Ingredients.GetAll());
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public IActionResult GetIngredient(int id)
        {
            if (_unitOfWork.Ingredients is null)
            {
              return NotFound();
            }

            var ingredient = _unitOfWork.Ingredients.Get(id);

            if (ingredient is null)
            {
                return NotFound();
            }

            return new OkObjectResult(ingredient);
        }

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostIngredient(Ingredient ingredient)
        {
            if (_unitOfWork.Ingredients is null)
            {
                return Problem("Entity set 'YummyrContext.Ingredients'  is null.");
            }

            _unitOfWork.Ingredients.AddUnique(ingredient);
            _unitOfWork.Complete();

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public IActionResult DeleteIngredient(int id)
        {
            if (_unitOfWork.Ingredients is null)
            {
                return NotFound();
            }

            var ingredient = _unitOfWork.Ingredients.Get(id);

            if (ingredient is null)
            {
                return NotFound();
            }

            _unitOfWork.Ingredients.Remove(ingredient);
            _unitOfWork.Complete();

            return new NoContentResult();
        }
    }
}
