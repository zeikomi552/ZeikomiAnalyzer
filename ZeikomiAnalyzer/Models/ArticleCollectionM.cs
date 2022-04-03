using Google.Apis.AnalyticsReporting.v4.Data;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace ZeikomiAnalyzer.Models
{
    public class ArticleCollectionM : ModelBase
    {
		#region 投稿ページ[Pages]プロパティ
		/// <summary>
		/// 投稿ページ[Pages]プロパティ用変数
		/// </summary>
		ModelList<Page> _Pages = new ModelList<Page>();
		/// <summary>
		/// 投稿ページ[Pages]プロパティ
		/// </summary>
		public ModelList<Page> Pages
		{
			get
			{
				return _Pages;
			}
			set
			{
				if (_Pages == null || !_Pages.Equals(value))
				{
					_Pages = value;
					NotifyPropertyChanged("Pages");
				}
			}
		}
		#endregion

		#region 固定ページ[Posts]プロパティ
		/// <summary>
		/// 固定ページ[Posts]プロパティ用変数
		/// </summary>
		ModelList<Post> _Posts = new ModelList<Post>();
		/// <summary>
		/// 固定ページ[Posts]プロパティ
		/// </summary>
		public ModelList<Post> Posts
		{
			get
			{
				return _Posts;
			}
			set
			{
				if (_Posts == null || !_Posts.Equals(value))
				{
					_Posts = value;
					NotifyPropertyChanged("Posts");
				}
			}
		}
		#endregion

		#region GoogleAnalyticsの結果[Analytics]プロパティ
		/// <summary>
		/// GoogleAnalyticsの結果[Analytics]プロパティ用変数
		/// </summary>
		ModelList<GoogleAnalyticsM> _Analytics = new ModelList<GoogleAnalyticsM>();
		/// <summary>
		/// GoogleAnalyticsの結果[Analytics]プロパティ
		/// </summary>
		public ModelList<GoogleAnalyticsM> Analytics
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

		#region ニート記事リスト[ZeroTitles]プロパティ
		/// <summary>
		/// ニート記事リスト[ZeroTitles]プロパティ用変数
		/// </summary>
		ModelList<ArticleM> _ZeroTitles = new ModelList<ArticleM>();
		/// <summary>
		/// ニート記事リスト[ZeroTitles]プロパティ
		/// </summary>
		public ModelList<ArticleM> ZeroTitles
		{
			get
			{
				return _ZeroTitles;
			}
			set
			{
				if (_ZeroTitles == null || !_ZeroTitles.Equals(value))
				{
					_ZeroTitles = value;
					NotifyPropertyChanged("ZeroTitles");
				}
			}
		}
		#endregion

		#region Google Analyticsデータとブログ記事情報をくっつけたもの[CombineAnalyticsItems]プロパティ
		/// <summary>
		/// Google Analyticsデータとブログ記事情報をくっつけたもの[CombineAnalyticsItems]プロパティ用変数
		/// </summary>
		ModelList<ArticleM> _CombineAnalyticsItems = new ModelList<ArticleM>();
		/// <summary>
		/// Google Analyticsデータとブログ記事情報をくっつけたもの[CombineAnalyticsItems]プロパティ
		/// </summary>
		public ModelList<ArticleM> CombineAnalyticsItems
		{
			get
			{
				return _CombineAnalyticsItems;
			}
			set
			{
				if (_CombineAnalyticsItems == null || !_CombineAnalyticsItems.Equals(value))
				{
					_CombineAnalyticsItems = value;
					NotifyPropertyChanged("CombineAnalyticsItems");
				}
			}
		}
		#endregion



		#region アナリティクスデータの追加処理
		/// <summary>
		/// アナリティクスデータの追加処理
		/// </summary>
		/// <param name="report_rows">行データ</param>
		public void AddAnalytics(IEnumerable<ReportRow> report_rows)
		{
			foreach (var row in report_rows)
			{
				var tmp = new GoogleAnalyticsM();
				tmp.SetValue(row);

				this.Analytics.Items.Add(tmp);
			}

		}
		#endregion

		#region 投稿記事の追加
		/// <summary>
		/// 投稿記事の追加
		/// </summary>
		/// <param name="posts">投稿記事リスト</param>
		public void Add(IEnumerable<Post> posts)
		{
			foreach (var post in posts)
			{
				this.Posts.Items.Add(post);
			}
		}
		#endregion

		#region 固定ページの追加
		/// <summary>
		/// 固定ページの追加
		/// </summary>
		/// <param name="pages">固定ページリスト</param>
		public void Add(IEnumerable<Page> pages)
		{
			foreach (var page in pages)
			{
				this.Pages.Items.Add(page);
			}
		}
		#endregion

		#region アクセス数ゼロの記事の追加処理
		/// <summary>
		/// アクセス数ゼロの記事の追加処理
		/// </summary>
		/// <param name="url">URL</param>
		/// <param name="title">タイトル</param>
		/// <param name="content">コンテンツ</param>
		/// <param name="type">タイプ post or page</param>
		/// <param name="categories">カテゴリ</param>
		public void AddNeet(string url, string title, string content, string type, int[] categories = null)
		{
			this.ZeroTitles.Items.Add(
				new ArticleM()
				{
					Title = title,
					Contents = content,
					Link = url,
					Type = type,
					Categories = categories
				}
				);
		}
		#endregion

		#region アクセス数ゼロの記事の追加処理
		/// <summary>
		/// アクセス数ゼロの記事の追加処理
		/// </summary>
		/// <param name="url">URL</param>
		/// <param name="title">タイトル</param>
		/// <param name="content">コンテンツ</param>
		/// <param name="type">タイプ post or page</param>
		/// <param name="organic_pv">ページビュー数</param>
		/// <param name="twitter_page_views">ページビュー数</param>
		/// <param name="categories">カテゴリ</param>
		public void AddArticleAnalytics(string url, string title, string content, string type, int organic_pv, int twitter_page_views, int[] categories = null)
		{
			this.CombineAnalyticsItems.Items.Add(
				new ArticleM()
				{
					Title = title,
					Contents = content,
					Link = url,
					Type = type,
					Categories = categories,
					OrganicPageViews = organic_pv,
					TwitterPageViews = twitter_page_views,
				}
				);
		}
		#endregion
		#region ニート記事の抽出処理
		/// <summary>
		/// ニート記事の抽出処理
		/// </summary>
		public void OutputNeet()
        {
			// ニート記事一覧の初期化
			this.ZeroTitles.Items.Clear();

			foreach (var article in this.Pages.Items)
			{
				string slug = "/" + article.Slug + "/";

				// 一部一致のものを探す //amp=等の対策
				if ((from x in this.Analytics.Items
					 where x.Page.Contains(slug)
					 select x).Any())
				{
					continue;
				}

				this.AddNeet(article.Link, article.Title.Rendered.Replace("&#8211;", "―"), article.Content.Rendered, "page");
			}

			foreach (var article in this.Posts.Items)
			{
				string slug = "/" + article.Slug + "/";

				// 一部一致のものを探す //amp=等の対策
				if ((from x in this.Analytics.Items
					 where x.Page.Contains(slug)
					 select x).Any())
				{
					continue;
				}

				this.AddNeet(article.Link, article.Title.Rendered.Replace("&#8211;", "―"), article.Content.Rendered, "post", article.Categories);
			}

		}
		#endregion

		public void CombineArticleAndAnalytics()
		{

			foreach (var article in this.Posts.Items)
			{
				string slug = "/" + article.Slug + "/";

				var tmp = (from x in this.Analytics.Items
						   where x.Page.Contains(slug)
						   select x);

				// 一部一致のものを探す //amp=等の対策
				if (tmp.Any())
				{
					var t_pv = (from x in tmp
						where x.DefaultChannelGroup.Equals("Social") && x.SocialNetwork.Equals("Twitter")
						select x).Sum(x=>x.PageViews);

					var o_pv = (from x in tmp
							  where x.DefaultChannelGroup.Equals("Organic Search")
							  select x).Sum(x => x.PageViews);

					this.AddArticleAnalytics(article.Link, article.Title.Rendered.Replace("&#8211;", "―"), article.Content.Rendered, "post", o_pv, t_pv, article.Categories);
				}

			}

		}

		#region クリア処理
		/// <summary>
		/// クリア処理
		/// </summary>
		public void Clear()
        {
			this.Posts.Items.Clear();
			this.Pages.Items.Clear();
			this.Analytics.Items.Clear();
			this.ZeroTitles.Items.Clear();
		}
        #endregion
    }
}
