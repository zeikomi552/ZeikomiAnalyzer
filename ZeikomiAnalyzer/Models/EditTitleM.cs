using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeikomiAnalyzer.Common;

namespace ZeikomiAnalyzer.Models
{
    public class EditTitleM : ModelBase
    {


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

		/// <summary>
		/// タイトル長さ
		/// </summary>
		public int TitleLength
        {
            get
            {
				return this.Title.Length;
            }

        }


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
					NotifyPropertyChanged("TitleLength");
				}
			}
        }
        #endregion
    }
}
