using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using SfRadialMenuTest.WPF.Commands;
using SfRadialMenuTest.WPF.Factories;
using SfRadialMenuTest.WPF.Models;
using SfRadialMenuTest.WPF.Services;
using SfRadialMenuTest.WPF.Stores;
using SfRadialMenuTest.WPF.ViewModels;
using Syncfusion.SfSkinManager;

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
						.AddChild(CreateItem("Themes", "/Resources/Images/Theme-icon.png")
							.AddChild(CreateItem("Light", "/Resources/Images/Theme-iconLight.png", new ChangeThemeCommand(), VisualStyles.Windows11Light))
                            .AddChild(CreateItem("FluentDark", "/Resources/Images/Theme-iconLight.png", new ChangeThemeCommand(), VisualStyles.FluentDark))
                            .AddChild(CreateItem("Dark", "/Resources/Images/Theme-iconDark.png", new ChangeThemeCommand(), VisualStyles.Windows11Dark)))
						.AddChild(CreateItem("Things", "/Resources/Images/information.png", new NavigateCommand(s.GetRequiredService<NavigationService<InfoViewModel>>()))));
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
			var stackPanel = new StackPanel();

			

			var imageSource = new BitmapImage();
			imageSource.BeginInit();
			imageSource.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
			imageSource.EndInit();
			var image = new Image()
			{
				Width = 64,
				Height = 64,
				Source = imageSource,
			};
			image.Source = imageSource;

			var textBlock = new TextBlock()
			{
				Text = string.IsNullOrEmpty(description) ? "Home" : description, // Use "Home" as fallback value
				HorizontalAlignment = HorizontalAlignment.Center,
			};

			stackPanel.Children.Add(image);
			stackPanel.Children.Add(textBlock);

			//var brush = Brushes.DarkGreen;
			var menuItem = new RadialMenuItem()
			{
				Header = stackPanel,
				FontSize = 18,
				ShowMouseOverStyle = true,
				CloseOnExecute = true,
				Command = command,
				CommandParameter = commandParameter,
				//RimActiveBrush = brush,
				//RimInactiveBrush = brush,
				Cursor = Cursors.Hand,
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
