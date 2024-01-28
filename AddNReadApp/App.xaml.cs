using AddNReadApp.Service;
using AddNReadApp.Service.CartProviders;
using AddNReadApp.Service.ProductProviders;
using AddNReadApp.Store;
using AddNReadApp.ViewModel;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace AddNReadApp
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly IHost _host;

		public App()
		{
			_host = Host.CreateDefaultBuilder()
				.ConfigureServices(service =>
				{
					service.AddSingleton<IProductProvider, ProductProvider>();
					service.AddSingleton<ICartProvider, CartProvider>();

					service.AddSingleton<Entities>();
					service.AddSingleton<NavigationStore>();
					service.AddSingleton<MainViewModel>();

					service.AddTransient((s) => CreateProductViewModel(s));
					service.AddSingleton<Func<ProductViewModel>>(s => () => s.GetRequiredService<ProductViewModel>());
					service.AddSingleton<NavigationService<ProductViewModel>>();

					service.AddSingleton<LoginUserViewModel>();
					service.AddSingleton<Func<LoginUserViewModel>>(s => () => s.GetRequiredService<LoginUserViewModel>());
					service.AddSingleton<NavigationService<LoginUserViewModel>>();

					service.AddSingleton((s) => CreateCartViewModel(s));
					service.AddSingleton<Func<CartViewModel>>(s => () => s.GetRequiredService<CartViewModel>());
					service.AddSingleton<NavigationService<CartViewModel>>();

					service.AddSingleton<NavigationService<ProductViewModel>>();
					service.AddSingleton<NavigationService<LoginUserViewModel>>();
					service.AddSingleton<NavigationService<CartViewModel>>();

					service.AddSingleton<MainWindow>(s => new MainWindow()
					{
						DataContext = s.GetRequiredService<MainViewModel>()
					});
				})
				.Build();
		}

		private ProductViewModel CreateProductViewModel(IServiceProvider s)
		{
			return ProductViewModel.LoadViewModel(s.GetRequiredService<Entities>(),
				s.GetRequiredService<IProductProvider>(), 
				s.GetRequiredService<ICartProvider>());
		}

		private CartViewModel CreateCartViewModel(IServiceProvider s)
		{
			return CartViewModel.LoadViewModel(s.GetRequiredService<ProductViewModel>(),
				s.GetRequiredService<ICartProvider>());
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			_host.Start();

			NavigationService<ProductViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ProductViewModel>>();
			navigationService.Navigate();

			MainWindow = _host.Services.GetRequiredService<MainWindow>();
			MainWindow.Show();

			base.OnStartup(e);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			_host.Dispose();

			base.OnExit(e);
		}
	}
}
