using System.ComponentModel.DataAnnotations;

namespace Api.Products.Entities
{

	public class Product
	{
		[Key]
		public int Id { get; set; }  //as using vs version issue, not able to use 'init accessors' by now

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }
	}
}
