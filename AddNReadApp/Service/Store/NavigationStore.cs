using AddNReadApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Store
{
	internal class NavigationStore : ObservaleObject
	{
		private ObservaleObject _currentViewModel;

		public ObservaleObject CurrentViewModel 
		{
			get { return _currentViewModel; }
			set 
			{ 
				_currentViewModel = value;
				OnCurrentViewModelChanged();
			}
		}

		private void OnCurrentViewModelChanged()
		{
			CurrentViewModelChanged?.Invoke();
		}

		public event Action CurrentViewModelChanged;
	}
}
