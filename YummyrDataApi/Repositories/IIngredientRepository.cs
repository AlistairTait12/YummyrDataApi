using YummyrDataApi.Models;

namespace YummyrDataApi.Repositories
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        IEnumerable<Ingredient> GetIngredientsBeginningWith(char letter);
    }
}
