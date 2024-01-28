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
    internal class AddProductCartCommand : BaseCommand
    {
        private readonly ICartProvider _cartProvider;
        private readonly ProductViewModel _productViewModel;
        private readonly Entities _db;
        public AddProductCartCommand(ProductViewModel ProductViewModel, ICartProvider cartProvider)
        {
            _cartProvider = cartProvider;
            _productViewModel = ProductViewModel;
            _productViewModel.PropertyChanged += OnProductViewModelPropertyChanged;
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
            Product product = _productViewModel.SelectedProduct as Product;

            if(!_cartProvider.IsBe(product))
            {
				Cart addCartProduct = new Cart()
				{
					IDProduct = product.IDProduct,
					Quantity = 1,
					Product = product,
					DateAdded = DateTime.Now,
					AdditionalInfo = ""
				};

				//_productViewModel.SelectedProduct as Product;

				_cartProvider.AddToCartAsync(addCartProduct);

				MessageBox.Show($"Успешно добавлено {product.Name}");

			}
            else
            {
				MessageBox.Show($"Количество было изменино");
			}

			

        }
    }
}
