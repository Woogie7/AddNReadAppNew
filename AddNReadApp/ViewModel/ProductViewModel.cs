using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AddNReadApp.Command;
using AddNReadApp.Core;

namespace AddNReadApp.ViewModel
{
	internal class ProductViewModel : ObservaleObject
	{
		private readonly Entities _db;

		public ObservableCollection<Product> Products {  get; private set; }


		public ICommand DeleteCommand { get;}
		public ProductViewModel(Entities DB)
		{
			_db = DB;
			LoadProducts();
			DeleteCommand = new DeleteProductCommand(this, DB);
		}

		private void LoadProducts()
		{
			Products = new ObservableCollection<Product>(_db.Product.ToList());
		}

		private Product _selectedProduct;

		public Product SelectedProduct
		{
			get { return _selectedProduct; }
			set 
			{ 
				_selectedProduct = value;
				OnPropertyChanged();
			}
		}

	}
}
