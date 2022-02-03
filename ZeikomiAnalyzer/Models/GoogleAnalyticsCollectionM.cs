using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeikomiAnalyzer.Models
{
    public class GoogleAnalyticsCollectionM : ModelBase
    {
		#region Google Analyticsの分析結果データ[GoogleAnalyticsItems]プロパティ
		/// <summary>
		/// Google Analyticsの分析結果データ[GoogleAnalyticsItems]プロパティ用変数
		/// </summary>
		ModelList<GoogleAnalyticsM> _GoogleAnalyticsItems = new ModelList<GoogleAnalyticsM>();
		/// <summary>
		/// Google Analyticsの分析結果データ[GoogleAnalyticsItems]プロパティ
		/// </summary>
		public ModelList<GoogleAnalyticsM> GoogleAnalyticsItems
		{
			get
			{
				return _GoogleAnalyticsItems;
			}
			set
			{
				if (_GoogleAnalyticsItems == null || !_GoogleAnalyticsItems.Equals(value))
				{
					_GoogleAnalyticsItems = value;
					NotifyPropertyChanged("GoogleAnalyticsItems");
				}
			}
		}
		#endregion

		#region クリア処理
		/// <summary>
		/// クリア処理
		/// </summary>
		public void Clear()
        {
			this.GoogleAnalyticsItems.Items.Clear();
        }
		#endregion
	}
}
