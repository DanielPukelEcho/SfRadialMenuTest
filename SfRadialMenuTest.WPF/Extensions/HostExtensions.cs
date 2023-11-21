using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SfRadialMenuTest.WPF.Commands;
using SfRadialMenuTest.WPF.Factories;
using SfRadialMenuTest.WPF.Models;
using SfRadialMenuTest.WPF.Services;
using SfRadialMenuTest.WPF.Stores;
using SfRadialMenuTest.WPF.ViewModels;

namespace SfRadialMenuTest.WPF.Extensions.Extensions
{
	public static class HostExtensions
	{
		public static void AddViewModels(this IServiceCollection services)
		{
			services.AddNavigationType<INavigationStore, InfoViewModel>();
			services.AddNavigationType<INavigationStore, SettingsViewModel>();
			services.AddNavigationType<INavigationStore, ChartViewModel>();
		}
			public static void AddNavigationComponents(this IServiceCollection services)
		{
			services.AddSingleton<INavigationStore, NavigationStore>();
			services.AddSingleton<IRadialNavigationItemsStore>((s) =>
			{
				return new RadialNavigationItemsStore()
					.AddRadialItem(CreateItem("Info", "/Resources/Images/information.png", new NavigateCommand(s.GetRequiredService<NavigationService<InfoViewModel>>())))
					.AddRadialItem(CreateItem("Settings", "/Resources/Images/settings.png", new NavigateCommand(s.GetRequiredService<NavigationService<SettingsViewModel>>())))
					.AddRadialItem(CreateItem("Chart", "/Resources/Images/bar-chart.png", new NavigateCommand(s.GetRequiredService<NavigationService<ChartViewModel>>())))
					.AddRadialItem(CreateItem("Sub Menu", "/Resources/Images/folder.png")
						.AddChild(CreateItem("Things", "/Resources/Images/internet.png", new NavigateCommand(s.GetRequiredService<NavigationService<InfoViewModel>>())))
						.AddChild(CreateItem("Other Things", "/Resources/Images/information.png", new NavigateCommand(s.GetRequiredService<NavigationService<InfoViewModel>>()))));
			});

		}
		public static RadialMenuItem AddChild(this RadialMenuItem parent, RadialMenuItem child)
		{
			parent.Items.Add(child);
			return parent;
		}
		public static IRadialNavigationItemsStore AddRadialItem(this IRadialNavigationItemsStore store, RadialMenuItem item)
		{
			store.Add(item);
			return store;
		}
		public static RadialMenuItem CreateItem(string description, string imagePath, ICommand? command = default, object? commandParameter = default)
		{
			var menuItem = new RadialMenuItem()
			{
				Name = description,
				ImagePath = imagePath,
				Command = command,
				CommandParameter = commandParameter
			};
			return menuItem;
		}
		public static void AddNavigationType<TStore, TViewModel>(this IServiceCollection services, bool singleton = false)
			where TViewModel : ViewModelBase
			where TStore : INavigationStore
		{
			if (singleton)
			{
				services.AddSingleton<TViewModel>();
			}
			else
			{
				services.AddTransient<TViewModel>();
			}
			services.AddSingleton<Func<TViewModel>>(s => () => s.GetRequiredService<TViewModel>());
			services.AddSingleton<IAbstractFactory<TViewModel>, AbstractFactory<TViewModel>>();
			services.AddSingleton((s) =>
			{
				return new NavigationService<TViewModel>(s.GetRequiredService<TStore>(), s.GetRequiredService<IAbstractFactory<TViewModel>>());
			});
		}
	}
}
