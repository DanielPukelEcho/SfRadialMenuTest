using Syncfusion.SfSkinManager;
using Syncfusion.Themes.FluentDark.WPF;
using Syncfusion.Themes.Windows11Dark.WPF;
using Syncfusion.Themes.Windows11Light.WPF;
using System.Windows;
using System.Windows.Media;

namespace SfRadialMenuTest.WPF.Stores
{
	public sealed class ThemeStore
	{
		private static readonly Lazy<ThemeStore> _instance = new(() => new ThemeStore());
		public static ThemeStore Instance => _instance.Value;

		public static event Action? ThemeChanged;

		public static void RegisterThemeStore()
		{
            var greenBrush = new SolidColorBrush(Color.FromArgb(255, 168, 207, 69));
            var fontSize = 18.0;

            SfSkinManager.RegisterThemeSettings(nameof(VisualStyles.Windows11Light), new Windows11LightThemeSettings()
            {
                PrimaryBackground = greenBrush,
                HeaderFontSize = fontSize,
                SubHeaderFontSize = fontSize,
                SubTitleFontSize = fontSize,
                TitleFontSize = fontSize,
                BodyAltFontSize = fontSize,
                BodyFontSize = fontSize,
            });

            SfSkinManager.RegisterThemeSettings(nameof(VisualStyles.Windows11Dark), new Windows11DarkThemeSettings()
            {
                PrimaryBackground = greenBrush,
                HeaderFontSize = fontSize,
                SubHeaderFontSize = fontSize,
                SubTitleFontSize = fontSize,
                TitleFontSize = fontSize,
                BodyAltFontSize = fontSize,
                BodyFontSize = fontSize,
            });


            SfSkinManager.RegisterThemeSettings(nameof(VisualStyles.FluentDark), new FluentDarkThemeSettings()
            {
                PrimaryBackground = greenBrush,
                HeaderFontSize = fontSize,
                SubHeaderFontSize = fontSize,
                SubTitleFontSize = fontSize,
                TitleFontSize = fontSize,
                BodyAltFontSize = fontSize,
                BodyFontSize = fontSize,
            });
        }


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
					if(theme == VisualStyles.FluentDark)
					{
                        SfSkinManager.SetTheme(window, new FluentTheme() { ThemeName = "FluentDark"});
                    }
					else
					{

                        SfSkinManager.SetTheme(window, new Theme(theme.ToString()));
                    }

                }

            });
            
        }
		private static void OnThemeChanged()
		{
			ThemeChanged?.Invoke();
		}
	}
}
