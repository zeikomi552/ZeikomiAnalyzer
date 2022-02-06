using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeikomiAnalyzer.Common.Config;

namespace ZeikomiAnalyzer.Common
{
    public class CommonValues : ModelBase
    {
        #region インスタンス
        /// <summary>
        /// インスタンス
        /// </summary>
        static CommonValues _Instance = new CommonValues();
        #endregion

        private CommonValues()
        {

        }

        #region インスタンスの取得処理
        /// <summary>
        /// インスタンスの取得処理
        /// </summary>
        /// <returns></returns>
        public static CommonValues GetInstance()
        {
            return _Instance;

        }
        #endregion

        #region コンフィグ情報[Config]プロパティ
        /// <summary>
        /// コンフィグ情報[Config]プロパティ用変数
        /// </summary>
        static ZeikomiAnalyzerConfigM _Config = new ZeikomiAnalyzerConfigM();
        /// <summary>
        /// コンフィグ情報[Config]プロパティ
        /// </summary>
        public ZeikomiAnalyzerConfigM Config
        {
            get
            {
                return _Config;
            }
            set
            {
                if (_Config == null || !_Config.Equals(value))
                {
                    _Config = value;
                    NotifyPropertyChanged("Config");
                }
            }
        }
        #endregion

    }
}
