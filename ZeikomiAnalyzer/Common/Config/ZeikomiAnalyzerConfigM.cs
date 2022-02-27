using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeikomiAnalyzer.Models;

namespace ZeikomiAnalyzer.Common.Config
{
    public class ZeikomiAnalyzerConfigM : ModelBase
	{
		#region WordPressのユーザーアカウント[UserAccount]プロパティ
		/// <summary>
		/// WordPressのユーザーアカウント[UserAccount]プロパティ用変数
		/// </summary>
		string _UserAccount = string.Empty;
		/// <summary>
		/// WordPressのユーザーアカウント[UserAccount]プロパティ
		/// </summary>
		public string UserAccount
		{
			get
			{
				return _UserAccount;
			}
			set
			{
				if (_UserAccount == null || !_UserAccount.Equals(value))
				{
					_UserAccount = value;
					NotifyPropertyChanged("UserAccount");
				}
			}
		}
		#endregion

		#region WordPressのパスワード[Password]プロパティ
		/// <summary>
		/// WordPressのパスワード[Password]プロパティ用変数
		/// </summary>
		string _Password = string.Empty;
		/// <summary>
		/// WordPressのパスワード[Password]プロパティ
		/// </summary>
		public string Password
		{
			get
			{
				return _Password;
			}
			set
			{
				if (_Password == null || !_Password.Equals(value))
				{
					_Password = value;
					NotifyPropertyChanged("Password");
				}
			}
		}
		#endregion

		#region WordPressを使用しているURL
		/// <summary>
		/// WordPressを使用しているURL
		/// </summary>
		string _Url = string.Empty;
		/// <summary>
		/// WordPressを使用しているURL 例：https://www.premium-tsubu-hero.net/[Url]プロパティ
		/// </summary>
		public string Url
		{
			get
			{
				return _Url;
			}
			set
			{
				if (_Url == null || !_Url.Equals(value))
				{
					_Url = value;
					NotifyPropertyChanged("Url");
				}
			}
		}
		#endregion

		#region タイトルの長さ最小値[TitleLengthMin]プロパティ
		/// <summary>
		/// タイトルの長さ最小値[TitleLengthMin]プロパティ用変数
		/// </summary>
		int _TitleLengthMin = 30;
		/// <summary>
		/// タイトルの長さ最小値[TitleLengthMin]プロパティ
		/// </summary>
		public int TitleLengthMin
		{
			get
			{
				return _TitleLengthMin;
			}
			set
			{
				if (!_TitleLengthMin.Equals(value))
				{
					_TitleLengthMin = value;
					NotifyPropertyChanged("TitleLengthMin");
				}
			}
		}
		#endregion

		#region タイトルの長さ最大値[TitleLengthMax]プロパティ
		/// <summary>
		/// タイトルの長さ最大値[TitleLengthMax]プロパティ用変数
		/// </summary>
		int _TitleLengthMax = 40;
		/// <summary>
		/// タイトルの長さ最大値[TitleLengthMax]プロパティ
		/// </summary>
		public int TitleLengthMax
		{
			get
			{
				return _TitleLengthMax;
			}
			set
			{
				if (!_TitleLengthMax.Equals(value))
				{
					_TitleLengthMax = value;
					NotifyPropertyChanged("TitleLengthMax");
				}
			}
		}
		#endregion

		#region キーワードリスト[KeywordList]プロパティ
		/// <summary>
		/// キーワードリスト[KeywordList]プロパティ用変数
		/// </summary>
		ModelList<TitleKeywordM> _KeywordList = new ModelList<TitleKeywordM>();
		/// <summary>
		/// キーワードリスト[KeywordList]プロパティ
		/// </summary>
		public ModelList<TitleKeywordM> KeywordList
		{
			get
			{
				return _KeywordList;
			}
			set
			{
				if (_KeywordList == null || !_KeywordList.Equals(value))
				{
					_KeywordList = value;
					NotifyPropertyChanged("KeywordList");
				}
			}
		}
		#endregion

		#region GoogleアナリティクスAPI用秘密鍵ファイルパス[GoogleAnalyticsPrivateKey]プロパティ
		/// <summary>
		/// GoogleアナリティクスAPI用秘密鍵ファイルパス[GoogleAnalyticsPrivateKey]プロパティ用変数
		/// </summary>
		string _GoogleAnalyticsPrivateKey = string.Empty;
		/// <summary>
		/// GoogleアナリティクスAPI用秘密鍵ファイルパス[GoogleAnalyticsPrivateKey]プロパティ
		/// </summary>
		public string GoogleAnalyticsPrivateKey
		{
			get
			{
				return _GoogleAnalyticsPrivateKey;
			}
			set
			{
				if (_GoogleAnalyticsPrivateKey == null || !_GoogleAnalyticsPrivateKey.Equals(value))
				{
					_GoogleAnalyticsPrivateKey = value;
					NotifyPropertyChanged("GoogleAnalyticsPrivateKey");
				}
			}
		}
		#endregion

