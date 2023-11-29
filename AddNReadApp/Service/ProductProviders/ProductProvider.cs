using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Service.ProductProviders
{
	internal class ProductProvider : IProductProvider
	{
		private readonly Entities _db;

		public ProductProvider(Entities DB) 
		{
			_db = DB;
		}

		public IEnumerable<Product> GetProducts()
		{
			return _db.Product.ToList();
		}

		public async Task<IEnumerable<Product>> GetProductsAsync()
		{
			return await _db.Product.ToListAsync();
		}
	}
}
