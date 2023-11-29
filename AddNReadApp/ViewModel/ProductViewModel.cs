using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AddNReadApp.Command;
using AddNReadApp.Core;
using AddNReadApp.Service;
using AddNReadApp.Service.ProductProviders;
using AddNReadApp.Store;

namespace AddNReadApp.ViewModel
{
	internal class ProductViewModel : ObservaleObject
	{
		private readonly Entities _db;
		private readonly IProductProvider _productProvider;

		public ObservableCollection<Product> Products {  get; private set; }

		public ICommand DeleteCommand { get;}
		public ICommand EditCommand { get;}
		public ProductViewModel(Entities DB, NavigationService navigationService, IProductProvider productProvider)
		{
			_db = DB;
			_productProvider = productProvider;
			LoadProducts();
			DeleteCommand = new DeleteProductCommand(this, DB);

			EditCommand = new NavigateCommand(navigationService);
		}

		private void LoadProducts()
		{
			_productProvider.GetProductsAsync();
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
