using System.ComponentModel.DataAnnotations.Schema;

namespace YummyrDataApi.Models
{
    public class RecipeStep
    {
        public int Id { get; set; }

        [ForeignKey("RecipeId")]
        public int RecipeId { get; set; }

        public int StepOrder { get; set; }

        public string? StepText { get; set; }
    }
}
