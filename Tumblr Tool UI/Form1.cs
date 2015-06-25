/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Tumblr_Tool.Common_Helpers;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Image_Ripper;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Objects;
using Tumblr_Tool.Properties;
using Tumblr_Tool.Tumblr_Objects;
using Tumblr_Tool.Tumblr_Stats;

namespace Tumblr_Tool
{
    public partial class mainForm : Form
    {
        public const string _MODE_FULLRESCAN = "Full Rescan";
        public const string _MODE_NEWESTONLY = "Newest Only";
        public const string _PERCENT = "{0}%";
        public const string _POSTCOUNT = "{0}/{1}";
        public const string _RESULT_DONE = " done";
        public const string _RESULT_FAIL = " fail";
        public const string _RESULT_OK = " ok";
        public const string _RESULT_SUCCESS = " success";
        public const string _SIZE = "{0} {1}";
        public const string _STATUS_DONE = "Done";
        public const string _STATUS_DOWNLOADING = "Downloading: {0}";
        public const string _STATUS_ERROR = "Error";
        public const string _STATUS_GETTINGSTATS = "Getting stats ...";
        public const string _STATUS_INDEXING = "Indexing ...";
        public const string _STATUS_INIT = "Initializing ...";
        public const string _STATUS_OPENSAVEFILE = "Opening save file ...";
        public const string _STATUS_READY = "Ready";
        public const string _SUFFIX_GB = "GB";
        public const string _SUFFIX_MB = "MB";
        public const string _VERSION = "1.2.8";
        public const string _WORKTEXT_CHECKINGCONNX = "Checking Internet connection ...";
        public const string _WORKTEXT_DOWNLOADINGIMAGES = "Downloading images ...";
        public const string _WORKTEXT_GETTINGBLOGINFO = "Getting Blog info ...";
        public const string _WORKTEXT_INDEXINGPOSTS = "Indexing posts ...";
        public const string _WORKTEXT_READINGLOG = "Reading log file ...";
        public const string _WORKTEXT_SAVELOG = "Saving Log File ...";
        public const string _WORKTEXT_STARTING = "Starting ...";
        public const string _WORKTEXT_UPDATINGLOG = "Updating Log File ...";

        public string optionsFileName;

        public mainForm()
        {
            InitializeComponent();
            this.GlobalInitialize();
        }

        public mainForm(string file)
        {
            InitializeComponent();

            this.GlobalInitialize();

            this.txt_SaveLocation.Text = Path.GetDirectoryName(file);

            UpdateStatusText(_STATUS_OPENSAVEFILE);

            OpenTumblrFile(file);
        }

        public string currentImage { get; set; }

        public int currentPercent { get; set; }

        public int currentPostCount { get; set; }

        public int currentSelectedTab { get; set; }

        public decimal currentSize { get; set; }

        public bool disableOtherTabs { get; set; }

        public List<string> downloadedList { get; set; }

        public List<int> downloadedSizesList { get; set; }

        public string errorMessage { get; set; }

        public DownloadManager downloadManager { get; set; }

        public bool isCancelled { get; set; }

        public bool isCrawlingDone { get; set; }

        public bool isDownloadDone { get; set; }

        public bool isExitTime { get; set; }

        public bool isFileDownloadDone { get; set; }

        public bool isReadyForDownload { get; set; }

        public List<string> notDownloadedList { get; set; }

        public ToolOptions options { get; set; }

        public Dictionary<string, ParseModes> parseModesDict { get; set; }

        public ImageRipper ripper { get; set; }

        public string saveLocation { get; set; }

        public SaveFile tumblrLogFile { get; set; }

        public SaveFile tumblrSaveFile { get; set; }

        public TumblrStats tumblrStats { get; set; }

        public string tumblrURL { get; set; }

        public string apiMode
        {
            get
            {
                return Enum.GetName(typeof(ApiModeEnum), this.select_Options_APIMode.SelectedIndex);
            }

            set
            {
                this.select_Options_APIMode.SelectedIndex = (int)Enum.Parse(typeof(ApiModeEnum), value);
            }
        }

        public void AddWorkStatusText(string str)
        {
            if (!this.txt_WorkStatus.Text.EndsWith(str))
            {
                this.txt_WorkStatus.Text += str;

                this.txt_WorkStatus.Update();
                this.txt_WorkStatus.Refresh();
            }
        }

        public void Btn_Browse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)

