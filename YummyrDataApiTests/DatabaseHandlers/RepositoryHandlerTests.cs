using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using FakeItEasy;
using System.Diagnostics.CodeAnalysis;
using YummyrDataApi.DatabaseHandlers;
using YummyrDataApi.enums;
using YummyrDataApi.Models;
using YummyrDataApi.UnitOfWork;

namespace YummyrDataApiTests.DatabaseHandlers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RepositoryHandlerTests
    {
        private IUnitOfWork _unitOfWork;
        private RepositoryHandler _repositoryHandler;
        private List<Ingredient> _fakeIngredientRepo;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();

            _fakeIngredientRepo = GetIngredientsAlreadyInDataBase();

            A.CallTo(() => _unitOfWork.Ingredients.GetAll()).Returns(_fakeIngredientRepo);
            
            _repositoryHandler = new(_unitOfWork);
        }

        [Test]
        public void WriteRecipeDataWritesBasicRecipeDataToDatabase()
        {
            // Arrange
            var recipeToPost = GetRecipeModelToPost();

            A.CallTo(() => _unitOfWork.Ingredients.Add(A<Ingredient>.Ignored))
                .Invokes(() => _fakeIngredientRepo.Add(new() { Id = 5, Name = "Pineapple" }));

            A.CallTo(() => _unitOfWork.Recipes.GetAllRecipes())
                .Returns(new List<Recipe>
                {
                    new()
                    {
                        Id = 1,
                        Title = "A delicious test recipe",
                        PrepMinutes = 0,
                        CookMinutes = 5,
                        Serves = 1
                    }
                });

            // Act
            _repositoryHandler.WriteRecipeData(recipeToPost);

            // Assert
            A.CallTo(() => _unitOfWork.Recipes.Add(recipeToPost.Recipe)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void WriteRecipeDataOnlyWritesIngredientsNotAlreadyInTheDataBase()
        {
            // Arrange
            var recipeToPost = GetRecipeModelToPost();

            A.CallTo(() => _unitOfWork.Ingredients.Add(A<Ingredient>.Ignored))
                .Invokes(() => _fakeIngredientRepo.Add(new() { Id = 5, Name = "Pineapple" }));

            // Act
            _repositoryHandler.WriteRecipeData(recipeToPost);

            // Assert
            A.CallTo(() => _unitOfWork.Ingredients
                .Add(recipeToPost.IngredientQuantityModels.FirstOrDefault(model => model.Ingredient.Name == "Milk").Ingredient))
                .MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.Ingredients
                .Add(recipeToPost.IngredientQuantityModels.FirstOrDefault(model => model.Ingredient.Name == "Pineapple").Ingredient))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void WriteRecipeDataWritesIngredientQuantitiesToTheDataBase()
        {
            // Arrange
            var recipeToPost = GetRecipeModelToPost();

            A.CallTo(() => _unitOfWork.Ingredients.Add(A<Ingredient>.Ignored))
                .Invokes(() => _fakeIngredientRepo.Add(new() { Id = 5, Name = "Pineapple" }));

            // Act
            _repositoryHandler.WriteRecipeData(recipeToPost);

            // Assert
            A.CallTo(() => _unitOfWork.IngredientQuantities
                .Add(recipeToPost.IngredientQuantityModels.FirstOrDefault(model => model.Ingredient.Name == "Milk").IngredientQuantity))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.IngredientQuantities
                .Add(recipeToPost.IngredientQuantityModels.FirstOrDefault(model => model.Ingredient.Name == "Pineapple").IngredientQuantity))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void WriteRecipeDataWritesRecipeStepsToTheDataBase()
        {
            // Arrange
            var recipeToPost = GetRecipeModelToPost();

            A.CallTo(() => _unitOfWork.Ingredients.Add(A<Ingredient>.Ignored))
                .Invokes(() => _fakeIngredientRepo.Add(new() { Id = 5, Name = "Pineapple" }));

            // Act
            _repositoryHandler.WriteRecipeData(recipeToPost);

            // Assert
            A.CallTo(() => _unitOfWork.RecipeSteps
                .Add(recipeToPost.RecipeSteps.FirstOrDefault(step => step.StepOrder == 1)))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.RecipeSteps
                .Add(recipeToPost.RecipeSteps.FirstOrDefault(step => step.StepOrder == 2)))
                .MustHaveHappenedOnceExactly();
        }

        private List<Ingredient> GetIngredientsAlreadyInDataBase() => new()
        {
            new() { Id = 1, Name = "Milk" },
            new() { Id = 2, Name = "Cheese" },
            new() { Id = 3, Name = "Apple" },
            new() { Id = 4, Name = "Carrot" }
        };

        private RecipeModel GetRecipeModelToPost() => new()
        {
            Recipe = new() 
            { 
                Title = "A delicious test recipe",
                PrepMinutes = 0,
                CookMinutes = 5,
                Serves = 1
            },
            RecipeSteps = new List<RecipeStep>()
            {
                new() { StepOrder = 1, StepText = "Prepare it" },
                new() { StepOrder = 2, StepText = "Eat it" }
            },
            IngredientQuantityModels = new List<IngredientQuantityModel>()
            {
                new()
                {
                    Ingredient = new() { Name = "Milk" },
                    IngredientQuantity = new()
                    {
                        Quantity = 20,
                        UnitOfMeasure = UnitOfMeasure.G
                    }
                },
                new()
                {
                    Ingredient = new() { Name = "Pineapple" },
                    IngredientQuantity = new()
                    {
                        Quantity = 40,
                        UnitOfMeasure = UnitOfMeasure.Tsp
                    }
                }
            }
        };
    }
}
