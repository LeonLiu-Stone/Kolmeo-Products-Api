using System;
using System.Net.Mime;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using Api.Products.Dtos;
using Api.Products.Services;
using Api.Common.Models;

namespace Api.Products.Controllers
{
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	[ApiVersion("1.1")]
	public class ProductsController : BaseController
	{
		protected readonly ILogger _logger;
		private readonly IProductProvider _productProvider;

		public ProductsController(
			ILogger<ProductsController> logger,
			IProductProvider productProvider)
		{
			_logger = logger;
			_productProvider = productProvider;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<List<ProductDto>>> GetAllAsync()
		{
			var products = await _productProvider.GetAll();
			return products;
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
		{
			var product = await _productProvider.GetById(id);
			if (product == null)
			{
				return NotFound();
			}
			return product;
		}

		[HttpPost]
		[Consumes(MediaTypeNames.Application.Json)]
		//[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<ProductDto>> CreateAsync(ProductDto product)
		{
			if (!ModelState.IsValid) {
				return BadRequest();
			}

				var newProduct = await _productProvider.Create(product);
			//return CreatedAtRoute("products", new { id = newProduct.Id }, newProduct);  this doesn't work at moment by .net core 3.1
			return newProduct;

		}

		[HttpPut("{id}")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		//[ProducesResponseType(StatusCodes.Status202Accepted)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateAsync(int id, ProductDto newProduct)
		{
			//custom validations -> failed then return BadRequest();
			newProduct.Id = id;

			var product = await _productProvider.GetById(id);
			if (product == null)
			{
				return NotFound();
			}

			await _productProvider.Update(newProduct);

			//return AcceptedAtAction(nameof(GetByIdAsync));  // save as create
			return Ok();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var product = await _productProvider.GetById(id);
			if (product == null)
			{
				return NotFound();
			}

			await _productProvider.Delete(id);
			return NoContent();
		}
	}
}
