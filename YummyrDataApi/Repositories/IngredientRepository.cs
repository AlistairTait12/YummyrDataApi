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

        public YummyrContext YummyrContext
        {
            get { return Context as YummyrContext;  }
        }
    }
}
