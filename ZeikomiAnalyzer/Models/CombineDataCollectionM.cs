using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeikomiAnalyzer.Models
{
    public class CombineDataCollectionM : ModelBase
	{
		#region 結合データリスト[CombineDataList]プロパティ
		/// <summary>
		/// 結合データリスト[CombineDataList]プロパティ用変数
		/// </summary>
		ModelList<CombineDataM> _CombineDataList = new ModelList<CombineDataM>();
        /// <summary>
        /// 結合データリスト[CombineDataList]プロパティ
        /// </summary>
        public ModelList<CombineDataM> CombineDataList
		{
			get
			{
				return _CombineDataList;
			}
			set
			{
				if (_CombineDataList == null || !_CombineDataList.Equals(value))
				{
					_CombineDataList = value;
					NotifyPropertyChanged("CombineDataList");
				}
			}
		}
		#endregion

		#region 追加処理
		/// <summary>
		/// 追加処理
		/// </summary>
		/// <param name="wp">WordPressの記事</param>
		/// <param name="google_analytics">Google Analyticsデータ</param>
		public void Add(ArticleM wp, GoogleAnalyticsM google_analytics)
		{
			var data = new CombineDataM();
			data.WordPress = wp;
			data.Analytics = google_analytics;
			this.CombineDataList.Items.Add(data);
		}
		#endregion

		#region クリア処理
		/// <summary>
		/// クリア処理
		/// </summary>
		public void Clear()
		{
			this.CombineDataList.Items.Clear();
		}
		#endregion
	}
}
