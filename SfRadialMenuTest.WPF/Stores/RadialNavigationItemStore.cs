using System.Collections.ObjectModel;
using SfRadialMenuTest.WPF.Models;

namespace SfRadialMenuTest.WPF.Stores
{
	public interface IRadialNavigationItemsStore
	{
		ObservableCollection<RadialMenuItem> RadialMenuItems { get; }

		void Add(RadialMenuItem sfRadialMenuItem);
	}

	public sealed class RadialNavigationItemsStore : IRadialNavigationItemsStore
	{
		private readonly ObservableCollection<RadialMenuItem> _radialMenuItems = new();
		public ObservableCollection<RadialMenuItem> RadialMenuItems => _radialMenuItems;
		public void Add(RadialMenuItem radialMenuItem)
		{
			_radialMenuItems.Add(radialMenuItem);
		}
	}
}
