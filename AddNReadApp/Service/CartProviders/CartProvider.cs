using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Service.CartProviders
{
	internal class CartProvider : ICartProvider
	{
		private readonly Entities _db;

		public CartProvider(Entities DB)
		{
			_db = DB;
		}

		public Cart GetCartById(int ID)
		{
			return _db.Cart.FirstOrDefault(c => c.IDCart == ID);
		}
		public IEnumerable<Cart> GetCart()
		{
			return _db.Cart.ToList();
		}

		public async Task<IEnumerable<Cart>> GetCartAsync()
		{
			return await _db.Cart.ToListAsync();
		}

		public async Task AddToCartAsync(Cart cartItem)
		{
			_db.Cart.Add(cartItem);
			await _db.SaveChangesAsync();
		}

		public async Task RemoveFromCartAsync(Cart cartItem)
		{
			_db.Cart.Remove(cartItem);
			await _db.SaveChangesAsync();
		}

		public bool IsBe(Product product)
		{
			var cart = _db.Cart.FirstOrDefault(c => c.IDProduct == product.IDProduct);

			if(cart is null)
			{
				return false;
			}
			else
			{
				cart.Quantity++;
				return true;
			}
		}
	}
}
