using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Service.ProductProviders
{
	internal interface IProductProvider
	{
		Task<IEnumerable<Product>> GetProductsAsync();
		IEnumerable<Product> GetProducts();
	}
}
