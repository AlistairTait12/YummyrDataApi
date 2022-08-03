namespace YummyrDataApi.Models
{
    public class IngredientModel
    {
        public string? IngredientName { get; set; }
        public IEnumerable<DietaryType>? DietaryTypes { get; set; }
    }
}
