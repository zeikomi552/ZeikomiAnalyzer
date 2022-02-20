using ClosedXML.Excel;
using Google.Apis.AnalyticsReporting.v4.Data;
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
			//SetValue(row, col_list);
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
		#region ページタイトル[PageTitle]プロパティ
		/// <summary>
		/// ページタイトル[PageTitle]プロパティ用変数
		/// </summary>
		string _PageTitle = string.Empty;
		/// <summary>
		/// ページタイトル[PageTitle]プロパティ
		/// </summary>
		public string PageTitle
		{
			get
			{
				return _PageTitle;
			}
			set
			{
				if (_PageTitle == null || !_PageTitle.Equals(value))
				{
					_PageTitle = value;
					NotifyPropertyChanged("PageTitle");
				}
			}
		}
		#endregion

		#region ページビュー数[PageViews]プロパティ
		/// <summary>
		/// ページビュー数[PageViews]プロパティ用変数
		/// </summary>
		int _PageViews = 0;
		/// <summary>
		/// ページビュー数[PageViews]プロパティ
		/// </summary>
		public int PageViews
		{
			get
			{
				return _PageViews;
			}
			set
			{
				if (!_PageViews.Equals(value))
				{
					_PageViews = value;
					NotifyPropertyChanged("PageViews");
				}
			}
		}
		#endregion

		#region 閲覧開始数[Entrances]プロパティ
		/// <summary>
		/// 閲覧開始数[Entrances]プロパティ用変数
		/// </summary>
		int _Entrances = 0;
		/// <summary>
		/// 閲覧開始数[Entrances]プロパティ
		/// </summary>
		public int Entrances
		{
			get
			{
				return _Entrances;
			}
			set
			{
				if (!_Entrances.Equals(value))
				{
					_Entrances = value;
					NotifyPropertyChanged("Entrances");
				}
			}
		}
		#endregion

		#region 閲覧開始率(Entrances / Pageviews)[EntranceRate]プロパティ
		/// <summary>
		/// 閲覧開始率(Entrances / Pageviews)[EntranceRate]プロパティ用変数
		/// </summary>
		double _EntranceRate = 0.0;
		/// <summary>
		/// 閲覧開始率(Entrances / Pageviews)[EntranceRate]プロパティ
		/// </summary>
		public double EntranceRate
		{
			get
			{
				return _EntranceRate;
			}
			set
			{
				if (!_EntranceRate.Equals(value))
				{
					_EntranceRate = value;
					NotifyPropertyChanged("EntranceRate");
				}
			}
		}
		#endregion

		#region 一セッションあたりのページビュー数[PageViewsPerSession]プロパティ
		/// <summary>
		/// 一セッションあたりのページビュー数[PageViewsPerSession]プロパティ用変数
		/// </summary>
		double _PageViewsPerSession = 0.0;
		/// <summary>
		/// 一セッションあたりのページビュー数[PageViewsPerSession]プロパティ
		/// </summary>
		public double PageViewsPerSession
		{
			get
			{
				return _PageViewsPerSession;
			}
			set
			{
				if (!_PageViewsPerSession.Equals(value))
				{
					_PageViewsPerSession = value;
					NotifyPropertyChanged("PageViewsPerSession");
				}
			}
		}
		#endregion

		#region ユニークページアクセス[UniquePageviews]プロパティ
		/// <summary>
		/// ユニークページアクセス[UniquePageviews]プロパティ用変数
		/// </summary>
		int _UniquePageviews = 0;
		/// <summary>
		/// ユニークページアクセス[UniquePageviews]プロパティ
		/// </summary>
		public int UniquePageviews
		{
			get
			{
				return _UniquePageviews;
			}
			set
			{
				if (!_UniquePageviews.Equals(value))
				{
					_UniquePageviews = value;
					NotifyPropertyChanged("UniquePageviews");
				}
			}
		}
		#endregion

		#region 滞在時間[TimeOnPage]プロパティ
		/// <summary>
		/// 滞在時間[TimeOnPage]プロパティ用変数
		/// </summary>
		double _TimeOnPage = 0.0;
		/// <summary>
		/// 滞在時間[TimeOnPage]プロパティ
		/// </summary>
		public double TimeOnPage
		{
			get
			{
				return _TimeOnPage;
			}
			set
			{
				if (!_TimeOnPage.Equals(value))
				{
					_TimeOnPage = value;
					NotifyPropertyChanged("TimeOnPage");
				}
			}
		}
		#endregion

		#region 平均滞在時間[AvgTimeOnPage]プロパティ
		/// <summary>
		/// 平均滞在時間[AvgTimeOnPage]プロパティ用変数
		/// </summary>
		double _AvgTimeOnPage = 0.0;
		/// <summary>
		/// 平均滞在時間[AvgTimeOnPage]プロパティ
		/// </summary>
		public double AvgTimeOnPage
		{
			get
			{
				return _AvgTimeOnPage;
			}
			set
			{
				if (!_AvgTimeOnPage.Equals(value))
				{
					_AvgTimeOnPage = value;
					NotifyPropertyChanged("AvgTimeOnPage");
				}
			}
		}
		#endregion

		#region 離脱数[Exits]プロパティ
		/// <summary>
		/// 離脱数[Exits]プロパティ用変数
		/// </summary>
		int _Exits = 0;
		/// <summary>
		/// 離脱数[Exits]プロパティ
		/// </summary>
		public int Exits
		{
			get
			{
				return _Exits;
			}
			set
			{
				if (!_Exits.Equals(value))
				{
					_Exits = value;
					NotifyPropertyChanged("Exits");
				}
			}
		}
		#endregion

		#region 離脱率[ExitRate]プロパティ
		/// <summary>
		/// 離脱率[ExitRate]プロパティ用変数
		/// </summary>
		double _ExitRate = 0.0;
		/// <summary>
		/// 離脱率[ExitRate]プロパティ
		/// </summary>
		public double ExitRate
		{
			get
			{
				return _ExitRate;
			}
			set
			{
				if (!_ExitRate.Equals(value))
				{
					_ExitRate = value;
					NotifyPropertyChanged("ExitRate");
				}
			}
		}
		#endregion



		public void SetValue(ReportRow row)
        {
            this.Page = row.Dimensions.ElementAt(0);
            this.PageTitle = row.Dimensions.ElementAt(1);




            this.PageViews = int.Parse(row.Metrics.ElementAt(0).Values.ElementAt(0));
            this.Entrances = int.Parse(row.Metrics.ElementAt(0).Values.ElementAt(1));
			this.EntranceRate = double.Parse(row.Metrics.ElementAt(0).Values.ElementAt(2));
			this.PageViewsPerSession = double.Parse(row.Metrics.ElementAt(0).Values.ElementAt(3));
			this.UniquePageviews = int.Parse(row.Metrics.ElementAt(0).Values.ElementAt(4));
			this.TimeOnPage = double.Parse(row.Metrics.ElementAt(0).Values.ElementAt(5));
			this.AvgTimeOnPage = double.Parse(row.Metrics.ElementAt(0).Values.ElementAt(6));
			this.Exits = int.Parse(row.Metrics.ElementAt(0).Values.ElementAt(7));
			this.ExitRate = double.Parse(row.Metrics.ElementAt(0).Values.ElementAt(8));
		}




		//#region ページ別訪問数[UniquePageView]プロパティ
		///// <summary>
		///// ページ別訪問数[UniquePageView]プロパティ用変数
		///// </summary>
		//int _UniquePageView = 0;
		///// <summary>
		///// ページ別訪問数[UniquePageView]プロパティ
		///// </summary>
		//public int UniquePageView
		//{
		//	get
		//	{
		//		return _UniquePageView;
		//	}
		//	set
		//	{
		//		if (!_UniquePageView.Equals(value))
		//		{
		//			_UniquePageView = value;
		//			NotifyPropertyChanged("UniquePageView");
		//		}
		//	}
		//}
		//#endregion

		//#region 平均ページ滞在時間[StayTime]プロパティ
		///// <summary>
		///// 平均ページ滞在時間[StayTime]プロパティ用変数
		///// </summary>
		//double _StayTime = 0.0;
		///// <summary>
		///// 平均ページ滞在時間[StayTime]プロパティ
		///// </summary>
		//public double StayTime
		//{
		//	get
		//	{
		//		return _StayTime;
		//	}
		//	set
		//	{
		//		if (!_StayTime.Equals(value))
		//		{
		//			_StayTime = value;
		//			NotifyPropertyChanged("StayTime");
		//		}
		//	}
		//}
		//#endregion

		//#region 閲覧開始数[PageViewStart]プロパティ
		///// <summary>
		///// 閲覧開始数[PageViewStart]プロパティ用変数
		///// </summary>
		//int _PageViewStart = 0;
		///// <summary>
		///// 閲覧開始数[PageViewStart]プロパティ
		///// </summary>
		//public int PageViewStart
		//{
		//	get
		//	{
		//		return _PageViewStart;
		//	}
		//	set
		//	{
		//		if (!_PageViewStart.Equals(value))
		//		{
		//			_PageViewStart = value;
		//			NotifyPropertyChanged("PageViewStart");
		//		}
		//	}
		//}
		//#endregion

		//#region 直帰率[ReturnRatio]プロパティ
		///// <summary>
		///// 直帰率[ReturnRatio]プロパティ用変数
		///// </summary>
		//double _ReturnRatio = 0.0;
		///// <summary>
		///// 直帰率[ReturnRatio]プロパティ
		///// </summary>
		//public double ReturnRatio
		//{
		//	get
		//	{
		//		return _ReturnRatio;
		//	}
		//	set
		//	{
		//		if (!_ReturnRatio.Equals(value))
		//		{
		//			_ReturnRatio = value;
		//			NotifyPropertyChanged("ReturnRatio");
		//		}
		//	}
		//}
		//#endregion

		//#region 離脱率[LeaveRatio]プロパティ
		///// <summary>
		///// 離脱率[LeaveRatio]プロパティ用変数
		///// </summary>
		//double _LeaveRatio = 0.0;
		///// <summary>
		///// 離脱率[LeaveRatio]プロパティ
		///// </summary>
		//public double LeaveRatio
		//{
		//	get
		//	{
		//		return _LeaveRatio;
		//	}
		//	set
		//	{
		//		if (!_LeaveRatio.Equals(value))
		//		{
		//			_LeaveRatio = value;
		//			NotifyPropertyChanged("LeaveRatio");
		//		}
		//	}
		//}
		//#endregion

		//#region ページの価値[PageValue]プロパティ
		///// <summary>
		///// ページの価値[PageValue]プロパティ用変数
		///// </summary>
		//double _PageValue = 0.0;
		///// <summary>
		///// ページの価値[PageValue]プロパティ
		///// </summary>
		//public double PageValue
		//{
		//	get
		//	{
		//		return _PageValue;
		//	}
		//	set
		//	{
		//		if (!_PageValue.Equals(value))
		//		{
		//			_PageValue = value;
		//			NotifyPropertyChanged("PageValue");
		//		}
		//	}
		//}
		//#endregion



		//#region 値の設定処理
		///// <summary>
		///// 値の設定処理
		///// </summary>
		///// <param name="row">Excelの行データ</param>
		///// <param name="col_list">カラムリスト</param>
		//public void SetValue(IXLRow row, List<string> col_list)
		//{
		//	int index = col_list.IndexOf("ページ");

		//	if (index >= 0)
		//	{
		//		this.Page = row.Cell(index + 1).Value == null ? string.Empty : row.Cell(index + 1).Value.ToString();
		//	}

		//	index = col_list.IndexOf("ページビュー数");

		//	if (index >= 0)
		//	{
		//		this._PageView = row.Cell(index + 1).Value == null ? 0 : int.Parse(row.Cell(index + 1).Value.ToString());
		//	}

		//	index = col_list.IndexOf("ページ別訪問数");

		//	if (index >= 0)
		//	{
		//		this.UniquePageView = row.Cell(index + 1).Value == null ? 0 : int.Parse(row.Cell(index + 1).Value.ToString());
		//	}

		//	index = col_list.IndexOf("平均ページ滞在時間");

		//	if (index >= 0)
		//	{
		//		this.StayTime = row.Cell(index + 1).Value == null ? 0 : double.Parse(row.Cell(index + 1).Value.ToString());
		//	}

		//	index = col_list.IndexOf("閲覧開始数");

		//	if (index >= 0)
		//	{
		//		this.PageViewStart = row.Cell(index + 1).Value == null ? 0 : int.Parse(row.Cell(index + 1).Value.ToString());
		//	}
		//	index = col_list.IndexOf("直帰率");

		//	if (index >= 0)
		//	{
		//		this.ReturnRatio = row.Cell(index + 1).Value == null ? 0 : double.Parse(row.Cell(index + 1).Value.ToString());
		//	}
		//	index = col_list.IndexOf("離脱率");

		//	if (index >= 0)
		//	{
		//		this.LeaveRatio = row.Cell(index + 1).Value == null ? 0 : double.Parse(row.Cell(index + 1).Value.ToString());
		//	}
		//	index = col_list.IndexOf("ページの価値");

		//	if (index >= 0)
		//	{
		//		this.PageValue = row.Cell(index + 1).Value == null ? 0 : double.Parse(row.Cell(index + 1).Value.ToString());
		//	}
		//}
		//#endregion
	}
}
