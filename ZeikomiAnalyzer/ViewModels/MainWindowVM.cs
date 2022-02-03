using ClosedXML.Excel;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WordPressPCL.Models;
using ZeikomiAnalyzer.Common.Config;
using ZeikomiAnalyzer.Models;

namespace ZeikomiAnalyzer.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        #region コンフィグ情報[Config]プロパティ
        /// <summary>
        /// コンフィグ情報[Config]プロパティ用変数
        /// </summary>
        ZeikomiAnalyzerConfigM _Config = new ZeikomiAnalyzerConfigM();
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
                this.IsExecute = true;
                // 一旦削除
                this.Articles.Clear();

                //// 記事一覧の取得
                var posts = await WordpressAPI.WpClient.Posts.GetAll();
                this.Articles.Add(new List<Post>(posts));

                //// 固定ページ一覧の取得
                var pages = await WordpressAPI.WpClient.Pages.GetAll();
                this.Articles.Add(new List<Page>(pages));
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
                // データのクリア
                this.CombineData.Clear();

                // 記事数分回す
                foreach (var article in this.Articles.Articles.Items)
                {
                    string link = article.Link;                 // リンク情報の取得
                    string url = AdjustURL(this.Config.Url);    // 末尾の/を取り除くいてURLを調整する

                    // WordPressの記事とアナリティクスデータの各URLが一致するものを取り出す
                    var tmp = (from x in this.AnalyticsList.GoogleAnalyticsItems.Items
                               where (url + x.Page).Equals(link)
                               select x).FirstOrDefault();

                    // 一致する場合
                    if (tmp != null)
                    {
                        // ワードプレス記事とアナリティクスデータを結合して登録
                        this.CombineData.Add(article, tmp);
                    }
                    // 一致しない場合
                    else
                    {
                        // ワードプレス記事を登録する
                        // アナリティクスデータはないので全て0で登録
                        this.CombineData.Add(article, new GoogleAnalyticsM());
                    }
                }
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
    }
}
