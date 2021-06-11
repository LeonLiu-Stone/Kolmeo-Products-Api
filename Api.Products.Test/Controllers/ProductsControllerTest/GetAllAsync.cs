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
	public class GetAllAsync
	{
		[Fact]
		public async Task WhenNoProductsReturnedFromDB_CanReturnAnEmptyArray()
		{
			//Arrange
			var productsInDB = new List<ProductDto>();

			var stubILogger = StubHelper.StubILogger<ProductsController>();
			var stubIProductProvider = new Mock<IProductProvider>();
			stubIProductProvider.Setup(x => x.GetAll())
				.Returns(Task.FromResult(productsInDB));

			var testedService = new ProductsController(
				stubILogger.Object,
				stubIProductProvider.Object);

			//Act
			var result = await testedService.GetAllAsync();

			//Assert
			var actionResult = Assert.IsType<ActionResult<List<ProductDto>>>(result);
			//Assert.IsType<NotFoundObjectResult>(actionResult.Result);
			var products = Assert.IsType<List<ProductDto>>(actionResult.Value);
			Assert.False(products.Any());
		}


		[Fact]
		public async Task WhenProductsReturnedFromDB_CanReturnProducts()
		{
			//Arrange
			var productsInDB = new List<ProductDto>() {
				new ProductDto() { Id= 1, Name = "Product A", Description="AAA", Price = 0.00M },
				new ProductDto() { Id= 2, Name = "Product B", Description="BBB", Price = 0.00M }
			};

			var stubILogger = StubHelper.StubILogger<ProductsController>();
			var stubIProductProvider = new Mock<IProductProvider>();
			stubIProductProvider.Setup(x => x.GetAll())
				.Returns(Task.FromResult(productsInDB));

			var testedService = new ProductsController(
				stubILogger.Object,
				stubIProductProvider.Object);

			//Act
			var result = await testedService.GetAllAsync();

			//Assert
			var actionResult = Assert.IsType<ActionResult<List<ProductDto>>>(result);
			var products = Assert.IsType<List<ProductDto>>(actionResult.Value);
			Assert.True(products.Any());
			Assert.Equal(productsInDB.Count, products.Count);
		}
	}
}
