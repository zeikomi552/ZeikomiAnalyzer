using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeikomiAnalyzer.Common.Converters
{
	[System.Windows.Data.ValueConversion(typeof(CoreTweet.Status), typeof(string))]
	public class CoreTweetStatusToUrlstringConverter : System.Windows.Data.IValueConverter
	{

		#region IValueConverter メンバ
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var target = (CoreTweet.Status)value;

			if (target != null && target.Entities != null && target.Entities.Urls.Count() > 0)
			{
				var url = target.Entities.Urls.ElementAt(0);

				return url.ExpandedUrl;

			}
			// ここに処理を記述する
			return string.Empty;
		}

		// TwoWayの場合に使用する
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion
	}

}
