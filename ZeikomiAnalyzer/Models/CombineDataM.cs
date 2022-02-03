using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeikomiAnalyzer.Models
{
    public class CombineDataM : ModelBase
	{
		#region ワードプレスデータ[WordPress]プロパティ
		/// <summary>
		/// ワードプレスデータ[WordPress]プロパティ用変数
		/// </summary>
		ArticleM _WordPress = new ArticleM();
		/// <summary>
		/// ワードプレスデータ[WordPress]プロパティ
		/// </summary>
		public ArticleM WordPress
		{
			get
			{
				return _WordPress;
			}
			set
			{
				if (_WordPress == null || !_WordPress.Equals(value))
				{
					_WordPress = value;
					NotifyPropertyChanged("WordPress");
				}
			}
		}
		#endregion

		#region GoogleAnalyticsデータ[Analytics]プロパティ
		/// <summary>
		/// GoogleAnalyticsデータ[Analytics]プロパティ用変数
		/// </summary>
		GoogleAnalyticsM _Analytics = new GoogleAnalyticsM();
		/// <summary>
		/// GoogleAnalyticsデータ[Analytics]プロパティ
		/// </summary>
		public GoogleAnalyticsM Analytics
		{
			get
			{
				return _Analytics;
			}
			set
			{
				if (_Analytics == null || !_Analytics.Equals(value))
				{
					_Analytics = value;
					NotifyPropertyChanged("Analytics");
				}
			}
		}
		#endregion


	}
}
