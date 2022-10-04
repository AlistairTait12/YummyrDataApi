using YummyrDataApi.Models;

namespace YummyrDataApi.DatabaseHandlers
{
    public interface IRecipeDatabaseWriter
    {
        void WriteRecipeData(RecipeModel postModel);
    }
}
