﻿namespace YummyrDataApi.Models
{
    public class DietaryInfo
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public DietaryValue DietaryValue { get; set; }
    }
}
