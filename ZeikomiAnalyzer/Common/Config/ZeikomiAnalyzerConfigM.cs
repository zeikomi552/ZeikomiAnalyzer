using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		#region コンフィグの保存処理
		/// <summary>
		/// コンフィグの保存処理
		/// </summary>
		public void Save()
		{
			try
			{
				XMLUtil.Seialize<ZeikomiAnalyzerConfigM>(ConfigManager.KeysFile, this);
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
				if (File.Exists(ConfigManager.KeysFile))
				{
					var conf = XMLUtil.Deserialize<ZeikomiAnalyzerConfigM>(ConfigManager.KeysFile);

					this.UserAccount = conf.UserAccount;
					this.Password = conf.Password;
					this.Url = conf.Url;
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
