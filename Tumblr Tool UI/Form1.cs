/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: March, 2017
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
using System.Threading;
using System.Windows.Forms;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Image_Ripper;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Objects;
using Tumblr_Tool.Objects.Tumblr_Objects;
using Tumblr_Tool.Properties;
using Tumblr_Tool.Tag_Scanner;
using Tumblr_Tool.Tumblr_Stats;

namespace Tumblr_Tool
{
    public partial class MainForm : Form
    {
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
        private const string StatusDone = "Done";
        private const string StatusDownloadingFormat = "Downloading: {0}";
        private const string StatusError = "Error";
        private const string StatusGettingStats = "Getting stats ...";
        private const string StatusGettingTags = "Getting tags ...";
        private const string StatusIndexing = "Indexing ...";
        private const string StatusInit = "Initializing ...";
        private const string StatusOpenSaveFile = "Opening save file ...";
        private const string StatusReady = "Ready";
        private const string SuffixGb = "GB";
        private const string SuffixKb = "KB";
        private const string SuffixMb = "MB";
        private const string Version = "1.4.2";
        private const string WelcomeMsg = "\r\n\r\n\r\n\r\n\r\nWelcome to Tumblr Tools!\r\nVersion: " + Version + "\r\n© 2013 - 2017 Shino Amakusa\r\ntumblrtools.codeplex.com";
        private const string WorktextCheckingConnx = "Checking connection ...";
        private const string WorktextDownloadingImages = "Downloading ...";
        private const string WorktextGettingBlogInfo = "Getting info ...";
        private const string WorktextIndexingPosts = "Indexing ...";
        private const string WorktextReadingLog = "Reading log ...";
        private const string WorktextSavingLog = "Saving log ...";
        private const string WorktextStarting = "Starting ...";
        private const string WorktextUpdatingLog = "Updating log...";
        private readonly AutoResetEvent _readyToDownload = new AutoResetEvent(false);

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

            OpenTumblrSaveFile(file);
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
        private DownloadManager DownloadManager { get; set; }
        private string ErrorMessage { get; set; }
        private ImageRipper ImageRipper { get; set; }
        private Dictionary<string, ImageSize> ImageSizesIndexDict { get; set; }
        private bool IsCancelled { get; set; }
        private bool IsCrawlingDone { get; set; }
        private bool IsDownloadDone { get; set; }
        private bool IsExitTime { get; set; }
        private bool IsFileDownloadDone { get; set; }
        private List<string> NotDownloadedList { get; set; }
        private ToolOptions Options { get; set; }
        private string OptionsFileName { get; set; }
        private string SaveLocation { get; set; }
        private TagScanner TagScanner { get; set; }
        private SaveFile TumblrLogFile { get; set; }

        private SaveFile TumblrSaveFile { get; set; }

        private TumblrStats TumblrStats { get; set; }

