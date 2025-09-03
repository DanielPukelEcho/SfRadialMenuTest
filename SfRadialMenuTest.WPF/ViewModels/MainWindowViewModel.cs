using CommunityToolkit.Mvvm.ComponentModel;
using SfRadialMenuTest.WPF.Models;
using SfRadialMenuTest.WPF.Stores;
using Syncfusion.SfSkinManager;

namespace SfRadialMenuTest.WPF.ViewModels
{
	public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly INavigationStore _navigationStore;
        private readonly IRadialNavigationItemsStore _radialNavigationItemsStore;
        public IEnumerable<RadialMenuItem> NavigationItems => _radialNavigationItemsStore.RadialMenuItems;
		public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
		public string Title => CurrentViewModel?.GetType().Name ?? "Header";

        [ObservableProperty]
		private bool _radialMenuIsOpen;
        public MainWindowViewModel(IRadialNavigationItemsStore radialNavigationItemsStore, INavigationStore navigationStore)
		{
			_radialNavigationItemsStore = radialNavigationItemsStore;
			_navigationStore = navigationStore;
			_navigationStore.CurrentViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
		}
	}
}
