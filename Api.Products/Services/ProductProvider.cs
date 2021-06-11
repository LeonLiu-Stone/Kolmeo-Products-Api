using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Api.Products.Dtos;

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

		public ProductProvider(ILogger<ProductProvider> logger) {
			_logger = logger;
		}

		public async Task<List<ProductDto>> GetAll()
		{
			throw new NotImplementedException();
		}

		public async Task<ProductDto> GetById(int Id)
		{
			throw new NotImplementedException();
		}

		public async Task<ProductDto> Create(ProductDto productDto)
		{
			throw new NotImplementedException();
		}

		public async Task Update(ProductDto product)
		{
			throw new NotImplementedException();
		}

		public async Task Delete(int Id)
		{
			throw new NotImplementedException();
		}
	}
}
