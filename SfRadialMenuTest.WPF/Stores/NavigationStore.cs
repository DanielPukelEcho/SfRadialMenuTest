using SfRadialMenuTest.WPF.ViewModels;

namespace SfRadialMenuTest.WPF.Stores
{
	public interface INavigationStore
	{
		public event Action? CurrentViewModelChanged;
		public ViewModelBase? CurrentViewModel { get; set; }
	}

	public sealed class NavigationStore : INavigationStore
	{
		public event Action? CurrentViewModelChanged;
		private ViewModelBase? _currentViewModel;

		public ViewModelBase? CurrentViewModel
		{
			get => _currentViewModel;
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
	}
}