        private string TumblrUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        private void AddWorkStatusText(string str)
        {
            if (!txt_Crawler_WorkStatus.Text.EndsWith(str))
            {
                txt_Crawler_WorkStatus.Text += str;

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

        private void ButtonOnMouseEnter(object sender, EventArgs e)
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
        private void ButtonOnMouseLeave(object sender, EventArgs e)
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
        private void CancelOperations(object sender, EventArgs e)
        {
            IsCancelled = true;
            if (ImageRipper != null)
            {
                ImageRipper.IsCancelled = true;
            }

            IsDownloadDone = true;
            EnableUI_Crawl(true);

            UpdateWorkStatusTextNewLine("Operation cancelled ...");
            img_Crawler_DisplayImage.Image = Resources.tumblrlogo;
            //MsgBox.Show("Current operation has been cancelled successfully!", "Cancel", MsgBox.Buttons.OK, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn, false);
            UpdateStatusText(StatusReady);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        private void ColorizeProgressBar(int value)
        {
            switch (value / 10)
            {
                case 0:
                    bar_Progress.BarColor = Color.Gainsboro;

                    break;

                case 1:
                    bar_Progress.BarColor = Color.Gainsboro;
                    break;

                case 2:
                    bar_Progress.BarColor = Color.Red;
                    break;

                case 3:
                    bar_Progress.BarColor = Color.Green;
                    break;

                case 4:
                    bar_Progress.BarColor = Color.Silver;
                    break;

                case 5:
                    bar_Progress.BarColor = Color.Gray;
                    break;

                case 6:
                    bar_Progress.BarColor = Color.DimGray;
                    break;

                case 7:
                    bar_Progress.BarColor = Color.SlateGray;
                    break;

                case 8:
                    bar_Progress.BarColor = Color.DarkSlateGray;
                    break;

                case 9:
                    bar_Progress.BarColor = Color.Black;
                    break;

                default:
                    bar_Progress.BarColor = Color.YellowGreen;
                    break;
            }

            bar_Progress.Update();
            bar_Progress.Refresh();
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
                if (ImageRipper != null)
                {
                    IsCrawlingDone = true;

                    if (ImageRipper.ProcessingStatusCode == ProcessingCode.Done)
                    {
                        TumblrSaveFile.Blog = ImageRipper.Blog;
                        //tumblrLogFile = null;
                        //ripper.tumblrPostLog = null;

                        TumblrLogFile = ImageRipper.TumblrPostLog;

                        IsCrawlingDone = true;

                        if (IsCrawlingDone && !check_Options_ParseOnly.Checked && !IsCancelled && ImageRipper.ImageList.Count != 0)
                        {
                            DownloadManager.TotalFilesToDownload = ImageRipper.ImageList.Count;

                            IsDownloadDone = false;
                            DownloadedList = new List<string>();
                            NotDownloadedList = new List<string>();
                            DownloadedSizesList = new List<int>();

                            imageDownloadWorkerUI.RunWorkerAsync();

                            imageDownloadWorker.RunWorkerAsync(ImageRipper.ImageList);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //MsgBox.Show(exception.Message);
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

                IsCancelled = false;

                lock (ImageRipper)
                {
                    if (TumblrSaveFile != null && Options.GenerateLog)
                    {
                        string file = SaveLocation + @"\" + Path.GetFileNameWithoutExtension(TumblrSaveFile.Filename) + ".log";

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

                    ImageRipper = new ImageRipper(new TumblrBlog(TumblrUrl), SaveLocation,
                        check_Options_GenerateLog.Checked, check_Options_ParsePhotoSets.Checked,
                        check_Options_ParseJPEG.Checked, check_Options_ParsePNG.Checked, check_Options_ParseGIF.Checked)
                    {
                        TumblrPostLog = TumblrLogFile
                    };
                    Invoke((MethodInvoker)delegate
                    {
                        this.ImageRipper.ImageSize = ImageSizesIndexDict[select_Crawler_ImagesSize.Items[select_Crawler_ImagesSize.SelectedIndex].ToString()];
                    });
                    ImageRipper.ProcessingStatusCode = ProcessingCode.Initializing;
                }

                lock (ImageRipper)
                {
                    ImageRipper.ProcessingStatusCode = ProcessingCode.CheckingConnection;
                }

                if (WebHelper.CheckForInternetConnection())
                {
                    lock (ImageRipper)
                    {
                        ImageRipper.ProcessingStatusCode = ProcessingCode.ConnectionOk;
                    }

                    if (ImageRipper != null)
                    {
                        //this.ImageRipper.SetAPIMode((ApiModeEnum) Enum.Parse(typeof(ApiModeEnum), Options.ApiMode));
                        ImageRipper.ApiVersion = TumblrApiVersion.V2Json;
                        ImageRipper.TumblrPostLog = TumblrLogFile;

                        if (ImageRipper.TumblrExists())
                        {
                            lock (ImageRipper)
                            {
                                ImageRipper.ProcessingStatusCode = ProcessingCode.GettingBlogInfo;
                            }

                            if (ImageRipper.GetTumblrBlogInfo())
                            {
                                lock (ImageRipper)
                                {
                                    ImageRipper.ProcessingStatusCode = ProcessingCode.BlogInfoOk;
                                }

                                if (!SaveTumblrFile(ImageRipper.Blog.Name))
                                {
                                    lock (ImageRipper)
                                    {
                                        ImageRipper.ProcessingStatusCode = ProcessingCode.SaveFileError;
                                    }
                                }
                                else
                                {
                                    lock (ImageRipper)
                                    {
                                        ImageRipper.ProcessingStatusCode = ProcessingCode.SaveFileOk;
                                    }

                                    if (ImageRipper != null)
                                    {
                                        BlogPostsScanMode mode = BlogPostsScanMode.NewestPostsOnly;
                                        Invoke((MethodInvoker)delegate
                                        {
                                            mode = this.BlogPostsScanModesDict[this.select_Crawler_Mode.SelectedItem.ToString()];
                                        });

                                        lock (ImageRipper)
                                        {
                                            ImageRipper.ProcessingStatusCode = ProcessingCode.Crawling;
                                        }

                                        ImageRipper.ParseBlogPosts(mode);

                                        lock (ImageRipper)
                                        {
                                            if (ImageRipper.IsLogUpdated)
                                            {
                                                ImageRipper.ProcessingStatusCode = ProcessingCode.SavingLogFile;

                                                if (!IsDisposed)
                                                {
                                                    Invoke((MethodInvoker)delegate
                                                    {
                                                        UpdateStatusText(WorktextSavingLog);
                                                        UpdateWorkStatusTextNewLine(WorktextSavingLog);
                                                    });
                                                }
                                                TumblrLogFile = ImageRipper.TumblrPostLog;
                                                SaveLogFile();

                                                if (!IsDisposed)
                                                {
                                                    Invoke((MethodInvoker)delegate
                                                    {
                                                        UpdateStatusText("Log Saved");
                                                        UpdateWorkStatusTextConcat(WorktextSavingLog, ResultDone);
                                                    });

                                                    ImageRipper.ProcessingStatusCode = ProcessingCode.Done;
                                                }
                                            }
                                        }
                                    }
                                }

                                lock (ImageRipper)
                                {
                                    ImageRipper.ProcessingStatusCode = ProcessingCode.Done;
                                }
                            }
                            else
                            {
                                lock (ImageRipper)
                                {
                                    ImageRipper.ProcessingStatusCode = ProcessingCode.BlogInfoError;
                                }
                            }
                        }
                        else
                        {
                            lock (ImageRipper)
                            {
                                ImageRipper.ProcessingStatusCode = ProcessingCode.InvalidUrl;
                            }
                        }
                    }
                }
                else
                {
                    lock (ImageRipper)
                    {
                        ImageRipper.ProcessingStatusCode = ProcessingCode.ConnectionError;
                    }
                }
            }
            catch (Exception)
            {
                //MsgBox.Show(exception.Message);
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
                        if (this.check_Options_ParseOnly.Checked)
                        {
                            EnableUI_Crawl(true);
                        }
                        else if (this.ImageRipper.ProcessingStatusCode != ProcessingCode.Done)
                        {
                            EnableUI_Crawl(true);
                        }
                        else
                        {
                            if (this.IsCrawlingDone && !this.check_Options_ParseOnly.Checked && !this.IsCancelled && this.ImageRipper.ImageList.Count != 0)
                            {
                                UpdateStatusText(string.Format(StatusDownloadingFormat, "Prepairing to download ..."));
                                this.bar_Progress.Value = 0;

                                this.lbl_PercentBar.Text = string.Format(PercentFormat, "0");

                                this.lbl_PostCount.Text = string.Format(PostCountFormat, "0", this.ImageRipper.ImageList.Count);
                            }
                            else
                            {
                                UpdateStatusText(StatusReady);
                                this.img_Crawler_DisplayImage.Image = Resources.tumblrlogo;
                                EnableUI_Crawl(true);
                            }
                        }
                    });
                }
            }
            catch (Exception)
            {
                //MsgBox.Show(exception.Message);
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
                if (ImageRipper != null)
                {
                    if (!IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                                {
                                    this.img_Crawler_DisplayImage.Image = Resources.crawling;
                                });
                    }

                    while (!IsCrawlingDone)
                    {
                        if (ImageRipper.ProcessingStatusCode == ProcessingCode.CheckingConnection)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(WorktextCheckingConnx);
                                    UpdateStatusText(StatusInit);
                                });
                            }
                        }

                        if (ImageRipper.ProcessingStatusCode == ProcessingCode.ConnectionOk)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextConcat(WorktextCheckingConnx, ResultSuccess);
                                    UpdateWorkStatusTextNewLine(WorktextStarting);
                                });
                            }
                        }
                        if (ImageRipper.ProcessingStatusCode == ProcessingCode.ConnectionError)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateStatusText(StatusError);
                                    UpdateWorkStatusTextConcat(WorktextCheckingConnx, ResultFail);
                                    this.btn_Crawler_Start.Enabled = true;
                                    this.img_Crawler_DisplayImage.Visible = true;
                                    this.img_Crawler_DisplayImage.Image = Resources.tumblrlogo;
                                    this.tab_TumblrStats.Enabled = true;
                                    this.IsCrawlingDone = true;
                                });
                            }
                        }

                        if (ImageRipper.ProcessingStatusCode == ProcessingCode.GettingBlogInfo)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(WorktextGettingBlogInfo);
                                });
                            }
                        }

                        if (ImageRipper.ProcessingStatusCode == ProcessingCode.BlogInfoOk)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextConcat(WorktextGettingBlogInfo, ResultSuccess);

                                    this.txt_Crawler_WorkStatus.Visible = true;

                                    this.txt_Crawler_WorkStatus.SelectionStart = this.txt_Crawler_WorkStatus.Text.Length;
                                });
                            }
                        }

                        if (ImageRipper.ProcessingStatusCode == ProcessingCode.Starting)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Indexing " + "\"" + this.ImageRipper.Blog.Title + "\" ... ");
                                });
                            }
                        }

                        if (ImageRipper.ProcessingStatusCode == ProcessingCode.UnableDownload)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Unable to get post info from API (Offset " + this.ImageRipper.NumberOfParsedPosts + " - " + (ImageRipper.NumberOfParsedPosts + (int)NumberOfPostsPerApiDocument.ApiV2) + ") ... ");
                                });
                            }
                            ImageRipper.ProcessingStatusCode = ProcessingCode.Crawling;
                        }

                        if (ImageRipper.ProcessingStatusCode == ProcessingCode.Crawling)
                        {
                            if (ImageRipper.TotalNumberOfPosts != 0 && ImageRipper.NumberOfParsedPosts == 0)
                            {
                                if (!IsDisposed)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        UpdateWorkStatusTextNewLine(this.ImageRipper.TotalNumberOfPosts + " photo posts found.");
                                    });
                                }
                            }

                            if (!IsDisposed && ImageRipper.NumberOfParsedPosts == 0)
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

                            var percent = ImageRipper.PercentComplete;

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

                            if (CurrentPostCount != ImageRipper.NumberOfParsedPosts)
                            {
                                CurrentPostCount = ImageRipper.NumberOfParsedPosts;
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
                                            this.lbl_PostCount.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                                        }
                                        this.lbl_PostCount.Text = string.Format(PostCountFormat, this.ImageRipper.NumberOfParsedPosts, this.ImageRipper.TotalNumberOfPosts);
                                    });
                                }
                            }
                        }
                    }

                    if (ImageRipper != null)
                    {
                        if (!IsDisposed && ImageRipper.ProcessingStatusCode == ProcessingCode.Done && !IsCancelled)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                UpdateWorkStatusTextConcat(WorktextIndexingPosts, ResultDone);

                                UpdateWorkStatusTextNewLine("Found " + (this.ImageRipper.ImageList.Count == 0 ? "no" : this.ImageRipper.ImageList.Count.ToString()) + " new image(s) to download");

                                this.bar_Progress.Value = 0;
                                this.lbl_PercentBar.Text = string.Empty;
                            });
                        }

                        if (ImageRipper.ProcessingStatusCode == ProcessingCode.UnableDownload)
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
                        if (ImageRipper.ProcessingStatusCode == ProcessingCode.InvalidUrl)
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
            catch (Exception)
            {
                // MsgBox.Show(exception.Message);
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
                SaveTumblrFile(ImageRipper.Blog.Name);

                IsDownloadDone = true;
            }
            catch (Exception)
            {
                // MsgBox.Show(exception.Message);
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
                IsCancelled = false;
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
                        if (IsCancelled)
                            break;

                        IsFileDownloadDone = false;

                        lock (DownloadManager)
                        {
                            DownloadManager.DownloadStatusCode = DownloadStatusCode.Downloading;
                        }

                        string fullPath = string.Empty;

                        FileInfo file;
                        while (!IsFileDownloadDone && !IsCancelled)
                        {
                            try
                            {
                                fullPath = FileHelper.GenerateLocalPathToFile(photoImage.Filename, SaveLocation);

                                var downloaded = DownloadManager.DownloadFile(DownloadMethod.WebClientAsync, photoImage.Url, SaveLocation);

                                if (downloaded)
                                {
                                    IsFileDownloadDone = true;
                                    photoImage.Downloaded = true;
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
                                    photoImage.Downloaded = false;

                                    if (FileHelper.FileExistsOnHdd(fullPath))
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
                                photoImage.Downloaded = false;
                                if (FileHelper.FileExistsOnHdd(fullPath))
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

                        if (IsCancelled)
                        {
                            if (FileHelper.FileExistsOnHdd(fullPath))
                            {
                                file = new FileInfo(fullPath);

                                if (!FileHelper.IsFileLocked(file)) file.Delete();
                            }
                        }
                    }

                    IsDownloadDone = true;
                }
            }
            catch (Exception)
            {
                //MsgBox.Show(exception.Message);
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
                                this.img_Crawler_DisplayImage.Image = Resources.tumblrlogo;
                            });
                        }
                        else
                        {
                            if (!IsCancelled)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    Bitmap img = GetImageFromFile(this.DownloadedList[this.DownloadedList.Count - 1]);

                                    if (img != null)
                                    {
                                        this.img_Crawler_DisplayImage.Image = GetImageFromFile(this.DownloadedList[this.DownloadedList.Count - 1]);
                                    }

                                    this.lbl_PostCount.Text = string.Format(PostCountFormat, this.DownloadedList.Count, this.ImageRipper.ImageList.Count);
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
                        this.img_Crawler_DisplayImage.Update();
                        this.img_Crawler_DisplayImage.Refresh();
                    });

                    if (DownloadedList.Count > 0)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(WorktextDownloadingImages, ResultDone);
                            UpdateWorkStatusTextNewLine("Downloaded " + this.DownloadedList.Count + " image(s).");
                            this.bar_Progress.Value = 0;
                            this.lbl_PercentBar.Text = string.Empty;
                        });
                    }
                    else
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(WorktextDownloadingImages, ResultError);
                            UpdateWorkStatusTextNewLine("We were unable to download images ...");
                            this.bar_Progress.Value = 0;
                            this.lbl_PercentBar.Text = string.Empty;
                            this.lbl_PostCount.Visible = false;
                            this.lbl_Size.Visible = false;
                        });
                    }

                    if (NotDownloadedList.Count > 0)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(WorktextDownloadingImages, ResultDone);
                            UpdateWorkStatusTextNewLine("Failed: " + this.NotDownloadedList.Count + " image(s).");
                            this.bar_Progress.Value = 0;
                            this.lbl_PercentBar.Text = string.Empty;
                        });
                    }

                    Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText(StatusDone);
                        EnableUI_Crawl(true);
                    });
                }

                if (Options.GenerateLog && DownloadedList.Count > 0)
                {
                    if (!IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextNewLine(WorktextUpdatingLog);
                            this.lbl_PercentBar.Visible = false;
                            this.bar_Progress.Visible = false;
                        });
                    }

                    SaveLogFile();

                    if (!IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(WorktextUpdatingLog, ResultDone);
                        });
                    }

                    ImageRipper.TumblrPostLog = null;
                    TumblrLogFile = null;
                }
            }
            catch (Exception)
            {
                //MsgBox.Show(exception.Message);
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
                if (DownloadManager == null) DownloadManager = new DownloadManager();

                if (ImageRipper.ImageList.Count == 0)
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
                            this.lbl_Size.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                        });

                        _readyToDownload.Set();
                    }

                    while (!IsDownloadDone && !IsCancelled)
                    {
                        int c = 0;
                        int f = 0;

                        if (NotDownloadedList.Count != 0 && f != NotDownloadedList.Count)
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    this.lbl_PostCount.ForeColor = Color.Maroon;
                                    this.bar_Progress.BarColor = Color.Maroon;
                                    this.bar_Progress.Update();
                                    this.bar_Progress.Refresh();
                                    this.lbl_PercentBar.ForeColor = Color.Maroon;

                                    this.ErrorMessage = "Error: Unable to download " + this.NotDownloadedList[NotDownloadedList.Count - 1];
                                    //updateWorkStatusTextNewLine(this.errorMessage);
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
                                            this.img_Crawler_DisplayImage.Image.Dispose();

                                            Bitmap img = GetImageFromFile((this.DownloadedList[c - 1]));

                                            if (img != null)
                                            {
                                                this.img_Crawler_DisplayImage.Image = img;
                                                this.img_Crawler_DisplayImage.Refresh();
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
                                        this.bar_Progress.Value = percent;
                                        this.lbl_PercentBar.Text = string.Format(PercentFormat, percent);
                                    });
                                }

                                if (CurrentPostCount != downloaded)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        this.lbl_PostCount.Text = string.Format(PostCountFormat, downloaded, total);
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
                                            this.lbl_Size.Text = string.Format(FileSizeFormat, (totalLengthNum).ToString("0.00"), suffix);
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
                                        UpdateStatusText(string.Format(StatusDownloadingFormat, "Connecting ..."));
                                    });
                                }
                                else if (percent != (int)DownloadManager.PercentDownloaded)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        UpdateStatusText(string.Format(StatusDownloadingFormat, this.DownloadManager.PercentDownloaded + "%"));
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
            catch (Exception)
            {
                //MsgBox.Show(exception.Message);
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
            fileToolStripMenuItem.Enabled = state;
            txt_Crawler_TumblrURL.Enabled = state;
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
            fileToolStripMenuItem.Enabled = state;
            txt_Stats_TumblrURL.Enabled = state;
            DisableOtherTabs = !state;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        private void EnableUI_TagScanner(bool state)
        {
            btn_TagScanner_Start.Enabled = state;
            txt_TagScanner_URL.Enabled = state;
            DisableOtherTabs = !state;

            btn_TagScanner_Start.Visible = state;
            btn_TagScanner_Stop.Visible = !state;

            bar_Progress.Visible = !state;
            lbl_PercentBar.Visible = !state;

            lbl_PostCount.Visible = !state;
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

                        ImageRipper.IsCancelled = true;
                        IsCancelled = true;
                        IsDownloadDone = true;
                        IsCrawlingDone = true;

                        if (TumblrLogFile == null) TumblrLogFile = ImageRipper.TumblrPostLog;

                        if (!IsDisposed)
                        {
                            UpdateStatusText("Exiting...");
                            UpdateWorkStatusTextNewLine("Exiting ...");
                        }

                        if (Options.GenerateLog && TumblrLogFile != null && ImageRipper.IsLogUpdated)
                        {
                            Thread thread = new Thread(SaveLogFile);
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
                    OpenTumblrSaveFile((string)e.Argument);
                });
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private Bitmap GetImageFromFile(string file)
        {
            try
            {
                using (Bitmap bm = new Bitmap(file))
                {
                    return new Bitmap(bm);
                }
            }
            catch
            {
                return null;
            }
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
                    TumblrStats = new TumblrStats(new TumblrBlog(TumblrUrl), TumblrUrl, TumblrApiVersion.V2Json)
                    {
                        ProcessingStatusCode = ProcessingCode.Initializing
                    };

                    TumblrStats.GetTumblrStats();
                }
                else
                {
                    TumblrStats.ProcessingStatusCode = ProcessingCode.ConnectionError;
                }
            }
            catch (Exception)
            {
                // MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetStatsWorkerUI_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        EnableUI_Stats(true);
                        UpdateStatusText(StatusDone);
                        this.lbl_PostCount.Visible = false;
                        this.bar_Progress.Visible = false;
                        this.lbl_PercentBar.Visible = false;
                    });
                }
            }
            catch (Exception)
            {
                // MsgBox.Show(exception.Message);
            }
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
                            this.bar_Progress.Value = 0;
                            this.bar_Progress.Visible = true;
                            this.lbl_Size.Visible = false;
                            this.lbl_PercentBar.Visible = true;
                        });
                }

                while (TumblrStats.ProcessingStatusCode != ProcessingCode.Done && TumblrStats.ProcessingStatusCode != ProcessingCode.ConnectionError && TumblrStats.ProcessingStatusCode != ProcessingCode.InvalidUrl)
                {
                    if (TumblrStats.Blog == null)
                    {
                        // wait till other worker created and populated blog info
                        Invoke((MethodInvoker)delegate
                        {
                            this.lbl_PercentBar.Text = @"Getting initial blog info ... ";
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
                                this.lbl_Stats_TotalCount.Visible = true;
                                this.lbl_Stats_BlogTitle.Text = TumblrStats.Blog.Title;
                                this.lbl_Stats_TotalCount.Text = TumblrStats.TotalPostsOverall.ToString();

                                this.lbl_PostCount.Text = string.Empty;
                                this.img_Stats_Avatar.LoadAsync(JsonHelper.GenerateAvatarQueryString(TumblrStats.Blog.Url));
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
                                    this.txt_Stats_BlogDescription.Visible = true;
                                    if (this.txt_Stats_BlogDescription.Text == string.Empty)
                                        this.txt_Stats_BlogDescription.Text = WebHelper.StripHtmlTags(this.TumblrStats.Blog.Description);
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
                                        this.bar_Progress.Value = percent1;
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
                                        this.lbl_PercentBar.Visible = true;
                                        this.lbl_Stats_TotalCount.Text = TumblrStats.TotalPostsOverall.ToString();
                                        this.lbl_Stats_PhotoCount.Text = TumblrStats.TotalPhotoPosts.ToString();
                                        this.lbl_Stats_TextCount.Text = TumblrStats.TotalTextPosts.ToString();
                                        this.lbl_Stats_QuoteStats.Text = TumblrStats.TotalQuotePosts.ToString();
                                        this.lbl_Stats_LinkCount.Text = TumblrStats.TotalLinkPosts.ToString();
                                        this.lbl_Stats_AudioCount.Text = TumblrStats.TotalAudioPosts.ToString();
                                        this.lbl_Stats_VideoCount.Text = TumblrStats.TotalVideoPosts.ToString();
                                        this.lbl_Stats_ChatCount.Text = TumblrStats.TotalChatPosts.ToString();
                                        this.lbl_Stats_AnswerCount.Text = TumblrStats.TotalAnswerPosts.ToString();
                                        this.lbl_PercentBar.Text = string.Format(PercentFormat, percent1);
                                        this.lbl_PostCount.Visible = true;
                                        this.lbl_PostCount.Text = string.Format(PostCountFormat, TumblrStats.PostTypesProcessedCount, (typesCount));
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
                            this.lbl_PostCount.Visible = false;
                            this.bar_Progress.Visible = false;
                            this.lbl_Size.Visible = false;

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
            catch (Exception)
            {
                // MsgBox.Show(exception.Message);
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
            ImageRipper = new ImageRipper();
            TumblrStats = new TumblrStats();

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
            txt_Stats_BlogDescription.Visible = false;
            lbl_Stats_BlogTitle.Text = string.Empty;
            lbl_PercentBar.Text = string.Empty;

            bar_Progress.Visible = true;
            bar_Progress.Minimum = 0;
            bar_Progress.Maximum = 100;
            bar_Progress.Step = 1;
            bar_Progress.Value = 0;

            DownloadManager = new DownloadManager();
            Text += @" " + Version + " - © 2013 - 2017 - Shino Amakusa";
            lbl_About_Version.Text = @"Version: " + Version;

            string file = OptionsFileName;

            if (File.Exists(file))
            {
                LoadOptions(file);
            }
            else
            {
                RestoreOptions();
                //SetOptions();
                SaveOptions(file);
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
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="urlString"></param>
        /// <returns></returns>
        private bool IsValidUrl(string urlString)
        {
            try
            {
                return urlString.IsValidUrl();
            }
            catch (Exception)
            {
                return false;
            }
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
        /// <param name="filename"></param>
        private void LoadOptions(string filename)
        {
            Options = JsonHelper.ReadObjectFromFile<ToolOptions>(filename);
            //this.Options.ApiMode = ApiModeEnum.v2JSON.ToString();
            RestoreOptions();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
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
        /// <param name="file"></param>
        private void OpenTumblrSaveFile(string file)
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
                        txt_Crawler_TumblrURL.Text = TumblrSaveFile.Blog.Url;
                    }
                    else if (TumblrSaveFile?.Blog != null && string.IsNullOrEmpty(TumblrSaveFile.Blog.Url) && !string.IsNullOrEmpty(TumblrSaveFile.Blog.Cname))
                    {
                        txt_Crawler_TumblrURL.Text = TumblrSaveFile.Blog.Cname;
                    }
                    else
                    {
                        txt_Crawler_TumblrURL.Text = @"Error parsing save file...";
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsSave(object sender, EventArgs e)
        {
            SetOptions();
            SaveOptions(OptionsFileName);
            UpdateStatusText("Options saved");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsUiRestore(object sender, EventArgs e)
        {
            RestoreOptions();
        }

        /// <summary>
        ///
        /// </summary>
        private void RestoreOptions()
        {
            check_Options_ParseGIF.Checked = Options.ParseGif;
            check_Options_ParseJPEG.Checked = Options.ParseJpeg;
            check_Options_ParseOnly.Checked = !Options.DownloadFiles;
            check_Options_ParsePhotoSets.Checked = Options.ParsePhotoSets;
            check_Options_ParsePNG.Checked = Options.ParsePng;
            //this.apiMode = (ApiModeEnum) Enum.Parse(typeof(ApiModeEnum), this.Options.ApiMode);

            check_Options_GenerateLog.Checked = Options.GenerateLog;
            check_Options_OldToNewDownloadOrder.Checked = Options.OldToNewDownloadOrder;
        }

        /// <summary>
        ///
        /// </summary>
        private void SaveLogFile()
        {
            FileHelper.SaveTumblrFile(SaveLocation + @"\" + TumblrLogFile.Filename, TumblrLogFile, SaveFileFormat.JsonCompressed);
            FileHelper.SaveTumblrFile(SaveLocation + @"\" + TumblrLogFile.Filename + ".txt", TumblrLogFile);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        private void SaveOptions(string filename)
        {
            JsonHelper.SaveObjectToFile(filename, Options);
        }

        private void SaveTagListAsFile(object sender, EventArgs e)
        {
            tagListSaveWorker.RunWorkerAsync();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile saveFile = new SaveFile(ImageRipper.Blog.Name + ".tumblr", ImageRipper.Blog);
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.AddExtension = true;
                sfd.DefaultExt = ".tumblr";
                sfd.Filter = @"Tumblr Tools Files (.tumblr)|*.tumblr";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (FileHelper.SaveTumblrFile(sfd.FileName, saveFile))
                    {
                        MessageBox.Show(@"Saved", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool SaveTumblrFile(string name)
        {
            TumblrSaveFile = new SaveFile(name + ".tumblr", ImageRipper.Blog);

            return FileHelper.SaveTumblrFile(SaveLocation + @"\" + TumblrSaveFile.Filename, TumblrSaveFile);
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
        private void SetOptions()
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
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGetStats(object sender, EventArgs e)
        {
            lbl_PostCount.Visible = false;
            bar_Progress.BarColor = Color.Black;
            lbl_PercentBar.ForeColor = Color.Black;
            lbl_PostCount.ForeColor = Color.Black;
            lbl_PercentBar.Text = string.Empty;
            TumblrUrl = WebHelper.RemoveTrailingBackslash(txt_Stats_TumblrURL.Text);

            UpdateStatusText(StatusInit);
            if (IsValidUrl(TumblrUrl))
            {
                EnableUI_Stats(false);

                blogGetStatsWorker.RunWorkerAsync();
                blogGetStatsWorkerUI.RunWorkerAsync();
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
        private void StartImageCrawl(object sender, EventArgs e)
        {
            EnableUI_Crawl(false);
            IsCancelled = false;
            btn_Crawler_Stop.Visible = true;
            lbl_PercentBar.Text = string.Empty;
            IsCrawlingDone = false;
            txt_Crawler_WorkStatus.Clear();
            SaveLocation = txt_Crawler_SaveLocation.Text;
            TumblrUrl = txt_Crawler_TumblrURL.Text;

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
            DownloadManager = new DownloadManager();
            ImageRipper = new ImageRipper();

            if (ValidateInputFields())
            {
                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText(StatusInit);
                        UpdateWorkStatusTextNewLine(StatusInit);
                    });
                }

                IsCrawlingDone = false;
                TumblrLogFile = null;

                if (!IsCancelled)
                {
                    imageCrawlWorker.RunWorkerAsync(ImageRipper);

                    imageCrawlWorkerUI.RunWorkerAsync(ImageRipper);
                }
            }
            else
            {
                EnableUI_Crawl(true);
            }
        }

        private void StartTagScan(object sender, EventArgs e)
        {
            EnableUI_TagScanner(false);
            IsCancelled = false;
            lbl_PercentBar.Text = string.Empty;
            TumblrUrl = WebHelper.RemoveTrailingBackslash(txt_TagScanner_URL.Text);
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
            DownloadManager = new DownloadManager();
            TagScanner = new TagScanner(new TumblrBlog(TumblrUrl));

            if (IsValidUrl(TumblrUrl))
            {
                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText(StatusInit);
                    });
                }

                if (!IsCancelled)
                {
                    blogTagListWorker.RunWorkerAsync(TagScanner);

                    blogTagLIstWorkerUI.RunWorkerAsync(TagScanner);
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
        private void TabControl_Main_SelectedIndexChanging(object sender, KRBTabControl.KRBTabControl.SelectedIndexChangingEventArgs e)
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
                    TagScanner = new TagScanner(new TumblrBlog(TumblrUrl))
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
                        this.lbl_PostCount.Visible = false;
                        this.bar_Progress.Visible = false;
                        this.lbl_PercentBar.Visible = false;
                    });
                }
            }
            catch (Exception)
            {
                // MsgBox.Show(exception.Message);
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
                            this.bar_Progress.Value = 0;
                            this.bar_Progress.Visible = true;
                            this.lbl_Size.Visible = false;
                            this.lbl_PercentBar.Visible = true;
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
                            this.lbl_PercentBar.Text = @"Getting initial blog info ... ";
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
                        while (percent < 100 && !IsCancelled)
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
                                        this.bar_Progress.Value = percent1;
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
                                        this.lbl_PercentBar.Visible = true;
                                        this.lbl_PercentBar.Text = string.Format(PercentFormat, percent1);
                                        this.lbl_PostCount.Visible = true;
                                        this.lbl_PostCount.Text = string.Format(PostCountFormat, TagScanner.NumberOfParsedPosts, TagScanner.TotalNumberOfPosts);
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
                            this.lbl_PostCount.Visible = false;
                            this.bar_Progress.Visible = false;
                            this.lbl_Size.Visible = false;

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
            catch (Exception)
            {
                // MsgBox.Show(exception.Message);
            }
        }

        private void TagScanner_Stop(object sender, EventArgs e)
        {
            IsCancelled = true;
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
        private void ToolStripMenuItem_Paint(object sender, PaintEventArgs e)
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateOtherUrlFields_Crawler(object sender, EventArgs e)
        {
            txt_Stats_TumblrURL.Text = txt_Crawler_TumblrURL.Text;
            txt_TagScanner_URL.Text = txt_Crawler_TumblrURL.Text;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateOtherUrlFields_Stats(object sender, EventArgs e)
        {
            txt_Crawler_TumblrURL.Text = txt_Stats_TumblrURL.Text;
            txt_TagScanner_URL.Text = txt_Stats_TumblrURL.Text;
        }

        private void UpdateOtherUrlFields_Tags(object sender, EventArgs e)
        {
            txt_Crawler_TumblrURL.Text = txt_TagScanner_URL.Text;
            txt_Stats_TumblrURL.Text = txt_TagScanner_URL.Text;
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
            if (txt_Crawler_WorkStatus.Text.Contains(strToReplace) && !txt_Crawler_WorkStatus.Text.Contains(string.Concat(strToReplace, strToAdd)))
            {
                txt_Crawler_WorkStatus.Text = txt_Crawler_WorkStatus.Text.Replace(strToReplace, string.Concat(strToReplace, strToAdd));

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
                txt_Crawler_WorkStatus.Text += txt_Crawler_WorkStatus.Text != "" ? "\r\n" : "";
                txt_Crawler_WorkStatus.Text += @":: " + text;
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
                if (!IsValidUrl(TumblrUrl))
                {
                    MsgBox.Show("Please enter valid url!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                    txt_Crawler_TumblrURL.Focus();
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