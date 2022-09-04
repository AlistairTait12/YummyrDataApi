using YummyrDataApi.Models;

namespace YummyrDataApi.Repositories
{
    public interface IIngredientRepository
    {
        IEnumerable<Ingredient> GetIngredientsBeginningWith(char letter);
    }
}
