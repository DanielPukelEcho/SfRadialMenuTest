using SfRadialMenuTest.WPF.Stores;
using Syncfusion.SfSkinManager;

namespace SfRadialMenuTest.WPF.Commands
{
	public sealed class ChangeThemeCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{
			if (parameter is VisualStyles visualStyle)
			{
				ThemeStore.ChangeTheme(visualStyle);
			}
		}
	}
}
