using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyrDataApi.Controllers;

namespace YummyrDataApiTests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class IngredientsControllerTests
    {
        private IngredientsController _ingredientsController;

        [SetUp]
        public void SetUp()
        {
            _ingredientsController = new IngredientsController();
        }
    }
}
