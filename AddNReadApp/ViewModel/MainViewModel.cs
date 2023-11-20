using AddNReadApp.Core;
using AddNReadApp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.ViewModel
{
	internal class MainViewModel : ObservaleObject
	{
		private readonly NavigationStore _navigationStore;
		public ObservaleObject CurrentViewModel => _navigationStore.CurrentViewModel;

		public MainViewModel(NavigationStore navigationStore) 
		{
			_navigationStore = navigationStore;
		}
	}
}
