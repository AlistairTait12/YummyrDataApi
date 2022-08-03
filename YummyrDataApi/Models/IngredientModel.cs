namespace YummyrDataApi.Models
{
    public class IngredientModel
    {
        public int Id { get; set; }
        public string? IngredientName { get; set; }
        public IEnumerable<DietaryType>? DietaryTypes { get; set; }
    }
}