                    this.txt_SaveLocation.Text = ofd.SelectedPath;
            }
        }

        public void Btn_Crawl_Click(object sender, EventArgs e)
        {
            EnableUI_Crawl(false);
            this.bar_Progress.Visible = true;
            this.lbl_PercentBar.Text = string.Empty;
            this.lbl_PercentBar.Visible = true;
            this.isCrawlingDone = false;
            this.txt_WorkStatus.Clear();
            this.saveLocation = this.txt_SaveLocation.Text;
            this.tumblrURL = this.txt_TumblrURL.Text;

            this.lbl_PostCount.Visible = false;
            this.lbl_PostCount.ForeColor = Color.Black;
            this.bar_Progress.BarColor = Color.Black;
            this.lbl_PercentBar.ForeColor = Color.Black;
            this.lbl_PercentBar.Text = string.Empty;
            this.txt_WorkStatus.Visible = true;

            this.bar_Progress.Value = 0;

            this.lbl_Size.Text = string.Format(_SIZE, "0.00", _SUFFIX_MB);
            this.lbl_PostCount.Text = string.Format(_POSTCOUNT, "", "");
            this.lbl_Size.Visible = false;
            this.downloadManager = new DownloadManager();
            this.ripper = new ImageRipper();

            if (CheckFields())
            {
                if (!this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText(_STATUS_INIT);
                        UpdateWorkStatusTextNewLine(_STATUS_INIT);
                    });
                }

                this.isCrawlingDone = false;
                this.tumblrLogFile = null;

                this.crawl_Worker.RunWorkerAsync(ripper);

                this.crawl_UpdateUI_Worker.RunWorkerAsync(ripper);
            }
            else
            {
                this.btn_Start.Enabled = true;
                this.lbl_PostCount.Visible = false;
                this.lbl_Size.Visible = false;
                this.bar_Progress.Visible = false;
                this.img_DisplayImage.Visible = true;
                this.tab_TumblrStats.Enabled = true;
            }
        }

        public void Btn_GetStats_Click(object sender, EventArgs e)
        {
            this.lbl_PostCount.Visible = false;
            this.bar_Progress.BarColor = Color.Black;
            this.lbl_PercentBar.ForeColor = Color.Black;
            this.lbl_PostCount.ForeColor = Color.Black;
            this.lbl_PercentBar.Text = string.Empty;
            this.tumblrURL = WebHelper.FixURL(txt_Stats_TumblrURL.Text);

            UpdateStatusText(_STATUS_INIT);
            if (IsValidURL(this.tumblrURL))
            {
                EnableUI_Stats(false);

                getStats_Worker.RunWorkerAsync();
                getStatsUI_Worker.RunWorkerAsync();
            }
            else
            {
                MsgBox.Show("Please enter valid url!", _STATUS_ERROR, MsgBox.Buttons.OK, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                UpdateStatusText(_STATUS_READY);
            }
        }

        public void Button_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;

            button.UseVisualStyleBackColor = false;
            button.ForeColor = Color.Maroon;
            button.FlatAppearance.BorderColor = Color.Maroon;
            button.FlatAppearance.MouseOverBackColor = Color.White;
            button.FlatAppearance.BorderSize = 1;
        }

        public void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.UseVisualStyleBackColor = true;
            button.ForeColor = Color.Black;
            button.FlatAppearance.BorderSize = 0;
        }

        public bool CheckFields()
        {
            bool saveLocationEmpty = string.IsNullOrEmpty(this.saveLocation);
            bool urlValid = true;

            if (saveLocationEmpty)
            {
                MsgBox.Show("Save Location cannot be left empty! \r\nSelect a valid location on disk", _STATUS_ERROR, MsgBox.Buttons.OK, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                EnableUI_Crawl(true);
                this.btn_Browse.Focus();
            }
            else
            {
                if (!IsValidURL(this.tumblrURL))
                {
                    MsgBox.Show("Please enter valid url!", _STATUS_ERROR, MsgBox.Buttons.OK, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                    this.txt_TumblrURL.Focus();
                    EnableUI_Crawl(true);
                    urlValid = false;
                }
            }

            return (!saveLocationEmpty && urlValid);
        }

        public void ColorizeProgressBar(int value)
        {
            switch (value / 10)
            {
                case 0:
                    this.bar_Progress.BarColor = Color.Gainsboro;

                    break;

                case 1:
                    this.bar_Progress.BarColor = Color.Gainsboro;
                    break;

                case 2:
                    this.bar_Progress.BarColor = Color.Red;
                    break;

                case 3:
                    this.bar_Progress.BarColor = Color.Green;
                    break;

                case 4:
                    this.bar_Progress.BarColor = Color.Silver;
                    break;

                case 5:
                    this.bar_Progress.BarColor = Color.Gray;
                    break;

                case 6:
                    this.bar_Progress.BarColor = Color.DimGray;
                    break;

                case 7:
                    this.bar_Progress.BarColor = Color.SlateGray;
                    break;

                case 8:
                    this.bar_Progress.BarColor = Color.DarkSlateGray;
                    break;

                case 9:
                    this.bar_Progress.BarColor = Color.Black;
                    break;

                default:
                    this.bar_Progress.BarColor = Color.YellowGreen;
                    break;
            }

            this.bar_Progress.Update();
            this.bar_Progress.Refresh();
        }

        public void CrawlWorker_AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (this.ripper != null)
                {
                    this.isCrawlingDone = true;

                    if (this.ripper.statusCode == ProcessingCodes.Done)
                    {
                        this.tumblrSaveFile.blog = this.ripper.blog;
                        //tumblrLogFile = null;
                        //ripper.tumblrPostLog = null;

                        this.tumblrLogFile = this.ripper.tumblrPostLog;

                        this.isCrawlingDone = true;

                        if (this.isCrawlingDone && !this.check_Options_ParseOnly.Checked && !this.isCancelled)
                        {
                            this.downloadManager.totalToDownload = this.ripper.imageList.Count;

                            this.isDownloadDone = false;
                            this.downloadedList = new List<string>();
                            this.notDownloadedList = new List<string>();
                            this.downloadedSizesList = new List<int>();

                            this.download_UIUpdate_Worker.RunWorkerAsync();

                            this.download_Worker.RunWorkerAsync(ripper.imageList);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void CrawlWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread.Sleep(200);

                this.isReadyForDownload = false;

                lock (this.ripper)
                {
                    if (this.tumblrSaveFile != null && this.options.generateLog)
                    {
                        string file = this.saveLocation + @"\" + Path.GetFileNameWithoutExtension(this.tumblrSaveFile.filename) + ".log";

                        if (File.Exists(file))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                UpdateStatusText(_WORKTEXT_READINGLOG);
                                UpdateWorkStatusTextNewLine(_WORKTEXT_READINGLOG);
                            });

                            this.tumblrLogFile = FileHelper.ReadTumblrFile(file);

                            this.Invoke((MethodInvoker)delegate
                            {
                                UpdateWorkStatusTextConcat(_WORKTEXT_READINGLOG, _RESULT_DONE);
                            });
                        }
                    }

                    //this.tumblrBlog = new TumblrBlog();
                    //this.tumblrBlog.url = this.tumblrURL;

                    this.ripper = new ImageRipper(new TumblrBlog(this.tumblrURL), this.saveLocation, this.check_Options_GenerateLog.Checked, this.check_Options_ParsePhotoSets.Checked,
                        this.check_Options_ParseJPEG.Checked, this.check_Options_ParsePNG.Checked, this.check_Options_ParseGIF.Checked, 0);
                    ripper.tumblrPostLog = this.tumblrLogFile;
                    this.ripper.statusCode = ProcessingCodes.Initializing;
                }

                lock (this.ripper)
                {
                    this.ripper.statusCode = ProcessingCodes.checkingConnection;
                }

                if (WebHelper.CheckForInternetConnection())
                {
                    lock (this.ripper)
                    {
                        this.ripper.statusCode = ProcessingCodes.connectionOK;
                    }

                    if (this.ripper != null)
                    {
                        this.ripper.SetAPIMode(options.apiMode);
                        this.ripper.SetLogFile(tumblrLogFile);

                        if (this.ripper.IsValidTumblr())
                        {
                            lock (this.ripper)
                            {
                                this.ripper.statusCode = ProcessingCodes.gettingBlogInfo;
                            }

                            if (this.ripper.SetBlogInfo())
                            {
                                lock (this.ripper)
                                {
                                    this.ripper.statusCode = ProcessingCodes.blogInfoOK;
                                }

                                if (!SaveTumblrFile(this.ripper.blog.name))
                                {
                                    lock (this.ripper)
                                    {
                                        this.ripper.statusCode = ProcessingCodes.saveFileError;
                                    }
                                }
                                else
                                {
                                    lock (this.ripper)
                                    {
                                        this.ripper.statusCode = ProcessingCodes.saveFileOK;
                                    }

                                    if (this.ripper != null)
                                    {
                                        ParseModes mode = ParseModes.NewestOnly;
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            mode = this.parseModesDict[this.select_Mode.SelectedItem.ToString()];
                                        });

                                        lock (this.ripper)
                                        {
                                            this.ripper.statusCode = ProcessingCodes.Crawling;
                                        }

                                        this.ripper.ParseBlogPosts(mode);

                                        lock (this.ripper)
                                        {
                                            if (this.ripper.isLogUpdated)
                                            {
                                                this.ripper.statusCode = ProcessingCodes.SavingLogFile;

                                                if (!this.IsDisposed)
                                                {
                                                    this.Invoke((MethodInvoker)delegate
                                                    {
                                                        UpdateStatusText(_WORKTEXT_SAVELOG);
                                                        UpdateWorkStatusTextNewLine(_WORKTEXT_SAVELOG);
                                                    });
                                                }
                                                this.tumblrLogFile = this.ripper.tumblrPostLog;
                                                SaveLogFile();

                                                if (!this.IsDisposed)
                                                {
                                                    this.Invoke((MethodInvoker)delegate
                                                    {
                                                        UpdateStatusText("Log Saved");
                                                        UpdateWorkStatusTextConcat(_WORKTEXT_SAVELOG, _RESULT_DONE);
                                                    });

                                                    this.ripper.statusCode = ProcessingCodes.Done;
                                                }
                                            }
                                        }
                                    }
                                }

                                lock (this.ripper)
                                {
                                    this.ripper.statusCode = ProcessingCodes.Done;
                                }
                            }
                            else
                            {
                                lock (this.ripper)
                                {
                                    this.ripper.statusCode = ProcessingCodes.blogInfoError;
                                }
                            }
                        }
                        else
                        {
                            lock (this.ripper)
                            {
                                this.ripper.statusCode = ProcessingCodes.invalidURL;
                            }
                        }
                    }
                }
                else
                {
                    lock (this.ripper)
                    {
                        this.ripper.statusCode = ProcessingCodes.connectionError;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public void CrawlWorker_UI__AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!this.IsDisposed)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (this.check_Options_ParseOnly.Checked)
                    {
                        EnableUI_Crawl(true);
                        this.lbl_PostCount.Visible = false;
                    }
                    else if (this.ripper.statusCode != ProcessingCodes.Done)
                    {
                        EnableUI_Crawl(true);
                    }
                    else
                    {
                        UpdateStatusText(string.Format(_STATUS_DOWNLOADING, "Initializing"));
                        this.bar_Progress.Value = 0;

                        if (this.ripper.imageList.Count != 0)
                        {
                            this.lbl_PercentBar.Text = string.Format(_PERCENT, "0");

                            this.lbl_PostCount.Text = string.Format(_POSTCOUNT, "0", this.ripper.imageList.Count);
                        }
                    }
                });
            }
        }

        public void CrawlWorker_UI__DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this.ripper != null)
                {
                    if (!this.IsDisposed)
                    {
                        this.Invoke((MethodInvoker)delegate
                                {
                                    this.img_DisplayImage.Image = Resources.crawling;
                                });
                    }

                    int percent = 0;

                    while (!this.isCrawlingDone)
                    {
                        if (this.ripper.statusCode == ProcessingCodes.checkingConnection)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(_WORKTEXT_CHECKINGCONNX);
                                    UpdateStatusText(_STATUS_INIT);
                                });
                            }
                        }

                        if (this.ripper.statusCode == ProcessingCodes.connectionOK)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextConcat(_WORKTEXT_CHECKINGCONNX, _RESULT_SUCCESS);
                                    UpdateWorkStatusTextNewLine(_WORKTEXT_STARTING);
                                });
                            }
                        }
                        if (this.ripper.statusCode == ProcessingCodes.connectionError)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateStatusText(_STATUS_ERROR);
                                    UpdateWorkStatusTextConcat(_WORKTEXT_CHECKINGCONNX, _RESULT_FAIL);
                                    this.btn_Start.Enabled = true;
                                    this.lbl_PostCount.Visible = false;
                                    this.img_DisplayImage.Visible = true;
                                    this.img_DisplayImage.Image = Resources.tumblrlogo;
                                    this.tab_TumblrStats.Enabled = true;
                                    this.isCrawlingDone = true;
                                });
                            }
                        }

                        if (this.ripper.statusCode == ProcessingCodes.gettingBlogInfo)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(_WORKTEXT_GETTINGBLOGINFO);
                                });
                            }
                        }

                        if (this.ripper.statusCode == ProcessingCodes.blogInfoOK)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextConcat(_WORKTEXT_GETTINGBLOGINFO, _RESULT_SUCCESS);

                                    this.lbl_PostCount.Visible = false;
                                    this.lbl_PostCount.Text = string.Empty;
                                    this.txt_WorkStatus.Visible = true;

                                    this.txt_WorkStatus.SelectionStart = this.txt_WorkStatus.Text.Length;
                                });
                            }
                        }

                        if (this.ripper.statusCode == ProcessingCodes.Starting)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Indexing " + "\"" + this.ripper.blog.title + "\" ... ");
                                });
                            }
                        }

                        if (this.ripper.statusCode == ProcessingCodes.UnableDownload)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Unable to get post info from API (Offset " + this.ripper.numberOfParsedPosts.ToString() + " - " + (ripper.numberOfParsedPosts + (int)PostStepEnum.JSON).ToString() + ") ... ");
                                });
                            }
                            ripper.statusCode = ProcessingCodes.Crawling;
                        }

                        if (this.ripper.statusCode == ProcessingCodes.Crawling)
                        {
                            if (this.ripper.totalNumberOfPosts != 0 && this.ripper.numberOfParsedPosts == 0)
                            {
                                if (!this.IsDisposed)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        UpdateWorkStatusTextNewLine(this.ripper.totalNumberOfPosts + " photo posts found.");
                                    });
                                }
                            }

                            if (!this.IsDisposed && this.ripper.numberOfParsedPosts == 0)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(_WORKTEXT_INDEXINGPOSTS);
                                    UpdateStatusText(_STATUS_INDEXING);
                                    //this.lbl_PostCount.Text = string.Format(_POSTCOUNT, "0", this.ripper.totalNumberOfPosts.ToString());
                                    //this.lbl_PercentBar.Text = string.Format(_PERCENT, "0");

                                    //this.lbl_PostCount.Text = string.Empty;
                                    //this.lbl_PercentBar.Text = string.Empty;

                                    //this.lbl_PostCount.Visible = true;
                                });
                            }

                            percent = this.ripper.percentComplete;

                            if (percent > 100)
                                percent = 100;

                            if (this.currentPercent != percent)
                            {
                                this.currentPercent = percent;
                                if (!this.IsDisposed)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                       {
                                           if (!this.lbl_PercentBar.Visible)
                                           {
                                               this.lbl_PercentBar.Visible = true;
                                           }

                                           this.lbl_PercentBar.Text = string.Format(_PERCENT, percent.ToString());
                                           this.bar_Progress.Value = percent;
                                       });
                                }
                            }

                            if (this.currentPostCount != this.ripper.numberOfParsedPosts)
                            {
                                this.currentPostCount = this.ripper.numberOfParsedPosts;
                                if (!this.IsDisposed)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        if (!this.lbl_PostCount.Visible)
                                        {
                                            this.lbl_PostCount.Visible = true;
                                        }
                                        this.lbl_PostCount.Text = string.Format(_POSTCOUNT, this.ripper.numberOfParsedPosts.ToString(), this.ripper.totalNumberOfPosts.ToString());
                                    });
                                }
                            }
                        }
                    }

                    if (this.ripper != null)
                    {
                        if (!this.IsDisposed && this.ripper.statusCode == ProcessingCodes.Done)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                UpdateWorkStatusTextConcat(_WORKTEXT_INDEXINGPOSTS, _RESULT_DONE);

                                UpdateWorkStatusTextNewLine("Found " + (this.ripper.imageList.Count == 0 ? "no" : this.ripper.imageList.Count.ToString()) + " new image(s) to download");

                                this.bar_Progress.Value = 0;
                                this.lbl_PercentBar.Text = string.Empty;
                            });
                        }

                        if (this.ripper.statusCode == ProcessingCodes.UnableDownload)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Error downloading the blog post XML");
                                    UpdateStatusText(_STATUS_ERROR);
                                    EnableUI_Crawl(true);
                                });
                            }
                        }
                        if (this.ripper.statusCode == ProcessingCodes.invalidURL)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Invalid Tumblr URL");
                                    UpdateStatusText(_STATUS_ERROR);
                                    EnableUI_Crawl(true);
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                //
            }
        }

        public void DownloadWorker_AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            lock (this.downloadManager)
            {
                this.downloadManager.statusCode = DownloadStatusCodes.Done;
            }

            try
            {
                this.tumblrSaveFile.blog.posts = null;
                SaveTumblrFile(this.ripper.blog.name);

                if (this.options.generateLog && this.downloadedList.Count > 0)
                {
                    if (!this.IsDisposed)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextNewLine(_WORKTEXT_UPDATINGLOG);
                            this.lbl_PercentBar.Visible = false;
                            this.bar_Progress.Visible = false;
                        });
                    }

                    SaveLogFile();

                    if (!this.IsDisposed)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(_WORKTEXT_UPDATINGLOG, _RESULT_DONE);
                        });
                    }

                    this.ripper.tumblrPostLog = null;
                    this.tumblrLogFile = null;
                }
                this.isDownloadDone = true;
            }
            catch
            {
            }
        }

        public void DownloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(200);
            try
            {
                this.downloadedList = new List<string>();
                this.downloadedSizesList = new List<int>();
                this.isDownloadDone = false;
                this.isFileDownloadDone = false;
                FileInfo file;
                HashSet<PhotoPostImage> imagesList = (HashSet<PhotoPostImage>)e.Argument;
                downloadManager.totalToDownload = imagesList.Count;

                lock (this.downloadManager)
                {
                    this.downloadManager.statusCode = DownloadStatusCodes.Preparing;
                }

                while (!isReadyForDownload)
                {
                    //wait
                }

                if (imagesList != null && imagesList.Count != 0)
                {
                    int j = 0;

                    lock (this.downloadManager)
                    {
                        this.downloadManager.statusCode = DownloadStatusCodes.Downloading;
                    }

                    foreach (PhotoPostImage photoImage in imagesList)
                    {
                        if (this.isCancelled)
                            break;

                        this.isFileDownloadDone = false;

                        lock (this.downloadManager)
                        {
                            this.downloadManager.statusCode = DownloadStatusCodes.Downloading;
                        }

                        bool downloaded = false;
                        string fullPath = string.Empty;

                        while (!this.isFileDownloadDone && !this.isCancelled)
                        {
                            try
                            {
                                fullPath = FileHelper.GetFullFilePath(photoImage.filename, this.saveLocation);

                                downloaded = downloadManager.DownloadFile(photoImage.url, this.saveLocation);

                                if (downloaded)
                                {
                                    j++;
                                    this.isFileDownloadDone = true;
                                    photoImage.downloaded = true;
                                    fullPath = FileHelper.FixFileName(fullPath);

                                    file = new FileInfo(fullPath);

                                    lock (this.downloadedList)
                                    {
                                        this.downloadedList.Add(fullPath);
                                    }

                                    lock (this.downloadedSizesList)
                                    {
                                        this.downloadedSizesList.Add((int)file.Length);
                                    }
                                }
                                else if (this.downloadManager.statusCode == DownloadStatusCodes.UnableDownload)
                                {
                                    lock (this.notDownloadedList)
                                    {
                                        this.notDownloadedList.Add(photoImage.url);
                                    }
                                    photoImage.downloaded = false;

                                    if (FileHelper.FileExists(fullPath))
                                    {
                                        file = new FileInfo(fullPath);

                                        if (!FileHelper.IsFileLocked(file)) file.Delete();
                                    }
                                }
                            }
                            catch
                            {
                                lock (this.notDownloadedList)
                                {
                                    this.notDownloadedList.Add(photoImage.url);
                                }
                                photoImage.downloaded = false;
                                if (FileHelper.FileExists(fullPath))
                                {
                                    file = new FileInfo(fullPath);

                                    if (!FileHelper.IsFileLocked(file)) file.Delete();
                                }
                            }
                            finally
                            {
                                this.isFileDownloadDone = true;
                            }
                        }

                        if (this.isCancelled)
                        {
                            if (FileHelper.FileExists(fullPath))
                            {
                                file = new FileInfo(fullPath);

                                if (!FileHelper.IsFileLocked(file)) file.Delete();
                            }
                        }
                    }

                    this.isDownloadDone = true;
                }
            }
            catch
            {
            }
        }

        public void DownloadWorker_UI__AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    try
                    {
                        if (this.downloadedList.Count == 0)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.img_DisplayImage.Image = Resources.tumblrlogo;
                            });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.img_DisplayImage.Image = GetImage(this.downloadedList[this.downloadedList.Count - 1]);
                                this.lbl_PostCount.Text = string.Format(_POSTCOUNT, this.downloadedList.Count, this.ripper.imageList.Count);
                            });
                        }
                    }
                    catch
                    {
                        //
                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.img_DisplayImage.Update();
                        this.img_DisplayImage.Refresh();
                    });

                    if (this.downloadedList.Count > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(_WORKTEXT_DOWNLOADINGIMAGES, _RESULT_DONE);
                            UpdateWorkStatusTextNewLine("Downloaded " + this.downloadedList.Count.ToString() + " image(s).");
                            this.bar_Progress.Value = 0;
                            this.lbl_PercentBar.Text = string.Empty;
                        });
                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText(_STATUS_DONE);
                        EnableUI_Crawl(true);
                    });
                }
            }
            catch
            {
            }
        }

        public void DownloadWorker_UI__DoWork(object sender, DoWorkEventArgs e)
        {
            this.currentPercent = 0;
            this.currentPostCount = 0;
            this.currentSize = 0;
            try
            {
                if (this.downloadManager == null) this.downloadManager = new DownloadManager();

                if (this.ripper.imageList.Count == 0)
                {
                    if (!this.IsDisposed)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.bar_Progress.Visible = false;

                            this.lbl_PostCount.Visible = false;
                            UpdateStatusText(_STATUS_DONE); ;
                        });
                    }
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (!this.bar_Progress.Visible)
                        {
                            this.bar_Progress.Visible = true;
                        }

                        if (!this.lbl_PercentBar.Visible)
                        {
                            this.lbl_PercentBar.Text = string.Format(_PERCENT, "0");
                            this.lbl_PercentBar.Visible = true;
                        }

                        if (!this.lbl_PostCount.Visible)
                            this.lbl_PostCount.Visible = true;

                        if (!this.lbl_Size.Visible)
                        {
                            this.lbl_Size.Text = string.Format(_SIZE, "0.00", _SUFFIX_MB);
                            this.lbl_Size.Visible = true;
                        }
                    });

                    decimal totalLength = 0;

                    if (!this.IsDisposed && downloadedList.Count < 10)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextNewLine(_WORKTEXT_DOWNLOADINGIMAGES);
                        });

                        this.isReadyForDownload = true;
                    }

                    while (!this.isDownloadDone && !this.isCancelled)
                    {
                        int c = 0;
                        int f = 0;

                        if (this.notDownloadedList.Count != 0 && f != this.notDownloadedList.Count)
                        {
                            f = this.notDownloadedList.Count;

                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    this.lbl_PostCount.ForeColor = Color.Maroon;
                                    this.bar_Progress.BarColor = Color.Maroon;
                                    this.bar_Progress.Update();
                                    this.bar_Progress.Refresh();
                                    this.lbl_PercentBar.ForeColor = Color.Maroon;

                                    this.errorMessage = "Error: Unable to download " + this.notDownloadedList[notDownloadedList.Count - 1];
                                    //updateWorkStatusTextNewLine(this.errorMessage);
                                });
                            }
                        }

                        if (this.downloadedList.Count != 0 && c != this.downloadedList.Count)
                        {
                            c = this.downloadedList.Count;

                            if (!this.IsDisposed)
                            {
                                if (this.currentImage != this.downloadedList[c - 1])
                                {
                                    this.currentImage = this.downloadedList[c - 1];
                                    try
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            // this.img_DisplayImage.ImageLocation = this.downloadedList[c - 1];
                                            this.img_DisplayImage.Image.Dispose();
                                            this.img_DisplayImage.Image = GetImage((this.downloadedList[c - 1]));
                                            this.img_DisplayImage.Refresh();
                                        });
                                    }
                                    catch (Exception)
                                    {
                                        this.img_DisplayImage.Image = Resources.tumblrlogo;
                                        this.img_DisplayImage.Update();
                                        this.img_DisplayImage.Refresh();
                                    }
                                }

                                int downloaded = this.downloadedList.Count;
                                int total = this.downloadManager.totalToDownload;

                                if (downloaded > total)
                                    downloaded = total;

                                //this.Invoke((MethodInvoker)delegate
                                //{
                                //    if (!this.bar_Progress.Visible)
                                //    {
                                //        this.bar_Progress.Visible = true;
                                //    }

                                //    if (!this.lbl_PercentBar.Visible)
                                //        this.lbl_PercentBar.Visible = true;

                                //    if (!this.lbl_PostCount.Visible)
                                //        this.lbl_PostCount.Visible = true;

                                //    if (!this.lbl_Size.Visible)
                                //        this.lbl_Size.Visible = true;
                                //});

                                int percent = total > 0 ? (int)(((double)downloaded / (double)total) * 100.00) : 0;

                                if (this.currentPercent != percent)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        this.bar_Progress.Value = percent;
                                        this.lbl_PercentBar.Text = string.Format(_PERCENT, percent.ToString());
                                    });
                                }

                                if (this.currentPostCount != downloaded)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        this.lbl_PostCount.Text = string.Format(_POSTCOUNT, downloaded.ToString(), total.ToString());
                                    });
                                }

                                try
                                {
                                    totalLength = (this.downloadedSizesList.Sum(x => Convert.ToInt32(x)) / (decimal)1024 / (decimal)1024);
                                    decimal totalLengthNum = totalLength > 1024 ? totalLength / 1024 : totalLength;
                                    string suffix = totalLength > 1024 ? _SUFFIX_GB : _SUFFIX_MB;

                                    if (this.currentSize != totalLength)
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            this.lbl_Size.Text = string.Format(_SIZE, (totalLengthNum).ToString("0.00"), suffix);
                                        });
                                    }
                                }
                                catch (Exception)
                                {
                                    //
                                }

                                if ((int)this.downloadManager.percentDownloaded <= 0)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        UpdateStatusText(string.Format(_STATUS_DOWNLOADING, "Connecting"));
                                    });
                                }
                                else if (percent != (int)this.downloadManager.percentDownloaded)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        UpdateStatusText(string.Format(_STATUS_DOWNLOADING, this.downloadManager.percentDownloaded.ToString() + "%"));
                                    });
                                }

                                this.currentPercent = percent;
                                this.currentPostCount = downloaded;
                                this.currentSize = totalLength;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void EnableUI_Crawl(bool state)
        {
            this.btn_Browse.Enabled = state;
            this.btn_Start.Enabled = state;
            this.select_Mode.Enabled = state;
            this.fileToolStripMenuItem.Enabled = state;
            this.txt_TumblrURL.Enabled = state;
            this.txt_SaveLocation.Enabled = state;
            this.disableOtherTabs = !state;
        }

        public void EnableUI_Stats(bool state)
        {
            this.btn_GetStats.Enabled = state;
            this.fileToolStripMenuItem.Enabled = state;
            this.txt_Stats_TumblrURL.Enabled = state;
            this.disableOtherTabs = !state;
        }

        public void FileBW_AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        public void FileBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateStatusText(_STATUS_OPENSAVEFILE);
                    OpenTumblrFile((string)e.Argument);
                });
            }
            catch
            {
            }
        }

        public void Form_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!isExitTime)
                {
                    this.isExitTime = true;
                    DialogResult dialogResult = MsgBox.Show("Are you sure you want to exit Tumblr Tools?", "Exit", MsgBox.Buttons.YesNo, MsgBox.Icon.Question, MsgBox.AnimateStyle.FadeIn, false);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something

                        this.ripper.isCancelled = true;
                        this.isCancelled = true;
                        this.isDownloadDone = true;
                        this.isCrawlingDone = true;

                        if (this.tumblrLogFile == null) this.tumblrLogFile = this.ripper.tumblrPostLog;

                        if (!this.IsDisposed)
                        {
                            UpdateStatusText("Exiting...");
                            UpdateWorkStatusTextNewLine("Exiting ...");
                        }

                        if (this.options.generateLog && this.tumblrLogFile != null && this.ripper.isLogUpdated)
                        {
                            Thread thread = new Thread(SaveLogFile);
                            thread.Start();
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        e.Cancel = true;
                        this.isExitTime = false;
                    }
                }
                else
                {
                    //Environment.Exit(0);
                }
            }
            catch
            {
                Environment.Exit(0);
            }
        }

        public Bitmap GetImage(string file)
        {
            using (Bitmap bm = new Bitmap(file))
            {
                return new Bitmap(bm);
            }
        }

        public void GetStatsWorker_AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            this.tumblrStats.statusCode = ProcessingCodes.Done;
        }

        public void GetStatsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.tumblrStats.statusCode = ProcessingCodes.Initializing;
            try
            {
                this.tumblrStats = new TumblrStats();

                if (WebHelper.CheckForInternetConnection())
                {
                    this.tumblrStats = new TumblrStats(new TumblrBlog(this.tumblrURL), this.tumblrURL, this.options.apiMode);
                    this.tumblrStats.statusCode = ProcessingCodes.Initializing;

                    this.tumblrStats.getStats();
                }
                else
                {
                    this.tumblrStats.statusCode = ProcessingCodes.connectionError;
                }
            }
            catch
            {
            }
        }

        public void GetStatsWorker_UI__DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var values = Enum.GetValues(typeof(TumblrPostTypes)).Cast<TumblrPostTypes>();
                int typesCount = values.Count() - 3;

                if (!this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate
                        {
                            this.bar_Progress.Value = 0;
                            this.bar_Progress.Visible = true;
                            this.lbl_Size.Visible = false;
                            this.lbl_PercentBar.Visible = true;
                        });
                }

                while (this.tumblrStats.statusCode != ProcessingCodes.Done && this.tumblrStats.statusCode != ProcessingCodes.connectionError && this.tumblrStats.statusCode != ProcessingCodes.invalidURL)
                {
                    if (this.tumblrStats.blog == null)
                    {
                        // wait till other worker created and populated blog info
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.lbl_PercentBar.Text = "Getting initial blog info ... ";
                        });
                    }
                    else if (string.IsNullOrEmpty(this.tumblrStats.blog.title) && string.IsNullOrEmpty(this.tumblrStats.blog.description) && this.tumblrStats.totalPosts <= 0)
                    {
                        // wait till we got the blog title and desc and posts number
                    }
                    else
                    {
                        if (!this.IsDisposed)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.lbl_Stats_TotalCount.Visible = true;
                                this.lbl_Stats_BlogTitle.Text = tumblrStats.blog.title;
                                this.lbl_Stats_TotalCount.Text = tumblrStats.totalPostsOverall.ToString();

                                this.lbl_PostCount.Text = string.Empty;
                                this.img_Stats_Avatar.LoadAsync(JSONHelper.GetAvatarQueryString(tumblrStats.blog.url));
                            });
                        }

                        if (!this.IsDisposed)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                UpdateStatusText(_STATUS_GETTINGSTATS);
                            });
                        }

                        int percent = 0;
                        while (percent < 100)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    this.txt_Stats_BlogDescription.Visible = true;
                                    if (this.txt_Stats_BlogDescription.Text == string.Empty)
                                        this.txt_Stats_BlogDescription.Text = WebHelper.StripHTMLTags(this.tumblrStats.blog.description);
                                });
                            }

                            percent = (int)(((double)this.tumblrStats.parsed / (double)typesCount) * 100.00);
                            if (percent < 0)
                                percent = 0;

                            if (percent >= 100)
                                percent = 100;

                            if (this.currentPercent != percent)
                            {
                                if (!this.IsDisposed)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        this.bar_Progress.Value = percent;
                                    });
                                }
                            }

                            if (this.currentPostCount != this.tumblrStats.parsed)
                            {
                                if (!this.IsDisposed)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        this.lbl_PercentBar.Visible = true;
                                        this.lbl_Stats_TotalCount.Text = tumblrStats.totalPostsOverall.ToString();
                                        this.lbl_Stats_PhotoCount.Text = tumblrStats.photoPosts.ToString();
                                        this.lbl_Stats_TextCount.Text = tumblrStats.textPosts.ToString();
                                        this.lbl_Stats_QuoteStats.Text = tumblrStats.quotePosts.ToString();
                                        this.lbl_Stats_LinkCount.Text = tumblrStats.linkPosts.ToString();
                                        this.lbl_Stats_AudioCount.Text = tumblrStats.audioPosts.ToString();
                                        this.lbl_Stats_VideoCount.Text = tumblrStats.videoPosts.ToString();
                                        this.lbl_Stats_ChatCount.Text = tumblrStats.chatPosts.ToString();
                                        this.lbl_Stats_AnswerCount.Text = tumblrStats.answerPosts.ToString();
                                        this.lbl_PercentBar.Text = string.Format(_PERCENT, percent.ToString());
                                        this.lbl_PostCount.Visible = true;
                                        this.lbl_PostCount.Text = string.Format(_POSTCOUNT, tumblrStats.parsed.ToString(), (typesCount).ToString());
                                    });
                                }
                            }

                            this.currentPostCount = tumblrStats.parsed;
                            this.currentPercent = percent;
                        }
                    }
                }

                if (this.tumblrStats.statusCode == ProcessingCodes.invalidURL)
                {
                    if (!this.IsDisposed)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateStatusText(_STATUS_ERROR);
                            this.lbl_PostCount.Visible = false;
                            this.bar_Progress.Visible = false;
                            this.lbl_Size.Visible = false;

                            MessageBox.Show("Invalid Tumblr URL", _STATUS_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                }
                else if (this.tumblrStats.statusCode == ProcessingCodes.connectionError)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("No Internet connection detected!", _STATUS_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatusText(_STATUS_ERROR);
                    });
                }
            }
            catch (Exception)
            {
                // throw ex;
            }
        }

        public void GetStatsWorker_UI_AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        EnableUI_Stats(true);
                        UpdateStatusText(_STATUS_DONE);
                        this.lbl_PostCount.Visible = false;
                        this.bar_Progress.Visible = false;
                        this.lbl_PercentBar.Visible = false;
                    });
                }
            }
            catch
            {
            }
        }

        public void GlobalInitialize()
        {
            string appFolderLocation = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            string appFolderName = "Tumblr Tools";
            string fullAppFolderPath = Path.Combine(appFolderLocation, appFolderName);

            if (!Directory.Exists(fullAppFolderPath))
            {
                Directory.CreateDirectory(fullAppFolderPath);
            }

            this.optionsFileName = Path.Combine(fullAppFolderPath, "Tumblr Tools.options");

            this.parseModesDict = new Dictionary<string, ParseModes>();
            this.downloadedList = new List<string>();
            this.downloadedSizesList = new List<int>();
            this.notDownloadedList = new List<string>();
            this.options = new ToolOptions();
            this.ripper = new ImageRipper();
            this.tumblrStats = new TumblrStats();

            this.select_Mode.Items.Clear();
            this.select_Mode.Height = 21;

            this.select_Mode.Items.Add(_MODE_NEWESTONLY);
            this.select_Mode.Items.Add(_MODE_FULLRESCAN);

            this.parseModesDict.Add(_MODE_NEWESTONLY, ParseModes.NewestOnly);
            this.parseModesDict.Add(_MODE_FULLRESCAN, ParseModes.FullRescan);

            this.select_Mode.SelectItem(_MODE_NEWESTONLY);
            this.select_Mode.DropDownStyle = ComboBoxStyle.DropDownList;

            this.tumblrStats.blog = null;

            AdvancedMenuRenderer renderer = new AdvancedMenuRenderer();
            renderer.HighlightForeColor = Color.Maroon;
            renderer.HighlightBackColor = Color.White;
            renderer.ForeColor = Color.Black;
            renderer.BackColor = Color.White;

            this.menu_TopMenu.Renderer = renderer;
            this.txt_WorkStatus.Visible = false;
            this.txt_Stats_BlogDescription.Visible = false;
            this.lbl_Stats_BlogTitle.Text = string.Empty;
            this.lbl_PercentBar.Text = string.Empty;

            this.bar_Progress.Visible = true;
            this.bar_Progress.Minimum = 0;
            this.bar_Progress.Maximum = 100;
            this.bar_Progress.Step = 1;
            this.bar_Progress.Value = 0;

            this.downloadManager = new DownloadManager();
            this.Text += " " + _VERSION + "";
            this.lbl_About_Version.Text = "Version: " + _VERSION;

            string file = this.optionsFileName;

            if (File.Exists(file))
            {
                this.LoadOptions(file);
            }
            else
            {
                SetOptions();
                this.SaveOptions(file);
            }

            this.lbl_Size.Text = string.Empty;
            this.lbl_Size.Visible = false;
            this.lbl_PostCount.Text = string.Empty;
            this.lbl_PostCount.Visible = false;
            this.lbl_Status.Text = _STATUS_READY;
            this.lbl_Status.Visible = true;
            this.lbl_PercentBar.Text = string.Empty;
            this.lbl_PercentBar.Visible = true;

            SetDoubleBuffering(this.bar_Progress, true);
            SetDoubleBuffering(this.img_DisplayImage, true);

            this.tabControl_Main.SelectedIndex = 0;
        }

        public bool IsValidURL(string urlString)
        {
            try
            {
                return urlString.IsValidUrl();
            }
            catch (Exception e)
            {
                string error = e.Message;
                return false;
            }
        }

        private void btn_Accept_Click(object sender, EventArgs e)
        {
            this.SetOptions();
            this.SaveOptions(this.optionsFileName);
            this.UpdateStatusText("Options saved");
        }

        private void check_ParseDownload_CheckedChanged(object sender, EventArgs e)
        {
            if (this.check_Options_ParseDownload.Checked)
            {
                this.check_Options_ParseOnly.Checked = false;
            }
        }

        private void check_ParseOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (this.check_Options_ParseOnly.Checked)
            {
                this.check_Options_ParseDownload.Checked = false;
            }
        }

        public void LoadOptions(string filename)
        {
            this.options = JSONHelper.ReadObject<ToolOptions>(filename);
            this.options.apiMode = ApiModeEnum.v2JSON.ToString();
            this.RestoreOptions();
        }

        public void RestoreOptions()
        {
            this.check_Options_ParseGIF.Checked = this.options.parseGIF;
            this.check_Options_ParseJPEG.Checked = this.options.parseJPEG;
            this.check_Options_ParseDownload.Checked = !this.options.parseOnly;
            this.check_Options_ParseOnly.Checked = this.options.parseOnly;
            this.check_Options_ParsePhotoSets.Checked = this.options.parsePhotoSets;
            this.check_Options_ParsePNG.Checked = this.options.parsePNG;
            this.apiMode = this.options.apiMode;
            this.check_Options_GenerateLog.Checked = this.options.generateLog;
        }

        public void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btn_Start.Enabled = false;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.AddExtension = true;
                ofd.DefaultExt = ".tumblr";
                ofd.RestoreDirectory = true;
                ofd.Filter = "Tumblr Tools Files (.tumblr)|*.tumblr|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    UpdateStatusText(_STATUS_OPENSAVEFILE);

                    this.fileBackgroundWorker.RunWorkerAsync(ofd.FileName);
                }
                else
                {
                    this.btn_Start.Enabled = true;
                }
            }
        }

        public void OpenTumblrFile(string file)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    this.tumblrSaveFile = !string.IsNullOrEmpty(file) ? FileHelper.ReadTumblrFile(file) : null;

                    //this.tumblrBlog = this.tumblrSaveFile != null ? this.tumblrSaveFile.blog : null;

                    this.txt_SaveLocation.Text = !string.IsNullOrEmpty(file) ? Path.GetDirectoryName(file) : string.Empty;

                    if (this.tumblrSaveFile != null && this.tumblrSaveFile.blog != null && !string.IsNullOrEmpty(this.tumblrSaveFile.blog.url))
                    {
                        this.txt_TumblrURL.Text = this.tumblrSaveFile.blog.url;
                    }
                    else if (this.tumblrSaveFile != null && this.tumblrSaveFile.blog != null && string.IsNullOrEmpty(this.tumblrSaveFile.blog.url) && !string.IsNullOrEmpty(this.tumblrSaveFile.blog.cname))
                    {
                        this.txt_TumblrURL.Text = this.tumblrSaveFile.blog.cname;
                    }
                    else
                    {
                        this.txt_TumblrURL.Text = "Error parsing save file...";
                    }

                    UpdateStatusText(_STATUS_READY);
                    this.btn_Start.Enabled = true;
                }
            }
            catch
            {
            }
        }

        public void SaveLogFile()
        {
            FileHelper.SaveTumblrFile(this.saveLocation + @"\" + this.tumblrLogFile.filename, this.tumblrLogFile);
        }

        public void SaveOptions(string filename)
        {
            JSONHelper.SaveObject<ToolOptions>(filename, this.options);
        }

        public void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile saveFile = new SaveFile(this.ripper.blog.name + ".tumblr", this.ripper.blog);
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.AddExtension = true;
                sfd.DefaultExt = ".tumblr";
                sfd.Filter = "Tumblr Tools Files (.tumblr)|*.tumblr";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (FileHelper.SaveTumblrFile(sfd.FileName, saveFile))
                    {
                        MessageBox.Show("Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public bool SaveTumblrFile(string name)
        {
            this.tumblrSaveFile = new SaveFile(name + ".tumblr", this.ripper.blog);

            return FileHelper.SaveTumblrFile(this.saveLocation + @"\" + this.tumblrSaveFile.filename, this.tumblrSaveFile);
        }

        public void SetDoubleBuffering(System.Windows.Forms.Control control, bool value)
        {
            System.Reflection.PropertyInfo controlProperty = typeof(System.Windows.Forms.Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            controlProperty.SetValue(control, value, null);
        }

        public void SetOptions()
        {
            this.options.parsePNG = this.check_Options_ParsePNG.Checked;
            this.options.parseJPEG = this.check_Options_ParseJPEG.Checked;
            this.options.parseGIF = this.check_Options_ParseGIF.Checked;
            this.options.parsePhotoSets = this.check_Options_ParsePhotoSets.Checked;
            this.options.apiMode = Enum.GetName(typeof(ApiModeEnum), this.select_Options_APIMode.SelectedIndex);
            this.options.parseOnly = this.check_Options_ParseOnly.Checked;
            this.options.apiMode = this.apiMode;
            this.options.generateLog = this.check_Options_GenerateLog.Checked;
        }

        public void StatsTumblrURLUpdate(object sender, EventArgs e)
        {
            this.txt_Stats_TumblrURL.Text = this.txt_TumblrURL.Text;
        }

        public void TabControl_Main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (this.disableOtherTabs)
            {
                KRBTabControl.KRBTabControl tabWizardControl = sender as KRBTabControl.KRBTabControl;

                int selectedTab = tabWizardControl.SelectedIndex;

                KRBTabControl.TabPageEx tabPage = (KRBTabControl.TabPageEx)tabWizardControl.SelectedTab;

                //Disable the tab selection
                if (this.currentSelectedTab != selectedTab)
                {
                    //If selected tab is different than the current one, re-select the current tab.
                    //This disables the navigation using the tab selection.
                    tabWizardControl.SelectTab(this.currentSelectedTab);
                }
            }
        }

        public void TabMainTabSelect_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        public void TabPage_Enter(object sender, EventArgs e)
        {
            this.currentSelectedTab = this.tabControl_Main.SelectedIndex;
        }

        public void ToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItem TSMI = sender as ToolStripMenuItem;

            AdvancedMenuRenderer renderer = TSMI.GetCurrentParent().Renderer as AdvancedMenuRenderer;

            renderer.ChangeTextForeColor(TSMI, e);
        }

        public void Txt_StatsTumblrURL_TextChanged(object sender, EventArgs e)
        {
            this.txt_TumblrURL.Text = this.txt_Stats_TumblrURL.Text;
        }

        public void UpdateStatusText(string text)
        {
            if (!this.lbl_Status.Text.Contains(text))
            {
                this.lbl_Status.Text = text;
                this.lbl_Status.Invalidate();
                this.status_Strip.Update();
                this.status_Strip.Refresh();
            }
        }

        public void UpdateWorkStatusTextConcat(string strToReplace, string strToAdd = "")
        {
            if (this.txt_WorkStatus.Text.Contains(strToReplace) && !this.txt_WorkStatus.Text.Contains(string.Concat(strToReplace, strToAdd)))
            {
                this.txt_WorkStatus.Text = txt_WorkStatus.Text.Replace(strToReplace, string.Concat(strToReplace, strToAdd));

                this.txt_WorkStatus.Update();
                this.txt_WorkStatus.Refresh();
            }
        }

        public void UpdateWorkStatusTextNewLine(string text)
        {
            if (!this.txt_WorkStatus.Text.Contains(text))
            {
                this.txt_WorkStatus.Text += txt_WorkStatus.Text != "" ? "\r\n" : "";
                this.txt_WorkStatus.Text += text;
                this.txt_WorkStatus.Update();
                this.txt_WorkStatus.Refresh();
            }
        }

        public void UpdateWorkStatusTextReplace(string str, string replaceStr)
        {
            if (this.txt_WorkStatus.Text.Contains(str))
            {
                this.txt_WorkStatus.Text = txt_WorkStatus.Text.Replace(str, replaceStr);

                this.txt_WorkStatus.Update();
                this.txt_WorkStatus.Refresh();
            }
        }

        public void WorkStatusAutoScroll(object sender, EventArgs e)
        {
            this.txt_WorkStatus.SelectionStart = this.txt_WorkStatus.TextLength;
            this.txt_WorkStatus.ScrollToCaret();
        }

        private void select_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void TabControl_Main_SelectedIndexChanging(object sender, KRBTabControl.KRBTabControl.SelectedIndexChangingEventArgs e)
        {
            if (this.disableOtherTabs)
            {
                KRBTabControl.KRBTabControl tabWizardControl = sender as KRBTabControl.KRBTabControl;

                int selectedTab = tabWizardControl.SelectedIndex;

                KRBTabControl.TabPageEx tabPage = (KRBTabControl.TabPageEx)tabWizardControl.SelectedTab;

                //Disable the tab selection
                if (e.TabPage != tabPage)
                {
                    //If selected tab is different than the current one, re-select the current tab.
                    //This disables the navigation using the tab selection.

                    e.Cancel = true;
                    tabWizardControl.SelectTab(this.currentSelectedTab);
                }
            }

            if (e.TabPage.Text == "IsSelectable?")
                e.Cancel = true;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.RestoreOptions();
        }

        private void lbl_About_Click(object sender, EventArgs e)
        {
        }
    }
}