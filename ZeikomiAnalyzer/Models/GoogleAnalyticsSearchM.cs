using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZeikomiAnalyzer.Common;

namespace ZeikomiAnalyzer.Models
{
    public class GoogleAnalyticsSearchM : ModelBase
	{
		#region Googleアナリティクスで検索開始日[SearchStart]プロパティ
		/// <summary>
		/// Googleアナリティクスで検索開始日[SearchStart]プロパティ用変数
		/// </summary>
		DateTime _SearchStart = DateTime.Today.AddDays(-28);
		/// <summary>
		/// Googleアナリティクスで検索開始日[SearchStart]プロパティ
		/// </summary>
		public DateTime SearchStart
		{
			get
			{
				return _SearchStart;
			}
			set
			{
				if (!_SearchStart.Equals(value))
				{
					_SearchStart = value;
					NotifyPropertyChanged("SearchStart");
				}
			}
		}
		#endregion

		#region Googleアナリティクスで検索終了日[SearchEnd]プロパティ
		/// <summary>
		/// Googleアナリティクスで検索終了日[SearchEnd]プロパティ用変数
		/// </summary>
		DateTime _SearchEnd = DateTime.Today;
		/// <summary>
		/// Googleアナリティクスで検索終了日[SearchEnd]プロパティ
		/// </summary>
		public DateTime SearchEnd
		{
			get
			{
				return _SearchEnd;
			}
			set
			{
				if (!_SearchEnd.Equals(value))
				{
					_SearchEnd = value;
					NotifyPropertyChanged("SearchEnd");
				}
			}
		}
		#endregion

		#region GoogleAnalyticsViewId[ViewId]プロパティ
		/// <summary>
		/// GoogleAnalyticsViewId[ViewId]プロパティ
		/// </summary>
		public string ViewId
		{
			get
			{
				return CommonValues.GetInstance().Config.ViewId;
			}
			set
			{
				CommonValues.GetInstance().Config.ViewId = value;
				NotifyPropertyChanged("ViewId");
			}
		}
		#endregion

		#region Google Analyticsのデータ取得処理
		/// <summary>
		/// Google Analyticsのデータ取得処理
		/// </summary>
		/// <param name="key_json">秘密鍵のJsonファイル</param>
		/// <returns>取得結果</returns>
		public List<GetReportsResponse> GetAnalytics(string key_json)
		{
			List<GetReportsResponse> result = new List<GetReportsResponse>();

			string next_token = string.Empty;
			do
			{
				// 秘密鍵の読み込み
				FileStream stream = new FileStream(key_json, FileMode.Open, FileAccess.Read);
				GoogleCredential credential = GoogleCredential.FromStream(stream).CreateScoped(AnalyticsReportingService.Scope.AnalyticsReadonly);

				// 初期化処理
				AnalyticsReportingService.Initializer initializer = new AnalyticsReportingService.Initializer();
				initializer.HttpClientInitializer = credential;

				// アプリケーション名のセット(何でも良いのでアセンブリ名から取得)
				initializer.ApplicationName = Assembly.GetExecutingAssembly().GetName().Name;
				AnalyticsReportingService service = new AnalyticsReportingService(initializer);

				GetReportsRequest request = new GetReportsRequest();

				ReportRequest report_request = new ReportRequest();
				report_request.ViewId = this.ViewId;            // Google Analytics ViewId
				report_request.PageToken = next_token;

				// ディメンションのセット
				// 参考：https://ga-dev-tools.web.app/dimensions-metrics-explorer/
				report_request.Dimensions = new[] {
					new Dimension { Name = "ga:pagePath" },
					new Dimension { Name = "ga:pageTitle" }
				};

				// メトリクスのセット
				// 参考：https://ga-dev-tools.web.app/dimensions-metrics-explorer/
				report_request.Metrics = new[] {
					new Metric { Expression = "ga:pageviews" },
					new Metric { Expression = "ga:entrances" },
					new Metric { Expression = "ga:entranceRate" },
					new Metric { Expression = "ga:pageviewsPerSession" },
					new Metric { Expression = "ga:uniquePageviews" },
					new Metric { Expression = "ga:timeOnPage" },
					new Metric { Expression = "ga:avgTimeOnPage" },
					new Metric { Expression = "ga:exits" },
					new Metric { Expression = "ga:exitRate" }
				};

				// データの取得期間のセット
				report_request.DateRanges = new[] {
					new DateRange {
						StartDate = this.SearchStart.ToString("yyyy-MM-dd"),
						EndDate = this.SearchEnd.ToString("yyyy-MM-dd") }
				};

				//// ソート順 日付で昇順
				//report_request.OrderBys = new[] {
				//	new OrderBy {
				//		FieldName = "ga:date",
				//		SortOrder = "DESCENDING"
				//	}
				//};

				request.ReportRequests = new[] { report_request };
				ReportsResource.BatchGetRequest batchRequest = service.Reports.BatchGet(request);

				var tmp = batchRequest.Execute();
				next_token = tmp.Reports.First().NextPageToken;
				result.Add(batchRequest.Execute());
			}
			while (!string.IsNullOrEmpty(next_token));

			// 実行
			return result;
		}
		#endregion
	}
}
