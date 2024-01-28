using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Service.CartProviders
{
	internal interface ICartProvider
	{
		Cart GetCartById(int ID);
		Task<IEnumerable<Cart>> GetCartAsync();
		IEnumerable<Cart> GetCart();
		Task AddToCartAsync(Cart cartItem);
		Task RemoveFromCartAsync(Cart cartItem);

		bool IsBe(Product product);
	}
}
