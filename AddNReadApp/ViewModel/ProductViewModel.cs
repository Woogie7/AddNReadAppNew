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
		public ObservableCollection<Product> Products { get; private set; }
		public ObservableCollection<Product> cartProduct { get; private set; }

		public ICommand DeleteCommand { get;}
		public ICommand EditCommand { get;}
		public ICommand AddProductCartCommand { get;}
		public ICommand LoadProductCommandAsync { get;}

		public ProductViewModel(Entities DB, IProductProvider productProvider)
		{
			_db = DB;
			_productProvider = productProvider;

			DeleteCommand = new DeleteProductCommand(this, DB);
			LoadProductCommandAsync = new LoadProductCommandAsync(this);
			AddProductCartCommand = new AddProductCartCommand(this);

			cartProduct = new ObservableCollection<Product>();
            Products = new ObservableCollection<Product>(_db.Product.ToList());
		}

		public static ProductViewModel LoadViewModel(Entities DB, IProductProvider productProvider)
		{
			ProductViewModel productViewModel = new ProductViewModel(DB, productProvider);

			productViewModel.LoadProductCommandAsync.Execute(null);

			return productViewModel;
		}

		public void UpdateProductLoad(ObservableCollection<Product> products)
		{
			Products = new ObservableCollection<Product>(_db.Product.ToList());
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

        private int countCart;

        public int CountCart
        {
            get { return countCart; }
            set 
			{ 
				countCart = value;
				OnPropertyChanged();
			}
        }

    }
}
