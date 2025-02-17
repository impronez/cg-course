using AlchemyGame.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AlchemyGame.Utilities
{
	public class ElementToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ElementType elementType)
			{
				return ElementImages.GetImagePath(elementType);
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
