using Syncfusion.SfSkinManager;
using System.Windows;

namespace SfRadialMenuTest.WPF.Stores
{
	public sealed class ThemeStore
	{
		private static readonly Lazy<ThemeStore> _instance = new(() => new ThemeStore());
		public static ThemeStore Instance => _instance.Value;

		public static event Action? ThemeChanged;
		public VisualStyles CurrentTheme
		{
			get => _currentTheme;
			private set
			{
				if (_currentTheme != value)
				{
					_currentTheme = value;
					OnThemeChanged();
				}
			}
		}
		private VisualStyles _currentTheme = VisualStyles.Windows11Light;

		public static void ChangeTheme(VisualStyles theme)
		{
			Instance.CurrentTheme = theme;

			Application.Current.Dispatcher.Invoke(() =>
			{
                SfSkinManager.ApplyThemeAsDefaultStyle = true;
                foreach (Window window in Application.Current.Windows)
                {
                    SfSkinManager.SetTheme(window, new Theme(theme.ToString()));
                }

            });
            
        }
		private static void OnThemeChanged()
		{
			ThemeChanged?.Invoke();
		}
	}
}
