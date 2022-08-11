namespace YummyrDataApi.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public int PrepMinutes { get; set; }

        public int CookMinutes { get; set; }

        public int Serves { get; set; }
    }
}
