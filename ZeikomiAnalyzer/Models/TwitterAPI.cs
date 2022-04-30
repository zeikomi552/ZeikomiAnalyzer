using CoreTweet;
using CoreTweet.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using ZeikomiAnalyzer.Common;
using ZeikomiAnalyzer.Common.Config;

namespace Twapi.Twitter
{
    /// <summary>
    /// 参考：
    /// http://westplain.sakuraweb.com/translate/twitter/Documentation/REST-APIs/Public-API/REST-APIs.cgi
    /// </summary>
    public class TwitterAPI : INotifyPropertyChanged
    {

        public static ZeikomiAnalyzerConfigM Config
        {
            get
            {
                return CommonValues.GetInstance().Config;
            }
        }

        #region トークンの作成
        /// <summary>
        /// トークンの作成
        /// </summary>
        /// <returns>トークン</returns>
        public static Tokens CreateToken()
        {
            // トークンの作成
            return CoreTweet.Tokens.Create(Config.TwConsumerKey,
                    Config.TwConsumerSecret,
                    Config.TwAccessToken,
                    Config.TwAccessSecret);
        }
        #endregion
        
        #region トークン
        /// <summary>
        /// トークン
        /// </summary>
        public static Tokens Token
        {
            get
            {
                return CreateToken();
            }
        }
        #endregion

        #region RateLimitの取得
        /// <summary>
        /// RateLimitの取得
        /// </summary>
        /// <param name="keys">アクセスキー</param>
        public static DictionaryResponse<string, Dictionary<string, RateLimit>> GetRateLimit()
        {
            // トークンの作成
            var token = CreateToken();

            // RateLimitの返却
            return token.Application.RateLimitStatus();
        }
        #endregion

        #region ツイート処理
        /// <summary>
        /// ツイート処理
        /// </summary>
        /// <param name="message">送信するメッセージ</param>
        /// <returns>true:ツイート成功 false:トークンが作成されていない それ以外はエクセプション</returns>
        public static StatusResponse Tweet(string message)
        {
            // トークンの作成
            var token = CreateToken();

            // ツイート
            return token.Statuses.Update(status => message);
        }
        #endregion

        #region Tweetの検索処理
        /// <summary>
        /// ツイートの検索
        /// </summary>
        /// <param name="keyword">検索キーワード</param>
        /// <returns>検索結果</returns>
        public static CoreTweet.SearchResult TweetSearch(string keyword)
        {
            // トークンの作成
            var token = CreateToken();

            // 検索
            var result = token.Search.Tweets(count => 100, q => keyword, lang => "ja"); ;

            // 検索結果
            return result;
        }
        #endregion

        #region Tweetの検索処理


        /// <summary>
        /// 可能な位置情報の検索
        /// </summary>
        /// <param name="keys">キー</param>
        /// <returns>検索結果</returns>
        public static ListedResponse<TrendLocation> Available()
        {
            // トークンの作成
            var token = CreateToken();

            // 検索
            var result = token.Trends.Available();

            // 検索結果
            return result;
        }
        #endregion

        #region 位置情報の検索
        /// <summary>
        /// 位置情報の検索
        /// </summary>
        /// <param name="keys">キー</param>
        /// <returns>検索結果</returns>
        public static ListedResponse<TrendLocation> Closets(long lat, long lon)
        {
            // トークンの作成
            var token = CreateToken();

            // 検索
            var result = token.Trends.Closest(lat, lon);

            // 検索結果
            return result;
        }
        #endregion

        #region 位置情報の検索
        /// <summary>
        /// 位置情報の検索
        /// </summary>
        /// <param name="keys">キー</param>
        /// <returns>検索結果</returns>
        public static ListedResponse<TrendsResult> Place(long id)
        {
            // トークンの作成
            var token = CreateToken();

            // 検索
            var result = token.Trends.Place(id);

            // 検索結果
            return result;
        }
        #endregion

        #region Tweetの検索処理

