using YummyrDataApi.Repositories;

namespace YummyrDataApi.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRecipeRepository Recipes { get; }

        IRecipeStepRepository RecipeSteps { get; }

        IIngredientQuantityRepository IngredientQuantities { get; }

        IIngredientRepository Ingredients { get; }

        int Complete();
    }
}
