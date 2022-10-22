using YummyrDataApi.Models;

namespace YummyrDataApi.DatabaseHandlers
{
    public interface IRepositoryHandler
    {
        void WriteRecipeData(RecipeModel postModel);
    }
}