        /// <summary>
        /// ツイートの検索
        /// </summary>
        /// <param name="keys">キー</param>
        /// <param name="keyword">検索キーワード</param>
        /// <returns>検索結果</returns>
        public static ListedResponse<User> UserSearch(string screen_name)
        {
            // トークンの作成
            var token = CreateToken();

            // 検索
            var result = token.Users.Search(q => screen_name);

            // 検索結果
            return result;
        }
        #endregion

        #region フォローを実行する
        /// <summary>
        /// フォローを実行する
        /// </summary>
        /// <param name="keys">キー情報</param>
        /// <param name="id">フォローするId</param>
        /// <returns>結果</returns>
        public static UserResponse CreateFollow(string screen_name)
        {
            return ManageFollow(screen_name, true);
        }
        #endregion

        #region フォローを解除する
        /// <summary>
        /// フォローを解除する
        /// </summary>
        /// <param name="keys">キー情報</param>
        /// <param name="id">解除するId</param>
        /// <returns>結果</returns>
        public static UserResponse BreakFollow(string screen_name)
        {
            return ManageFollow(screen_name, false);
        }
        #endregion

        #region フォローの実行と解除を行う関数
        /// <summary>
        /// フォローの実行と解除を行う関数
        /// </summary>
        /// <param name="keys">キー情報</param>
        /// <param name="screen_name">スクリーン名</param>
        /// <param name="follow">true:フォロー false:解除</param>
        /// <returns>結果</returns>
        private static UserResponse ManageFollow(string screen_name, bool follow)
        {
            // トークンの作成
            var token = CreateToken();

            // ユーザー名の検索
            var users = UserSearch(screen_name);

            // 取得できた場合
            if (users.Count > 0)
            {
                var user = users.ElementAt(0);
                long? id = user.Id;

                if (id.HasValue && id.Value > 0)
                {
                    // プライベートアカウントもしくはツイッターからロックされている場合は実行しない
                    if(!user.IsProtected &&
                        !(user.IsSuspended.HasValue && user.IsSuspended.Value))
                    {
                        if (follow)
                        {
                            // フォローの実行
                            var result = token.Friendships.Create(id.Value);
                            return result;
                        }
                        else
                        {
                            // フォロー解除の実行
                            var result = token.Friendships.Destroy(id.Value);
                            return result;
                        }
                    }
                }
            }

            return null;
        }
        #endregion

        #region ユーザータイムラインの取得
        /// <summary>
        /// ユーザータイムラインの取得
        /// </summary>
        /// <param name="screen_name"></param>
        /// <returns></returns>
        public static List<Status> GetUserTimeLine(string screen_name)
        {
            List<Status> ret = new List<Status>();
            // トークンの作成
            var token = CreateToken();

            // 検索
            var status = token.Statuses.UserTimeline(q => screen_name, count => 200);

            if(status != null)
                ret.AddRange(status.ToList<Status>());

            while (status.Count > 0)
            {
                var tmp = status.Last();

                if (tmp != null)
                {
                    status = token.Statuses.UserTimeline(q => screen_name, count => 200, max_id => tmp.Id - 1);

                    ret.AddRange(status.ToList<Status>());
                }
                else
                {
                    break;
                }
            }
            return ret;

        }
        #endregion

        public static List<Status> GetTweet(string query)
        {
            List<Status> ret = new List<Status>();
            // トークンの作成
            var token = CreateToken();

            // 検索
            var tweet = token.Search.Tweets(q=> query, count=>100, lang => "ja");

            if (tweet != null)
                ret.AddRange(tweet.ToList<Status>());

            while (tweet.Count > 0)
            {
                System.Threading.Thread.Sleep(1000);
                var tmp = tweet.Last();

                if (tmp != null)
                {
                    tweet = token.Search.Tweets(q => query, count => 100, max_id => tmp.Id - 1, lang=>"ja");
                    ret.AddRange(tweet.ToList<Status>());
                }
                else
                {
                    break;
                }

                if (tweet.RateLimit.Remaining <= 0)
                {
                    break;
                }
            }
            return ret;

        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

    }
}
