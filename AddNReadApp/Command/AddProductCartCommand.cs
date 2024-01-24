using AddNReadApp.Core;
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
        private readonly ProductViewModel _productViewModel;
        public AddProductCartCommand(ProductViewModel ProductViewModel)
        {
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
            Product addCartProduct  = _productViewModel.SelectedProduct as Product;

            _productViewModel.cartProduct.Add(addCartProduct);

            _productViewModel.CountCart++;

            MessageBox.Show($"Успешно добавлено {addCartProduct.Name}", "sad", MessageBoxButton.YesNo);

            MessageBoxResult messageBoxResult = MessageBox.Show($"Успешно добавлено {addCartProduct.Name}", "sad", MessageBoxButton.YesNo);

            if(messageBoxResult == MessageBoxResult.Yes)
            {

            }
        }
    }
}
