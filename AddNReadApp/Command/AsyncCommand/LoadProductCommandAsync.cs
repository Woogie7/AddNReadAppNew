using AddNReadApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Command.AsyncCommand
{
	internal class LoadProductCommandAsync : AsyncComandBase
	{
		private readonly ProductViewModel _productViewModel;
		public LoadProductCommandAsync(ProductViewModel productViewModel)
		{
			_productViewModel = productViewModel;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			ObservableCollection<Product> products = new ObservableCollection<Product>(await _productViewModel.GetAllProduct());
			_productViewModel.UpdateProductLoad(products);
		}
	}
}
