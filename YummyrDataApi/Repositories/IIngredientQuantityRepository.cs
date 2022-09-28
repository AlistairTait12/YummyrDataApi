using YummyrDataApi.Models;

namespace YummyrDataApi.Repositories
{
    public interface IIngredientQuantityRepository : IRepository<IngredientQuantity>
    {
        IEnumerable<IngredientQuantity> GetIngredientQuantitiesForRecipe(int recipeId);
    }
}
