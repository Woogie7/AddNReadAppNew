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

		public App()
		{
			_db = new Entities();
			_navigationStore = new NavigationStore();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			MainWindow = new MainWindow()
			{
				DataContext = new MainViewModel(_navigationStore)
			};

			MainWindow.Show();

			base.OnStartup(e);
		}
	}
}
