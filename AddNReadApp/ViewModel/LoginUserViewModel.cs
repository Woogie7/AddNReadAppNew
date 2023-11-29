using AddNReadApp.Command;
using AddNReadApp.Core;
using AddNReadApp.Service;
using AddNReadApp.Store;
using System;
using System.Windows.Input;

namespace AddNReadApp.ViewModel
{
	internal class LoginUserViewModel : ObservaleObject
	{
		public ICommand LoginNavigateCommand { get; }

		public LoginUserViewModel(NavigationService navigationService)
		{
			LoginNavigateCommand = new NavigateCommand(navigationService);
		}
	}
}
