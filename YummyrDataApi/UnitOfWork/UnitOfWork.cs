using YummyrDataApi.Models;
using YummyrDataApi.Repositories;

namespace YummyrDataApi.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly YummyrContext _context;

        public UnitOfWork(YummyrContext context)
        {
            _context = context;
            Ingredients = new IngredientRepository(_context);
        }

        public IRecipeRepository Recipes { get; private set; }

        public IRecipeStepRepository RecipeSteps { get; private set; }

        public IIngredientQuantityRepository IngredientQuantities { get; private set; }

        public IIngredientRepository Ingredients { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
