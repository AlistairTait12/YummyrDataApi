using System.ComponentModel.DataAnnotations.Schema;
using YummyrDataApi.enums;

namespace YummyrDataApi.Models
{
    public class IngredientQuantity
    {
        public int Id { get; set; }

        [ForeignKey("RecipeId")]
        [Column(Order = 1)]
        public int RecipeId { get; set; }

        [ForeignKey("IngredientId")]
        [Column(Order = 2)]
        public int IngredientId { get; set; }

        public int Quantity { get; set; }

        public UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
