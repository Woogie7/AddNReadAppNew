using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AddNReadApp.Command;
using AddNReadApp.Command.AsyncCommand;
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

		public IEnumerable<Product> Products {  get; private set; }

		public ICommand DeleteCommand { get;}
		public ICommand EditCommand { get;}
		public ICommand LoadProductCommandAsync { get;}
		public ProductViewModel(Entities DB, NavigationService navigationService, IProductProvider productProvider)
		{
			_db = DB;
			_productProvider = productProvider;

			DeleteCommand = new DeleteProductCommand(this, DB);
			EditCommand = new NavigateCommand(navigationService);
			LoadProductCommandAsync = new LoadProductCommandAsync(this);
		}

		public static ProductViewModel LoadViewModel(Entities DB, NavigationService navigationService, IProductProvider productProvider)
		{
			ProductViewModel productViewModel = new ProductViewModel(DB, navigationService, productProvider);

			productViewModel.LoadProductCommandAsync.Execute(null);

			return productViewModel;
		}

		public void UpdateProductLoad(IEnumerable<Product> products)
		{
			Products = products;
		}


		public async Task<IEnumerable<Product>> GetAllProduct()
		{
			return await _productProvider.GetProductsAsync();
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
