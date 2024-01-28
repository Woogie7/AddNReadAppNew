using AddNReadApp.Core;
using AddNReadApp.Service.CartProviders;
using AddNReadApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AddNReadApp.Command
{
	internal class DeleteCartCommand : BaseCommand
	{
		private readonly CartViewModel _cartViewModel;
		private readonly ICartProvider _cartProvider;

		public DeleteCartCommand(CartViewModel cartViewModel, ICartProvider cartProvider)
		{

			_cartViewModel = cartViewModel;
			_cartProvider = cartProvider;
		}

		public override bool CanExecute(object parameter)
		{
			return true;
		}
		public override void Execute(object parameter)
		{
			if (parameter is Cart cartItem)
			{
				int ID = cartItem.IDCart;
				Cart deleteCart = _cartProvider.GetCartById(ID);

				_cartProvider.RemoveFromCartAsync(deleteCart);

				_cartViewModel.CartProduct.Remove(deleteCart);
				MessageBox.Show("Удаление прошло удачно", "Сообщение", MessageBoxButton.OK);
			}

		}
	}
}
