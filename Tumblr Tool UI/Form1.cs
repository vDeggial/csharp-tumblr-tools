/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2018
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using KRBTabControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private const string AppCopyright = "© 2013 - 2018 Shino Amakusa\r\n" + AppLinkUrl;
        private const string AppLinkUrl = "git.io/v9S3h";
        private const string AppVersion = "1.6.4";
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
        private const string TrayIconMessageDownloadComplete = "Image download complete. Downloaded {0} images";
        private const string TrayIconMessageGetStatsComplete = "Finished getting blog stats";
        private const string TrayIconMessageGetTagsComplete = "Finished getting post tag list";
        private const string TrayIconMessageIndexingCancel = "Post indexing canceled";
        private const string TrayIconMessageIndexingComplete = "Post indexing complete";
        private const string TrayIconMessageIndexingCompleteNoDownload = "Post indexing complete. Found no images to download";
        private const string TrayIconMessageMinimized = "Still here, but minimized";
        private const string WorktextCheckingConnx = "Checking connection ...";
        private const string WorktextDownloadingImages = "Downloading ...";
        private const string WorktextGettingBlogInfo = "Getting info ...";
        private const string WorktextIndexingPosts = "Indexing ...";
        private const string WorktextReadingLog = "Reading log ...";
        private const string WorktextSavingLog = "Saving log ...";
        private const string WorktextStarting = "Starting ...";
        private const string WorktextUpdatingLog = "Updating log...";
        private const string WorktextWelcomeMsg = "\r\n\r\n\r\n\r\n:: Welcome to Tumblr Tools!\r\n:: Version: "
            + AppVersion + "\r\n:: © 2013 - 2018\r\n:: Shino Amakusa\r\n:: " + AppLinkUrl;

        /// <summary>
        ///
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            GlobalInitialize();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        public MainForm(string file)
        {
            InitializeComponent();

            GlobalInitialize();

            txt_Crawler_SaveLocation.Text = Path.GetDirectoryName(file);

            UpdateStatusText(StatusOpenSaveFile);

            SaveFile_Open(file);
        }

        public string TumblrUrl { get; set; }
        private Dictionary<string, BlogPostsScanMode> BlogPostsScanModesDict { get; set; }
        private int CurrentSelectedTab { get; set; }
        private bool DisableOtherTabs { get; set; }
        private FileDownloadManager DownloadManager { get; set; }
        private Dictionary<string, ImageSize> ImageSizesIndexDict { get; set; }
        private bool IsBackupCancelled { get; set; }
        private bool IsExitTime { get; set; }
        private bool IsFileDownloadDone { get; set; }
        private ToolOptions Options { get; set; }
        private string OptionsFileName { get; set; }
        private PhotoPostParseManager PhotoPostParser { get; set; }
        private string SaveLocation { get; set; }
        private TagScanManager TagScanner { get; set; }
        private SaveFile TumblrLogFile { get; set; }

        private SaveFile TumblrSaveFile { get; set; }

        private TumblrStatsManager TumblrStats { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        private void AddWorkStatusText(string str)
        {
            if (!txt_Crawler_WorkStatus.Text.EndsWith(str, StringComparison.Ordinal))
            {
                txt_Crawler_WorkStatus.Text = new StringBuilder(txt_Crawler_WorkStatus.Text).Append(str).ToString();

                txt_Crawler_WorkStatus.Update();
                txt_Crawler_WorkStatus.Refresh();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseLocalPath(object sender, EventArgs e)
        {
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)

                    txt_Crawler_SaveLocation.Text = ofd.SelectedPath;
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OnMouseEnter(object sender, EventArgs e)
        {
            Button button = new Button();
            if (sender is Button) button = sender as Button;

            if (button != null && button is Button)
            {
                button.UseVisualStyleBackColor = false;
                button.FlatStyle = FlatStyle.Flat;
                button.ForeColor = Color.Maroon;
                button.FlatAppearance.BorderColor = Color.Maroon;
                button.FlatAppearance.MouseOverBackColor = Color.White;
                button.FlatAppearance.MouseDownBackColor = Color.White;
                button.FlatAppearance.BorderSize = 0;
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
            if (button != null && button is Button)
            {
                button.UseVisualStyleBackColor = true;
                button.ForeColor = Color.Black;
                button.FlatAppearance.BorderSize = 0;
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
            IsBackupCancelled = true;
            if (PhotoPostParser != null)
            {
                PhotoPostParser.IsCancelled = true;
            }

            EnableUI_Crawl(true);

            UpdateWorkStatusTextNewLine("Operation cancelled ...");
            img_Crawler_ImagePreview.Image = Resources.tumblrlogo;
            //MsgBox.Show("Current operation has been cancelled successfully!", "Cancel", MsgBox.Buttons.OK, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn, false);
            UpdateStatusText(StatusReady);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_TagScanner(object sender, EventArgs e)
        {
            IsBackupCancelled = true;
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

            lbl_Status_PostCount.Visible = !state;
            lbl_Status_Size.Visible = !state;
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

            lbl_Status_PostCount.Visible = !state;

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
                    DialogResult dialogResult = MsgBox.Show("Are you sure you want to exit Tumblr Tools?", "Exit", MsgBox.Buttons.YesNo, MsgBox.Icon.Question, false);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something

                        PhotoPostParser.IsCancelled = true;
                        IsBackupCancelled = true;

                        if (!IsDisposed)
                        {
                            UpdateStatusText("Exiting...");
                            UpdateWorkStatusTextNewLine("Exiting ...");
                        }
                        trayIcon.Visible = false;
                        //Environment.Exit(0);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        e.Cancel = true;
                        IsExitTime = false;
                    }
                }
            }
            catch (Exception)
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
            trayIcon.BalloonTipText = TrayIconMessageGetStatsComplete;
            if (Options.ShowNotifications) trayIcon.ShowBalloonTip(500);
            if (!IsDisposed)
            {
                Invoke((MethodInvoker)delegate
                {
                    EnableUI_Stats(true);
                    UpdateStatusText(StatusDone);
                    lbl_Status_PostCount.Visible = false;
                    bar_Progress.Visible = false;
                    lbl_PercentBar.Visible = false;
                });
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetStatsWorker_Work(object sender, DoWorkEventArgs e)
        {
            TumblrStats.ProcessingStatusCode = ProcessingCode.Initializing;
            Invoke((MethodInvoker)delegate
            {
                bar_Progress.Value = 0;
                bar_Progress.Visible = true;
                lbl_PercentBar.Visible = true;

                bar_Progress.ForeColor = Color.Black;
                lbl_Status_PostCount.ForeColor = Color.Black;
                lbl_PercentBar.ForeColor = Color.Black;

                lbl_Stats_AnswerCount.Text = "0";
                lbl_Stats_AudioCount.Text = "0";
                lbl_Stats_ChatCount.Text = "0";
                lbl_Stats_LinkCount.Text = "0";
                lbl_Stats_PhotoCount.Text = "0";
                lbl_Stats_QuoteCount.Text = "0";
                lbl_Stats_VideoCount.Text = "0";
                lbl_Stats_TotalCount.Text = "0";
                lbl_Stats_BlogTitle.Text = "";
                lbl_Stats_BlogDescription.Text = "";
                img_Stats_Avatar.Image = Resources.avatar;
            });
            try
            {
                if (WebHelper.CheckForInternetConnection())
                {
                    TumblrStats = new TumblrStatsManager(new TumblrBlog(TumblrUrl), TumblrUrl, TumblrApiVersion.V2Json);
                    TumblrStats.PropertyChanged += new PropertyChangedEventHandler(UpdateUI_GetStats);
                    TumblrStats.ProcessingStatusCode = ProcessingCode.Initializing;
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
                                lbl_Stats_BlogDescription.Text = WebHelper.StripHtmlTags(TumblrStats.Blog.Description);
                                lbl_Stats_TotalCount.Text = TumblrStats.TotalPostsOverall.ToString();

                                lbl_Status_PostCount.Text = string.Empty;
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

                        TumblrStats.GetTumblrStats();
                    }
                }
                else
                {
                    TumblrStats.ProcessingStatusCode = ProcessingCode.ConnectionError;
                }

                switch (TumblrStats.ProcessingStatusCode)
                {
                    case ProcessingCode.InvalidUrl:

                        if (!IsDisposed)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                UpdateStatusText(StatusError);
                                lbl_Status_PostCount.Visible = false;
                                bar_Progress.Visible = false;
                                lbl_Status_Size.Visible = false;

                                MessageBox.Show(@"Invalid Tumblr URL", StatusError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            });
                        }
                        break;

                    case ProcessingCode.ConnectionError:

                        Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show(@"No Internet connection detected!", StatusError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            UpdateStatusText(StatusError);
                        });
                        break;

                    case ProcessingCode.Done:

                        try
                        {
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    EnableUI_Stats(true);
                                    UpdateStatusText(StatusDone);
                                    lbl_Status_PostCount.Visible = false;
                                    bar_Progress.Visible = false;
                                    lbl_PercentBar.Visible = false;
                                });
                            }
                        }
                        catch (Exception exception)
                        {
                            MsgBox.Show(exception.Message);
                        }
                        break;

                    default:
                        Invoke((MethodInvoker)delegate
                        {
                            EnableUI_Stats(true);
                        });
                        break;
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
            txt_Crawler_WorkStatus.Text = WorktextWelcomeMsg;
            lbl_Stats_BlogDescription.Visible = true;
            lbl_PercentBar.Text = string.Empty;

            bar_Progress.Visible = true;
            bar_Progress.Minimum = 0;
            bar_Progress.Maximum = 100;
            bar_Progress.Step = 1;
            bar_Progress.Value = 0;

            DownloadManager = new FileDownloadManager();
            Text = new StringBuilder(Text).Append(@" ").Append(AppVersion).Append(" - © 2013 - 2018 - Shino Amakusa").ToString();
            lbl_About_Version.Text = new StringBuilder(@"Version: ").Append(AppVersion).ToString();

            string file = OptionsFileName;

            if (File.Exists(file))
            {
                Options_Load(file);
            }
            else
            {
                Options_Restore();
                Options_Save(file);
            }

            lbl_Status_Size.Text = string.Empty;
            lbl_Status_Size.Visible = false;
            lbl_Status_PostCount.Text = string.Empty;
            lbl_Status_PostCount.Visible = false;
            lbl_Status_Status.Text = StatusReady;
            lbl_Status_Status.Visible = true;
            lbl_PercentBar.Text = string.Empty;
            lbl_PercentBar.Visible = true;

            btn_TagScanner_Stop.Visible = false;
            btn_TagScanner_SaveAsFile.Visible = false;

            SetDoubleBuffering();

            tabControl_Main.SelectedIndex = 0;
            tabControl_Main.ShowToolTips = true;

            lbl_About_Copyright.Text = AppCopyright;

            lbl_Stats_BlogDescription.Text = "Click Get Stats to start ...";
            lbl_Stats_BlogTitle.Text = "Tumblr Stats";

            trayIcon.BalloonTipIcon = ToolTipIcon.Info; //Shows the info icon so the user doesn't thing there is an error.

            trayIcon.BalloonTipTitle = "Tumblr Tools";
            trayIcon.Icon = Icon; //The tray icon to use
            trayIcon.Text = "Tumblr Tools";
            trayIcon.ContextMenuStrip = trayIconContextMenu;
            trayIcon_MenuItem_Restore.Visible = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageDownloadWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            lock (DownloadManager)
            {
                DownloadManager.DownloadStatusCode = DownloadStatusCode.Done;
            }

            try
            {
                if (!IsDisposed)
                {
                    try
                    {
                        switch (DownloadManager.NumberOfFilesDownloaded)
                        {
                            case 0:

                                Invoke((MethodInvoker)delegate
                                {
                                    img_Crawler_ImagePreview.Image = Resources.tumblrlogo;
                                    UpdateWorkStatusTextConcat(WorktextDownloadingImages, ResultError);
                                    UpdateWorkStatusTextNewLine("We were unable to download images ...");
                                    bar_Progress.Value = 0;
                                    lbl_PercentBar.Text = string.Empty;
                                    lbl_Status_PostCount.Visible = false;
                                    lbl_Status_Size.Visible = false;
                                });
                                break;

                            default:

                                Invoke((MethodInvoker)delegate
                                {
                                    Bitmap img = FileHelper.GetImageFromFile(DownloadManager.DownloadedList.Last());

                                    if (img != null)
                                    {
                                        img_Crawler_ImagePreview.Image = img;
                                        img_Crawler_ImagePreview.Update();
                                        img_Crawler_ImagePreview.Refresh();
                                    }

                                    lbl_Status_PostCount.Text = string.Format(PostCountFormat, DownloadManager.NumberOfFilesDownloaded, PhotoPostParser.ImageList.Count);
                                    bar_Progress.Visible = false;
                                    UpdateWorkStatusTextConcat(WorktextDownloadingImages, ResultDone);
                                    UpdateWorkStatusTextNewLine(new StringBuilder("Downloaded ").Append(DownloadManager.NumberOfFilesDownloaded.ToString()).Append(" image(s).").ToString());
                                    trayIcon.BalloonTipText = string.Format(TrayIconMessageDownloadComplete, DownloadManager.NumberOfFilesDownloaded.ToString());
                                    if (Options.ShowNotifications) trayIcon.ShowBalloonTip(500);
                                    bar_Progress.Value = 0;
                                    lbl_PercentBar.Text = string.Empty;
                                });

                                break;
                        }
                    }
                    catch
                    {
                        //
                    }

                    if (DownloadManager.NumberOfFilesDownloaded != DownloadManager.NumberOfFilesToDownload && !IsBackupCancelled)
                    {
                        int dif = Math.Abs(DownloadManager.NumberOfFilesToDownload - DownloadManager.NumberOfFilesDownloaded);
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(WorktextDownloadingImages, ResultDone);
                            UpdateWorkStatusTextNewLine(new StringBuilder("Failed: ").Append(dif.ToString()).Append(" image(s).").ToString());
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
                TumblrSaveFile.Blog.Posts = null;
                SaveFile_Save(PhotoPostParser.Blog.Name);
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
        private void ImageDownloadWorker_Work(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(200);
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    bar_Progress.ForeColor = Color.Black;
                    lbl_PercentBar.ForeColor = Color.Black;
                    lbl_Status_PostCount.ForeColor = Color.Black;
                });

                IsFileDownloadDone = false;
                IsBackupCancelled = false;
                HashSet<PhotoPostImage> imagesList = (HashSet<PhotoPostImage>)e.Argument;
                DownloadManager.PropertyChanged += new PropertyChangedEventHandler(UpdateUI_Download);
                DownloadManager.NumberOfFilesToDownload = imagesList.Count;

                lock (DownloadManager)
                {
                    DownloadManager.DownloadStatusCode = DownloadStatusCode.Preparing;
                }

                // _readyToDownload.WaitOne();

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
                        if (IsBackupCancelled)
                            break;

                        IsFileDownloadDone = false;

                        lock (DownloadManager)
                        {
                            DownloadManager.DownloadStatusCode = DownloadStatusCode.Downloading;
                        }

                        string fullPath = string.Empty;

                        FileInfo file;
                        if (!IsFileDownloadDone && !IsBackupCancelled)
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

                                    Invoke((MethodInvoker)delegate
                                        {
                                            img_Crawler_ImagePreview.Image.Dispose();

                                            Bitmap img = FileHelper.GetImageFromFile(fullPath);

                                            if (img != null)
                                            {
                                                img_Crawler_ImagePreview.Image = img;
                                                img_Crawler_ImagePreview.Refresh();
                                            }
                                        });
                                }
                                else if (DownloadManager.DownloadStatusCode == DownloadStatusCode.UnableDownload)
                                {
                                    if (FileHelper.FileExists(fullPath))
                                    {
                                        file = new FileInfo(fullPath);

                                        if (!FileHelper.IsFileLocked(file)) file.Delete();
                                    }
                                }
                            }
                            catch
                            {
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

                        if (IsBackupCancelled)
                        {
                            if (FileHelper.FileExists(fullPath))
                            {
                                file = new FileInfo(fullPath);

                                if (!FileHelper.IsFileLocked(file)) file.Delete();
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
        private void ImageParseWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (PhotoPostParser != null)
                {
                    if (PhotoPostParser.ProcessingStatusCode == ProcessingCode.Done)
                    {
                        TumblrSaveFile.Blog = PhotoPostParser.Blog;

                        TumblrLogFile = PhotoPostParser.TumblrPostLog;

                        if (!check_Options_ParseOnly.Checked && !IsBackupCancelled && PhotoPostParser.ImageList.Count != 0)
                        {
                            //DownloadManager.NumberOfFilesToDownload = PhotoPostParser.ImageList.Count;

                            //imageDownloadWorkerUI.RunWorkerAsync();

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
        private void ImageParseWorker_Work(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread.Sleep(200);

                IsBackupCancelled = false;
                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate
                            {
                                img_Crawler_ImagePreview.Image = Resources.crawling;
                                bar_Progress.ForeColor = Color.Black;
                                lbl_Status_PostCount.ForeColor = Color.Black;
                                lbl_PercentBar.ForeColor = Color.Black;
                            });
                }

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
                    PhotoPostParser.PropertyChanged += new PropertyChangedEventHandler(UpdateUI_PostParse);

                    PhotoPostParser.ProcessingStatusCode = ProcessingCode.Initializing;
                }

                lock (PhotoPostParser)
                {
                    PhotoPostParser.ProcessingStatusCode = ProcessingCode.CheckingConnection;
                }

                switch (WebHelper.CheckForInternetConnection())
                {
                    case true:

                        lock (PhotoPostParser)
                        {
                            PhotoPostParser.ProcessingStatusCode = ProcessingCode.ConnectionOk;
                        }

                        if (PhotoPostParser != null)
                        {
                            PhotoPostParser.ApiVersion = TumblrApiVersion.V2Json;
                            PhotoPostParser.TumblrPostLog = TumblrLogFile;

                            switch (PhotoPostParser.TumblrUrl.TumblrBlogExists())
                            {
                                case true:

                                    lock (PhotoPostParser)
                                    {
                                        PhotoPostParser.ProcessingStatusCode = ProcessingCode.GettingBlogInfo;
                                    }

                                    switch (PhotoPostParser.GetTumblrBlogInfo())
                                    {
                                        case true:

                                            lock (PhotoPostParser)
                                            {
                                                PhotoPostParser.ProcessingStatusCode = ProcessingCode.BlogInfoOk;
                                            }

                                            switch (SaveFile_Save(PhotoPostParser.Blog.Name))
                                            {
                                                case true:

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
                                                        Invoke((MethodInvoker)delegate
                                                        {
                                                            lbl_PercentBar.Text = string.Format(PercentFormat, 0);
                                                        });
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
                                                    break;

                                                case false:

                                                    lock (PhotoPostParser)
                                                    {
                                                        PhotoPostParser.ProcessingStatusCode = ProcessingCode.SaveFileError;
                                                    }
                                                    break;
                                            }

                                            lock (PhotoPostParser)
                                            {
                                                PhotoPostParser.ProcessingStatusCode = ProcessingCode.Done;
                                            }
                                            break;

                                        case false:

                                            lock (PhotoPostParser)
                                            {
                                                PhotoPostParser.ProcessingStatusCode = ProcessingCode.BlogInfoError;
                                            }
                                            break;
                                    }
                                    break;

                                case false:

                                    lock (PhotoPostParser)
                                    {
                                        PhotoPostParser.ProcessingStatusCode = ProcessingCode.InvalidUrl;
                                    }
                                    break;
                            }
                        }
                        break;

                    case false:

                        lock (PhotoPostParser)
                        {
                            PhotoPostParser.ProcessingStatusCode = ProcessingCode.ConnectionError;
                        }
                        break;
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

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                trayIcon.Visible = true;
                trayIcon.BalloonTipText = TrayIconMessageMinimized;
                if (Options.ShowNotifications) trayIcon.ShowBalloonTip(500);
                trayIcon_MenuItem_Restore.Visible = true;
                this.Hide();
            }
            else trayIcon_MenuItem_Restore.Visible &= FormWindowState.Normal != this.WindowState;
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
            check_Options_GenerateLog.Checked = Options.GenerateLog;
            check_Options_OldToNewDownloadOrder.Checked = Options.OldToNewDownloadOrder;
            check_Options_GenerateUncompressedLog.Checked = Options.GenerateUncompressedLog;
            check_Options_ShowNotifications.Checked = Options.ShowNotifications;
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
            Options.DownloadFiles = !check_Options_ParseOnly.Checked;
            Options.GenerateLog = check_Options_GenerateLog.Checked;
            Options.OldToNewDownloadOrder = check_Options_OldToNewDownloadOrder.Checked;
            Options.GenerateUncompressedLog = check_Options_GenerateUncompressedLog.Checked;
            Options.ShowNotifications = check_Options_ShowNotifications.Checked;
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

        
        /// <summary>
        /// 
        /// </summary>
        private void SetDoubleBuffering()
        {
            //Progress bar
            bar_Progress.SetDoubleBuffering(true);

            //Images
            img_Crawler_ImagePreview.SetDoubleBuffering(true);
            img_Stats_Avatar.SetDoubleBuffering(true);

            //Tabs
            //tabControl_Main.SetDoubleBuffering(true);
            //tab_PhotoPostsParser.SetDoubleBuffering(true);
            //tab_About.SetDoubleBuffering(true);
            //tab_Options.SetDoubleBuffering(true);
            //tab_TumblrStats.SetDoubleBuffering(true);
            //tab_TagScanner.SetDoubleBuffering(true);

            //Labels
            //lbl_Crawler_ImageSize.SetDoubleBuffering(true);
            //lbl_Crawler_Mode.SetDoubleBuffering(true);
            //lbl_Crawler_SaveLocation.SetDoubleBuffering(true);
            //lbl_Crawler_TumblrURL.SetDoubleBuffering(true);
            //lbl_About_Copyright.SetDoubleBuffering(true);
            //lbl_About_Info.SetDoubleBuffering(true);
            //lbl_About_Title.SetDoubleBuffering(true);
            //lbl_About_Version.SetDoubleBuffering(true);
            //lbl_PercentBar.SetDoubleBuffering(true);
            //lbl_Stats_Answer.SetDoubleBuffering(true);
            //lbl_Stats_AnswerCount.SetDoubleBuffering(true);
            //lbl_Stats_Audio.SetDoubleBuffering(true);
            //lbl_Stats_AudioCount.SetDoubleBuffering(true);
            //lbl_Stats_BlogDescription.SetDoubleBuffering(true);
            //lbl_Stats_BlogTitle.SetDoubleBuffering(true);
            //lbl_Stats_Chat.SetDoubleBuffering(true);
            //lbl_Stats_ChatCount.SetDoubleBuffering(true);
            //lbl_Stats_Link.SetDoubleBuffering(true);
            //lbl_Stats_LinkCount.SetDoubleBuffering(true);
            //lbl_Stats_Photo.SetDoubleBuffering(true);
            //lbl_Stats_PhotoCount.SetDoubleBuffering(true);
            //lbl_Stats_Quote.SetDoubleBuffering(true);
            //lbl_Stats_QuoteCount.SetDoubleBuffering(true);
            //lbl_Stats_Text.SetDoubleBuffering(true);
            //lbl_Stats_TextCount.SetDoubleBuffering(true);
            //lbl_Stats_Total.SetDoubleBuffering(true);
            //lbl_Stats_TotalCount.SetDoubleBuffering(true);
            //lbl_Stats_Video.SetDoubleBuffering(true);
            //lbl_Stats_VideoCount.SetDoubleBuffering(true);
            //lbl_TagScanner_TagCount.SetDoubleBuffering(true);

            //Lists
            list_TagScanner_TagList.SetDoubleBuffering(true);

            //Text Fields
            //txt_Crawler_SaveLocation.SetDoubleBuffering(true);
            //txt_Crawler_WorkStatus.SetDoubleBuffering(true);
            //txt_TumblrURL.SetDoubleBuffering(true);

            //Sections (GroupBoxes)
            section_Crawler_BackupLocation.SetDoubleBuffering(true);
            section_Crawler_ImagePreview.SetDoubleBuffering(true);
            section_Crawler_Options.SetDoubleBuffering(true);
            section_Options_Options.SetDoubleBuffering(true);
            section_Options_BackupOptions.SetDoubleBuffering(true);
            section_Options_ImageTypes.SetDoubleBuffering(true);
            section_Options_LogOptions.SetDoubleBuffering(true);
            section_Options_Notifications.SetDoubleBuffering(true);
            section_PostStats.SetDoubleBuffering(true);
            section_Stats_Avatar.SetDoubleBuffering(true);
            section_Stats_BlogDescription.SetDoubleBuffering(true);
            section_Stats_BlogTitle.SetDoubleBuffering(true);
            section_Tags_ListOfTags.SetDoubleBuffering(true);
            section_Tags_NumberOfTags.SetDoubleBuffering(true);

            //Buttons
            //btn_Crawler_Browse.SetDoubleBuffering(true);
            //btn_Crawler_Start.SetDoubleBuffering(true);
            //btn_Crawler_Stop.SetDoubleBuffering(true);
            //btn_Options_Reset.SetDoubleBuffering(true);
            //btn_Options_Save.SetDoubleBuffering(true);
            //btn_Stats_Start.SetDoubleBuffering(true);
            //btn_TagScanner_SaveAsFile.SetDoubleBuffering(true);
            //btn_TagScanner_Start.SetDoubleBuffering(true);
            //btn_TagScanner_Stop.SetDoubleBuffering(true);

            //Checkboxes
            //check_Options_GenerateLog.SetDoubleBuffering(true);
            //check_Options_GenerateUncompressedLog.SetDoubleBuffering(true);
            //check_Options_OldToNewDownloadOrder.SetDoubleBuffering(true);
            //check_Options_ParseGIF.SetDoubleBuffering(true);
            //check_Options_ParseJPEG.SetDoubleBuffering(true);
            //check_Options_ParseOnly.SetDoubleBuffering(true);
            //check_Options_ParsePhotoSets.SetDoubleBuffering(true);
            //check_Options_ParsePNG.SetDoubleBuffering(true);
            //check_Options_ShowNotifications.SetDoubleBuffering(true);
            //check_Tags_PhotoOnly.SetDoubleBuffering(true);

            //Tables
            //table_Stats_PostStats.SetDoubleBuffering(true);

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
            lbl_Status_PostCount.Visible = false;
            bar_Progress.BarColor = Color.Black;
            lbl_PercentBar.ForeColor = Color.Black;
            lbl_Status_PostCount.ForeColor = Color.Black;
            lbl_PercentBar.Text = string.Empty;
            TumblrUrl = WebHelper.RemoveTrailingBackslash(txt_TumblrURL.Text);

            UpdateStatusText(StatusStarting);

            if (TumblrUrl.IsValidUrl())
            {
                if (WebHelper.CheckForInternetConnection())
                {
                    if (TumblrApiHelper.GenerateInfoQueryUrl(WebHelper.GetDomainName(TumblrUrl)).TumblrBlogExists())
                    {
                        EnableUI_Stats(false);

                        blogGetStatsWorker.RunWorkerAsync();
                        //blogGetStatsWorkerUI.RunWorkerAsync();
                    }
                    else
                    {
                        MsgBox.Show("Tumblr blog doesn't exist", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, true);
                        UpdateStatusText(StatusReady);
                    }
                }
                else
                {
                    MsgBox.Show("No internet connection detected!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, true);
                    UpdateStatusText(StatusReady);
                }
            }
            else
            {
                MsgBox.Show("Please enter valid url!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, true);
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
            IsBackupCancelled = false;
            btn_Crawler_Stop.Visible = true;
            lbl_PercentBar.Text = string.Empty;
            txt_Crawler_WorkStatus.Clear();
            SaveLocation = txt_Crawler_SaveLocation.Text;
            TumblrUrl = txt_TumblrURL.Text;

            lbl_Status_PostCount.ForeColor = Color.Black;
            bar_Progress.BarColor = Color.Black;
            lbl_PercentBar.ForeColor = Color.Black;
            lbl_PercentBar.Text = string.Empty;
            txt_Crawler_WorkStatus.Visible = true;

            bar_Progress.Value = 0;

            lbl_Status_Size.Text = string.Empty;
            lbl_Status_Size.DisplayStyle = ToolStripItemDisplayStyle.Text;
            lbl_Status_PostCount.Text = string.Empty;
            lbl_Status_PostCount.DisplayStyle = ToolStripItemDisplayStyle.Text;
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

                TumblrLogFile = null;

                if (!IsBackupCancelled)
                {
                    imageParseWorker.RunWorkerAsync(PhotoPostParser);
                }
            }
            else
            {
                EnableUI_Crawl(true);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_TagScan(object sender, EventArgs e)
        {
            EnableUI_TagScanner(false);
            IsBackupCancelled = false;
            lbl_PercentBar.Text = string.Empty;
            TumblrUrl = WebHelper.RemoveTrailingBackslash(txt_TumblrURL.Text);
            list_TagScanner_TagList.DataSource = null;
            list_TagScanner_TagList.Items.Clear();

            lbl_Status_PostCount.ForeColor = Color.Black;
            bar_Progress.BarColor = Color.Black;
            lbl_PercentBar.ForeColor = Color.Black;
            lbl_PercentBar.Text = string.Empty;

            bar_Progress.Value = 0;
            bar_Progress.Maximum = 100;
            bar_Progress.Minimum = 0;

            btn_TagScanner_SaveAsFile.Visible = false;

            lbl_Status_PostCount.Text = string.Empty;
            lbl_Status_PostCount.DisplayStyle = ToolStripItemDisplayStyle.Text;
            DownloadManager = new FileDownloadManager();
            TagScanner = new TagScanManager(new TumblrBlog(TumblrUrl));

            if (TumblrUrl.IsValidUrl())
            {
                if (WebHelper.CheckForInternetConnection())
                {
                    if (TumblrApiHelper.GenerateInfoQueryUrl(WebHelper.GetDomainName(TumblrUrl)).TumblrBlogExists())
                    {
                        if (!IsDisposed)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                UpdateStatusText(StatusGettingTags);
                            });
                        }

                        if (!IsBackupCancelled)
                        {
                            blogTagListWorker.RunWorkerAsync(TagScanner);

                            //blogTagLIstWorkerUI.RunWorkerAsync(TagScanner);
                        }
                    }
                    else
                    {
                        MsgBox.Show("Tumblr blog doesn't exist", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, true);
                        UpdateStatusText(StatusReady);
                    }
                }
                else
                {
                    MsgBox.Show("No internet connection detected!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, true);
                    UpdateStatusText(StatusReady);
                }
            }
            else
            {
                MsgBox.Show("Please enter valid url!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, true);
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagList_SaveAsFile(object sender, EventArgs e)
        {
            tagListSaveWorker.RunWorkerAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagListSaveWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                }
                Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText("Tag List File Saved");
                    });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText(StatusDone);
                    });
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagListWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            TagScanner.ProcessingStatusCode = ProcessingCode.Done;
            try
            {
                trayIcon.BalloonTipText = TrayIconMessageGetTagsComplete;
                if (Options.ShowNotifications) trayIcon.ShowBalloonTip(500);
                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        list_TagScanner_TagList.DataSource = TagScanner.TagList.ToList();
                        btn_TagScanner_SaveAsFile.Visible |= TagScanner.TagList.Count != 0;
                    });

                    Invoke((MethodInvoker)delegate
                    {
                        EnableUI_TagScanner(true);
                        UpdateStatusText(StatusDone);
                        lbl_Status_PostCount.Visible = false;
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagListWorker_Work(object sender, DoWorkEventArgs e)
        {
            TagScanner.ProcessingStatusCode = ProcessingCode.Initializing;
            if (!IsDisposed)
            {
                Invoke((MethodInvoker)delegate
                    {
                        bar_Progress.ForeColor = Color.Black;
                        lbl_Status_PostCount.ForeColor = Color.Black;
                        lbl_PercentBar.ForeColor = Color.Black;
                        bar_Progress.Value = 0;
                        bar_Progress.Visible = true;
                        lbl_Status_Size.Visible = false;
                        lbl_PercentBar.Visible = true;
                        list_TagScanner_TagList.Items.Add("The list of tags will appear after indexing is done ...");
                        lbl_PercentBar.Text = string.Format(PercentFormat, 0);
                    });
            }
            try
            {
                if (WebHelper.CheckForInternetConnection())
                {
                    TagScanner = new TagScanManager(new TumblrBlog(TumblrUrl), check_Tags_PhotoOnly.Checked);
                    TagScanner.PropertyChanged += new PropertyChangedEventHandler(UpdateUI_TagScan);
                    TagScanner.ProcessingStatusCode = ProcessingCode.Initializing;

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

        private void TrayIcon_MenuItem_Exit_Click(object sender, EventArgs e)
        {
            //this.Show();
            ExitApplication(null, new FormClosingEventArgs(CloseReason.UserClosing, false));
        }

        private void TrayIcon_MenuItem_Restore_Click(object sender, EventArgs e)
        {
            TrayIcon_MouseDoubleClick(sender, null);
        }

        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Show();
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                this.Visible = false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        private void UpdateStatusText(string text)
        {
            if (!lbl_Status_Status.Text.Contains(text))
            {
                lbl_Status_Status.Text = text;
                lbl_Status_Status.Invalidate();
                status_Strip.Update();
                status_Strip.Refresh();
            }
        }

        private void UpdateUI_Download(object sender, PropertyChangedEventArgs e)
        {
            var caller = sender as FileDownloadManager;
            switch (e.PropertyName)
            {
                case "NumberOfFilesDownloaded":
                    int downloaded = caller.NumberOfFilesDownloaded;
                    int total = caller.NumberOfFilesToDownload;

                    if (downloaded > total)
                        downloaded = total;

                    int percent = total > 0 ? (int)((downloaded / (double)total) * 100.00) : 0;

                    Invoke((MethodInvoker)delegate
                    {
                        bar_Progress.Value = percent;
                        lbl_PercentBar.Text = string.Format(PercentFormat, percent);
                        lbl_Status_PostCount.Text = string.Format(PostCountFormat, downloaded, total);
                    });
                    break;

                case "PercentDownloaded":
                    if ((int)caller.PercentDownloaded <= 0)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateStatusText(string.Format(StatusDownloadingFormat, StatusDownloadConnecting));
                        });
                    }
                    else
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            UpdateStatusText(string.Format(StatusDownloadingFormat, caller.PercentDownloaded + "%"));
                        });
                    }
                    break;

                case "DownloadedFilesSize":
                    decimal totalLength = (Convert.ToDecimal(caller.DownloadedFilesSize) / (decimal)1024 / 1024);
                    decimal totalLengthNum = totalLength > 1024 ? totalLength / 1024 : totalLength;
                    string suffix = totalLength > 1024 ? SuffixGb : SuffixMb;

                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Status_Size.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                        lbl_Status_Size.Text = string.Format(FileSizeFormat, (totalLengthNum).ToString("0.00"), suffix);
                    });
                    break;

                case "DownloadStatusCode":
                    if (DownloadManager.DownloadStatusCode == DownloadStatusCode.UnableDownload)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            bar_Progress.ForeColor = Color.Maroon;
                            lbl_PercentBar.ForeColor = Color.Maroon;
                            lbl_Status_PostCount.ForeColor = Color.Maroon;
                        });
                    }
                        break;
            }
        }

        private void UpdateUI_GetStats(object sender, PropertyChangedEventArgs e)
        {
            var caller = sender as TumblrStatsManager;
            switch (e.PropertyName)
            {
                case "PostTypesProcessedCount":
                    int percent = (int)((caller.PostTypesProcessedCount / (double)caller.TypesCount) * 100.00);
                    if (percent < 0)
                        percent = 0;

                    if (percent >= 100)
                        percent = 100;

                    if (!IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            lbl_Status_PostCount.Visible = true;
                            bar_Progress.Value = percent;
                            lbl_PercentBar.Text = string.Format(PercentFormat, percent);
                            lbl_Status_PostCount.Visible = true;
                            lbl_Status_PostCount.Text = string.Format(PostCountFormat, caller.PostTypesProcessedCount, (caller.TypesCount));
                        });
                    }

                    break;

                case "TotalAnswerPosts":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Stats_AnswerCount.Text = TumblrStats.TotalAnswerPosts.ToString();
                    });
                    break;

                case "TotalAudioPosts":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Stats_AudioCount.Text = TumblrStats.TotalAudioPosts.ToString();
                    });
                    break;

                case "TotalChatPosts":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Stats_ChatCount.Text = TumblrStats.TotalChatPosts.ToString();
                    });
                    break;

                case "TotalLinkPosts":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Stats_LinkCount.Text = TumblrStats.TotalLinkPosts.ToString();
                    });
                    break;

                case "TotalPhotoPosts":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Stats_PhotoCount.Text = TumblrStats.TotalPhotoPosts.ToString();
                    });
                    break;

                case "TotalQuotePosts":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Stats_QuoteCount.Text = TumblrStats.TotalQuotePosts.ToString();
                    });
                    break;

                case "TotalTextPosts":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Stats_TextCount.Text = TumblrStats.TotalTextPosts.ToString();
                    });
                    break;

                case "TotalVideoPosts":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Stats_VideoCount.Text = TumblrStats.TotalVideoPosts.ToString();
                    });
                    break;

                case "TotalPostsOverall":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Stats_TotalCount.Text = TumblrStats.TotalPostsOverall.ToString();
                    });
                    break;
            }
        }

        private void UpdateUI_PostParse(object sender, PropertyChangedEventArgs e)
        {
            var caller = sender as PhotoPostParseManager;
            switch (e.PropertyName)
            {
                case "PercentComplete":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_PercentBar.Text = string.Format(PercentFormat, caller.PercentComplete.ToString());
                        bar_Progress.Value = caller.PercentComplete;
                    });
                    break;

                case "NumberOfParsedPosts":
                    Invoke((MethodInvoker)delegate
                                {
                                    if (lbl_Status_PostCount.DisplayStyle != ToolStripItemDisplayStyle.ImageAndText)
                                    {
                                        lbl_Status_PostCount.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                                    }
                                    lbl_Status_PostCount.Text = string.Format(PostCountFormat, caller.NumberOfParsedPosts,
                                        caller.TotalNumberOfPosts);
                                });
                    break;

                case "ProcessingStatusCode":
                    switch (caller.ProcessingStatusCode)
                    {
                        case ProcessingCode.CheckingConnection:

                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(WorktextCheckingConnx);
                                    UpdateStatusText(StatusCheckConnx);
                                });
                            }
                            break;

                        case ProcessingCode.ConnectionOk:

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
                            break;

                        case ProcessingCode.ConnectionError:

                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateStatusText(StatusError);
                                    UpdateWorkStatusTextConcat(WorktextCheckingConnx, ResultFail);
                                    btn_Crawler_Start.Enabled = true;
                                    img_Crawler_ImagePreview.Visible = true;
                                    img_Crawler_ImagePreview.Image = Resources.tumblrlogo;
                                    tab_TumblrStats.Enabled = true;
                                });
                            }
                            break;

                        case ProcessingCode.BlogInfoOk:

                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextConcat(WorktextGettingBlogInfo, ResultSuccess);

                                    txt_Crawler_WorkStatus.Visible = true;

                                    txt_Crawler_WorkStatus.SelectionStart = txt_Crawler_WorkStatus.Text.Length;
                                });
                            }
                            break;

                        case ProcessingCode.Starting:

                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(new StringBuilder(
                                        "Indexing ").Append("\"").Append(caller.Blog.Title).Append("\" ... ").ToString());
                                });
                            }
                            break;

                        case ProcessingCode.UnableDownload:

                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    bar_Progress.ForeColor = Color.Maroon;
                                    lbl_PercentBar.ForeColor = Color.Maroon;
                                    lbl_Status_PostCount.ForeColor = Color.Maroon;

                                    /*UpdateWorkStatusTextNewLine(
                                        new StringBuilder("Unable to get post info from API (Offset ")
                                        .Append(caller.NumberOfParsedPosts.ToString()).Append(" - ")
                                        .Append((caller.NumberOfParsedPosts + (int)NumberOfPostsPerApiDocument.ApiV2).ToString())
                                        .Append(") ... ").ToString()); */
                                });
                            }
                            caller.ProcessingStatusCode = ProcessingCode.Crawling;
                            break;

                        case ProcessingCode.InvalidUrl:
                            if (!IsDisposed)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Invalid Tumblr URL");
                                    UpdateStatusText(StatusError);
                                    EnableUI_Crawl(true);
                                });
                            }
                            break;

                        case ProcessingCode.Done:
                            Invoke((MethodInvoker)delegate
                            {
                                UpdateWorkStatusTextConcat(WorktextIndexingPosts, ResultDone);
                                if (!IsBackupCancelled)
                                {
                                    UpdateWorkStatusTextNewLine(new StringBuilder("Found ")
                                        .Append(caller.ImageList.Count == 0 ? "no" : caller.ImageList.Count.ToString())
                                        .Append(" new image(s) to download").ToString());
                                }

                                bar_Progress.Value = 0;
                                lbl_PercentBar.Text = string.Empty;
                            });
                            if (check_Options_ParseOnly.Checked)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    EnableUI_Crawl(true);
                                });
                                trayIcon.BalloonTipText = TrayIconMessageIndexingComplete;
                                if (Options.ShowNotifications) trayIcon.ShowBalloonTip(500);
                            }
                            else
                            {
                                if (!IsBackupCancelled && caller.ImageList.Count != 0)
                                {
                                    Invoke((MethodInvoker)delegate
                                {
                                    UpdateStatusText(string.Format(StatusDownloadingFormat, "Preparing to download ..."));
                                    bar_Progress.Value = 0;

                                    lbl_PercentBar.Text = string.Format(PercentFormat, "0");

                                    lbl_Status_PostCount.Text = string.Format(PostCountFormat, "0", caller.ImageList.Count);
                                });
                                }
                                else if (caller.ImageList.Count == 0)
                                {
                                    Invoke((MethodInvoker)delegate
                                {
                                    UpdateStatusText(StatusReady);
                                    img_Crawler_ImagePreview.Image = Resources.tumblrlogo;
                                    EnableUI_Crawl(true);
                                });
                                    trayIcon.BalloonTipText = TrayIconMessageIndexingCompleteNoDownload;
                                    if (Options.ShowNotifications) trayIcon.ShowBalloonTip(500);
                                }
                                else
                                {
                                    Invoke((MethodInvoker)delegate
                                {
                                    UpdateStatusText(StatusReady);
                                    img_Crawler_ImagePreview.Image = Resources.tumblrlogo;
                                    EnableUI_Crawl(true);
                                });
                                    trayIcon.BalloonTipText = TrayIconMessageIndexingCancel;
                                    if (Options.ShowNotifications) trayIcon.ShowBalloonTip(500);
                                }
                            }
                            break;

                        case ProcessingCode.Crawling:

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
                                });
                            }

                            break;
                    }

                    break;
            }
        }

        private void UpdateUI_TagScan(object sender, PropertyChangedEventArgs e)
        {
            var caller = sender as TagScanManager;
            switch (e.PropertyName)
            {
                case "PercentComplete":
                    int percent = caller.PercentComplete;
                    if (percent < 0)
                        percent = 0;

                    if (percent >= 100)
                        percent = 100;

                    if (!IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            bar_Progress.Value = percent;
                            lbl_PercentBar.Visible = true;
                            lbl_PercentBar.Text = string.Format(PercentFormat, percent);
                        });
                    }

                    break;

                case "ProcessingStatusCode":
                    break;

                case "NumberOfParsedPosts":
                    Invoke((MethodInvoker)delegate
                    {
                        lbl_Status_PostCount.Visible = true;
                        lbl_Status_PostCount.Text = string.Format(PostCountFormat, caller.NumberOfParsedPosts, caller.TotalNumberOfPosts);
                    });
                    break;

                case "TagCount":
                    Invoke((MethodInvoker)delegate
                        {
                            lbl_TagScanner_TagCount.Text = caller.TagCount.ToString();
                        });
                    break;
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
                MsgBox.Show("Backup Location cannot be left empty! \r\nSelect a valid location on disk", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, true);
                EnableUI_Crawl(true);
                btn_Crawler_Browse.Focus();
            }
            else
            {
                if (!TumblrUrl.IsValidUrl())
                {
                    MsgBox.Show("Please enter valid url!", StatusError, MsgBox.Buttons.Ok, MsgBox.Icon.Error, true);
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
            txt_Crawler_WorkStatus.SelectionStart = txt_Crawler_WorkStatus.Text.Length;
            txt_Crawler_WorkStatus.ScrollToCaret();
        }
    }
}