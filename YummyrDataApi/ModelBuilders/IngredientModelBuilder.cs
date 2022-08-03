using YummyrDataApi.Models;

namespace YummyrDataApi.ModelBuilders
{
    public class IngredientModelBuilder
    {
        public IngredientModel Build(Ingredient ingredient, List<DietaryInfo> dietaryInfo)
        {
            var dietaryValues = dietaryInfo.Select(info => info.DietaryValue).ToList();

            return new IngredientModel
            {
                Id = ingredient.Id,
                IngredientName = ingredient.Name,
                DietaryValues = dietaryValues
            };
        }
    }
}
