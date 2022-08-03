using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyrDataApi.ModelBuilders;
using YummyrDataApi.Models;

namespace YummyrDataApiTests.ModelBuilders
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class IngredientModelBuilderTests
    {
        private IngredientModelBuilder _modelBuilder;

        [SetUp]
        public void SetUp()
        {
            _modelBuilder = new IngredientModelBuilder();
        }

        [Test]
        public void BuildWhenPassedIngredientIdReturnsIngredientModel()
        {
            // Arrange
            var expected = new IngredientModel
            {
                Id = 1,
                IngredientName = "Cheese",
                DietaryValues = new List<DietaryValue>
                {
                    DietaryValue.Vegetarian,
                    DietaryValue.Dairy
                }
            };

            var ingredient = GetIngredients().Where(x => x.Name == "Cheese").First();
            var dietaryInfo = GetDietaryInfo().Where(x => x.IngredientId == 1).ToList();

            // Act
            var actual = _modelBuilder.Build(ingredient, dietaryInfo);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        public List<Ingredient> GetIngredients()
        {
            return new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Cheese" },
                new Ingredient { Id = 2, Name = "Waffer thin ham" },
                new Ingredient { Id = 3, Name = "Wholemeal bread" },
                new Ingredient { Id = 4, Name = "Slightly salted butter" },
                new Ingredient { Id = 5, Name = "Light mayonnaise" }
            };
        }

        public List<DietaryInfo> GetDietaryInfo()
        {
            return new List<DietaryInfo>
            {
                new DietaryInfo { Id = 1, IngredientId = 1, DietaryValue = DietaryValue.Vegetarian},
                new DietaryInfo { Id = 2, IngredientId = 1, DietaryValue = DietaryValue.Dairy },
                new DietaryInfo { Id = 3, IngredientId = 2, DietaryValue = DietaryValue.Meat },
                new DietaryInfo { Id = 4, IngredientId = 3, DietaryValue = DietaryValue.Gluten },
                new DietaryInfo { Id = 5, IngredientId = 3, DietaryValue = DietaryValue.Vegetarian },
                new DietaryInfo { Id = 6, IngredientId = 4, DietaryValue = DietaryValue.Dairy }
            };
        }
    }
}