		#region GoogleAnalyticsViewId[ViewId]プロパティ
		/// <summary>
		/// GoogleAnalyticsViewId[ViewId]プロパティ用変数
		/// </summary>
		string _ViewId = string.Empty;
		/// <summary>
		/// GoogleAnalyticsViewId[ViewId]プロパティ
		/// </summary>
		public string ViewId
		{
			get
			{
				return _ViewId;
			}
			set
			{
				if (_ViewId == null || !_ViewId.Equals(value))
				{
					_ViewId = value;
					NotifyPropertyChanged("ViewId");
				}
			}
		}
		#endregion

		#region 固定ページ出力処理[PageOut]プロパティ
		/// <summary>
		/// 固定ページ出力処理[PageOut]プロパティ用変数
		/// </summary>
		bool _PageOut = true;
		/// <summary>
		/// 固定ページ出力処理[PageOut]プロパティ
		/// </summary>
		public bool PageOut
		{
			get
			{
				return _PageOut;
			}
			set
			{
				if (!_PageOut.Equals(value))
				{
					_PageOut = value;
					NotifyPropertyChanged("PageOut");
				}
			}
		}
		#endregion

		#region 固定ページ出力処理[IsPageForZeroOut]プロパティ
		/// <summary>
		/// 固定ページ出力処理[IsPageForZeroOut]プロパティ用変数
		/// </summary>
		bool _IsPageForZeroOut = true;
		/// <summary>
		/// 固定ページ出力処理[IsPageForZeroOut]プロパティ
		/// </summary>
		public bool IsPageForZeroOut
		{
			get
			{
				return _IsPageForZeroOut;
			}
			set
			{
				if (!_IsPageForZeroOut.Equals(value))
				{
					_IsPageForZeroOut = value;
					NotifyPropertyChanged("IsPageForZeroOut");
				}
			}
		}
		#endregion

		#region 投稿出力処理[IsPostForZeroOut]プロパティ
		/// <summary>
		/// 投稿出力処理[IsPostForZeroOut]プロパティ用変数
		/// </summary>
		bool _IsPostForZeroOut = true;
		/// <summary>
		/// 投稿出力処理[IsPostForZeroOut]プロパティ
		/// </summary>
		public bool IsPostForZeroOut
		{
			get
			{
				return _IsPostForZeroOut;
			}
			set
			{
				if (!_IsPostForZeroOut.Equals(value))
				{
					_IsPostForZeroOut = value;
					NotifyPropertyChanged("IsPostForZeroOut");
				}
			}
		}
		#endregion

		#region 出力するカテゴリ[OutputCategoryType]プロパティ
		/// <summary>
		/// 出力するカテゴリ[OutputCategoryType]プロパティ用変数
		/// </summary>
		string _OutputCategoryType = string.Empty;
		/// <summary>
		/// 出力するカテゴリ[OutputCategoryType]プロパティ
		/// </summary>
		public string OutputCategoryType
		{
			get
			{
				return _OutputCategoryType;
			}
			set
			{
				if (_OutputCategoryType == null || !_OutputCategoryType.Equals(value))
				{
					_OutputCategoryType = value;
					NotifyPropertyChanged("OutputCategoryType");
				}
			}
		}
		#endregion

		#region ハッシュタグ[HashtagZeroOut]プロパティ
		/// <summary>
		/// ハッシュタグ[HashtagZeroOut]プロパティ用変数
		/// </summary>
		string _HashtagZeroOut = string.Empty;
		/// <summary>
		/// ハッシュタグ[HashtagZeroOut]プロパティ
		/// </summary>
		public string HashtagZeroOut
		{
			get
			{
				return _HashtagZeroOut;
			}
			set
			{
				if (_HashtagZeroOut == null || !_HashtagZeroOut.Equals(value))
				{
					_HashtagZeroOut = value;
					NotifyPropertyChanged("HashtagZeroOut");
				}
			}
		}
		#endregion


		#region ツイッター用コンシューマーキー[TwConsumerKey]プロパティ
		/// <summary>
		/// ツイッター用コンシューマーキー[TwConsumerKey]プロパティ用変数
		/// </summary>
		string _TwConsumerKey = string.Empty;
		/// <summary>
		/// ツイッター用コンシューマーキー[TwConsumerKey]プロパティ
		/// </summary>
		public string TwConsumerKey
		{
			get
			{
				return _TwConsumerKey;
			}
			set
			{
				if (_TwConsumerKey == null || !_TwConsumerKey.Equals(value))
				{
					_TwConsumerKey = value;
					NotifyPropertyChanged("TwConsumerKey");
				}
			}
		}
		#endregion

