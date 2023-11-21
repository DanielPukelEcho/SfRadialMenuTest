using SfRadialMenuTest.WPF.Factories;
using SfRadialMenuTest.WPF.Stores;
using SfRadialMenuTest.WPF.ViewModels;

namespace SfRadialMenuTest.WPF.Services
{
	public interface INavigationService
	{
		void Navigate(CancellationToken ct = default);
	}
	public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
	{
		private readonly INavigationStore _navigationStore;
		private readonly IAbstractFactory<TViewModel> _viewModelFactory;

		public NavigationService(INavigationStore navigationStore, IAbstractFactory<TViewModel> viewModelFactory)
		{
			_navigationStore = navigationStore;
			_viewModelFactory = viewModelFactory;
		}

		public void Navigate(CancellationToken ct = default)
		{
			_navigationStore.CurrentViewModel = _viewModelFactory.Create();
		}
	}
}
