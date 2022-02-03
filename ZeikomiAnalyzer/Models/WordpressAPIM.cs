using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;
using ZeikomiAnalyzer.Common.Config;

namespace ZeikomiAnalyzer.Models
{
    public class WordpressAPIM : ModelBase
    {
        public WordPressClient WpClient = null;

        #region 接続用クライアントの作成
        /// <summary>
        /// 接続用クライアントの作成
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="username">ワードプレスのユーザー名</param>
        /// <param name="password">ワードプレスのパスワード</param>
        /// <returns>Task</returns>
        public async Task CreateClient(string url, string username, string password)
        {
            if (WpClient == null)
            {
                // JWT authentication
                WpClient = new WordPressClient($"{url}/wp-json/");
                WpClient.AuthMethod = AuthMethod.JWT;
                await WpClient.RequestJWToken(username, password);
            }
        }
        #endregion
    }
}
