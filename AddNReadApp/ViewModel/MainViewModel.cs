using AddNReadApp.Command;
using AddNReadApp.Core;
using AddNReadApp.Service;
using AddNReadApp.Store;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace AddNReadApp.ViewModel
{
	internal class MainViewModel : ObservaleObject
	{
		private readonly NavigationStore _navigationStore;
		public ObservaleObject CurrentViewModel => _navigationStore.CurrentViewModel;

		public ICommand ProductNavigateCommand { get; }
		public ICommand LoginNavigateCommand { get; }

		public MainViewModel(NavigationStore navigationStore, NavigationService<LoginUserViewModel> loginUserNavigationService, NavigationService<ProductViewModel> productNavigationService) 
		{
			_navigationStore = navigationStore;

			LoginNavigateCommand = new NavigateCommand<LoginUserViewModel>(loginUserNavigationService);
			ProductNavigateCommand = new NavigateCommand<ProductViewModel>(productNavigationService);

			_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
		}

		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}
}
