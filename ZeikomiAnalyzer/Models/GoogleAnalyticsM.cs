using ClosedXML.Excel;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeikomiAnalyzer.Models
{
    public class GoogleAnalyticsM : ModelBase
	{
		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GoogleAnalyticsM()
		{

		}
		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="row">行</param>
		/// <param name="col_list">カラムリスト</param>
		public GoogleAnalyticsM(IXLRow row, List<string> col_list)
		{
			SetValue(row, col_list);
		}
        #endregion

        #region [Page]プロパティ
        /// <summary>
        /// [Page]プロパティ用変数
        /// </summary>
        string _Page = string.Empty;
		/// <summary>
		/// [Page]プロパティ
		/// </summary>
		public string Page
		{
			get
			{
				return _Page;
			}
			set
			{
				if (_Page == null || !_Page.Equals(value))
				{
					_Page = value;
					NotifyPropertyChanged("Page");
				}
			}
		}
		#endregion


		#region ページビュー数[PageView]プロパティ
		/// <summary>
		/// ページビュー数[PageView]プロパティ用変数
		/// </summary>
		int _PageView = 0;
		/// <summary>
		/// ページビュー数[PageView]プロパティ
		/// </summary>
		public int PageView
		{
			get
			{
				return _PageView;
			}
			set
			{
				if (!_PageView.Equals(value))
				{
					_PageView = value;
					NotifyPropertyChanged("PageView");
				}
			}
		}
		#endregion

		#region ページ別訪問数[UniquePageView]プロパティ
		/// <summary>
		/// ページ別訪問数[UniquePageView]プロパティ用変数
		/// </summary>
		int _UniquePageView = 0;
		/// <summary>
		/// ページ別訪問数[UniquePageView]プロパティ
		/// </summary>
		public int UniquePageView
		{
			get
			{
				return _UniquePageView;
			}
			set
			{
				if (!_UniquePageView.Equals(value))
				{
					_UniquePageView = value;
					NotifyPropertyChanged("UniquePageView");
				}
			}
		}
		#endregion

		#region 平均ページ滞在時間[StayTime]プロパティ
		/// <summary>
		/// 平均ページ滞在時間[StayTime]プロパティ用変数
		/// </summary>
		double _StayTime = 0.0;
		/// <summary>
		/// 平均ページ滞在時間[StayTime]プロパティ
		/// </summary>
		public double StayTime
		{
			get
			{
				return _StayTime;
			}
			set
			{
				if (!_StayTime.Equals(value))
				{
					_StayTime = value;
					NotifyPropertyChanged("StayTime");
				}
			}
		}
		#endregion

		#region 閲覧開始数[PageViewStart]プロパティ
		/// <summary>
		/// 閲覧開始数[PageViewStart]プロパティ用変数
		/// </summary>
		int _PageViewStart = 0;
		/// <summary>
		/// 閲覧開始数[PageViewStart]プロパティ
		/// </summary>
		public int PageViewStart
		{
			get
			{
				return _PageViewStart;
			}
			set
			{
				if (!_PageViewStart.Equals(value))
				{
					_PageViewStart = value;
					NotifyPropertyChanged("PageViewStart");
				}
			}
		}
		#endregion

		#region 直帰率[ReturnRatio]プロパティ
		/// <summary>
		/// 直帰率[ReturnRatio]プロパティ用変数
		/// </summary>
		double _ReturnRatio = 0.0;
		/// <summary>
		/// 直帰率[ReturnRatio]プロパティ
		/// </summary>
		public double ReturnRatio
		{
			get
			{
				return _ReturnRatio;
			}
			set
			{
				if (!_ReturnRatio.Equals(value))
				{
					_ReturnRatio = value;
					NotifyPropertyChanged("ReturnRatio");
				}
			}
		}
		#endregion

		#region 離脱率[LeaveRatio]プロパティ
		/// <summary>
		/// 離脱率[LeaveRatio]プロパティ用変数
		/// </summary>
		double _LeaveRatio = 0.0;
		/// <summary>
		/// 離脱率[LeaveRatio]プロパティ
		/// </summary>
		public double LeaveRatio
		{
			get
			{
				return _LeaveRatio;
			}
			set
			{
				if (!_LeaveRatio.Equals(value))
				{
					_LeaveRatio = value;
					NotifyPropertyChanged("LeaveRatio");
				}
			}
		}
		#endregion

		#region ページの価値[PageValue]プロパティ
		/// <summary>
		/// ページの価値[PageValue]プロパティ用変数
		/// </summary>
		double _PageValue = 0.0;
		/// <summary>
		/// ページの価値[PageValue]プロパティ
		/// </summary>
		public double PageValue
		{
			get
			{
				return _PageValue;
			}
			set
			{
				if (!_PageValue.Equals(value))
				{
					_PageValue = value;
					NotifyPropertyChanged("PageValue");
				}
			}
		}
		#endregion

		#region 値の設定処理
		/// <summary>
		/// 値の設定処理
		/// </summary>
		/// <param name="row">Excelの行データ</param>
		/// <param name="col_list">カラムリスト</param>
		public void SetValue(IXLRow row, List<string> col_list)
		{
			int index = col_list.IndexOf("ページ");

			if (index >= 0)
			{
				this.Page = row.Cell(index + 1).Value == null ? string.Empty : row.Cell(index + 1).Value.ToString();
			}

			index = col_list.IndexOf("ページビュー数");

			if (index >= 0)
			{
				this._PageView = row.Cell(index + 1).Value == null ? 0 : int.Parse(row.Cell(index + 1).Value.ToString());
			}

			index = col_list.IndexOf("ページ別訪問数");

			if (index >= 0)
			{
				this.UniquePageView = row.Cell(index + 1).Value == null ? 0 : int.Parse(row.Cell(index + 1).Value.ToString());
			}

			index = col_list.IndexOf("平均ページ滞在時間");

			if (index >= 0)
			{
				this.StayTime = row.Cell(index + 1).Value == null ? 0 : double.Parse(row.Cell(index + 1).Value.ToString());
			}

			index = col_list.IndexOf("閲覧開始数");

			if (index >= 0)
			{
				this.PageViewStart = row.Cell(index + 1).Value == null ? 0 : int.Parse(row.Cell(index + 1).Value.ToString());
			}
			index = col_list.IndexOf("直帰率");

			if (index >= 0)
			{
				this.ReturnRatio = row.Cell(index + 1).Value == null ? 0 : double.Parse(row.Cell(index + 1).Value.ToString());
			}
			index = col_list.IndexOf("離脱率");

			if (index >= 0)
			{
				this.LeaveRatio = row.Cell(index + 1).Value == null ? 0 : double.Parse(row.Cell(index + 1).Value.ToString());
			}
			index = col_list.IndexOf("ページの価値");

			if (index >= 0)
			{
				this.PageValue = row.Cell(index + 1).Value == null ? 0 : double.Parse(row.Cell(index + 1).Value.ToString());
			}
		}
		#endregion
	}
}
