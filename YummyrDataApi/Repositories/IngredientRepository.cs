using YummyrDataApi.Models;

namespace YummyrDataApi.Repositories
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(YummyrContext context)
            : base(context)
        {
        }

        public IEnumerable<Ingredient> GetIngredientsBeginningWith(char letter)
        {
            return YummyrContext.Ingredients
                .Where(ingredient => ingredient.Name.StartsWith(letter))
                .OrderBy(ingredient => ingredient.Name)
                .ToList();
        }

        public IEnumerable<Ingredient> GetIngredientsForIngredientQuantities(IEnumerable<IngredientQuantity> ingredientQuantities)
        {
            return YummyrContext.Ingredients
                .Where(ingredient => ingredientQuantities
                    .Select(quantity => quantity.IngredientId)
                    .Contains(ingredient.Id))
                .ToList();
        }
    }
}
