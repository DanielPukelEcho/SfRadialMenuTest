using Syncfusion.SfSkinManager;

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
		}
		private static void OnThemeChanged()
		{
			ThemeChanged?.Invoke();
		}
	}
}
