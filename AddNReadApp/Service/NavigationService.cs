using AddNReadApp.Core;
using AddNReadApp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Service
{
	internal class NavigationService<TViewModel> where TViewModel : ObservaleObject
	{
		private readonly NavigationStore _navigationStore;
		private readonly Func<TViewModel> _createViewModel;

		public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
		{
			_navigationStore = navigationStore;
			_createViewModel = createViewModel;
		}
		public void Navigate()
		{
			_navigationStore.CurrentViewModel = _createViewModel();
		}
	}
}
