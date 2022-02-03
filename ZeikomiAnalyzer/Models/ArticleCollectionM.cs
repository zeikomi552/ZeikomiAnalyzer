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
		#region 記事リスト[Articles]プロパティ
		/// <summary>
		/// 記事リスト[Articles]プロパティ用変数
		/// </summary>
		ModelList<ArticleM> _Articles = new ModelList<ArticleM>();
		/// <summary>
		/// 記事リスト[Articles]プロパティ
		/// </summary>
		public ModelList<ArticleM> Articles
		{
			get
			{
				return _Articles;
			}
			set
			{
				if (_Articles == null || !_Articles.Equals(value))
				{
					_Articles = value;
					NotifyPropertyChanged("Articles");
				}
			}
		}
		#endregion

		public void Add(IEnumerable<Post> posts)
		{
			foreach (var post in posts)
			{
				this.Articles.Items.Add(new ArticleM()
				{
					Id = post.Id,
					Link = post.Link,
					Title = post.Title.Rendered,
					Contents = post.Content.Rendered
				}
				);
			}
		}
		public void Add(IEnumerable<Page> pages)
		{
			foreach (var page in pages)
			{
				this.Articles.Items.Add(new ArticleM()
				{
					Id = page.Id,
					Link = page.Link,
					Title = page.Title.Rendered,
					Contents = page.Content.Rendered
				}
				);
			}
		}

		public void Clear()
        {
			this.Articles.Items.Clear();
        }
	}
}
