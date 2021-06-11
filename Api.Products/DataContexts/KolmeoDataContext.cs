using System;

using Microsoft.EntityFrameworkCore;

using Api.Products.Entities;

namespace Api.Products.Data
{
	public class KolmeoDataContext : DbContext
	{
		public KolmeoDataContext(DbContextOptions<KolmeoDataContext> options)
		: base(options)
		{ }

		public DbSet<Product> Products { get; set; }
	}
}
