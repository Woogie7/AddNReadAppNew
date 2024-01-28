using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Command.AsyncCommand
{
	internal class LoadCartCommandAsync : AsyncCommandBase
	{
		private readonly CartViewModel _cartViewModel;

		public LoadCartCommandAsync(CartViewModel cartViewModel)
		{
			_cartViewModel = cartViewModel;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			ObservableCollection<Cart> carts = new ObservableCollection<Cart>(await _cartViewModel.GetAllCart());
			_cartViewModel.UpdateCartLoad(carts);
		}
	}
}
