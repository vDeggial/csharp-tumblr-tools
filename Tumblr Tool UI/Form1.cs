/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: November, 2017
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using KRBTabControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Objects;
using Tumblr_Tool.Objects.Tumblr_Objects;
using Tumblr_Tool.Properties;

namespace Tumblr_Tool
{
    public partial class MainForm : Form
    {
        private const string AppCopyright = "© 2013 - 2017 Shino Amakusa\r\n" + AppLinkUrl;
        private const string AppLinkUrl = "git.io/v9S3h";
        private const string AppVersion = "1.5.8";
        private const string FileSizeFormat = "{0} {1}";
        private const string ImageSizeLarge = "Large";
        private const string ImageSizeMedium = "Medium";
        private const string ImageSizeOriginal = "Original";
        private const string ImageSizeSmall = "Small";
        private const string ImageSizeSquare = "Square";
        private const string ImageSizeXsmall = "xSmall";
        private const string ModeFullRescan = "Full Rescan";
        private const string ModeNewestOnly = "Newest Only";
        private const string PercentFormat = "{0}%";
        private const string PostCountFormat = "{0}/{1}";
        private const string ResultDone = " done";
        private const string ResultError = "error";
        private const string ResultFail = " fail";
        private const string ResultOk = " ok";
        private const string ResultSuccess = " success";
        private const string StatusCheckConnx = "Checking connection ...";
        private const string StatusDone = "Done";
        private const string StatusDownloadConnecting = "...";
        private const string StatusDownloadingFormat = "Downloading: {0}";
        private const string StatusError = "Error";
        private const string StatusGettingInfo = "Getting blog info ...";
        private const string StatusGettingStats = "Getting stats ...";
        private const string StatusGettingTags = "Getting tags ...";
        private const string StatusIndexing = "Indexing ...";
        private const string StatusOpenSaveFile = "Opening save file ...";
        private const string StatusProcessStarted = "Here we go ...";
        private const string StatusReady = "Ready";
        private const string StatusStarting = "Starting ...";
        private const string SuffixGb = "GB";
        private const string SuffixKb = "KB";
        private const string SuffixMb = "MB";
        private const string WelcomeMsg = "\r\n\r\n\r\n\r\nWelcome to Tumblr Tools!\r\nVersion: " + AppVersion + "\r\n© 2013 - 2017 Shino Amakusa\r\n" + AppLinkUrl;
        private const string WorktextCheckingConnx = "Checking connection ...";
        private const string WorktextDownloadingImages = "Downloading ...";
        private const string WorktextGettingBlogInfo = "Getting info ...";
        private const string WorktextIndexingPosts = "Indexing ...";
        private const string WorktextReadingLog = "Reading log ...";
        private const string WorktextSavingLog = "Saving log ...";
        private const string WorktextStarting = "Starting ...";
        private const string WorktextUpdatingLog = "Updating log...";
        private readonly AutoResetEvent _readyToDownload = new AutoResetEvent(false);
        private readonly AutoResetEvent _readyToGetStats = new AutoResetEvent(false);

        public MainForm()
        {
            InitializeComponent();
            GlobalInitialize();
        }

        public MainForm(string file)
        {
            InitializeComponent();

            GlobalInitialize();

            txt_Crawler_SaveLocation.Text = Path.GetDirectoryName(file);

            UpdateStatusText(StatusOpenSaveFile);

            SaveFile_Open(file);
        }

        private Dictionary<string, BlogPostsScanMode> BlogPostsScanModesDict { get; set; }
        private string CurrentImage { get; set; }
        private int CurrentPercent { get; set; }
        private int CurrentPostCount { get; set; }
        private int CurrentSelectedTab { get; set; }
        private decimal CurrentSize { get; set; }
        private bool DisableOtherTabs { get; set; }
        private List<string> DownloadedList { get; set; }
        private List<int> DownloadedSizesList { get; set; }
        private FileDownloadManager DownloadManager { get; set; }
        private string ErrorMessage { get; set; }
        private Dictionary<string, ImageSize> ImageSizesIndexDict { get; set; }
        private bool IsCrawlingCancelled { get; set; }
        private bool IsCrawlingDone { get; set; }
        private bool IsDownloadDone { get; set; }
        private bool IsExitTime { get; set; }
        private bool IsFileDownloadDone { get; set; }
        private List<string> NotDownloadedList { get; set; }
        private ToolOptions Options { get; set; }
        private string OptionsFileName { get; set; }
        private PhotoPostParseManager PhotoPostParser { get; set; }
        private string SaveLocation { get; set; }
        private TagScanManager TagScanner { get; set; }
        private SaveFile TumblrLogFile { get; set; }

        private SaveFile TumblrSaveFile { get; set; }

        private TumblrStatsManager TumblrStats { get; set; }

        private string TumblrUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        private void AddWorkStatusText(string str)
        {
            if (!txt_Crawler_WorkStatus.Text.EndsWith(str))
            {
                txt_Crawler_WorkStatus.Text = new StringBuilder(txt_Crawler_WorkStatus.Text).Append(str).ToString();

                txt_Crawler_WorkStatus.Update();
                txt_Crawler_WorkStatus.Refresh();
            }
        }

        private void BrowseLocalPath(object sender, EventArgs e)
        {
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)

