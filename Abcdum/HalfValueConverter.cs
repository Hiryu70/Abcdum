using System;
using System.Globalization;
using System.Windows.Data;

namespace Abcdum
{
	public class HalfValueConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (int.TryParse(value?.ToString(), out int result))
			{
				return result / 2 -25;
			}

			return 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

