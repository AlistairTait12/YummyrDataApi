﻿using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyrDataApi.Controllers;
using YummyrDataApi.ModelBuilders;
using YummyrDataApi.Models;
using YummyrDataApi.Repositories;

namespace YummyrDataApiTests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RecipeControllerTests
    {
        private IRecipeModelBuilder _recipeModelBuilder;
        private IRecipeRepository _recipeRepository;
        private IRecipeStepRepository _recipeStepRepository;
        private IIngredientQuantityRepository _ingredientQuantityRepository;
        private IIngredientRepository _ingredientRepository;
        private RecipeController _recipeController;

        [SetUp]
        public void SetUp()
        {
            _recipeModelBuilder = A.Fake<IRecipeModelBuilder>();
            _recipeRepository = A.Fake<IRecipeRepository>();
            _recipeStepRepository = A.Fake<IRecipeStepRepository>();
            _ingredientQuantityRepository = A.Fake<IIngredientQuantityRepository>();
            _ingredientRepository = A.Fake<IIngredientRepository>();

            A.CallTo(() => _recipeRepository.GetAllRecipes()).Returns(GetTestRecipes());

            _recipeController = new RecipeController(
                _recipeModelBuilder,
                _recipeRepository,
                _recipeStepRepository,
                _ingredientQuantityRepository,
                _ingredientRepository);
        }

        [Test]
        public void GetRecipesGetsListOfAllRecipes()
        {
            // Arrange
            var expected = GetTestRecipes();

            // Act
            var actual = _recipeController.GetRecipes();

            // Assert
            actual.Should().BeEquivalentTo(expected, a => a.WithStrictOrdering());
        }

        public IEnumerable<Recipe> GetTestRecipes() => new List<Recipe>
        {
            new Recipe { Id = 1, Title = "Apple pie" },
            new Recipe { Id = 2, Title = "Banana split" },
            new Recipe { Id = 3, Title = "Canteloupe salad" },
            new Recipe { Id = 4, Title = "Durian smoothie" }
        };

        public IEnumerable<Ingredient> GetTestIngredients() => new List<Ingredient>
        {
            new Ingredient { Id = 1, Name = "Apples" }
        };
    }
}