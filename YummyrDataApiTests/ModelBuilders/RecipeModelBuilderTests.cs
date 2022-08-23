using FluentAssertions;
using YummyrDataApi.enums;
using YummyrDataApi.ModelBuilders;
using YummyrDataApi.Models;

namespace YummyrDataApiTests.ModelBuilders
{
    public class RecipeModelBuilderTests
    {
        private RecipeModelBuilder _modelBuilder;

        [SetUp]
        public void SetUp()
        {
            _modelBuilder = new RecipeModelBuilder();
        }

        // TODO: When passed items, builds model
        [Test]
        public void BuildWhenPassedCorrectArgumentsBuildsRecipeModel()
        {
            // Arrange
            var expected = new RecipeModel
            {
                Recipe = new Recipe
                {
                    Id = 5,
                    Title = "A delicious test meal",
                    PrepMinutes = 5,
                    CookMinutes = 10
                },
                IngredientQuantityModels = new List<IngredientQuantityModel>
                {
                    new IngredientQuantityModel
                    {
                        IngredientQuantity = new IngredientQuantity
                        {
                            Id = 2,
                            IngredientId = 1,
                            RecipeId = 5,
                            Quantity = 30,
                            UnitOfMeasure = UnitOfMeasure.G
                        },
                        Ingredient = new Ingredient { Id = 1, Name = "Just a test ingredient" }
                    },
                    new IngredientQuantityModel
                    {
                        IngredientQuantity = new IngredientQuantity
                        {
                            Id = 5,
                            IngredientId = 2,
                            RecipeId = 5,
                            Quantity = 50,
                            UnitOfMeasure = UnitOfMeasure.G
                        },
                        Ingredient = new Ingredient { Id = 2, Name = "A second test ingredient" }
                    }
                },
                RecipeSteps = new List<RecipeStep>
                {
                    new RecipeStep { Id = 3, RecipeId = 5, StepOrder = 1, StepText = "Get ingredient one and put it on ingredient two" },
                    new RecipeStep { Id = 7, RecipeId = 5, StepOrder = 2, StepText = "Boil them for 10 minutes" },
                    new RecipeStep { Id = 2, RecipeId = 5, StepOrder = 3, StepText = "Drain the ingredients" }
                }
            };

            // Act
            var actual = _modelBuilder.Build(GetRecipe(), GetIngredientQuantities(), GetIngredients(), GetRecipeSteps());

            // Assert
            actual.Should().BeEquivalentTo(expected, a => a.WithStrictOrdering());
        }

        private Recipe GetRecipe()
        {
            return new Recipe
            {
                Id = 5,
                Title = "A delicious test meal",
                PrepMinutes = 5,
                CookMinutes = 10
            };
        }

        private List<IngredientQuantity> GetIngredientQuantities()
        {
            return new List<IngredientQuantity>
            {
                new IngredientQuantity
                {
                    Id = 2,
                    IngredientId = 1,
                    RecipeId = 5,
                    Quantity = 30,
                    UnitOfMeasure = UnitOfMeasure.G
                },
                new IngredientQuantity
                {
                    Id = 5,
                    IngredientId = 2,
                    RecipeId = 5,
                    Quantity = 50,
                    UnitOfMeasure = UnitOfMeasure.G
                }
            };
        }

        private List<Ingredient> GetIngredients()
        {
            return new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Just a test ingredient" },
                new Ingredient { Id = 2, Name = "A second test ingredient" }
            };
        }

        private List<RecipeStep> GetRecipeSteps()
        {
            return new List<RecipeStep>
            {
                new RecipeStep { Id = 2, RecipeId = 5, StepOrder = 3, StepText = "Drain the ingredients" },
                new RecipeStep { Id = 3, RecipeId = 5, StepOrder = 1, StepText = "Get ingredient one and put it on ingredient two" },
                new RecipeStep { Id = 7, RecipeId = 5, StepOrder = 2, StepText = "Boil them for 10 minutes" }
            };
        }
    }
}
