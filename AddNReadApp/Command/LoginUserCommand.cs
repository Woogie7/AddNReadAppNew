using AddNReadApp.Core;
using AddNReadApp.Service;
using AddNReadApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp.Command
{
	internal class LoginUserCommand : BaseCommand
	{
		private readonly NavigationService<ProductViewModel> _navigationService;
		public LoginUserCommand(NavigationService<ProductViewModel> navigationService)
		{
			_navigationService = navigationService;
		}


		public override void Execute(object parameter)
		{
			_navigationService.Navigate();
		}
	}
}
