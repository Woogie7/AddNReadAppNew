using AddNReadApp.Core;
using AddNReadApp.Service;

namespace AddNReadApp.Command
{
	internal class NavigateCommand<TViewModel> : BaseCommand where TViewModel : ObservaleObject
	{
		private readonly NavigationService<TViewModel> _navigationService;
		public NavigateCommand(NavigationService<TViewModel> navigationService)
		{
			_navigationService = navigationService;
		}

		public override void Execute(object parameter)
		{
			_navigationService.Navigate();
		}
	}
}
