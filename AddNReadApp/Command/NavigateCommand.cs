using AddNReadApp.Core;
using AddNReadApp.Service;

namespace AddNReadApp.Command
{
	internal class NavigateCommand : BaseCommand
	{
		private readonly NavigationService _navigationService;
		public NavigateCommand(NavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public override void Execute(object parameter)
		{
			_navigationService.Navigate();
		}
	}
}
