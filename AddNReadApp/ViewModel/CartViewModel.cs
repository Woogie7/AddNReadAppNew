using AddNReadApp.Core;
using AddNReadApp.Service.CartProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.ViewModel
{
	internal class CartViewModel : ObservaleObject
	{
		public ObservableCollection<Cart> CartProduct { get; private set; }
		private readonly ProductViewModel _productViewModel;
		private readonly ICartProvider _cartProvider;

		public CartViewModel(ProductViewModel productViewModel, ICartProvider cartProvider)
		{
			_productViewModel = productViewModel;
			_cartProvider = cartProvider;
			LoadCart(); // Загрузка корзины при инициализации
		}

		private async void LoadCart()
		{
			var cartItems = await GetAllCart();
			CartProduct = new ObservableCollection<Cart>(cartItems);
			OnPropertyChanged(nameof(CartProduct)); 
		}

		public async Task<IEnumerable<Cart>> GetAllCart()
		{
			return await _cartProvider.GetCartAsync();
		}
	}
}