		#region ツイッター用コンシューマーシークレット[TwConsumerSecret]プロパティ
		/// <summary>
		/// ツイッター用コンシューマーシークレット[TwConsumerSecret]プロパティ用変数
		/// </summary>
		string _TwConsumerSecret = string.Empty;
		/// <summary>
		/// ツイッター用コンシューマーシークレット[TwConsumerSecret]プロパティ
		/// </summary>
		public string TwConsumerSecret
		{
			get
			{
				return _TwConsumerSecret;
			}
			set
			{
				if (_TwConsumerSecret == null || !_TwConsumerSecret.Equals(value))
				{
					_TwConsumerSecret = value;
					NotifyPropertyChanged("TwConsumerSecret");
				}
			}
		}
		#endregion

		#region ツイッター用アクセストークン[TwAccessToken]プロパティ
		/// <summary>
		/// ツイッター用アクセストークン[TwAccessToken]プロパティ用変数
		/// </summary>
		string _TwAccessToken = string.Empty;
		/// <summary>
		/// ツイッター用アクセストークン[TwAccessToken]プロパティ
		/// </summary>
		public string TwAccessToken
		{
			get
			{
				return _TwAccessToken;
			}
			set
			{
				if (_TwAccessToken == null || !_TwAccessToken.Equals(value))
				{
					_TwAccessToken = value;
					NotifyPropertyChanged("TwAccessToken");
				}
			}
		}
		#endregion

		#region ツイッター用アクセスシークレット[TwAccessSecret]プロパティ
		/// <summary>
		/// ツイッター用アクセスシークレット[TwAccessSecret]プロパティ用変数
		/// </summary>
		string _TwAccessSecret = string.Empty;
		/// <summary>
		/// ツイッター用アクセスシークレット[TwAccessSecret]プロパティ
		/// </summary>
		public string TwAccessSecret
		{
			get
			{
				return _TwAccessSecret;
			}
			set
			{
				if (_TwAccessSecret == null || !_TwAccessSecret.Equals(value))
				{
					_TwAccessSecret = value;
					NotifyPropertyChanged("TwAccessSecret");
				}
			}
		}
		#endregion

		#region ツイッター用スクリーン名[TwScreenName]プロパティ
		/// <summary>
		/// ツイッター用スクリーン名[TwScreenName]プロパティ用変数
		/// </summary>
		string _TwScreenName = string.Empty;
		/// <summary>
		/// ツイッター用スクリーン名[TwScreenName]プロパティ
		/// </summary>
		public string TwScreenName
		{
			get
			{
				return _TwScreenName;
			}
			set
			{
				if (_TwScreenName == null || !_TwScreenName.Equals(value))
				{
					_TwScreenName = value;
					NotifyPropertyChanged("TwScreenName");
				}
			}
		}
		#endregion






		#region コンフィグの保存処理
		/// <summary>
		/// コンフィグの保存処理
		/// </summary>
		public void Save()
		{
			try
			{
				XMLUtil.Seialize<ZeikomiAnalyzerConfigM>(ConfigManager.ConfigFilePath, this);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message, "Error");
			}
		}
		#endregion

		#region コンフィグのロード処理
		/// <summary>
		/// コンフィグのロード処理
		/// </summary>
		public void Load()
        {
			try
			{
				if (File.Exists(ConfigManager.ConfigFilePath))
				{
					var conf = XMLUtil.Deserialize<ZeikomiAnalyzerConfigM>(ConfigManager.ConfigFilePath);

					this.UserAccount = conf.UserAccount;
					this.Password = conf.Password;
					this.Url = conf.Url;
					this.KeywordList = conf.KeywordList;
					this.TitleLengthMin = conf.TitleLengthMin;
					this.TitleLengthMax = conf.TitleLengthMax;
					this.GoogleAnalyticsPrivateKey = conf.GoogleAnalyticsPrivateKey;
					this.ViewId = conf.ViewId;
					this.IsPageForZeroOut = conf.IsPageForZeroOut;
					this.IsPostForZeroOut = conf.IsPostForZeroOut;
					this.OutputCategoryType = conf.OutputCategoryType;
					this.HashtagZeroOut = conf.HashtagZeroOut;

					this.TwConsumerKey = conf.TwConsumerKey;
					this.TwConsumerSecret = conf.TwConsumerSecret;
					this.TwAccessToken = conf.TwAccessToken;
					this.TwAccessSecret = conf.TwAccessSecret;
					this.TwScreenName = conf.TwScreenName;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message, "Error");
				throw;
			}
		}
		#endregion
	}
}
