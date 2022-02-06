using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeikomiAnalyzer.Common.Converters
{
	[System.Windows.Data.ValueConversion(typeof(bool), typeof(string))]
	public class boolToMaruBatsuConverter : System.Windows.Data.IValueConverter
	{

		#region IValueConverter メンバ
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var target = (bool)value;
			// ここに処理を記述する
			return target ? "〇" : "×";
		}

		// TwoWayの場合に使用する
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion
	}

}
