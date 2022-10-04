using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using YummyrDataApi.Controllers;
using YummyrDataApi.enums;
using YummyrDataApi.ModelBuilders;
using YummyrDataApi.Models;
using YummyrDataApi.UnitOfWork;

namespace YummyrDataApiTests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RecipeControllerTests
    {
        private IRecipeModelBuilder _recipeModelBuilder;
        private IUnitOfWork _unitOfWork;
        private RecipeController _recipeController;

        [SetUp]
        public void SetUp()
        {
            _recipeModelBuilder = A.Fake<IRecipeModelBuilder>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            A.CallTo(() => _unitOfWork.Recipes.GetAllRecipes()).Returns(GetTestRecipes());

            _recipeController = new RecipeController(
                _recipeModelBuilder,
                _unitOfWork);
        }

        [Test]
        public void GetRecipesGetsListOfAllRecipes()
        {
            // Arrange
            var expected = new OkObjectResult(GetTestRecipes());

            // Act
            var actual = _recipeController.GetRecipes();

            // Assert
            actual.Should().BeEquivalentTo(expected, a => a.WithStrictOrdering());
            A.CallTo(() => _unitOfWork.Recipes.GetAllRecipes()).MustHaveHappenedOnceExactly();
        }

        [Ignore("Not sure how repository can be null")]
        [Test]
        public void GetRecipesWhenDataBaseNullReturnsNotFound()
        {
            // Arrange
            A.CallTo(() => _unitOfWork.Recipes.GetAllRecipes()).Returns(null);
            var expected = new NotFoundResult();

            // Act
            var actual = _recipeController.GetRecipes();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetRecipeWhenNotInDataBaseReturnsNotFound()
        {
            // Arrange
            A.CallTo(() => _unitOfWork.Recipes.GetRecipe(99)).Returns(null);
            var expected = new NotFoundResult();

            // Act
            var actual = _recipeController.GetRecipe(99);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetRecipeWithIdReturnsRecipe()
        {
            // Arrange
            A.CallTo(() => _unitOfWork.Recipes.GetRecipe(1)).Returns(GetTestRecipes().First());

            A.CallTo(() => _recipeModelBuilder.Build(
                A<Recipe>.Ignored,
                A<IEnumerable<IngredientQuantity>>.Ignored,
                A<IEnumerable<Ingredient>>.Ignored,
                A<IEnumerable<RecipeStep>>.Ignored))
                .Returns(GetTestRecipeModel());

            var expected = new OkObjectResult(GetTestRecipeModel());

            // Act
            var actual = _recipeController.GetRecipe(1);

            // Assert
            A.CallTo(() => _unitOfWork.Recipes.GetRecipe(1)).MustHaveHappenedOnceExactly();
            actual.Should().BeEquivalentTo(expected);
        }

        [Ignore("Handler needs writing first")]
        [Test]
        public void PostRecipeCreatesARecipeInTheRepository()
        {
            // Arrange
            var recipeModel = GetTestPostRecipeModel();
            var expectedIngredientsList = GetTestIngredients();
            expectedIngredientsList.Add(new() { Id = 4, Name = "Lychee" });

            // Act
            _recipeController.PostRecipe(recipeModel);

            // Assert
            A.CallTo(() => _unitOfWork.Recipes.Add(recipeModel.Recipe)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.RecipeSteps.AddRange(recipeModel.RecipeSteps)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.IngredientQuantities
                .AddRange(recipeModel.IngredientQuantityModels
                .Select(model => model.IngredientQuantity)))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Ingredients
                .AddRange(recipeModel.IngredientQuantityModels
                .Select(model => model.Ingredient)))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
        }

        private List<Recipe> GetTestRecipes() => new()
        {
            new Recipe { Id = 1, Title = "Apple pie" },
            new Recipe { Id = 2, Title = "Banana split" },
            new Recipe { Id = 3, Title = "Canteloupe salad" },
            new Recipe { Id = 4, Title = "Durian smoothie" }
        };

        private List<Ingredient> GetTestIngredients() => new()
        {
            new() { Id = 1, Name = "Apple" },
            new() { Id = 2, Name = "Pastry" },
            new() { Id = 3, Name = "Sugar" }
        };

        private RecipeModel GetTestRecipeModel() => new()
        {
            Recipe = new Recipe { Id = 1, Title = "Apple pie" },
            IngredientQuantityModels = new List<IngredientQuantityModel>(),
            RecipeSteps = new List<RecipeStep>()
        };

        private RecipeModel GetTestPostRecipeModel() => new()
        {
            Recipe = new() { Title = "Lychee sorbet" },
            RecipeSteps = new List<RecipeStep>
            {
                new() { StepOrder = 1, StepText = "Step 1" },
                new() { StepOrder = 2, StepText = "Step 2" },
                new() { StepOrder = 3, StepText = "Eat it" },
            },
            IngredientQuantityModels = new List<IngredientQuantityModel>
            {
                new()
                {
                    IngredientQuantity = new() 
                    {
                        Quantity = 40,
                        UnitOfMeasure = UnitOfMeasure.G
                    },
                    Ingredient = new() { Name = "Lychee" }
                }
            }
        };

    }
}
