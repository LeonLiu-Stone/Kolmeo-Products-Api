using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Products.Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }

		[Required]
		[StringLength(20)]
		public string Name { get; set; }

		public string Description { get; set; }

		[Required]
		[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
		public decimal Price { get; set; }
	}
}
