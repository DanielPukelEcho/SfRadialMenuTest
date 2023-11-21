using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SfRadialMenuTest.WPF.ViewModels;
using Microsoft.Extensions.Hosting;
using Syncfusion.Licensing;
using SfRadialMenuTest.WPF.Extensions.Extensions;
using SfRadialMenuTest.WPF.Services;

namespace SfRadialMenuTest.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public static IHost? AppHost { get; private set; }
	public App()
	{
		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices((context, services) =>
			{
				services.AddSingleton<MainWindowViewModel>();
				services.AddSingleton((services) => new MainWindow()
				{
					DataContext = services.GetRequiredService<MainWindowViewModel>()
				});
				services.AddViewModels();
				services.AddNavigationComponents();
			})
			.Build();
	}
	protected override async void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		await AppHost!.StartAsync();
		var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
		MainWindow = mainWindow;
		mainWindow.Show();
	}
	protected override async void OnExit(ExitEventArgs e)
	{
		await AppHost!.StopAsync();
		AppHost.Dispose();
		base.OnExit(e);
	}
}

