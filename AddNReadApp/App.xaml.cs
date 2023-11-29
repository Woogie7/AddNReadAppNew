using AddNReadApp.Service;
using AddNReadApp.Service.ProductProviders;
using AddNReadApp.Store;
using AddNReadApp.View;
using AddNReadApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AddNReadApp
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly Entities _db; 
		private readonly NavigationStore _navigationStore;
		IProductProvider productProvider;

		public App()
		{
			_db = new Entities();
			_navigationStore = new NavigationStore();

			productProvider = new ProductProvider(_db);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			_navigationStore.CurrentViewModel = createUserViewModel();
			MainWindow = new MainWindow()
			{
				DataContext = new MainViewModel(_navigationStore, new NavigationService(_navigationStore, createUserViewModel), 
								new NavigationService(_navigationStore, createProductViewModel))
			};

			MainWindow.Show();

			base.OnStartup(e);
		}

		private LoginUserViewModel createUserViewModel()
		{
			return new LoginUserViewModel(new NavigationService(_navigationStore, createProductViewModel));
		}

		private ProductViewModel createProductViewModel()
		{
			return ProductViewModel.LoadViewModel(_db, new NavigationService(_navigationStore, createUserViewModel), productProvider);
		}
	}
}
