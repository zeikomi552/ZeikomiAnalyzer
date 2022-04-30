using ClosedXML.Excel;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WordPressPCL.Models;
using ZeikomiAnalyzer.Common;
using ZeikomiAnalyzer.Common.Config;
using ZeikomiAnalyzer.Models;
using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Twapi.Twitter;
using ZeikomiAnalyzer.Common.Utilities;

namespace ZeikomiAnalyzer.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
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
                return CommonValues.GetInstance().Config;
            }
            set
            {
                if (CommonValues.GetInstance().Config == null || !CommonValues.GetInstance().Config.Equals(value))
                {
                    CommonValues.GetInstance().Config = value;
                    NotifyPropertyChanged("Config");
                }
            }
        }
        #endregion
        public WordpressAPIM WordpressAPI { get; set; } = new WordpressAPIM();

        #region 記事リスト[Articles]プロパティ
        /// <summary>
        /// 記事リスト[Articles]プロパティ用変数
        /// </summary>
        ArticleCollectionM _Articles = new ArticleCollectionM();
        /// <summary>
        /// 記事リスト[Articles]プロパティ
        /// </summary>
        public ArticleCollectionM Articles
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

        #region 分析結果データ[AnalyticsList]プロパティ
        /// <summary>
        /// 分析結果データ[AnalyticsList]プロパティ用変数
        /// </summary>
        GoogleAnalyticsCollectionM _AnalyticsList = new GoogleAnalyticsCollectionM();
        /// <summary>
        /// 分析結果データ[AnalyticsList]プロパティ
        /// </summary>
        public GoogleAnalyticsCollectionM AnalyticsList
        {
            get
            {
                return _AnalyticsList;
            }
            set
            {
                if (_AnalyticsList == null || !_AnalyticsList.Equals(value))
                {
                    _AnalyticsList = value;
                    NotifyPropertyChanged("AnalyticsList");
                }
            }
        }
        #endregion

        #region 結合データ[CombineData]プロパティ
        /// <summary>
        /// 結合データ[CombineData]プロパティ用変数
        /// </summary>
        CombineDataCollectionM _CombineData = new CombineDataCollectionM();
        /// <summary>
        /// 結合データ[CombineData]プロパティ
        /// </summary>
        public CombineDataCollectionM CombineData
        {
            get
            {
                return _CombineData;
            }
            set
            {
                if (_CombineData == null || !_CombineData.Equals(value))
                {
                    _CombineData = value;
                    NotifyPropertyChanged("CombineData");
                }
            }
        }
        #endregion

        #region 実行中フラグ[IsExecute]プロパティ
        /// <summary>
        /// 実行中フラグ[IsExecute]プロパティ用変数
        /// </summary>
        bool _IsExecute = false;
        /// <summary>
        /// 実行中フラグ[IsExecute]プロパティ
        /// </summary>
        public bool IsExecute
        {
            get
            {
                return _IsExecute;
            }
            set
            {
                if (!_IsExecute.Equals(value))
                {
                    _IsExecute = value;
                    NotifyPropertyChanged("IsExecute");
                }
            }
        }
        #endregion

        #region タイトル編集用オブジェクト[ETitle]プロパティ
        /// <summary>
        /// タイトル編集用オブジェクト[ETitle]プロパティ用変数
        /// </summary>
        EditTitleM _ETitle = new EditTitleM();
        /// <summary>
        /// タイトル編集用オブジェクト[ETitle]プロパティ
        /// </summary>
        public EditTitleM ETitle
        {
            get
            {
                return _ETitle;
            }
            set
            {
                if (_ETitle == null || !_ETitle.Equals(value))
                {
                    _ETitle = value;
                    NotifyPropertyChanged("ETitle");
                }
            }
        }
        #endregion

        #region GoogleAnalytics検索条件[AnalyticsSearchCondition]プロパティ
        /// <summary>
        /// GoogleAnalytics検索条件[AnalyticsSearchCondition]プロパティ用変数
        /// </summary>
        GoogleAnalyticsSearchM _AnalyticsSearchCondition = new GoogleAnalyticsSearchM();
        /// <summary>
        /// GoogleAnalytics検索条件[AnalyticsSearchCondition]プロパティ
        /// </summary>
        public GoogleAnalyticsSearchM AnalyticsSearchCondition
        {
            get
            {
                return _AnalyticsSearchCondition;
            }
            set
            {
                if (_AnalyticsSearchCondition == null || !_AnalyticsSearchCondition.Equals(value))
                {
                    _AnalyticsSearchCondition = value;
                    NotifyPropertyChanged("AnalyticsSearchCondition");
                }
            }
        }
        #endregion

        #region ツイッターのツイートタイムライン[TweetTimeline]プロパティ
        /// <summary>
        /// ツイッターのツイートタイムライン[TweetTimeline]プロパティ用変数
        /// </summary>
        ModelList<CoreTweet.Status> _TweetTimeline = new ModelList<CoreTweet.Status>();
        /// <summary>
        /// ツイッターのツイートタイムライン[TweetTimeline]プロパティ
        /// </summary>
        public ModelList<CoreTweet.Status> TweetTimeline
        {
            get
            {
                return _TweetTimeline;
            }
            set
            {
                if (_TweetTimeline == null || !_TweetTimeline.Equals(value))
                {
                    _TweetTimeline = value;
                    NotifyPropertyChanged("TweetTimeline");
                }
            }
        }
        #endregion

        #region ツイートした内容にPV数を付与したもの[TweetPVList]プロパティ
        /// <summary>
        /// ツイートした内容にPV数を付与したもの[TweetPVList]プロパティ用変数
        /// </summary>
        ModelList<StatusEx> _TweetPVList = new ModelList<StatusEx>();
        /// <summary>
        /// ツイートした内容にPV数を付与したもの[TweetPVList]プロパティ
        /// </summary>
        public ModelList<StatusEx> TweetPVList
        {
            get
            {
                return _TweetPVList;
            }
            set
            {
                if (_TweetPVList == null || !_TweetPVList.Equals(value))
                {
                    _TweetPVList = value;
                    NotifyPropertyChanged("TweetPVList");
                }
            }
        }
        #endregion





        #region 初期化処理
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public override async void Init(object sender, EventArgs ev)
        {
            try
            {
                this.Config.Load();

                if (!string.IsNullOrEmpty(this.Config.Url)
                    && !string.IsNullOrEmpty(this.Config.UserAccount)
                    && !string.IsNullOrEmpty(this.Config.Password)
                    )
                {
                    // 接続用Clientの作成
                    await this.WordpressAPI.CreateClient(this.Config.Url, this.Config.UserAccount, this.Config.Password);
                }

                this.AnalyticsSearchCondition.ViewId = this.Config.ViewId;

            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region 画面を閉じる
        /// <summary>
        /// 画面を閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public override void Close(object sender, EventArgs ev)
        {
            try
            {
                // コンフィグの保存処理
                this.Config.Save();

            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region 全記事の取得
        /// <summary>
        /// 全記事の取得
        /// </summary>
        /// <returns>Task</returns>
        private async void GetAllPost()
        {
            try
            {
                this.Articles.Pages.Items.Clear();
                this.Articles.Posts.Items.Clear();

                var tmp = new ArticleCollectionM();
                this.IsExecute = true;

                //// 記事一覧の取得
                var posts = await WordpressAPI.WpClient.Posts.GetAll();
                tmp.Add(new List<Post>(posts));

                //// 固定ページ一覧の取得
                var pages = await WordpressAPI.WpClient.Pages.GetAll();
                tmp.Add(new List<Page>(pages));

                this.Articles = tmp;
                this.IsExecute = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
        }
        #endregion

        #region 記事の取得
        /// <summary>
        /// 記事の取得
        /// </summary>
        public void GetArticles()
        {
            try
            {
                GetAllPost();
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region GoogleAnalyticsの結果取得
        /// <summary>
        /// GoogleAnalyticsの結果取得
        /// </summary>
        public void GetAnalytics()
        {
            try
            {
                Task.Run(() =>
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                       new Action(() =>
                       {
                           this.IsExecute = true;

                           // 記事の削除
                           this.Articles.Analytics.Items.Clear();
                       }));

                    // Analyticsデータの取得処理
                    var results = this.AnalyticsSearchCondition.GetAnalytics(this.Config.GoogleAnalyticsPrivateKey);

                    ArticleCollectionM tmp = new ArticleCollectionM();
                    foreach (var result in results)
                    {
                        var report = result.Reports.First();

                        // Google Analyticsデータの登録
                        tmp.AddAnalytics(report.Data.Rows);
                    }

                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                       new Action(() =>
                       {
                           // Analyticsデータの入れ替え
                           this.Articles.Analytics = tmp.Analytics;
                           this.IsExecute = false;
                       }));
                });

            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region GoogleAnalyticsの結果を開く
        /// <summary>
        /// GoogleAnalyticsの結果を開く
        /// </summary>
        public void OpenAnalytics()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new OpenFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "GoogleAnalytics (*.xlsx)|*.xlsx";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    this.IsExecute = true;
                    GetAnalytics(dialog.FileName, "データセット1");
                    this.IsExecute = false;
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region ニート記事の抽出
        /// <summary>
        /// ニート記事の抽出
        /// </summary>
        public void OutputZeroAccess()
        {
            try
            {
                // ニート記事の抽出
                this.Articles.OutputNeet();
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region アナリティクスデータの取得処理
        /// <summary>
        /// アナリティクスデータの取得処理
        /// </summary>
        /// <param name="path">ファイルパス(.xlsx)</param>
        /// <param name="sheet_name">シート名</param>
        private void GetAnalytics(string path, string sheet_name)
        {
            Task.Run(() =>
            {
                try
                {
                    GoogleAnalyticsCollectionM tmp = new GoogleAnalyticsCollectionM();
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                       new Action(() =>
                       {

                           this.IsExecute = true;
                       }));

                    List<string> col_list = new List<string>();
                    // 読み取り専用で開く
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        // Bookの操作
                        using (XLWorkbook book = new XLWorkbook(fs, XLEventTracking.Disabled))
                        {
                            var sheet = (from x in book.Worksheets
                                         where x.Name.Equals(sheet_name)
                                         select x).FirstOrDefault();

                            // シートが見つからない場合
                            if (sheet == null)
                                return;

                            // ヘッダ情報の抜き出し
                            for (int col = 1; ; col++)
                            {
                                object cell_val = sheet.Cell(1, col).Value; // ヘッダの文字列取得

                                // nullチェックと空文字チェック
                                if (cell_val != null && !string.IsNullOrEmpty(cell_val.ToString()))
                                {
                                    // ヘッダとして文字列が存在するのでカラムリストに追加
                                    col_list.Add(cell_val.ToString());
                                }
                                else
                                {
                                    // ヘッダの存在しないところに来たので抜ける
                                    break;
                                }
                            }


                            int row = 2;    // 先頭行はヘッダとして扱う

                            // ループ
                            while (true)
                            {
                                // 行データを分解して取り出し
                                GoogleAnalyticsM item = new GoogleAnalyticsM(sheet.Row(row++), col_list);

                                // 空文字チェック
                                if (!string.IsNullOrWhiteSpace(item.Page))
                                {
                                    // データが存在するので登録
                                    tmp.GoogleAnalyticsItems.Items.Add(item);
                                }
                                else
                                {
                                    // データが存在しないので抜ける
                                    break;
                                }
                            }
                        }
                    }

                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                       new Action(() =>
                       {
                           // データのセット
                           this.AnalyticsList = tmp;
                           this.IsExecute = false;
                       }));

                }
                catch (Exception e)
                {
                    ShowMessage.ShowErrorOK(e.Message, "Error");
                }
            });
        }
        #endregion

        #region 結合処理
        /// <summary>
        /// 結合処理
        /// </summary>
        public void Combine()
        {
            try
            {
                this.Articles.CombineAnalyticsItems.Items.Clear();
                this.Articles.CombineArticleAndAnalytics();
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region URLの調整処理(末尾の/を取り除く)
        /// <summary>
        /// URLの調整処理(末尾の/を取り除く)
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>調整後URL</returns>
        private string AdjustURL(string url)
        {
            // URLの長さを確認
            if (url.Length > 0)
            {
                // 末尾の/を確認
                if (url.Substring(url.Length - 1, 1).Equals("/"))
                {
                    // あれば取り除く
                    return url.Substring(0, url.Length - 1);
                }
                else
                {
                    // 無ければそのまま返却
                    return url;
                }
            }
            else
            {
                // そのまま返却する
                return url;
            }
        }
        #endregion

        #region ツイートの取得処理
        /// <summary>
        /// ツイートの取得処理
        /// </summary>
        public void GetTweet()
        {
            try
            {
                GetTweet2();
                //var timeline = TwitterAPI.GetUserTimeLine(this.Config.TwScreenName);


                //this.TweetTimeline.Items = new System.Collections.ObjectModel.ObservableCollection<CoreTweet.Status>(timeline);
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        public void GetTweet2()
        {
            try
            {
                var timeline = TwitterAPI.GetTweet("Python");


                this.TweetTimeline.Items = new System.Collections.ObjectModel.ObservableCollection<CoreTweet.Status>(timeline);
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }


        #region 行のダブルクリック処理
        /// <summary>
        /// 行のダブルクリック処理
        /// </summary>
        public void RowDoubleClick()
        {
            try
            {
                ProcessStartInfo pi = new ProcessStartInfo()
                {
                    FileName = this.Articles.CombineAnalyticsItems.SelectedItem.Link,
                    UseShellExecute = true,
                };

                Process.Start(pi);
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        /// <summary>
        /// Excelファイルの保存処理
        /// </summary>
        public void SaveExcel()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "Excelファイル (*.xlsx)|*.xlsx";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    XLWorkbook book = new XLWorkbook();
                    book.Worksheets.Add("出力");
                    int row = 1, col = 1;
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ID";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "タイトル";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "カテゴリ";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "文字数";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "文字数(タグ除外)";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "タイトル文字数";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "URL";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ツイッターPV";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "オーガニックサーチPV";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ダイレクトPV";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "リファラーPV";

                    foreach (var tmp in this.Articles.CombineAnalyticsItems.Items)
                    {
                        col = 1;
                        row++;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.Id;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.Title;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.CategoriesText;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.ContentLength;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.ContentLength2;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.TitleLength;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.Link;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.TwitterPageViews;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.OrganicPageViews;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.DirectPageViews;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.ReferralPageViews;
                    }

                    book.SaveAs(dialog.FileName);
                    ShowMessage.ShowNoticeOK("ファイルを保存しました。", "通知");

                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }

        public void SaveExcelZeroArticle()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "Excelファイル (*.xlsx)|*.xlsx";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    XLWorkbook book = new XLWorkbook();
                    book.Worksheets.Add("出力");
                    int row = 1, col = 1;
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "メッセージ(全角140文字 半角280文字まで)";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "URL(23文字+改行1文字扱い)";

                    foreach (var tmp in this.Articles.ZeroTitles.Items)
                    {
                        // 投稿の出力をしない場合
                        if (tmp.Type.Equals("post") && !this.Config.IsPostForZeroOut)
                            continue;

                        // 固定ページの出力をしない場合
                        if (tmp.Type.Equals("page") && !this.Config.IsPageForZeroOut)
                            continue;

                        if (!string.IsNullOrEmpty(this.Config.OutputCategoryType))
                        {
                            // カテゴリがセットされていないので対象外
                            if (tmp.Categories == null)
                                continue;

                            bool is_execute = false;
                            foreach (var cate in tmp.Categories)
                            {
                                if ((from x in this.Config.OutputCategoryType.Split(",")
                                     where x.Trim().Equals(cate.ToString())
                                     select x).Any()
                                    )
                                {
                                    is_execute = true;
                                    break;
                                }
                            }

                            // カテゴリが合致しなかった
                            if (!is_execute)
                                continue;
                        }

                        col = 1;
                        row++;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = this.Config.HashtagZeroOut + "\r\n" + tmp.Title;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.Link;
                    }

                    book.SaveAs(dialog.FileName);
                    ShowMessage.ShowNoticeOK("ファイルを保存しました。", "通知");


                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }

        /// <summary>
        /// タイトルのセット
        /// </summary>
        public void SetTitle()
        {
            try
            {
                if (this.CombineData.CombineDataList.SelectedItem != null)
                {
                    this.ETitle.Title = this.CombineData.CombineDataList.SelectedItem.WordPress.Title;
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }

        #region 秘密鍵のファイルパスを開く
        /// <summary>
        /// 秘密鍵のファイルパスを開く
        /// </summary>
        public void OpenPrivateKey()
        {
            // ダイアログのインスタンスを生成
            var dialog = new OpenFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "JSONファイル (*.json)|*.json";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                this.Config.GoogleAnalyticsPrivateKey = dialog.FileName;
            }
        }
        #endregion

        #region ツイート内容のPV数を計算
        /// <summary>
        /// ツイート内容のPV数を計算
        /// </summary>
        public void CalcTweetPV()
        {

            try
            {
                List<StatusEx> list = new List<StatusEx>();
                foreach (var tweet in this.TweetTimeline.Items)
                {
                    // nullチェック URLが登録されているもののみ抽出
                    if (tweet != null && tweet.Entities != null && tweet.Entities.Urls.Count() > 0)
                    {
                        var tmp = new StatusEx();
                        tmp.CreatedAt = tweet.CreatedAt;
                        tmp.Id = tweet.Id;
                        tmp.Text = tweet.Text;
                        tmp.FavoriteCount = tweet.FavoriteCount;
                        tmp.RetweetCount = tweet.RetweetCount;
                        tmp.Entities = tweet.Entities;

                        var analytics = (from x in this.Articles.Analytics.Items
                                         // Twitterのみ抽出
                                         where x.SocialNetwork.Equals("Twitter")
                                            // Google Analytics結果のランディングページとPV数を数えているページが一致しているもののみ抽出
                                            && x.LandingPage.Equals(x.Page)
                                            // TwitterのURLとGoogleAnalyticsのURLが合致しているもののみ抽出
                                            && x.LandingPage.Equals(tweet.Entities.Urls.ElementAt(0).ExpandedUrl.Replace(PathManager.TrimLastText(this.Config.Url,"/"), ""))
                                            // Tweet日とGoogle Analyticsの日が一致するものを抽出
                                            && x.TargetDate.Equals(tmp.CreatedAt.LocalDateTime.Date)
                                         select x).FirstOrDefault();

                        // 合致するものが見つかった
                        if (analytics != null)
                        {
                            // 追加
                            tmp.PageView = analytics.PageViews;
                        }

                        list.Add(tmp);
                    }
                }
                this.TweetPVList.Items = new System.Collections.ObjectModel.ObservableCollection<StatusEx>(list);
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        /// <summary>
        /// TweetPV用のExcelファイルの保存処理
        /// </summary>
        public void SaveExcelTweetPV()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "Excelファイル (*.xlsx)|*.xlsx";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    XLWorkbook book = new XLWorkbook();
                    book.Worksheets.Add("出力");
                    int row = 1, col = 1;
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ID";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ツイート日時";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ツイート内容";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "いいね数";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "リツイート数";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "PV数";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "URL";

                    foreach (var tmp in this.TweetPVList.Items)
                    {
                        col = 1;
                        row++;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.Id;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.CreatedAt.LocalDateTime;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.Text;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.FavoriteCount;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.RetweetCount;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.PageView;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.Entities != null && tmp.Entities.Urls.Any() ? tmp.Entities.Urls.ElementAt(0).ExpandedUrl : string.Empty;
                    }

                    book.SaveAs(dialog.FileName);
                    ShowMessage.ShowNoticeOK("ファイルを保存しました。", "通知");


                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }

        /// <summary>
        /// TweetPV用のExcelファイルの保存処理
        /// </summary>
        public void SaveExcelGoogleAnalytics()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "Excelファイル (*.xlsx)|*.xlsx";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    XLWorkbook book = new XLWorkbook();
                    book.Worksheets.Add("出力");
                    int row = 1, col = 1;
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "対象日";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "グループ";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ソーシャル";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ページ";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ページタイトル";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ランディングページ";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ページビュー数";

                    foreach (var tmp in Articles.Analytics.Items)
                    {
                        col = 1;
                        row++;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.TargetDate;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.DefaultChannelGroup;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.SocialNetwork;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.Page;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.PageTitle;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.LandingPage;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.PageViews;
                    }

                    book.SaveAs(dialog.FileName);
                    ShowMessage.ShowNoticeOK("ファイルを保存しました。", "通知");

                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }


        /// <summary>
        /// TweetPV用のExcelファイルの保存処理
        /// </summary>
        public void SaveExcelTweetContent()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "Excelファイル (*.xlsx)|*.xlsx";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    XLWorkbook book = new XLWorkbook();
                    book.Worksheets.Add("出力");
                    int row = 1, col = 1;
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ID";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "スクリーン名";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "フォロー数";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "フォロワー数";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ツイート日時";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "ツイート内容";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "いいね数";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "リツイート数";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "本家(いいね)";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "本家(リツイートカウント)";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "本家(スクリーン名)";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "本家(フォロー数)";
                    book.Worksheets.ElementAt(0).Cell(row, col++).Value = "本家(フォロワー数)";

                    foreach (var tmp in TweetTimeline.Items)
                    {
                        col = 1;
                        row++;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.Id;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.User.ScreenName;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.User.FriendsCount;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.User.FollowersCount;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.CreatedAt.DateTime.ToString("yyyy/MM/dd HH:mm:ss");
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.Text;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.FavoriteCount;
                        book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.RetweetCount;
                        if (tmp.RetweetedStatus != null)
                        {
                            book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.RetweetedStatus.FavoriteCount;
                            book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.RetweetedStatus.RetweetCount;
                            book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.RetweetedStatus.User.ScreenName;
                            book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.RetweetedStatus.User.FriendsCount;
                            book.Worksheets.ElementAt(0).Cell(row, col++).Value = tmp.RetweetedStatus.User.FollowersCount;
                        }
                    }

                    book.SaveAs(dialog.FileName);
                    ShowMessage.ShowNoticeOK("ファイルを保存しました。", "通知");

                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
    }
}
