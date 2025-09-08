using CommunityToolkit.Mvvm.Input;
using SfRadialMenuTest.WPF.Stores;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SfRadialMenuTest.WPF.ViewModels
{
	public partial class InfoViewModel : ViewModelBase
	{




		[RelayCommand]
		private async Task ShowInfo()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				var window = new ChromelessWindow();
				window.Title = "Info";
				window.Content = new TextBlock()
				{
					Text = "Just sample dialog to show the current theme."
				};

                SfSkinManager.ApplyThemeAsDefaultStyle = true;
                SfSkinManager.SetTheme(window, new Theme(ThemeStore.Instance.CurrentTheme.ToString()));

                window.ShowDialog();
            });
        }
	}
}
