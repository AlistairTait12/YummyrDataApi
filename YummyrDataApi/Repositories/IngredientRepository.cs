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
            return new List<Ingredient>();
        }
    }
}
