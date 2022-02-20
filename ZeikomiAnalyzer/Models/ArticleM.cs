using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordPressPCL.Models;
using ZeikomiAnalyzer.Common;

namespace ZeikomiAnalyzer.Models
{
	public class ArticleM : ModelBase
	{
		#region ID[Id]プロパティ
		/// <summary>
		/// ID[Id]プロパティ用変数
		/// </summary>
		int _Id = 0;
		/// <summary>
		/// ID[Id]プロパティ
		/// </summary>
		public int Id
		{
			get
			{
				return _Id;
			}
			set
			{
				if (!_Id.Equals(value))
				{
					_Id = value;
					NotifyPropertyChanged("Id");
				}
			}
		}
		#endregion

		#region タイトル[Title]プロパティ
		/// <summary>
		/// タイトル[Title]プロパティ用変数
		/// </summary>
		string _Title = string.Empty;
		/// <summary>
		/// タイトル[Title]プロパティ
		/// </summary>
		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				if (_Title == null || !_Title.Equals(value))
				{
					_Title = value;
					NotifyPropertyChanged("Title");
					NotifyPropertyChanged("LengthCheck");
					NotifyPropertyChanged("KeywordCheck");
				}
			}
		}
		#endregion

		#region コンテンツ[Contents]プロパティ
		/// <summary>
		/// コンテンツ[Contents]プロパティ用変数
		/// </summary>
		string _Contents = string.Empty;
		/// <summary>
		/// コンテンツ[Contents]プロパティ
		/// </summary>
		public string Contents
		{
			get
			{
				return _Contents;
			}
			set
			{
				if (_Contents == null || !_Contents.Equals(value))
				{
					_Contents = value;
					NotifyPropertyChanged("Contents");
					NotifyPropertyChanged("ContentsEx");
					NotifyPropertyChanged("ContentLength");
					NotifyPropertyChanged("ContentLength2");
				}
			}
		}
		#endregion

		public string ContentsEx
		{
			get
            {
				return Regex.Replace(this.Contents, "<[^>]*?>", "");
			}
		}

		#region リンク[Link]プロパティ
		/// <summary>
		/// リンク[Link]プロパティ用変数
		/// </summary>
		string _Link = string.Empty;
		/// <summary>
		/// リンク[Link]プロパティ
		/// </summary>
		public string Link
		{
			get
			{
				return _Link;
			}
			set
			{
				if (_Link == null || !_Link.Equals(value))
				{
					_Link = value;
					NotifyPropertyChanged("Link");
				}
			}
		}
		#endregion

		#region 記事の種別 post or page[Type]プロパティ
		/// <summary>
		/// 記事の種別 post or page[Type]プロパティ用変数
		/// </summary>
		string _Type = string.Empty;
		/// <summary>
		/// 記事の種別 post or page[Type]プロパティ
		/// </summary>
		public string Type
		{
			get
			{
				return _Type;
			}
			set
			{
				if (_Type == null || !_Type.Equals(value))
				{
					_Type = value;
					NotifyPropertyChanged("Type");
				}
			}
		}
		#endregion


		#region 記事の長さ
		/// <summary>
		/// 記事の長さ
		/// </summary>
		public int ContentLength
        {
			get
			{
				return this.Contents.Length;
			}
        }
		#endregion

		#region 記事の長さ(htmlタグを除いたもの)
		/// <summary>
		/// 記事の長さ(htmlタグを除いたもの)
		/// </summary>
		public int ContentLength2
        {
			get
			{
				var text = Regex.Replace(this.Contents, "<[^>]*?>", "");
				return text.Length;
			}
        }
		#endregion

		#region タイトルの長さ
		/// <summary>
		/// タイトルの長さ
		/// </summary>
		public int TitleLength
        {
            get
            {
				return Title.Length;
            }

        }
		#endregion

		#region タイトルの長さチェック(true:OK false:NG)[LengthCheck]プロパティ
		/// <summary>
		/// タイトルの長さチェック(true:OK false:NG)[LengthCheck]プロパティ
		/// </summary>
		public bool LengthCheck
		{
			get
			{
				if (this.Title.Length >= CommonValues.GetInstance().Config.TitleLengthMin
					&& this.Title.Length <= CommonValues.GetInstance().Config.TitleLengthMax)
				{
					return true;
				}
				else
				{
					return false;
				}
			}

		}
		#endregion

		#region タイトル内に含まれるキーワードチェック結果(true:OK false:NG)[KeywordCheck]プロパティ
		/// <summary>
		/// タイトル内に含まれるキーワードチェック結果(true:OK false:NG)[KeywordCheck]プロパティ
		/// </summary>
		public bool KeywordCheck
		{
			get
			{
				return (from x in CommonValues.GetInstance().Config.KeywordList.Items
						   where this.Title.Contains(x.Keyword)
						   select x).Any();
			}
		}
		#endregion


	}
}
