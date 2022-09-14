using YummyrDataApi.Repositories;

namespace YummyrDataApi.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IIngredientRepository Ingredients { get; }

        int Complete();
    }
}
