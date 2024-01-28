using AddNReadApp.Command;
using AddNReadApp.Command.AsyncCommand;
using AddNReadApp.Core;
using AddNReadApp.Service.CartProviders;
using AddNReadApp.Service.ProductProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AddNReadApp.ViewModel
{
	internal class CartViewModel : ObservaleObject
	{
		public ObservableCollection<Cart> CartProduct { get; private set; }
		
		private readonly ProductViewModel _productViewModel;
		private readonly ICartProvider _cartProvider;

		public ICommand DeleteCartCommand { get; }
		public ICommand CreatePdfCommand { get; }
		public ICommand LoadCartCommandAsync { get; }

		public CartViewModel(ProductViewModel productViewModel, ICartProvider cartProvider)
		{
			_productViewModel = productViewModel;
			_cartProvider = cartProvider;

			DeleteCartCommand = new DeleteCartCommand(this, _cartProvider);
			CreatePdfCommand = new CreatePdfCommand(_cartProvider);
			LoadCartCommandAsync = new LoadCartCommandAsync(this);
		}

		public static CartViewModel LoadViewModel(ProductViewModel productViewModel, ICartProvider cartProvider)
		{
			CartViewModel cartViewModel = new CartViewModel(productViewModel, cartProvider);

			cartViewModel.LoadCartCommandAsync.Execute(null);

			return cartViewModel;
		}

		public void UpdateCartLoad(ObservableCollection<Cart> carts)
		{
			CartProduct = carts;
		}

		public async Task<IEnumerable<Cart>> GetAllCart()
		{
			return await _cartProvider.GetCartAsync();
		}

		private Cart _selectedCartItem;
		public Cart SelectedCartItem
		{
			get { return _selectedCartItem; }
			set
			{
				_selectedCartItem = value;
				OnPropertyChanged(nameof(SelectedCartItem));
			}
		}
	}
}
