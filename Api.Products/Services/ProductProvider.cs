using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Api.Products.Dtos;
using Api.Common.Models;
using Api.Products.Data;

namespace Api.Products.Services
{
	public interface IProductProvider
	{
		Task<List<ProductDto>> GetAll();
		Task<ProductDto> GetById(int Id);
		Task<ProductDto> Create(ProductDto product);
		Task Update(ProductDto product);
		Task Delete(int Id);
	}

	public class ProductProvider : IProductProvider
	{
		protected readonly ILogger _logger;
		private readonly KolmeoDataContext _kolmeoDataContext;

		public ProductProvider(
			ILogger<ProductProvider> logger,
			KolmeoDataContext kolmeoDataContext)
		{
			_logger = logger;
			_kolmeoDataContext = kolmeoDataContext;
		}

		public async Task<List<ProductDto>> GetAll()
		{
			return await _kolmeoDataContext.Products.Select(p => new ProductDto(p)).ToListAsync();
		}

		public async Task<ProductDto> GetById(int Id)
		{
			var product = await _kolmeoDataContext.Products.FirstOrDefaultAsync(p => p.Id == Id);
			if (product == null) return null;

			return new ProductDto(product);
		}

		public async Task<ProductDto> Create(ProductDto productDto)
		{
			var newProduct = productDto.ToProduct();
			_kolmeoDataContext.Products.Add(newProduct);
			await _kolmeoDataContext.SaveChangesAsync();
			return new ProductDto(newProduct);
		}

		public async Task Update(ProductDto product)
		{
			var originalProduct = _kolmeoDataContext.Products.Find(product.Id);

			if (originalProduct == null)
			{
				_logger.LogWarning($"Failed to update prodcut({product.Id}), as the product is not found in DB.");
				throw new ApiException($"The product(id:{product.Id}) is not found.");
			}
			_kolmeoDataContext.Entry(originalProduct).CurrentValues.SetValues(product.ToProduct());

			_kolmeoDataContext.Products.Update(originalProduct);
			await _kolmeoDataContext.SaveChangesAsync();
		}

		public async Task Delete(int Id)
		{
			var product = _kolmeoDataContext.Products.Find(Id);
			if (product == null)
			{
				_logger.LogWarning($"Failed to delete prodcut({Id}), as the product is not found in DB.");
				throw new ApiException($"The product(id:{Id}) is not found.");
			}

			_kolmeoDataContext.Products.Remove(product);
			await _kolmeoDataContext.SaveChangesAsync();
		}
	}
}
