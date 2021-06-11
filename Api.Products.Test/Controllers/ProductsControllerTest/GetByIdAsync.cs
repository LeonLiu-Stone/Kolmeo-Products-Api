using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

using Api.Products.Dtos;
using Api.Products.Controllers;
using Api.Products.Services;
using System.Linq;

namespace Api.Products.Test.Controllers.ProductsControllerTest
{
	public class GetByIdAsync
	{
		[Fact]
		public async Task WhenNoProductFoundInDB_CanReturnNotFound()
		{
			//Arrange
			ProductDto productInDB = null;
			var testID = 123;

			var stubILogger = StubHelper.StubILogger<ProductsController>();
			var stubIProductProvider = new Mock<IProductProvider>();
			stubIProductProvider.Setup(x => x.GetById(It.IsAny<int>()))
				.Returns(Task.FromResult(productInDB));

			var testedService = new ProductsController(
				stubILogger.Object,
				stubIProductProvider.Object);

			//Act
			var result = await testedService.GetByIdAsync(testID);

			//Assert
			var actionResult = Assert.IsType<ActionResult<ProductDto>>(result);
			Assert.IsType<NotFoundResult>(actionResult.Result);
		}

		[Fact]
		public async Task WhenInProductInDB_CanReturnProduct()
		{
			//Arrange
			var productInDB = new ProductDto() { Id = 2, Name = "Product B", Description = "BBB", Price = 0.00M };
			var testID = productInDB.Id;

			var stubILogger = StubHelper.StubILogger<ProductsController>();
			var stubIProductProvider = new Mock<IProductProvider>();
			stubIProductProvider.Setup(x => x.GetById(It.IsAny<int>()))
				.Returns(Task.FromResult(productInDB));

			var testedService = new ProductsController(
				stubILogger.Object,
				stubIProductProvider.Object);

			//Act
			var result = await testedService.GetByIdAsync(testID);

			//Assert
			var actionResult = Assert.IsType<ActionResult<ProductDto>>(result);
			var product = Assert.IsType<ProductDto>(actionResult.Value);
			Assert.Equal(product.Id, productInDB.Id);
		}
	}
}