                    txt_Crawler_SaveLocation.Text = ofd.SelectedPath;
            }
        }

        private void Button_OnMouseEnter(object sender, EventArgs e)
        {
            Button button = new Button();
            if (sender is Button) button = sender as Button;

            if (button != null && button is Button)
            {
                button.UseVisualStyleBackColor = false;
                button.ForeColor = Color.Maroon;
                button.FlatAppearance.BorderColor = Color.Maroon;
                button.FlatAppearance.MouseOverBackColor = Color.White;
                button.FlatAppearance.BorderSize = 1;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OnMouseLeave(object sender, EventArgs e)
        {
            Button button = new Button();
            if (sender is Button) button = sender as Button;
            if (button != null)
            {
                button.UseVisualStyleBackColor = true;
                button.ForeColor = Color.Black;
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.White;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_PhotoPostParse(object sender, EventArgs e)
        {
            IsCrawlingCancelled = true;
            if (PhotoPostParser != null)
            {
                PhotoPostParser.IsCancelled = true;
            }

            IsDownloadDone = true;
            EnableUI_Crawl(true);

            UpdateWorkStatusTextNewLine("Operation cancelled ...");
            img_Crawler_DisplayImage.Image = Resources.tumblrlogo;
            //MsgBox.Show("Current operation has been cancelled successfully!", "Cancel", MsgBox.Buttons.OK, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn, false);
            UpdateStatusText(StatusReady);
        }

        private void Cancel_TagScanner(object sender, EventArgs e)
        {
            IsCrawlingCancelled = true;
            if (TagScanner != null)
            {
                TagScanner.IsCancelled = true;
            }

            EnableUI_TagScanner(true);

            //MsgBox.Show("Current operation has been cancelled successfully!", "Cancel", MsgBox.Buttons.OK, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn, false);
            UpdateStatusText(StatusReady);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrawlWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (PhotoPostParser != null)
                {
                    IsCrawlingDone = true;

                    if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.Done)
                    {
                        TumblrSaveFile.Blog = PhotoPostParser.Blog;
                        //tumblrLogFile = null;
                        //ripper.tumblrPostLog = null;

                        TumblrLogFile = PhotoPostParser.TumblrPostLog;

                        IsCrawlingDone = true;

                        if (IsCrawlingDone && !check_Options_ParseOnly.Checked && !IsCrawlingCancelled && PhotoPostParser.ImageList.Count != 0)
                        {
                            DownloadManager.TotalFilesToDownload = PhotoPostParser.ImageList.Count;

                            IsDownloadDone = false;
                            DownloadedList = new List<string>();
                            NotDownloadedList = new List<string>();
                            DownloadedSizesList = new List<int>();

                            imageDownloadWorkerUI.RunWorkerAsync();

                            imageDownloadWorker.RunWorkerAsync(PhotoPostParser.ImageList);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrawlWorker_Work(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread.Sleep(200);

                IsCrawlingCancelled = false;

                lock (PhotoPostParser)
                {
                    if (TumblrSaveFile != null && Options.GenerateLog)
                    {
                        string file = new StringBuilder(SaveLocation).Append(@"\")
                            .Append(Path.GetFileNameWithoutExtension(TumblrSaveFile.Filename)).Append(".log").ToString();

                        if (File.Exists(file))
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                UpdateStatusText(WorktextReadingLog);
                                UpdateWorkStatusTextNewLine(WorktextReadingLog);
                            });

                            TumblrLogFile = FileHelper.ReadTumblrFile(file);

                            Invoke((MethodInvoker)delegate
                            {
                                UpdateWorkStatusTextConcat(WorktextReadingLog, ResultDone);
                            });
                        }
                    }

                    //this.tumblrBlog = new TumblrBlog();
                    //this.tumblrBlog.url = this.tumblrURL;

                    PhotoPostParser = new PhotoPostParseManager(new TumblrBlog(TumblrUrl), SaveLocation,
                        check_Options_GenerateLog.Checked, check_Options_ParsePhotoSets.Checked,
                        check_Options_ParseJPEG.Checked, check_Options_ParsePNG.Checked, check_Options_ParseGIF.Checked)
                    {
                        TumblrPostLog = TumblrLogFile
                    };
                    Invoke((MethodInvoker)delegate
                    {
                        PhotoPostParser.ImageSize = ImageSizesIndexDict[select_Crawler_ImagesSize.Items[select_Crawler_ImagesSize.SelectedIndex].ToString()];
                    });
                    PhotoPostParser.ProcessingStatusCode = ProcessingCode.Initializing;
                }

                lock (PhotoPostParser)
                {
                    PhotoPostParser.ProcessingStatusCode = ProcessingCode.CheckingConnection;
                }

                if (WebHelper.CheckForInternetConnection())
                {
                    lock (PhotoPostParser)
                    {
                        PhotoPostParser.ProcessingStatusCode = ProcessingCode.ConnectionOk;
                    }

                    if (PhotoPostParser != null)
                    {
                        //this.ImageRipper.SetAPIMode((ApiModeEnum) Enum.Parse(typeof(ApiModeEnum), Options.ApiMode));
                        PhotoPostParser.ApiVersion = TumblrApiVersion.V2Json;
                        PhotoPostParser.TumblrPostLog = TumblrLogFile;

                        if (PhotoPostParser.TumblrExists())
                        {
                            lock (PhotoPostParser)
                            {
                                PhotoPostParser.ProcessingStatusCode = ProcessingCode.GettingBlogInfo;
                            }

                            if (PhotoPostParser.GetTumblrBlogInfo())
                            {
                                lock (PhotoPostParser)
                                {
                                    PhotoPostParser.ProcessingStatusCode = ProcessingCode.BlogInfoOk;
                                }

                                if (!SaveFile_Save(PhotoPostParser.Blog.Name))
                                {
                                    lock (PhotoPostParser)
                                    {
                                        PhotoPostParser.ProcessingStatusCode = ProcessingCode.SaveFileError;
                                    }
                                }
                                else
                                {
                                    lock (PhotoPostParser)
                                    {
                                        PhotoPostParser.ProcessingStatusCode = ProcessingCode.SaveFileOk;
                                    }

                                    if (PhotoPostParser != null)
                                    {
                                        BlogPostsScanMode mode = BlogPostsScanMode.NewestPostsOnly;
                                        Invoke((MethodInvoker)delegate
                                        {
                                            mode = BlogPostsScanModesDict[select_Crawler_Mode.SelectedItem.ToString()];
                                        });

                                        lock (PhotoPostParser)
                                        {
                                            PhotoPostParser.ProcessingStatusCode = ProcessingCode.Crawling;
                                        }

                                        PhotoPostParser.ParseAllBlogPhotoPosts(mode);

                                        lock (PhotoPostParser)
                                        {
                                            if (PhotoPostParser.IsLogUpdated)
                                            {
                                                PhotoPostParser.ProcessingStatusCode = ProcessingCode.SavingLogFile;

                                                if (!IsDisposed)
                                                {
                                                    Invoke((MethodInvoker)delegate
                                                    {
                                                        UpdateStatusText(WorktextSavingLog);
                                                        UpdateWorkStatusTextNewLine(WorktextSavingLog);
                                                    });
                                                }
                                                TumblrLogFile = PhotoPostParser.TumblrPostLog;
                                                LogFile_Save();

                                                if (!IsDisposed)
                                                {
                                                    Invoke((MethodInvoker)delegate
                                                    {
                                                        UpdateStatusText("Log Saved");
                                                        UpdateWorkStatusTextConcat(WorktextSavingLog, ResultDone);
                                                    });

                                                    PhotoPostParser.ProcessingStatusCode = ProcessingCode.Done;
                                                }
                                            }
                                        }
                                    }
                                }

                                lock (PhotoPostParser)
                                {
                                    PhotoPostParser.ProcessingStatusCode = ProcessingCode.Done;
                                }
                            }
                            else
                            {
                                lock (PhotoPostParser)
                                {
                                    PhotoPostParser.ProcessingStatusCode = ProcessingCode.BlogInfoError;
                                }
                            }
                        }
                        else
                        {
                            lock (PhotoPostParser)
                            {
                                PhotoPostParser.ProcessingStatusCode = ProcessingCode.InvalidUrl;
                            }
                        }
                    }
                }
                else
                {
                    lock (PhotoPostParser)
                    {
                        PhotoPostParser.ProcessingStatusCode = ProcessingCode.ConnectionError;
                    }
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrawlWorkerUI_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        if (check_Options_ParseOnly.Checked)
                        {
                            EnableUI_Crawl(true);
                        }
                        else if (PhotoPostParser.ProcessingStatusCode != ProcessingCode.Done)
                        {
                            EnableUI_Crawl(true);
                        }
                        else
                        {
                            if (IsCrawlingDone && !check_Options_ParseOnly.Checked && !IsCrawlingCancelled && PhotoPostParser.ImageList.Count != 0)
                            {
                                UpdateStatusText(string.Format(StatusDownloadingFormat, "Prepairing to download ..."));
                                bar_Progress.Value = 0;

                                lbl_PercentBar.Text = string.Format(PercentFormat, "0");

                                lbl_PostCount.Text = string.Format(PostCountFormat, "0", PhotoPostParser.ImageList.Count);
                            }
                            else
                            {
                                UpdateStatusText(StatusReady);
                                img_Crawler_DisplayImage.Image = Resources.tumblrlogo;
                                EnableUI_Crawl(true);
                            }
                        }
                    });
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrawlWorkerUI_Work(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (PhotoPostParser != null)
                {
                    if (!IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                                {
                                    img_Crawler_DisplayImage.Image = Resources.crawling;
                                });
                    }

                    while (!IsCrawlingDone)
                    {
                        if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.CheckingConnection)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(WorktextCheckingConnx);
                                    UpdateStatusText(StatusCheckConnx);
                                });
                            }
                        }

                        if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.ConnectionOk)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextConcat(WorktextCheckingConnx, ResultSuccess);
                                    UpdateWorkStatusTextNewLine(WorktextStarting);
                                    UpdateWorkStatusTextNewLine(WorktextGettingBlogInfo);
                                    UpdateStatusText(StatusGettingInfo);
                                });
                            }
                        }
                        if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.ConnectionError)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateStatusText(StatusError);
                                    UpdateWorkStatusTextConcat(WorktextCheckingConnx, ResultFail);
                                    btn_Crawler_Start.Enabled = true;
                                    img_Crawler_DisplayImage.Visible = true;
                                    img_Crawler_DisplayImage.Image = Resources.tumblrlogo;
                                    tab_TumblrStats.Enabled = true;
                                    IsCrawlingDone = true;
                                });
                            }
                        }

                        if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.BlogInfoOk)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextConcat(WorktextGettingBlogInfo, ResultSuccess);

                                    txt_Crawler_WorkStatus.Visible = true;

                                    txt_Crawler_WorkStatus.SelectionStart = txt_Crawler_WorkStatus.Text.Length;
                                });
                            }
                        }

                        if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.Starting)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(new StringBuilder(
                                        "Indexing ").Append("\"").Append(PhotoPostParser.Blog.Title).Append("\" ... ").ToString());
                                });
                            }
                        }

                        if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.UnableDownload)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(
                                        new StringBuilder("Unable to get post info from API (Offset ")
                                        .Append(PhotoPostParser.NumberOfParsedPosts.ToString()).Append(" - ")
                                        .Append((PhotoPostParser.NumberOfParsedPosts + (int)NumberOfPostsPerApiDocument.ApiV2).ToString())
                                        .Append(") ... ").ToString());
                                });
                            }
                            PhotoPostParser.ProcessingStatusCode = ProcessingCode.Crawling;
                        }

                        if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.Crawling)
                        {
                            if (PhotoPostParser.TotalNumberOfPosts != 0 && PhotoPostParser.NumberOfParsedPosts == 0)
                            {
                                if (!IsDisposed)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        UpdateWorkStatusTextNewLine(new StringBuilder(PhotoPostParser.TotalNumberOfPosts.ToString())
                                            .Append(" photo posts found.").ToString());
                                    });
                                }
                            }

                            if (!IsDisposed && PhotoPostParser.NumberOfParsedPosts == 0)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(WorktextIndexingPosts);
                                    UpdateStatusText(StatusIndexing);

                                    //this.lbl_PostCount.Text = string.Format(_POSTCOUNT, "0", this.ripper.totalNumberOfPosts.ToString());
                                    //this.lbl_PercentBar.Text = string.Format(_PERCENT, "0");

                                    //this.lbl_PostCount.Text = string.Empty;
                                    //this.lbl_PercentBar.Text = string.Empty;

                                    //this.lbl_PostCount.Visible = true;
                                });
                            }

                            var percent = PhotoPostParser.PercentComplete;

                            if (percent > 100)
                                percent = 100;

                            if (CurrentPercent != percent)
                            {
                                CurrentPercent = percent;
                                if (!IsDisposed)
                                {
                                    var percent1 = percent;
                                    Invoke((MethodInvoker)delegate
                                       {
                                           //if (!this.lbl_PercentBar.Visible)
                                           //{
                                           //    this.lbl_PercentBar.Visible = true;
                                           //}

                                           lbl_PercentBar.Text = string.Format(PercentFormat, percent1);
                                           bar_Progress.Value = percent1;
                                       });
                                }
                            }

                            if (CurrentPostCount != PhotoPostParser.NumberOfParsedPosts)
                            {
                                CurrentPostCount = PhotoPostParser.NumberOfParsedPosts;
                                if (!IsDisposed)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        //if (!this.lbl_PostCount.Visible)
                                        //{
                                        //    this.lbl_PostCount.Visible = true;
                                        //}
                                        if (lbl_PostCount.DisplayStyle != ToolStripItemDisplayStyle.ImageAndText)
                                        {
                                            lbl_PostCount.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                                        }
                                        lbl_PostCount.Text = string.Format(PostCountFormat, PhotoPostParser.NumberOfParsedPosts, PhotoPostParser.TotalNumberOfPosts);
                                    });
                                }
                            }
                        }
                    }

                    if (PhotoPostParser != null)
                    {
                        if (!IsDisposed && PhotoPostParser.ProcessingStatusCode == ProcessingCode.Done && !IsCrawlingCancelled)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                UpdateWorkStatusTextConcat(WorktextIndexingPosts, ResultDone);

                                UpdateWorkStatusTextNewLine(new StringBuilder("Found ")
                                    .Append(PhotoPostParser.ImageList.Count == 0 ? "no" : PhotoPostParser.ImageList.Count.ToString())
                                    .Append(" new image(s) to download").ToString());

                                bar_Progress.Value = 0;
                                lbl_PercentBar.Text = string.Empty;
                            });
                        }

                        if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.UnableDownload)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Error downloading the blog post XML");
                                    UpdateStatusText(StatusError);
                                    EnableUI_Crawl(true);
                                });
                            }
                        }
                        if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.InvalidUrl)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Invalid Tumblr URL");
                                    UpdateStatusText(StatusError);
                                    EnableUI_Crawl(true);
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            lock (DownloadManager)
            {
                DownloadManager.DownloadStatusCode = DownloadStatusCode.Done;
            }

            try
            {
                TumblrSaveFile.Blog.Posts = null;
                SaveFile_Save(PhotoPostParser.Blog.Name);

                IsDownloadDone = true;
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadWorker_Work(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(200);
            try
            {
                DownloadedList = new List<string>();
                DownloadedSizesList = new List<int>();
                IsDownloadDone = false;
                IsFileDownloadDone = false;
                IsCrawlingCancelled = false;
                HashSet<PhotoPostImage> imagesList = (HashSet<PhotoPostImage>)e.Argument;
                DownloadManager.TotalFilesToDownload = imagesList.Count;

                lock (DownloadManager)
                {
                    DownloadManager.DownloadStatusCode = DownloadStatusCode.Preparing;
                }

                _readyToDownload.WaitOne();

                if (imagesList.Count != 0)
                {
                    lock (DownloadManager)
                    {
                        DownloadManager.DownloadStatusCode = DownloadStatusCode.Downloading;
                    }

                    if (Options.OldToNewDownloadOrder)
                    {
                        imagesList = imagesList.Reverse().ToHashSet();
                    }

                    foreach (PhotoPostImage photoImage in imagesList)
                    {
                        if (IsCrawlingCancelled)
                            break;

                        IsFileDownloadDone = false;

                        lock (DownloadManager)
                        {
                            DownloadManager.DownloadStatusCode = DownloadStatusCode.Downloading;
                        }

                        string fullPath = string.Empty;

                        FileInfo file;
                        while (!IsFileDownloadDone && !IsCrawlingCancelled)
                        {
                            try
                            {
                                fullPath = FileHelper.GenerateLocalPathToFile(photoImage.Filename, SaveLocation);

                                var downloaded = DownloadManager.DownloadFile(DownloadMethod.WebClientAsync, photoImage.Url, SaveLocation);

                                if (downloaded)
                                {
                                    IsFileDownloadDone = true;
                                    fullPath = FileHelper.AddJpgExt(fullPath);

                                    file = new FileInfo(fullPath);

                                    lock (DownloadedList)
                                    {
                                        DownloadedList.Add(fullPath);
                                    }

                                    lock (DownloadedSizesList)
                                    {
                                        DownloadedSizesList.Add((int)file.Length);
                                    }
                                }
                                else if (DownloadManager.DownloadStatusCode == DownloadStatusCode.UnableDownload)
                                {
                                    lock (NotDownloadedList)
                                    {
                                        NotDownloadedList.Add(photoImage.Url);
                                    }

                                    if (FileHelper.FileExists(fullPath))
                                    {
                                        file = new FileInfo(fullPath);

                                        if (!FileHelper.IsFileLocked(file)) file.Delete();
                                    }
                                }
                            }
                            catch
                            {
                                lock (NotDownloadedList)
                                {
                                    NotDownloadedList.Add(photoImage.Url);
                                }
                                if (FileHelper.FileExists(fullPath))
                                {
                                    file = new FileInfo(fullPath);

                                    if (!FileHelper.IsFileLocked(file)) file.Delete();
                                }
                            }
                            finally
                            {
                                IsFileDownloadDone = true;
                            }
                        }

                        if (IsCrawlingCancelled)
                        {
                            if (FileHelper.FileExists(fullPath))
                            {
                                file = new FileInfo(fullPath);

                                if (!FileHelper.IsFileLocked(file)) file.Delete();
                            }
                        }
                    }

                    IsDownloadDone = true;
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadWorkerUI_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!IsDisposed)
                {
                    try
                    {
                        if (DownloadedList.Count == 0)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                img_Crawler_DisplayImage.Image = Resources.tumblrlogo;
                            });
                        }
                        else
                        {
                            if (!IsCrawlingCancelled)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    Image img = FileHelper.GetImageFromFile(DownloadedList[DownloadedList.Count - 1]);

                                    if (img != null)
                                    {
                                        img_Crawler_DisplayImage.Image = FileHelper.GetImageFromFile(DownloadedList[DownloadedList.Count - 1]);
                                    }

                                    lbl_PostCount.Text = string.Format(PostCountFormat, DownloadedList.Count, PhotoPostParser.ImageList.Count);
                                    bar_Progress.Visible = false;
                                    lbl_PercentBar.Text = string.Empty;
                                });
                            }
                        }
                    }
                    catch
                    {
                        //
                    }
                    Invoke((MethodInvoker)delegate
                    {
                        img_Crawler_DisplayImage.Update();
                        img_Crawler_DisplayImage.Refresh();
                    });

                    if (DownloadedList.Count > 0)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(WorktextDownloadingImages, ResultDone);
                            UpdateWorkStatusTextNewLine(new StringBuilder("Downloaded ").Append(DownloadedList.Count.ToString()).Append(" image(s).").ToString());
                            bar_Progress.Value = 0;
                            lbl_PercentBar.Text = string.Empty;
                        });
                    }
                    else
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(WorktextDownloadingImages, ResultError);
                            UpdateWorkStatusTextNewLine("We were unable to download images ...");
                            bar_Progress.Value = 0;
                            lbl_PercentBar.Text = string.Empty;
                            lbl_PostCount.Visible = false;
                            lbl_Size.Visible = false;
                        });
                    }

                    if (NotDownloadedList.Count > 0)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(WorktextDownloadingImages, ResultDone);
                            UpdateWorkStatusTextNewLine(new StringBuilder("Failed: ").Append(NotDownloadedList.Count.ToString()).Append(" image(s).").ToString());
                            bar_Progress.Value = 0;
                            lbl_PercentBar.Text = string.Empty;
                        });
                    }

                    Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText(StatusDone);
                        EnableUI_Crawl(true);
                    });
                }

                if (Options.GenerateLog)
                {
                    PhotoPostParser.TumblrPostLog = null;
                    TumblrLogFile = null;
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadWorkerUI_Work(object sender, DoWorkEventArgs e)
        {
            CurrentPercent = 0;
            CurrentPostCount = 0;
            CurrentSize = 0;
            try
            {
                if (DownloadManager == null) DownloadManager = new FileDownloadManager();

                if (PhotoPostParser.ImageList.Count == 0)
                {
                    if (!IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateStatusText(StatusDone);
                        });
                    }
                }
                else
                {
                    decimal totalLength = 0;

                    if (!IsDisposed && DownloadedList.Count < 10)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextNewLine(WorktextDownloadingImages);
                            lbl_Size.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                        });

                        _readyToDownload.Set();
                    }

                    while (!IsDownloadDone && !IsCrawlingCancelled)
                    {
                        int c = 0;
                        int f = 0;

                        if (NotDownloadedList.Count != 0 && f != NotDownloadedList.Count)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    lbl_PostCount.ForeColor = Color.Maroon;
                                    bar_Progress.BarColor = Color.Maroon;
                                    bar_Progress.Update();
                                    bar_Progress.Refresh();
                                    lbl_PercentBar.ForeColor = Color.Maroon;

                                    ErrorMessage = new StringBuilder("Error: Unable to download ").Append(NotDownloadedList[NotDownloadedList.Count - 1]).ToString();
                                    UpdateWorkStatusTextNewLine(ErrorMessage);
                                });
                            }
                        }

                        if (DownloadedList.Count != 0 && c != DownloadedList.Count)
                        {
                            c = DownloadedList.Count;

                            if (!IsDisposed)
                            {
                                if (CurrentImage != DownloadedList[c - 1])
                                {
                                    CurrentImage = DownloadedList[c - 1];
                                    try
                                    {
                                        Invoke((MethodInvoker)delegate
                                        {
                                            // this.img_DisplayImage.ImageLocation = this.downloadedList[c - 1];
                                            img_Crawler_DisplayImage.Image.Dispose();

                                            Image img = FileHelper.GetImageFromFile((DownloadedList[c - 1]));

                                            if (img != null)
                                            {
                                                img_Crawler_DisplayImage.Image = img;
                                                img_Crawler_DisplayImage.Refresh();
                                            }
                                        });
                                    }
                                    catch (Exception)
                                    {
                                        img_Crawler_DisplayImage.Image = Resources.tumblrlogo;
                                        img_Crawler_DisplayImage.Update();
                                        img_Crawler_DisplayImage.Refresh();
                                    }
                                }

                                int downloaded = DownloadedList.Count;
                                int total = DownloadManager.TotalFilesToDownload;

                                if (downloaded > total)
                                    downloaded = total;

                                int percent = total > 0 ? (int)((downloaded / (double)total) * 100.00) : 0;

                                if (CurrentPercent != percent)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        bar_Progress.Value = percent;
                                        lbl_PercentBar.Text = string.Format(PercentFormat, percent);
                                    });
                                }

                                if (CurrentPostCount != downloaded)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        lbl_PostCount.Text = string.Format(PostCountFormat, downloaded, total);
                                    });
                                }

                                try
                                {
                                    totalLength = (DownloadedSizesList.Sum(x => Convert.ToInt64(x)) / (decimal)1024 / 1024);
                                    decimal totalLengthNum = totalLength > 1024 ? totalLength / 1024 : totalLength;
                                    string suffix = totalLength > 1024 ? SuffixGb : SuffixMb;

                                    if (CurrentSize != totalLength)
                                    {
                                        Invoke((MethodInvoker)delegate
                                        {
                                            lbl_Size.Text = string.Format(FileSizeFormat, (totalLengthNum).ToString("0.00"), suffix);
                                        });
                                    }
                                }
                                catch (Exception)
                                {
                                    //
                                }

                                if ((int)DownloadManager.PercentDownloaded <= 0)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        UpdateStatusText(string.Format(StatusDownloadingFormat, StatusDownloadConnecting));
                                    });
                                }
                                else if (percent != (int)DownloadManager.PercentDownloaded)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        UpdateStatusText(string.Format(StatusDownloadingFormat, DownloadManager.PercentDownloaded + "%"));
                                    });
                                }

                                CurrentPercent = percent;
                                CurrentPostCount = downloaded;
                                CurrentSize = totalLength;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        private void EnableUI_Crawl(bool state)
        {
            btn_Crawler_Browse.Enabled = state;
            btn_Crawler_Start.Visible = state;
            btn_Crawler_Stop.Visible = !state;
            select_Crawler_Mode.Enabled = state;
            select_Crawler_ImagesSize.Enabled = state;
            menuItem_LoadFromFile.Enabled = state;
            txt_TumblrURL.Enabled = state;
            txt_Crawler_SaveLocation.Enabled = state;
            DisableOtherTabs = !state;

            bar_Progress.Visible = !state;
            lbl_PercentBar.Visible = !state;

            lbl_PostCount.Visible = !state;
            lbl_Size.Visible = !state;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        private void EnableUI_Stats(bool state)
        {
            btn_Stats_Start.Enabled = state;
            menuItem_LoadFromFile.Enabled = state;
            txt_TumblrURL.Enabled = state;
            DisableOtherTabs = !state;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        private void EnableUI_TagScanner(bool state)
        {
            btn_TagScanner_Start.Enabled = state;
            txt_TumblrURL.Enabled = state;
            DisableOtherTabs = !state;

            btn_TagScanner_Start.Visible = state;
            btn_TagScanner_Stop.Visible = !state;

            bar_Progress.Visible = !state;
            lbl_PercentBar.Visible = !state;

            lbl_PostCount.Visible = !state;

            check_Tags_PhotoOnly.Enabled = state;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitApplication(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!IsExitTime)
                {
                    IsExitTime = true;
                    DialogResult dialogResult = MsgBox.Show("Are you sure you want to exit Tumblr Tools?", "Exit", MsgBox.Buttons.YesNo, MsgBox.Icon.Question, MsgBox.AnimateStyle.FadeIn, false);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something

                        PhotoPostParser.IsCancelled = true;
                        IsCrawlingCancelled = true;
                        IsDownloadDone = true;
                        IsCrawlingDone = true;

                        if (TumblrLogFile == null) TumblrLogFile = PhotoPostParser.TumblrPostLog;

                        if (!IsDisposed)
                        {
                            UpdateStatusText("Exiting...");
                            UpdateWorkStatusTextNewLine("Exiting ...");
                        }

                        if (Options.GenerateLog && TumblrLogFile != null && PhotoPostParser.IsLogUpdated)
                        {
                            Thread thread = new Thread(LogFile_Save);
                            thread.Start();
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        e.Cancel = true;
                        IsExitTime = false;
                    }
                }
            }
            catch
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileOpenWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileOpenWorker_Work(object sender, DoWorkEventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    UpdateStatusText(StatusOpenSaveFile);
                    SaveFile_Open((string)e.Argument);
                });
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        private void GenerateLog_CheckedChange(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            check_Options_GenerateUncompressedLog.Enabled = box.Checked;
            check_Options_GenerateUncompressedLog.Checked = box.Checked;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetStatsWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            TumblrStats.ProcessingStatusCode = ProcessingCode.Done;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetStatsWorker_Work(object sender, DoWorkEventArgs e)
        {
            TumblrStats.ProcessingStatusCode = ProcessingCode.Initializing;
            try
            {
                if (WebHelper.CheckForInternetConnection())
                {
                    TumblrStats = new TumblrStatsManager(new TumblrBlog(TumblrUrl), TumblrUrl, TumblrApiVersion.V2Json)
                    {
                        ProcessingStatusCode = ProcessingCode.Initializing
                    };
                    _readyToGetStats.Set();

                    TumblrStats.GetTumblrStats();
                }
                else
                {
                    TumblrStats.ProcessingStatusCode = ProcessingCode.ConnectionError;
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetStatsWorkerUI_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            //
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetStatsWorkerUI_Work(object sender, DoWorkEventArgs e)
        {
            try
            {
                var values = Enum.GetValues(typeof(TumblrPostType)).Cast<TumblrPostType>();
                int typesCount = values.Count() - 3;

                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate
                        {
                            bar_Progress.Value = 0;
                            bar_Progress.Visible = true;
                            lbl_Size.Visible = false;
                            lbl_PercentBar.Visible = true;
                        });
                }

                while (TumblrStats.ProcessingStatusCode != ProcessingCode.Done && TumblrStats.ProcessingStatusCode != ProcessingCode.ConnectionError && TumblrStats.ProcessingStatusCode != ProcessingCode.InvalidUrl)
                {
                    _readyToGetStats.WaitOne();
                    if (TumblrStats.Blog == null)
                    {
                        // wait till other worker created and populated blog info
                        Invoke((MethodInvoker)delegate
                        {
                            lbl_PercentBar.Text = @"Getting initial blog info ... ";
                        });
                    }
                    else if (string.IsNullOrEmpty(TumblrStats.Blog.Title) && string.IsNullOrEmpty(TumblrStats.Blog.Description) && TumblrStats.TotalPostsForType <= 0)
                    {
                        // wait till we got the blog title and desc and posts number
                    }
                    else
                    {
                        if (!IsDisposed)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                lbl_Stats_TotalCount.Visible = true;
                                lbl_Stats_BlogTitle.Text = TumblrStats.Blog.Title;
                                lbl_Stats_TotalCount.Text = TumblrStats.TotalPostsOverall.ToString();

                                lbl_PostCount.Text = string.Empty;
                                img_Stats_Avatar.LoadAsync(TumblrApiHelper.GenerateAvatarQueryUrl(TumblrStats.Blog.Url));
                            });
                        }

                        if (!IsDisposed)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                UpdateStatusText(StatusGettingStats);
                            });
                        }

                        int percent = 0;
                        while (percent < 100)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    txt_Stats_BlogDescription.Visible = true;
                                    if (txt_Stats_BlogDescription.Text != WebHelper.StripHtmlTags(TumblrStats.Blog.Description))
                                        txt_Stats_BlogDescription.Text = WebHelper.StripHtmlTags(TumblrStats.Blog.Description);
                                });
                            }

                            percent = (int)((TumblrStats.PostTypesProcessedCount / (double)typesCount) * 100.00);
                            if (percent < 0)
                                percent = 0;

                            if (percent >= 100)
                                percent = 100;

                            if (CurrentPercent != percent)
                            {
                                if (!IsDisposed)
                                {
                                    var percent1 = percent;
                                    Invoke((MethodInvoker)delegate
                                    {
                                        bar_Progress.Value = percent1;
                                    });
                                }
                            }

                            if (CurrentPostCount != TumblrStats.PostTypesProcessedCount)
                            {
                                if (!IsDisposed)
                                {
                                    var percent1 = percent;
                                    Invoke((MethodInvoker)delegate
                                    {
                                        lbl_PercentBar.Visible = true;
                                        lbl_Stats_TotalCount.Text = TumblrStats.TotalPostsOverall.ToString();
                                        lbl_Stats_PhotoCount.Text = TumblrStats.TotalPhotoPosts.ToString();
                                        lbl_Stats_TextCount.Text = TumblrStats.TotalTextPosts.ToString();
                                        lbl_Stats_QuoteStats.Text = TumblrStats.TotalQuotePosts.ToString();
                                        lbl_Stats_LinkCount.Text = TumblrStats.TotalLinkPosts.ToString();
                                        lbl_Stats_AudioCount.Text = TumblrStats.TotalAudioPosts.ToString();
                                        lbl_Stats_VideoCount.Text = TumblrStats.TotalVideoPosts.ToString();
                                        lbl_Stats_ChatCount.Text = TumblrStats.TotalChatPosts.ToString();
                                        lbl_Stats_AnswerCount.Text = TumblrStats.TotalAnswerPosts.ToString();
                                        lbl_PercentBar.Text = string.Format(PercentFormat, percent1);
                                        lbl_PostCount.Visible = true;
                                        lbl_PostCount.Text = string.Format(PostCountFormat, TumblrStats.PostTypesProcessedCount, (typesCount));
                                    });
                                }
                            }

                            CurrentPostCount = TumblrStats.PostTypesProcessedCount;
                            CurrentPercent = percent;
                        }
                    }
                }

                if (TumblrStats.ProcessingStatusCode == ProcessingCode.InvalidUrl)
                {
                    if (!IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateStatusText(StatusError);
                            lbl_PostCount.Visible = false;
                            bar_Progress.Visible = false;
                            lbl_Size.Visible = false;

                            MessageBox.Show(@"Invalid Tumblr URL", StatusError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                }
                else if (TumblrStats.ProcessingStatusCode == ProcessingCode.ConnectionError)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show(@"No Internet connection detected!", StatusError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatusText(StatusError);
                    });
                }
                else if (TumblrStats.ProcessingStatusCode == ProcessingCode.Done)
                {
                    try
                    {
                        if (!IsDisposed)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                EnableUI_Stats(true);
                                UpdateStatusText(StatusDone);
                                lbl_PostCount.Visible = false;
                                bar_Progress.Visible = false;
                                lbl_PercentBar.Visible = false;
                            });
                        }
                    }
                    catch (Exception exception)
                    {
                        MsgBox.Show(exception.Message);
                    }
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void GlobalInitialize()
        {
            string appFolderLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolderName = "Tumblr Tools";
            string fullAppFolderPath = Path.Combine(appFolderLocation, appFolderName);

            if (!Directory.Exists(fullAppFolderPath))
            {
                Directory.CreateDirectory(fullAppFolderPath);
            }

            OptionsFileName = Path.Combine(fullAppFolderPath, "Tumblr Tools.options");

            BlogPostsScanModesDict = new Dictionary<string, BlogPostsScanMode>();
            DownloadedList = new List<string>();
            DownloadedSizesList = new List<int>();
            NotDownloadedList = new List<string>();
            Options = new ToolOptions();
            PhotoPostParser = new PhotoPostParseManager();
            TumblrStats = new TumblrStatsManager();

            select_Crawler_Mode.Items.Clear();
            select_Crawler_Mode.Height = 21;

            string[] modeData = { ModeFullRescan, ModeNewestOnly };

            select_Crawler_Mode.DataSource = modeData;

            BlogPostsScanModesDict.Add(ModeNewestOnly, BlogPostsScanMode.NewestPostsOnly);
            BlogPostsScanModesDict.Add(ModeFullRescan, BlogPostsScanMode.FullBlogRescan);

            select_Crawler_Mode.SelectItem(ModeNewestOnly);
            select_Crawler_Mode.DropDownStyle = ComboBoxStyle.DropDownList;

            ImageSizesIndexDict = new Dictionary<string, ImageSize>();

            string[] imageData = { ImageSizeOriginal, ImageSizeLarge, ImageSizeMedium, ImageSizeSmall, ImageSizeXsmall, ImageSizeSquare };
            select_Crawler_ImagesSize.Height = 21;
            select_Crawler_ImagesSize.DataSource = imageData;

            ImageSizesIndexDict.Add(ImageSizeOriginal, ImageSize.Original);
            ImageSizesIndexDict.Add(ImageSizeLarge, ImageSize.Large);
            ImageSizesIndexDict.Add(ImageSizeMedium, ImageSize.Medium);
            ImageSizesIndexDict.Add(ImageSizeSmall, ImageSize.Small);
            ImageSizesIndexDict.Add(ImageSizeXsmall, ImageSize.XSmall);
            ImageSizesIndexDict.Add(ImageSizeSquare, ImageSize.Square);
            select_Crawler_ImagesSize.SelectItem(ImageSizeOriginal);
            select_Crawler_ImagesSize.DropDownStyle = ComboBoxStyle.DropDownList;
            select_Crawler_ImagesSize.DropDownWidth = 48;

            //string[] apiSource = { "API v.1 (XML)", "API v.2 (JSON)" };
            //this.select_Options_APIMode.DataSource = apiSource;

            TumblrStats.Blog = null;

            AdvancedMenuRenderer renderer = new AdvancedMenuRenderer
            {
                HighlightForeColor = Color.Maroon,
                HighlightBackColor = Color.White,
                ForeColor = Color.Black,
                BackColor = Color.White
            };

            menu_TopMenu.Renderer = renderer;
            txt_Crawler_WorkStatus.Visible = true;
            txt_Crawler_WorkStatus.Text = WelcomeMsg;
            txt_Stats_BlogDescription.Visible = true;
            lbl_PercentBar.Text = string.Empty;

            bar_Progress.Visible = true;
            bar_Progress.Minimum = 0;
            bar_Progress.Maximum = 100;
            bar_Progress.Step = 1;
            bar_Progress.Value = 0;

            DownloadManager = new FileDownloadManager();
            Text = new StringBuilder(Text).Append( @" ").Append( AppVersion).Append(" - © 2013 - 2017 - Shino Amakusa").ToString();
            lbl_About_Version.Text = new StringBuilder(@"Version: ").Append(AppVersion).ToString();

            string file = OptionsFileName;

            if (File.Exists(file))
            {
                Options_Load(file);
            }
            else
            {
                Options_Restore();
                //SetOptions();
                Options_Save(file);
            }

            lbl_Size.Text = string.Empty;
            lbl_Size.Visible = false;
            lbl_PostCount.Text = string.Empty;
            lbl_PostCount.Visible = false;
            lbl_Status.Text = StatusReady;
            lbl_Status.Visible = true;
            lbl_PercentBar.Text = string.Empty;
            lbl_PercentBar.Visible = true;

            btn_TagScanner_Stop.Visible = false;
            btn_TagScanner_SaveAsFile.Visible = false;

            SetDoubleBuffering(bar_Progress, true);
            SetDoubleBuffering(img_Crawler_DisplayImage, true);

            tabControl_Main.SelectedIndex = 0;

            lbl_About_Copyright.Text = AppCopyright;

            txt_Stats_BlogDescription.Text = "Click Get Stats to start ...";
            lbl_Stats_BlogTitle.Text = "Tumblr Stats";
        }

        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush ForegroundBrushSelected = new SolidBrush(Color.Maroon);
            SolidBrush ForegroundBrush = new SolidBrush(Color.Black);
            SolidBrush BackgroundBrushSelected = new SolidBrush(Color.White);
            SolidBrush BackgroundBrush = new SolidBrush(Color.White);

            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            ListBox listBox = sender as ListBox;

            int index = e.Index;
            if (index >= 0 && index < listBox.Items.Count)
            {
                string text = listBox.Items[index].ToString();
                Graphics g = e.Graphics;

                //background:
                SolidBrush backgroundBrush;
                if (selected)
                    backgroundBrush = BackgroundBrushSelected;
                else
                    backgroundBrush = BackgroundBrush;

                g.FillRectangle(backgroundBrush, e.Bounds);

                //text:
                SolidBrush foregroundBrush = (selected) ? ForegroundBrushSelected : ForegroundBrush;
                g.DrawString(text, e.Font, foregroundBrush, listBox.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        /// <summary>
        ///
        /// </summary>
        private void LogFile_Save()
        {
            FileHelper.SaveTumblrFile(
                new StringBuilder(SaveLocation).Append(@"\").Append(TumblrLogFile.Filename).ToString(), TumblrLogFile, SaveFileFormat.JsonCompressed);
            if (Options.GenerateUncompressedLog)
            {
                FileHelper.SaveTumblrFile(
                    new StringBuilder(SaveLocation).Append(@"\").Append(TumblrLogFile.Filename).Append(".txt").ToString(), TumblrLogFile);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            if (sender is ToolStripMenuItem) tsmi = sender as ToolStripMenuItem;

            if (tsmi != null)
            {
                AdvancedMenuRenderer renderer = tsmi.GetCurrentParent().Renderer as AdvancedMenuRenderer;

                renderer?.ChangeTextForeColor(tsmi, e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        private void Options_Load(string filename)
        {
            Options = JsonHelper.ReadObjectFromFile<ToolOptions>(filename);
            //this.Options.ApiMode = ApiModeEnum.v2JSON.ToString();
            Options_Restore();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Options_Reset(object sender, EventArgs e)
        {
            Options_Restore();
        }

        /// <summary>
        ///
        /// </summary>
        private void Options_Restore()
        {
            check_Options_ParseGIF.Checked = Options.ParseGif;
            check_Options_ParseJPEG.Checked = Options.ParseJpeg;
            check_Options_ParseOnly.Checked = !Options.DownloadFiles;
            check_Options_ParsePhotoSets.Checked = Options.ParsePhotoSets;
            check_Options_ParsePNG.Checked = Options.ParsePng;
            //this.apiMode = (ApiModeEnum) Enum.Parse(typeof(ApiModeEnum), this.Options.ApiMode);

            check_Options_GenerateLog.Checked = Options.GenerateLog;
            check_Options_OldToNewDownloadOrder.Checked = Options.OldToNewDownloadOrder;
            check_Options_GenerateUncompressedLog.Checked = Options.GenerateUncompressedLog;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Options_Save(object sender, EventArgs e)
        {
            Options_Set();
            Options_Save(OptionsFileName);
            UpdateStatusText("Options saved");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        private void Options_Save(string filename)
        {
            JsonHelper.SaveObjectToFile(filename, Options);
        }

        /// <summary>
        ///
        /// </summary>
        private void Options_Set()
        {
            Options.ParsePng = check_Options_ParsePNG.Checked;
            Options.ParseJpeg = check_Options_ParseJPEG.Checked;
            Options.ParseGif = check_Options_ParseGIF.Checked;
            Options.ParsePhotoSets = check_Options_ParsePhotoSets.Checked;
            //this.select_Options_APIMode.SelectedIndex = 1;
            //this.Options.ApiMode = Enum.GetName(typeof(ApiModeEnum), this.select_Options_APIMode.SelectedIndex);
            Options.DownloadFiles = !check_Options_ParseOnly.Checked;
            //this.Options.ApiMode = this.apiMode.ToString();
            Options.GenerateLog = check_Options_GenerateLog.Checked;
            Options.OldToNewDownloadOrder = check_Options_OldToNewDownloadOrder.Checked;
            Options.GenerateUncompressedLog = check_Options_GenerateUncompressedLog.Checked;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        private void SaveFile_Open(string file)
        {
            try
            {
                if (!IsDisposed)
                {
                    TumblrSaveFile = !string.IsNullOrEmpty(file) ? FileHelper.ReadTumblrFile(file) : null;

                    //this.tumblrBlog = this.tumblrSaveFile != null ? this.tumblrSaveFile.blog : null;

                    txt_Crawler_SaveLocation.Text = !string.IsNullOrEmpty(file) ? Path.GetDirectoryName(file) : string.Empty;

                    if (!string.IsNullOrEmpty(TumblrSaveFile?.Blog?.Url))
                    {
                        txt_TumblrURL.Text = TumblrSaveFile.Blog.Url;
                    }
                    else if (TumblrSaveFile?.Blog != null && string.IsNullOrEmpty(TumblrSaveFile.Blog.Url) && !string.IsNullOrEmpty(TumblrSaveFile.Blog.Cname))
                    {
                        txt_TumblrURL.Text = TumblrSaveFile.Blog.Cname;
                    }
                    else
                    {
                        txt_TumblrURL.Text = @"Error parsing save file...";
                    }

                    UpdateStatusText(StatusReady);
                    btn_Crawler_Start.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool SaveFile_Save(string name)
        {
            TumblrSaveFile = new SaveFile(
                new StringBuilder(name).Append(".tumblr").ToString(), PhotoPostParser.Blog);

            return FileHelper.SaveTumblrFile(new StringBuilder(SaveLocation).Append(@"\").Append(TumblrSaveFile.Filename).ToString(), TumblrSaveFile);
        }

        private void SetDoubleBuffering(Control control, bool value)
        {
            PropertyInfo controlProperty = typeof(Control)
                .GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            controlProperty.SetValue(control, value, null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDialog_OpenFile(object sender, EventArgs e)
        {
            btn_Crawler_Start.Enabled = false;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.AddExtension = true;
                ofd.DefaultExt = ".tumblr";
                ofd.RestoreDirectory = true;
                ofd.Filter = @"Tumblr Tools Files (.tumblr)|*.tumblr|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    UpdateStatusText(StatusOpenSaveFile);

                    fileOpenWorker.RunWorkerAsync(ofd.FileName);
                }
                else
                {
                    btn_Crawler_Start.Enabled = true;
                }
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_GetStats(object sender, EventArgs e)
        {
            lbl_PostCount.Visible = false;
            bar_Progress.BarColor = Color.Black;
            lbl_PercentBar.ForeColor = Color.Black;
            lbl_PostCount.ForeColor = Color.Black;
            lbl_PercentBar.Text = string.Empty;
            TumblrUrl = WebHelper.RemoveTrailingBackslash(txt_TumblrURL.Text);

            UpdateStatusText(StatusStarting);

            if (TumblrUrl.IsValidUrl())
            {
                if (WebHelper.CheckForInternetConnection())
                {
                    if (TumblrApiHelper.GenerateInfoQueryUrl(WebHelper.GetDomainName(TumblrUrl)).TumblrExists())
                    {
                        EnableUI_Stats(false);

                        blogGetStatsWorker.RunWorkerAsync();
                        blogGetStatsWorkerUI.RunWorkerAsync();
                    }
                    else
                    {
                        MsgBox.Show("Tumblr blog doesn't exist", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                        UpdateStatusText(StatusReady);
                    }
                }
                else
                {
                    MsgBox.Show("No internet connection detected!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                    UpdateStatusText(StatusReady);
                }
            }
            else
            {
                MsgBox.Show("Please enter valid url!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                UpdateStatusText(StatusReady);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_PhotoPostParse(object sender, EventArgs e)
        {
            EnableUI_Crawl(false);
            IsCrawlingCancelled = false;
            btn_Crawler_Stop.Visible = true;
            lbl_PercentBar.Text = string.Empty;
            IsCrawlingDone = false;
            txt_Crawler_WorkStatus.Clear();
            SaveLocation = txt_Crawler_SaveLocation.Text;
            TumblrUrl = txt_TumblrURL.Text;

            lbl_PostCount.ForeColor = Color.Black;
            bar_Progress.BarColor = Color.Black;
            lbl_PercentBar.ForeColor = Color.Black;
            lbl_PercentBar.Text = string.Empty;
            txt_Crawler_WorkStatus.Visible = true;

            bar_Progress.Value = 0;

            lbl_Size.Text = string.Empty;
            lbl_Size.DisplayStyle = ToolStripItemDisplayStyle.Text;
            lbl_PostCount.Text = string.Empty;
            lbl_PostCount.DisplayStyle = ToolStripItemDisplayStyle.Text;
            DownloadManager = new FileDownloadManager();
            PhotoPostParser = new PhotoPostParseManager();

            if (ValidateInputFields())
            {
                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText(StatusProcessStarted);
                        UpdateWorkStatusTextNewLine(StatusProcessStarted);
                    });
                }

                IsCrawlingDone = false;
                TumblrLogFile = null;

                if (!IsCrawlingCancelled)
                {
                    imageCrawlWorker.RunWorkerAsync(PhotoPostParser);

                    imageCrawlWorkerUI.RunWorkerAsync(PhotoPostParser);
                }
            }
            else
            {
                EnableUI_Crawl(true);
            }
        }

        private void Start_TagScan(object sender, EventArgs e)
        {
            EnableUI_TagScanner(false);
            IsCrawlingCancelled = false;
            lbl_PercentBar.Text = string.Empty;
            TumblrUrl = WebHelper.RemoveTrailingBackslash(txt_TumblrURL.Text);
            list_TagScanner_TagList.DataSource = null;
            list_TagScanner_TagList.Items.Clear();

            lbl_PostCount.ForeColor = Color.Black;
            bar_Progress.BarColor = Color.Black;
            lbl_PercentBar.ForeColor = Color.Black;
            lbl_PercentBar.Text = string.Empty;

            bar_Progress.Value = 0;
            bar_Progress.Maximum = 100;
            bar_Progress.Minimum = 0;

            btn_TagScanner_SaveAsFile.Visible = false;

            lbl_PostCount.Text = string.Empty;
            lbl_PostCount.DisplayStyle = ToolStripItemDisplayStyle.Text;
            DownloadManager = new FileDownloadManager();
            TagScanner = new TagScanManager(new TumblrBlog(TumblrUrl));

            if (TumblrUrl.IsValidUrl())
            {
                if (WebHelper.CheckForInternetConnection())
                {
                    if (TumblrApiHelper.GenerateInfoQueryUrl(WebHelper.GetDomainName(TumblrUrl)).TumblrExists())
                    {
                        if (!IsDisposed)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                UpdateStatusText(StatusStarting);
                            });
                        }

                        if (!IsCrawlingCancelled)
                        {
                            blogTagListWorker.RunWorkerAsync(TagScanner);

                            blogTagLIstWorkerUI.RunWorkerAsync(TagScanner);
                        }
                    }
                    else
                    {
                        MsgBox.Show("Tumblr blog doesn't exist", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                        UpdateStatusText(StatusReady);
                    }
                }
                else
                {
                    MsgBox.Show("No internet connection detected!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                    UpdateStatusText(StatusReady);
                }
            }
            else
            {
                MsgBox.Show("Please enter valid url!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                UpdateStatusText(StatusReady);
                EnableUI_TagScanner(true);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabPage_Change(object sender, KRBTabControl.KRBTabControl.SelectedIndexChangingEventArgs e)
        {
            if (DisableOtherTabs)
            {
                KRBTabControl.KRBTabControl tabWizardControl = null;
                if (sender is KRBTabControl.KRBTabControl) tabWizardControl = tabWizardControl = sender as KRBTabControl.KRBTabControl;

                if (tabWizardControl != null)
                {
                    var tabPage = (TabPageEx)tabWizardControl.SelectedTab;

                    //Disable the tab selection
                    if (e.TabPage != tabPage)
                    {
                        //If selected tab is different than the current one, re-select the current tab.
                        //This disables the navigation using the tab selection.

                        e.Cancel = true;
                        tabWizardControl.SelectTab(CurrentSelectedTab);
                    }
                }
            }

            if (e.TabPage.Text == "IsSelectable?")
                e.Cancel = true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabPage_Enter(object sender, EventArgs e)
        {
            CurrentSelectedTab = tabControl_Main.SelectedIndex;
        }

        private void TagList_SaveAasFile(object sender, EventArgs e)
        {
            tagListSaveWorker.RunWorkerAsync();
        }
        private void TagListSaveWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText("File Saved");
                    });
        }

        private void TagListSaveWorker_Work(object sender, DoWorkEventArgs e)
        {
            ListBox listBox = list_TagScanner_TagList;
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text File|*.txt|CSV File|*.csv|All Files|*.*",
                Title = "Save a Tag List as File"
            };
            Invoke((MethodInvoker)delegate
                    {
                        saveFileDialog.ShowDialog();
                    });

            if (saveFileDialog.FileName != "")
            {
                using (StreamWriter myOutputStream = new StreamWriter(saveFileDialog.FileName))
                {
                    string tags = string.Join(",", listBox.Items.Cast<string>().ToList());
                    myOutputStream.Write(tags);
                    //foreach (var item in listBox.Items)
                    //{
                    //    myOutputStream.WriteLine(item.ToString());
                    //}
                }
            }
        }

        private void TagListWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            TagScanner.ProcessingStatusCode = ProcessingCode.Done;
        }

        private void TagListWorker_Work(object sender, DoWorkEventArgs e)
        {
            TagScanner.ProcessingStatusCode = ProcessingCode.Initializing;
            try
            {
                if (WebHelper.CheckForInternetConnection())
                {
                    TagScanner = new TagScanManager(new TumblrBlog(TumblrUrl), check_Tags_PhotoOnly.Checked)
                    {
                        ProcessingStatusCode = ProcessingCode.Initializing
                    };

                    TagScanner.GetTumblrBlogInfo();
                    TagScanner.ProcessingStatusCode = ProcessingCode.Parsing;
                    TagScanner.ScanTags();
                }
                else
                {
                    TagScanner.ProcessingStatusCode = ProcessingCode.ConnectionError;
                }
            }
            catch (Exception)
            {
                // MsgBox.Show(exception.Message);
            }
        }

        private void TagListWorkerUI_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        //txt_TagScanner_TagList.Text = "Populating the list ... ";
                        //txt_TagScanner_TagList.Text = string.Join(",", TagScanner.TagList);
                        list_TagScanner_TagList.DataSource = TagScanner.TagList.ToList();
                        if (TagScanner.TagList.Count != 0)
                        {
                            btn_TagScanner_SaveAsFile.Visible = true;
                        }
                    });

                    Invoke((MethodInvoker)delegate
                    {
                        EnableUI_TagScanner(true);
                        UpdateStatusText(StatusDone);
                        lbl_PostCount.Visible = false;
                        bar_Progress.Visible = false;
                        lbl_PercentBar.Visible = false;
                    });
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        private void TagListWorkerUI_Work(object sender, DoWorkEventArgs e)
        {
            try
            {
                int TagCount = 0;
                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate
                        {
                            bar_Progress.Value = 0;
                            bar_Progress.Visible = true;
                            lbl_Size.Visible = false;
                            lbl_PercentBar.Visible = true;
                            list_TagScanner_TagList.Items.Add("The list of tags will appear after indexing is done ...");
                        });
                }

                while (TagScanner.ProcessingStatusCode != ProcessingCode.Done && TagScanner.ProcessingStatusCode != ProcessingCode.ConnectionError && TagScanner.ProcessingStatusCode != ProcessingCode.InvalidUrl)
                {
                    if (TagScanner.Blog == null)
                    {
                        // wait till other worker created and populated blog info
                        Invoke((MethodInvoker)delegate
                        {
                            lbl_PercentBar.Text = @"Getting initial blog info ... ";
                        });
                    }
                    else
                    {
                        if (!IsDisposed)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                UpdateStatusText(StatusGettingTags);
                            });
                        }

                        int percent = 0;
                        while (percent < 100 && !IsCrawlingCancelled)
                        {
                            percent = TagScanner.PercentComplete;
                            if (percent < 0)
                                percent = 0;

                            if (percent >= 100)
                                percent = 100;

                            if (CurrentPercent != percent)
                            {
                                if (!IsDisposed)
                                {
                                    var percent1 = percent;
                                    Invoke((MethodInvoker)delegate
                                    {
                                        bar_Progress.Value = percent1;
                                    });
                                }
                            }

                            lock (TagScanner.TagList)
                            {
                                Invoke((MethodInvoker)delegate
                                    {
                                        if (TagScanner.TagList.Count != Convert.ToInt64(lbl_TagScanner_TagCount.Text))
                                        {
                                            lbl_TagScanner_TagCount.Text = TagScanner.TagList.Count.ToString();
                                        }
                                        //if (txt_TagScanner_TagList.Text != string.Join(",", TagScanner.TagList))
                                        //{
                                        //    txt_TagScanner_TagList.Text = string.Join(",", TagScanner.TagList);
                                        //}
                                    });
                            }

                            if (CurrentPostCount != TagScanner.NumberOfParsedPosts)
                            {
                                if (!IsDisposed)
                                {
                                    var percent1 = percent;
                                    Invoke((MethodInvoker)delegate
                                    {
                                        lbl_PercentBar.Visible = true;
                                        lbl_PercentBar.Text = string.Format(PercentFormat, percent1);
                                        lbl_PostCount.Visible = true;
                                        lbl_PostCount.Text = string.Format(PostCountFormat, TagScanner.NumberOfParsedPosts, TagScanner.TotalNumberOfPosts);
                                    });
                                }
                            }

                            CurrentPostCount = TagScanner.PercentComplete;
                            CurrentPercent = percent;
                            TagCount = TagScanner.TagList.Count;
                        }
                    }
                }

                if (TumblrStats.ProcessingStatusCode == ProcessingCode.InvalidUrl)
                {
                    if (!IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateStatusText(StatusError);
                            lbl_PostCount.Visible = false;
                            bar_Progress.Visible = false;
                            lbl_Size.Visible = false;

                            MessageBox.Show(@"Invalid Tumblr URL", StatusError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                }
                else if (TumblrStats.ProcessingStatusCode == ProcessingCode.ConnectionError)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show(@"No Internet connection detected!", StatusError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatusText(StatusError);
                    });
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        private void UpdateStatusText(string text)
        {
            if (!lbl_Status.Text.Contains(text))
            {
                lbl_Status.Text = text;
                lbl_Status.Invalidate();
                status_Strip.Update();
                status_Strip.Refresh();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="strToReplace"></param>
        /// <param name="strToAdd"></param>
        private void UpdateWorkStatusTextConcat(string strToReplace, string strToAdd = "")
        {
            if (txt_Crawler_WorkStatus.Text.Contains(strToReplace) &&
                !txt_Crawler_WorkStatus.Text.Contains(new StringBuilder(strToReplace).Append(strToAdd).ToString()))
            {
                txt_Crawler_WorkStatus.Text = txt_Crawler_WorkStatus.Text.Replace(strToReplace, new StringBuilder(strToReplace).Append(strToAdd).ToString());

                txt_Crawler_WorkStatus.Update();
                txt_Crawler_WorkStatus.Refresh();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        private void UpdateWorkStatusTextNewLine(string text)
        {
            if (!txt_Crawler_WorkStatus.Text.Contains(text))
            {
                txt_Crawler_WorkStatus.Text = new StringBuilder(txt_Crawler_WorkStatus.Text).Append(txt_Crawler_WorkStatus.Text != "" ? "\r\n" : "").ToString();
                txt_Crawler_WorkStatus.Text = new StringBuilder(txt_Crawler_WorkStatus.Text).Append(@":: ").Append(text).ToString();
                txt_Crawler_WorkStatus.Update();
                txt_Crawler_WorkStatus.Refresh();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="replaceStr"></param>
        private void UpdateWorkStatusTextReplace(string str, string replaceStr)
        {
            if (txt_Crawler_WorkStatus.Text.Contains(str))
            {
                txt_Crawler_WorkStatus.Text = txt_Crawler_WorkStatus.Text.Replace(str, replaceStr);

                txt_Crawler_WorkStatus.Update();
                txt_Crawler_WorkStatus.Refresh();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputFields()
        {
            bool saveLocationEmpty = string.IsNullOrEmpty(SaveLocation);
            bool urlValid = true;

            if (saveLocationEmpty)
            {
                MsgBox.Show("Save Location cannot be left empty! \r\nSelect a valid location on disk", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                EnableUI_Crawl(true);
                btn_Crawler_Browse.Focus();
            }
            else
            {
                if (!TumblrUrl.IsValidUrl())
                {
                    MsgBox.Show("Please enter valid url!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                    txt_TumblrURL.Focus();
                    EnableUI_Crawl(true);
                    urlValid = false;
                }
            }

            return (!saveLocationEmpty && urlValid);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkStatusAutoScroll(object sender, EventArgs e)
        {
            txt_Crawler_WorkStatus.SelectionStart = txt_Crawler_WorkStatus.TextLength;
            txt_Crawler_WorkStatus.ScrollToCaret();
        }
    }
}