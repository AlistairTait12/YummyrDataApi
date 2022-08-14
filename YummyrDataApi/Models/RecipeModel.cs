namespace YummyrDataApi.Models
{
    public class RecipeModel
    {
        public Recipe? Recipe { get; set; }

        public IEnumerable<IngredientQuantityModel>? IngredientQuantityModels { get; set; }

        public IEnumerable<RecipeStep>? RecipeSteps { get; set; }
    }
}
