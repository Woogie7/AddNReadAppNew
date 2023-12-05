using DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
	internal class StroyDB : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categorys { get; set; }
		public DbSet<Provider> Providers { get; set; }
		public DbSet<Manufacturer> Manufacturers { get; set; }
		public DbSet<Cost> Costs { get; set; }

		public StroyDB(DbContextOptions<StroyDB> options) : base(options)
		{
		
		}
	}
}
