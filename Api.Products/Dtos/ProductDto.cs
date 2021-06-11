using System;
using System.ComponentModel.DataAnnotations;
using Api.Products.Entities;

namespace Api.Products.Dtos
{
	public class ProductDto
	{
		public ProductDto() { }

		public ProductDto(Product product)
		{
			if (product == null) { return; }

			Id = product.Id;
			Name = product.Name;
			Description = product.Description;
			Price = product.Price;
		}

		public int Id { get; set; }

		[Required]
		[StringLength(20)]
		public string Name { get; set; }

		public string Description { get; set; }

		[Required]
		[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
		public decimal Price { get; set; }

		public Product ToProduct()
		{
			return new Product()
			{
				Id = this.Id,
				Name = this.Name,
				Description = this.Description,
				Price = this.Price,
			};
		}
	}
}
