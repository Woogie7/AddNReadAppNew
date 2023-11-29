using AddNReadApp.Core;
using AddNReadApp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Service
{
	internal class NavigationService
	{
		private readonly NavigationStore _navigationStore;
		private readonly Func<ObservaleObject> _createViewModel;

		public NavigationService(NavigationStore navigationStore, Func<ObservaleObject> createViewModel)
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
