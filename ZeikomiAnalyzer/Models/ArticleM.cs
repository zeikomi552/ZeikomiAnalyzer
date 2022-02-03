using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;

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
					NotifyPropertyChanged("ContentLength");
				}
			}
		}
		#endregion

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

		public int ContentLength
        {
			get
			{
				return this.Contents.Length;
			}
        }
	}
}
