using AddNReadApp.Core;
using AddNReadApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Command
{
	internal class DeleteCartCommand : BaseCommand
	{
		private readonly Entities _db;
		private readonly CartViewModel _cartViewModel;
		public DeleteCartCommand(CartViewModel cartViewModel, Entities DB)
		{
			_db = DB;
			_cartViewModel = cartViewModel;
			_cartViewModel.PropertyChanged += OnProductViewModelPropertyChanged;
		}

		private void OnProductViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(ProductViewModel.SelectedProduct))
			{
				OnCanExecuteChanged();
			}
		}

		public override bool CanExecute(object parameter)
		{
			return _productViewModel.SelectedProduct != null;
		}
		public override void Execute(object parameter)
		{
			//int ID =(_productViewModel.SelectedProduct.ID);
			//Product deleteProduct = (from m in _db.Product where m.ID == ID select m).SingleOrDefault();
			//_db.Product.Remove(deleteProduct);
			//_db.SaveChanges();
			//_productViewModel.Products.Remove(deleteProduct);
			//MessageBox.Show("Удаление прошло удачно", "Сообщение", MessageBoxButton.OK);
		}
	}
}
