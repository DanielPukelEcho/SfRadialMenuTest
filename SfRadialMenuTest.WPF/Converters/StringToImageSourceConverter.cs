using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SfRadialMenuTest.WPF.Converters
{
	public sealed class StringToImageSourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var path = value as string;
			if (path is not null)
			{
				return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
			}
			return Binding.DoNothing;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
