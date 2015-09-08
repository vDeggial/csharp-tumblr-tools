/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: September, 2015
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
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Image_Ripper;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Objects;
using Tumblr_Tool.Objects.Tumblr_Objects;
using Tumblr_Tool.Properties;
using Tumblr_Tool.Tumblr_Stats;

namespace Tumblr_Tool
{
    public partial class mainForm : Form
    {
        public const string _IMAGESIZE_LARGE = "Large";
        public const string _IMAGESIZE_MEDIUM = "Medium";
        public const string _IMAGESIZE_ORIGINAL = "Original";
        public const string _IMAGESIZE_SMALL = "Small";
        public const string _IMAGESIZE_SQUARE = "Square";
        public const string _IMAGESIZE_XSMALL = "xSmall";
        public const string _MODE_FULLRESCAN = "Full Rescan";
        public const string _MODE_NEWESTONLY = "Newest Only";
        public const string _PERCENT = "{0}%";
        public const string _POSTCOUNT = "{0}/{1}";
        public const string _RESULT_DONE = " done";
        public const string _RESULT_ERROR = "error";
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
        public const string _SUFFIX_KB = "KB";
        public const string _SUFFIX_MB = "MB";
        public const string _VERSION = "1.3.2";
        public const string _WELCOME_MSG = "\r\n\r\n\r\n\r\n\r\nWelcome to Tumblr Tools!\r\nVersion: " + _VERSION + "\r\n© 2013 - 2015 Shino Amakusa";
        public const string _WORKTEXT_CHECKINGCONNX = "Checking connection ...";
        public const string _WORKTEXT_DOWNLOADINGIMAGES = "Downloading ...";
        public const string _WORKTEXT_GETTINGBLOGINFO = "Getting info ...";
        public const string _WORKTEXT_INDEXINGPOSTS = "Indexing ...";
        public const string _WORKTEXT_READINGLOG = "Reading log ...";
        public const string _WORKTEXT_SAVELOG = "Saving log ...";
        public const string _WORKTEXT_STARTING = "Starting ...";
        public const string _WORKTEXT_UPDATINGLOG = "Updating log...";
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

        public string CurrentImage { get; set; }

        public int CurrentPercent { get; set; }

        public int CurrentPostCount { get; set; }

        public int CurrentSelectedTab { get; set; }

        public decimal CurrentSize { get; set; }

        public bool DisableOtherTabs { get; set; }

        public List<string> DownloadedList { get; set; }

        public List<int> DownloadedSizesList { get; set; }

        public DownloadManager DownManager { get; set; }

        public string ErrorMessage { get; set; }
        public Dictionary<string, ImageSizes> ImageSizesIndexDict { get; set; }
        public bool IsCancelled { get; set; }

        public bool IsCrawlingDone { get; set; }

        public bool IsDownloadDone { get; set; }

        public bool IsExitTime { get; set; }

        public bool IsFileDownloadDone { get; set; }

        public bool IsReadyForDownload { get; set; }

        public List<string> NotDownloadedList { get; set; }

        public ToolOptions Options { get; set; }

        public Dictionary<string, ParseModes> ParseModesDict { get; set; }
        public ImageRipper Ripper { get; set; }

        public string SaveLocation { get; set; }

        public SaveFile TumblrLogFile { get; set; }

        public SaveFile TumblrSaveFile { get; set; }

        public TumblrStats TumblrStats { get; set; }

        public string TumblrURL { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        public void AddWorkStatusText(string str)
        {
            if (!this.txt_WorkStatus.Text.EndsWith(str))
            {
                this.txt_WorkStatus.Text += str;

                this.txt_WorkStatus.Update();
                this.txt_WorkStatus.Refresh();
            }
        }

        public void BrowseLocation(object sender, EventArgs e)
        {
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)

                    this.txt_SaveLocation.Text = ofd.SelectedPath;
            }
        }

        public void ButtonOnMouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;

            button.UseVisualStyleBackColor = false;
            button.ForeColor = Color.Maroon;
            button.FlatAppearance.BorderColor = Color.Maroon;
            button.FlatAppearance.MouseOverBackColor = Color.White;
            button.FlatAppearance.BorderSize = 1;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonOnMouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.UseVisualStyleBackColor = true;
            button.ForeColor = Color.Black;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.White;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CrawlWorker_AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (this.Ripper != null)
                {
                    this.IsCrawlingDone = true;

                    if (this.Ripper.StatusCode == ProcessingCodes.Done)
                    {
                        this.TumblrSaveFile.Blog = this.Ripper.Blog;
                        //tumblrLogFile = null;
                        //ripper.tumblrPostLog = null;

                        this.TumblrLogFile = this.Ripper.TumblrPostLog;

                        this.IsCrawlingDone = true;

                        if (this.IsCrawlingDone && !this.check_Options_ParseOnly.Checked && !this.IsCancelled && this.Ripper.ImageList.Count != 0)
                        {
                            this.DownManager.TotalFilesToDownload = this.Ripper.ImageList.Count;

                            this.IsDownloadDone = false;
                            this.DownloadedList = new List<string>();
                            this.NotDownloadedList = new List<string>();
                            this.DownloadedSizesList = new List<int>();

                            this.download_UIUpdate_Worker.RunWorkerAsync();

                            this.download_Worker.RunWorkerAsync(Ripper.ImageList);
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
        public void CrawlWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread.Sleep(200);

                this.IsReadyForDownload = false;
                this.IsCancelled = false;

                lock (this.Ripper)
                {
                    if (this.TumblrSaveFile != null && this.Options.GenerateLog)
                    {
                        string file = this.SaveLocation + @"\" + Path.GetFileNameWithoutExtension(this.TumblrSaveFile.Filename) + ".log";

                        if (File.Exists(file))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                UpdateStatusText(_WORKTEXT_READINGLOG);
                                UpdateWorkStatusTextNewLine(_WORKTEXT_READINGLOG);
                            });

                            this.TumblrLogFile = FileHelper.ReadTumblrFile(file);

                            this.Invoke((MethodInvoker)delegate
                            {
                                UpdateWorkStatusTextConcat(_WORKTEXT_READINGLOG, _RESULT_DONE);
                            });
                        }
                    }

                    //this.tumblrBlog = new TumblrBlog();
                    //this.tumblrBlog.url = this.tumblrURL;

                    this.Ripper = new ImageRipper(new TumblrBlog(this.TumblrURL), this.SaveLocation, this.check_Options_GenerateLog.Checked, this.check_Options_ParsePhotoSets.Checked,
                        this.check_Options_ParseJPEG.Checked, this.check_Options_ParsePNG.Checked, this.check_Options_ParseGIF.Checked);
                    Ripper.TumblrPostLog = this.TumblrLogFile;
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Ripper.ImageSize = ImageSizesIndexDict[select_ImagesSize.Items[select_ImagesSize.SelectedIndex].ToString()];
                    });
                    this.Ripper.StatusCode = ProcessingCodes.Initializing;
                }

                lock (this.Ripper)
                {
                    this.Ripper.StatusCode = ProcessingCodes.checkingConnection;
                }

                if (WebHelper.CheckForInternetConnection())
                {
                    lock (this.Ripper)
                    {
                        this.Ripper.StatusCode = ProcessingCodes.connectionOK;
                    }

                    if (this.Ripper != null)
                    {
                        //this.ripper.SetAPIMode(options.apiMode);
                        this.Ripper.SetAPIMode(ApiModeEnum.v2JSON.ToString());
                        this.Ripper.SetLogFile(TumblrLogFile);

                        if (this.Ripper.TumblrExists())
                        {
                            lock (this.Ripper)
                            {
                                this.Ripper.StatusCode = ProcessingCodes.gettingBlogInfo;
                            }

                            if (this.Ripper.SetBlogInfo())
                            {
                                lock (this.Ripper)
                                {
                                    this.Ripper.StatusCode = ProcessingCodes.blogInfoOK;
                                }

                                if (!SaveTumblrFile(this.Ripper.Blog.Name))
                                {
                                    lock (this.Ripper)
                                    {
                                        this.Ripper.StatusCode = ProcessingCodes.saveFileError;
                                    }
                                }
                                else
                                {
                                    lock (this.Ripper)
                                    {
                                        this.Ripper.StatusCode = ProcessingCodes.saveFileOK;
                                    }

                                    if (this.Ripper != null)
                                    {
                                        ParseModes mode = ParseModes.NewestOnly;
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            mode = this.ParseModesDict[this.select_Mode.SelectedItem.ToString()];
                                        });

                                        lock (this.Ripper)
                                        {
                                            this.Ripper.StatusCode = ProcessingCodes.Crawling;
                                        }

                                        this.Ripper.ParseBlogPosts(mode);

                                        lock (this.Ripper)
                                        {
                                            if (this.Ripper.IsLogUpdated)
                                            {
                                                this.Ripper.StatusCode = ProcessingCodes.SavingLogFile;

                                                if (!this.IsDisposed)
                                                {
                                                    this.Invoke((MethodInvoker)delegate
                                                    {
                                                        UpdateStatusText(_WORKTEXT_SAVELOG);
                                                        UpdateWorkStatusTextNewLine(_WORKTEXT_SAVELOG);
                                                    });
                                                }
                                                this.TumblrLogFile = this.Ripper.TumblrPostLog;
                                                SaveLogFile();

                                                if (!this.IsDisposed)
                                                {
                                                    this.Invoke((MethodInvoker)delegate
                                                    {
                                                        UpdateStatusText("Log Saved");
                                                        UpdateWorkStatusTextConcat(_WORKTEXT_SAVELOG, _RESULT_DONE);
                                                    });

                                                    this.Ripper.StatusCode = ProcessingCodes.Done;
                                                }
                                            }
                                        }
                                    }
                                }

                                lock (this.Ripper)
                                {
                                    this.Ripper.StatusCode = ProcessingCodes.Done;
                                }
                            }
                            else
                            {
                                lock (this.Ripper)
                                {
                                    this.Ripper.StatusCode = ProcessingCodes.blogInfoError;
                                }
                            }
                        }
                        else
                        {
                            lock (this.Ripper)
                            {
                                this.Ripper.StatusCode = ProcessingCodes.invalidURL;
                            }
                        }
                    }
                }
                else
                {
                    lock (this.Ripper)
                    {
                        this.Ripper.StatusCode = ProcessingCodes.connectionError;
                    }
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CrawlWorker_UI__AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (this.check_Options_ParseOnly.Checked)
                        {
                            EnableUI_Crawl(true);
                        }
                        else if (this.Ripper.StatusCode != ProcessingCodes.Done)
                        {
                            EnableUI_Crawl(true);
                        }
                        else
                        {
                            if (this.IsCrawlingDone && !this.check_Options_ParseOnly.Checked && !this.IsCancelled && this.Ripper.ImageList.Count != 0)
                            {
                                UpdateStatusText(string.Format(_STATUS_DOWNLOADING, "Initializing"));
                                this.bar_Progress.Value = 0;

                                this.lbl_PercentBar.Text = string.Format(_PERCENT, "0");

                                this.lbl_PostCount.Text = string.Format(_POSTCOUNT, "0", this.Ripper.ImageList.Count);
                            }
                            else
                            {
                                UpdateStatusText(_STATUS_READY);
                                this.img_DisplayImage.Image = Resources.tumblrlogo;
                                EnableUI_Crawl(true);
                            }
                        }
                    });
                }
            }
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CrawlWorker_UI__DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this.Ripper != null)
                {
                    if (!this.IsDisposed)
                    {
                        this.Invoke((MethodInvoker)delegate
                                {
                                    this.img_DisplayImage.Image = Resources.crawling;
                                });
                    }

                    int percent = 0;

                    while (!this.IsCrawlingDone)
                    {
                        if (this.Ripper.StatusCode == ProcessingCodes.checkingConnection)
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

                        if (this.Ripper.StatusCode == ProcessingCodes.connectionOK)
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
                        if (this.Ripper.StatusCode == ProcessingCodes.connectionError)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateStatusText(_STATUS_ERROR);
                                    UpdateWorkStatusTextConcat(_WORKTEXT_CHECKINGCONNX, _RESULT_FAIL);
                                    this.btn_ImageCrawler_Start.Enabled = true;
                                    this.img_DisplayImage.Visible = true;
                                    this.img_DisplayImage.Image = Resources.tumblrlogo;
                                    this.tab_TumblrStats.Enabled = true;
                                    this.IsCrawlingDone = true;
                                });
                            }
                        }

                        if (this.Ripper.StatusCode == ProcessingCodes.gettingBlogInfo)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine(_WORKTEXT_GETTINGBLOGINFO);
                                });
                            }
                        }

                        if (this.Ripper.StatusCode == ProcessingCodes.blogInfoOK)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextConcat(_WORKTEXT_GETTINGBLOGINFO, _RESULT_SUCCESS);

                                    this.txt_WorkStatus.Visible = true;

                                    this.txt_WorkStatus.SelectionStart = this.txt_WorkStatus.Text.Length;
                                });
                            }
                        }

                        if (this.Ripper.StatusCode == ProcessingCodes.Starting)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Indexing " + "\"" + this.Ripper.Blog.Title + "\" ... ");
                                });
                            }
                        }

                        if (this.Ripper.StatusCode == ProcessingCodes.UnableDownload)
                        {
                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    UpdateWorkStatusTextNewLine("Unable to get post info from API (Offset " + this.Ripper.NumberOfParsedPosts.ToString() + " - " + (Ripper.NumberOfParsedPosts + (int)PostStepEnum.JSON).ToString() + ") ... ");
                                });
                            }
                            Ripper.StatusCode = ProcessingCodes.Crawling;
                        }

                        if (this.Ripper.StatusCode == ProcessingCodes.Crawling)
                        {
                            if (this.Ripper.TotalNumberOfPosts != 0 && this.Ripper.NumberOfParsedPosts == 0)
                            {
                                if (!this.IsDisposed)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        UpdateWorkStatusTextNewLine(this.Ripper.TotalNumberOfPosts + " photo posts found.");
                                    });
                                }
                            }

                            if (!this.IsDisposed && this.Ripper.NumberOfParsedPosts == 0)
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

                            percent = this.Ripper.PercentComplete;

                            if (percent > 100)
                                percent = 100;

                            if (this.CurrentPercent != percent)
                            {
                                this.CurrentPercent = percent;
                                if (!this.IsDisposed)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                       {
                                           //if (!this.lbl_PercentBar.Visible)
                                           //{
                                           //    this.lbl_PercentBar.Visible = true;
                                           //}

                                           this.lbl_PercentBar.Text = string.Format(_PERCENT, percent.ToString());
                                           this.bar_Progress.Value = percent;
                                       });
                                }
                            }

                            if (this.CurrentPostCount != this.Ripper.NumberOfParsedPosts)
                            {
                                this.CurrentPostCount = this.Ripper.NumberOfParsedPosts;
                                if (!this.IsDisposed)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        //if (!this.lbl_PostCount.Visible)
                                        //{
                                        //    this.lbl_PostCount.Visible = true;
                                        //}
                                        if (this.lbl_PostCount.DisplayStyle != ToolStripItemDisplayStyle.ImageAndText)
                                        {
                                            this.lbl_PostCount.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                                        }
                                        this.lbl_PostCount.Text = string.Format(_POSTCOUNT, this.Ripper.NumberOfParsedPosts.ToString(), this.Ripper.TotalNumberOfPosts.ToString());
                                    });
                                }
                            }
                        }
                    }

                    if (this.Ripper != null)
                    {
                        if (!this.IsDisposed && this.Ripper.StatusCode == ProcessingCodes.Done && !this.IsCancelled)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                UpdateWorkStatusTextConcat(_WORKTEXT_INDEXINGPOSTS, _RESULT_DONE);

                                UpdateWorkStatusTextNewLine("Found " + (this.Ripper.ImageList.Count == 0 ? "no" : this.Ripper.ImageList.Count.ToString()) + " new image(s) to download");

                                this.bar_Progress.Value = 0;
                                this.lbl_PercentBar.Text = string.Empty;
                            });
                        }

                        if (this.Ripper.StatusCode == ProcessingCodes.UnableDownload)
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
                        if (this.Ripper.StatusCode == ProcessingCodes.invalidURL)
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
        public void DownloadWorker_AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            lock (this.DownManager)
            {
                this.DownManager.StatusCode = DownloadStatusCodes.Done;
            }

            try
            {
                this.TumblrSaveFile.Blog.Posts = null;
                SaveTumblrFile(this.Ripper.Blog.Name);

                if (this.Options.GenerateLog && this.DownloadedList.Count > 0)
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

                    this.Ripper.TumblrPostLog = null;
                    this.TumblrLogFile = null;
                }
                this.IsDownloadDone = true;
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
        public void DownloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(200);
            try
            {
                this.DownloadedList = new List<string>();
                this.DownloadedSizesList = new List<int>();
                this.IsDownloadDone = false;
                this.IsFileDownloadDone = false;
                this.IsCancelled = false;
                FileInfo file;
                HashSet<PhotoPostImage> imagesList = (HashSet<PhotoPostImage>)e.Argument;
                DownManager.TotalFilesToDownload = imagesList.Count;

                lock (this.DownManager)
                {
                    this.DownManager.StatusCode = DownloadStatusCodes.Preparing;
                }

                while (!IsReadyForDownload)
                {
                    //wait
                }

                if (imagesList != null && imagesList.Count != 0)
                {
                    int j = 0;

                    lock (this.DownManager)
                    {
                        this.DownManager.StatusCode = DownloadStatusCodes.Downloading;
                    }

                    foreach (PhotoPostImage photoImage in imagesList)
                    {
                        if (this.IsCancelled)
                            break;

                        this.IsFileDownloadDone = false;

                        lock (this.DownManager)
                        {
                            this.DownManager.StatusCode = DownloadStatusCodes.Downloading;
                        }

                        bool downloaded = false;
                        string fullPath = string.Empty;

                        while (!this.IsFileDownloadDone && !this.IsCancelled)
                        {
                            try
                            {
                                fullPath = FileHelper.GenerateLocalPathToFile(photoImage.Filename, this.SaveLocation);

                                downloaded = DownManager.DownloadFile(photoImage.Url, this.SaveLocation);

                                if (downloaded)
                                {
                                    j++;
                                    this.IsFileDownloadDone = true;
                                    photoImage.Downloaded = true;
                                    fullPath = FileHelper.AddJpgExt(fullPath);

                                    file = new FileInfo(fullPath);

                                    lock (this.DownloadedList)
                                    {
                                        this.DownloadedList.Add(fullPath);
                                    }

                                    lock (this.DownloadedSizesList)
                                    {
                                        this.DownloadedSizesList.Add((int)file.Length);
                                    }
                                }
                                else if (this.DownManager.StatusCode == DownloadStatusCodes.UnableDownload)
                                {
                                    lock (this.NotDownloadedList)
                                    {
                                        this.NotDownloadedList.Add(photoImage.Url);
                                    }
                                    photoImage.Downloaded = false;

                                    if (FileHelper.FileExists(fullPath))
                                    {
                                        file = new FileInfo(fullPath);

                                        if (!FileHelper.IsFileLocked(file)) file.Delete();
                                    }
                                }
                            }
                            catch
                            {
                                lock (this.NotDownloadedList)
                                {
                                    this.NotDownloadedList.Add(photoImage.Url);
                                }
                                photoImage.Downloaded = false;
                                if (FileHelper.FileExists(fullPath))
                                {
                                    file = new FileInfo(fullPath);

                                    if (!FileHelper.IsFileLocked(file)) file.Delete();
                                }
                            }
                            finally
                            {
                                this.IsFileDownloadDone = true;
                            }
                        }

                        if (this.IsCancelled)
                        {
                            if (FileHelper.FileExists(fullPath))
                            {
                                file = new FileInfo(fullPath);

                                if (!FileHelper.IsFileLocked(file)) file.Delete();
                            }
                        }
                    }

                    this.IsDownloadDone = true;
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
        public void DownloadWorker_UI__AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    try
                    {
                        if (this.DownloadedList.Count == 0)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.img_DisplayImage.Image = Resources.tumblrlogo;
                            });
                        }
                        else
                        {
                            if (!IsCancelled)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    Bitmap img = GetImageFromFile(this.DownloadedList[this.DownloadedList.Count - 1]);

                                    if (img != null)
                                    {
                                        this.img_DisplayImage.Image = GetImageFromFile(this.DownloadedList[this.DownloadedList.Count - 1]);
                                    }

                                    this.lbl_PostCount.Text = string.Format(_POSTCOUNT, this.DownloadedList.Count, this.Ripper.ImageList.Count);
                                });
                            }
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

                    if (this.DownloadedList.Count > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(_WORKTEXT_DOWNLOADINGIMAGES, _RESULT_DONE);
                            UpdateWorkStatusTextNewLine("Downloaded " + this.DownloadedList.Count.ToString() + " image(s).");
                            this.bar_Progress.Value = 0;
                            this.lbl_PercentBar.Text = string.Empty;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextConcat(_WORKTEXT_DOWNLOADINGIMAGES, _RESULT_ERROR);
                            UpdateWorkStatusTextNewLine("We were unable to download images ...");
                            this.bar_Progress.Value = 0;
                            this.lbl_PercentBar.Text = string.Empty;
                            this.lbl_PostCount.Visible = false;
                            this.lbl_Size.Visible = false;
                        });
                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText(_STATUS_DONE);
                        EnableUI_Crawl(true);
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
        public void DownloadWorker_UI__DoWork(object sender, DoWorkEventArgs e)
        {
            this.CurrentPercent = 0;
            this.CurrentPostCount = 0;
            this.CurrentSize = 0;
            try
            {
                if (this.DownManager == null) this.DownManager = new DownloadManager();

                if (this.Ripper.ImageList.Count == 0)
                {
                    if (!this.IsDisposed)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            //this.bar_Progress.Visible = false;

                            //this.lbl_PostCount.Visible = false;
                            UpdateStatusText(_STATUS_DONE); ;
                        });
                    }
                }
                else
                {
                    //this.Invoke((MethodInvoker)delegate
                    //{
                    //    if (!this.bar_Progress.Visible)
                    //    {
                    //        this.bar_Progress.Visible = true;
                    //    }

                    //    if (!this.lbl_PercentBar.Visible)
                    //    {
                    //        this.lbl_PercentBar.Text = string.Format(_PERCENT, "0");
                    //        this.lbl_PercentBar.Visible = true;
                    //    }

                    //    if (!this.lbl_PostCount.Visible)
                    //        this.lbl_PostCount.Visible = true;

                    //    if (!this.lbl_Size.Visible)
                    //    {
                    //        this.lbl_Size.Text = string.Format(_SIZE, "0.00", _SUFFIX_MB);
                    //        this.lbl_Size.Visible = true;
                    //    }
                    //});

                    decimal totalLength = 0;

                    if (!this.IsDisposed && DownloadedList.Count < 10)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateWorkStatusTextNewLine(_WORKTEXT_DOWNLOADINGIMAGES);
                            this.lbl_Size.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                        });

                        this.IsReadyForDownload = true;
                    }

                    while (!this.IsDownloadDone && !this.IsCancelled)
                    {
                        int c = 0;
                        int f = 0;

                        if (this.NotDownloadedList.Count != 0 && f != this.NotDownloadedList.Count)
                        {
                            f = this.NotDownloadedList.Count;

                            if (!this.IsDisposed)
                            {
                                this.Invoke((MethodInvoker)delegate
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

                        if (this.DownloadedList.Count != 0 && c != this.DownloadedList.Count)
                        {
                            c = this.DownloadedList.Count;

                            if (!this.IsDisposed)
                            {
                                if (this.CurrentImage != this.DownloadedList[c - 1])
                                {
                                    this.CurrentImage = this.DownloadedList[c - 1];
                                    try
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            // this.img_DisplayImage.ImageLocation = this.downloadedList[c - 1];
                                            this.img_DisplayImage.Image.Dispose();

                                            Bitmap img = GetImageFromFile((this.DownloadedList[c - 1]));

                                            if (img != null)
                                            {
                                                this.img_DisplayImage.Image = img;
                                                this.img_DisplayImage.Refresh();
                                            }
                                        });
                                    }
                                    catch (Exception)
                                    {
                                        this.img_DisplayImage.Image = Resources.tumblrlogo;
                                        this.img_DisplayImage.Update();
                                        this.img_DisplayImage.Refresh();
                                    }
                                }

                                int downloaded = this.DownloadedList.Count;
                                int total = this.DownManager.TotalFilesToDownload;

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

                                if (this.CurrentPercent != percent)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        this.bar_Progress.Value = percent;
                                        this.lbl_PercentBar.Text = string.Format(_PERCENT, percent.ToString());
                                    });
                                }

                                if (this.CurrentPostCount != downloaded)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        this.lbl_PostCount.Text = string.Format(_POSTCOUNT, downloaded.ToString(), total.ToString());
                                    });
                                }

                                try
                                {
                                    totalLength = (this.DownloadedSizesList.Sum(x => Convert.ToInt32(x)) / (decimal)1024 / (decimal)1024);
                                    decimal totalLengthNum = totalLength > 1024 ? totalLength / 1024 : totalLength;
                                    string suffix = totalLength > 1024 ? _SUFFIX_GB : _SUFFIX_MB;

                                    if (this.CurrentSize != totalLength)
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

                                if ((int)this.DownManager.PercentDownloaded <= 0)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        UpdateStatusText(string.Format(_STATUS_DOWNLOADING, "Connecting"));
                                    });
                                }
                                else if (percent != (int)this.DownManager.PercentDownloaded)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        UpdateStatusText(string.Format(_STATUS_DOWNLOADING, this.DownManager.PercentDownloaded.ToString() + "%"));
                                    });
                                }

                                this.CurrentPercent = percent;
                                this.CurrentPostCount = downloaded;
                                this.CurrentSize = totalLength;
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
        public void EnableUI_Crawl(bool state)
        {
            this.btn_Browse.Enabled = state;
            this.btn_ImageCrawler_Start.Visible = state;
            this.btn_ImageCrawler_Stop.Visible = !state;
            this.select_Mode.Enabled = state;
            this.select_ImagesSize.Enabled = state;
            this.fileToolStripMenuItem.Enabled = state;
            this.txt_TumblrURL.Enabled = state;
            this.txt_SaveLocation.Enabled = state;
            this.DisableOtherTabs = !state;

            this.bar_Progress.Visible = !state;
            this.lbl_PercentBar.Visible = !state;

            this.lbl_PostCount.Visible = !state;
            this.lbl_Size.Visible = !state;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        public void EnableUI_Stats(bool state)
        {
            this.btn_Stats_Start.Enabled = state;
            this.fileToolStripMenuItem.Enabled = state;
            this.txt_Stats_TumblrURL.Enabled = state;
            this.DisableOtherTabs = !state;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExitApplication(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!IsExitTime)
                {
                    this.IsExitTime = true;
                    DialogResult dialogResult = MsgBox.Show("Are you sure you want to exit Tumblr Tools?", "Exit", MsgBox.Buttons.YesNo, MsgBox.Icon.Question, MsgBox.AnimateStyle.FadeIn, false);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something

                        this.Ripper.IsCancelled = true;
                        this.IsCancelled = true;
                        this.IsDownloadDone = true;
                        this.IsCrawlingDone = true;

                        if (this.TumblrLogFile == null) this.TumblrLogFile = this.Ripper.TumblrPostLog;

                        if (!this.IsDisposed)
                        {
                            UpdateStatusText("Exiting...");
                            UpdateWorkStatusTextNewLine("Exiting ...");
                        }

                        if (this.Options.GenerateLog && this.TumblrLogFile != null && this.Ripper.IsLogUpdated)
                        {
                            Thread thread = new Thread(SaveLogFile);
                            thread.Start();
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        e.Cancel = true;
                        this.IsExitTime = false;
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FileBW_AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        public Bitmap GetImageFromFile(string file)
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
        public void GetStatsWorker_AfterDone(object sender, RunWorkerCompletedEventArgs e)
        {
            this.TumblrStats.StatusCode = ProcessingCodes.Done;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetStatsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.TumblrStats.StatusCode = ProcessingCodes.Initializing;
            try
            {
                this.TumblrStats = new TumblrStats();

                if (WebHelper.CheckForInternetConnection())
                {
                    this.TumblrStats = new TumblrStats(new TumblrBlog(this.TumblrURL), this.TumblrURL, this.Options.ApiMode);
                    this.TumblrStats.StatusCode = ProcessingCodes.Initializing;

                    this.TumblrStats.getStats();
                }
                else
                {
                    this.TumblrStats.StatusCode = ProcessingCodes.connectionError;
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

                while (this.TumblrStats.StatusCode != ProcessingCodes.Done && this.TumblrStats.StatusCode != ProcessingCodes.connectionError && this.TumblrStats.StatusCode != ProcessingCodes.invalidURL)
                {
                    if (this.TumblrStats.Blog == null)
                    {
                        // wait till other worker created and populated blog info
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.lbl_PercentBar.Text = "Getting initial blog info ... ";
                        });
                    }
                    else if (string.IsNullOrEmpty(this.TumblrStats.Blog.Title) && string.IsNullOrEmpty(this.TumblrStats.Blog.Description) && this.TumblrStats.TotalPosts <= 0)
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
                                this.lbl_Stats_BlogTitle.Text = TumblrStats.Blog.Title;
                                this.lbl_Stats_TotalCount.Text = TumblrStats.TotalPostsOverall.ToString();

                                this.lbl_PostCount.Text = string.Empty;
                                this.img_Stats_Avatar.LoadAsync(JsonHelper.GetAvatarQueryString(TumblrStats.Blog.Url));
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
                                        this.txt_Stats_BlogDescription.Text = WebHelper.StripHTMLTags(this.TumblrStats.Blog.Description);
                                });
                            }

                            percent = (int)(((double)this.TumblrStats.Parsed / (double)typesCount) * 100.00);
                            if (percent < 0)
                                percent = 0;

                            if (percent >= 100)
                                percent = 100;

                            if (this.CurrentPercent != percent)
                            {
                                if (!this.IsDisposed)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        this.bar_Progress.Value = percent;
                                    });
                                }
                            }

                            if (this.CurrentPostCount != this.TumblrStats.Parsed)
                            {
                                if (!this.IsDisposed)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        this.lbl_PercentBar.Visible = true;
                                        this.lbl_Stats_TotalCount.Text = TumblrStats.TotalPostsOverall.ToString();
                                        this.lbl_Stats_PhotoCount.Text = TumblrStats.PhotoPosts.ToString();
                                        this.lbl_Stats_TextCount.Text = TumblrStats.TextPosts.ToString();
                                        this.lbl_Stats_QuoteStats.Text = TumblrStats.QuotePosts.ToString();
                                        this.lbl_Stats_LinkCount.Text = TumblrStats.LinkPosts.ToString();
                                        this.lbl_Stats_AudioCount.Text = TumblrStats.AudioPosts.ToString();
                                        this.lbl_Stats_VideoCount.Text = TumblrStats.VideoPosts.ToString();
                                        this.lbl_Stats_ChatCount.Text = TumblrStats.ChatPosts.ToString();
                                        this.lbl_Stats_AnswerCount.Text = TumblrStats.AnswerPosts.ToString();
                                        this.lbl_PercentBar.Text = string.Format(_PERCENT, percent.ToString());
                                        this.lbl_PostCount.Visible = true;
                                        this.lbl_PostCount.Text = string.Format(_POSTCOUNT, TumblrStats.Parsed.ToString(), (typesCount).ToString());
                                    });
                                }
                            }

                            this.CurrentPostCount = TumblrStats.Parsed;
                            this.CurrentPercent = percent;
                        }
                    }
                }

                if (this.TumblrStats.StatusCode == ProcessingCodes.invalidURL)
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
                else if (this.TumblrStats.StatusCode == ProcessingCodes.connectionError)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("No Internet connection detected!", _STATUS_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatusText(_STATUS_ERROR);
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
            catch (Exception exception)
            {
                MsgBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
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

            this.ParseModesDict = new Dictionary<string, ParseModes>();
            this.DownloadedList = new List<string>();
            this.DownloadedSizesList = new List<int>();
            this.NotDownloadedList = new List<string>();
            this.Options = new ToolOptions();
            this.Ripper = new ImageRipper();
            this.TumblrStats = new TumblrStats();

            this.select_Mode.Items.Clear();
            this.select_Mode.Height = 21;

            string[] modeData = { _MODE_FULLRESCAN, _MODE_NEWESTONLY };

            this.select_Mode.DataSource = modeData;

            this.ParseModesDict.Add(_MODE_NEWESTONLY, ParseModes.NewestOnly);
            this.ParseModesDict.Add(_MODE_FULLRESCAN, ParseModes.FullRescan);

            this.select_Mode.SelectItem(_MODE_NEWESTONLY);
            this.select_Mode.DropDownStyle = ComboBoxStyle.DropDownList;

            this.ImageSizesIndexDict = new Dictionary<string, ImageSizes>();

            string[] imageData = { _IMAGESIZE_ORIGINAL, _IMAGESIZE_LARGE, _IMAGESIZE_MEDIUM, _IMAGESIZE_SMALL, _IMAGESIZE_XSMALL, _IMAGESIZE_SQUARE };
            this.select_ImagesSize.Height = 21;
            this.select_ImagesSize.DataSource = imageData;

            this.ImageSizesIndexDict.Add(_IMAGESIZE_ORIGINAL, ImageSizes.Original);
            this.ImageSizesIndexDict.Add(_IMAGESIZE_LARGE, ImageSizes.Large);
            this.ImageSizesIndexDict.Add(_IMAGESIZE_MEDIUM, ImageSizes.Medium);
            this.ImageSizesIndexDict.Add(_IMAGESIZE_SMALL, ImageSizes.Small);
            this.ImageSizesIndexDict.Add(_IMAGESIZE_XSMALL, ImageSizes.xSmall);
            this.ImageSizesIndexDict.Add(_IMAGESIZE_SQUARE, ImageSizes.Square);
            this.select_ImagesSize.SelectItem(_IMAGESIZE_ORIGINAL);
            this.select_ImagesSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.select_ImagesSize.DropDownWidth = 48;

            string[] apiSource = { "API v.1 (XML)", "API v.2 (JSON)" };
            this.select_Options_APIMode.DataSource = apiSource;

            this.TumblrStats.Blog = null;

            AdvancedMenuRenderer renderer = new AdvancedMenuRenderer();
            renderer.HighlightForeColor = Color.Maroon;
            renderer.HighlightBackColor = Color.White;
            renderer.ForeColor = Color.Black;
            renderer.BackColor = Color.White;

            this.menu_TopMenu.Renderer = renderer;
            this.txt_WorkStatus.Visible = true;
            this.txt_WorkStatus.Text = _WELCOME_MSG;
            this.txt_Stats_BlogDescription.Visible = false;
            this.lbl_Stats_BlogTitle.Text = string.Empty;
            this.lbl_PercentBar.Text = string.Empty;

            this.bar_Progress.Visible = true;
            this.bar_Progress.Minimum = 0;
            this.bar_Progress.Maximum = 100;
            this.bar_Progress.Step = 1;
            this.bar_Progress.Value = 0;

            this.DownManager = new DownloadManager();
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="urlString"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        public void LoadOptions(string filename)
        {
            this.Options = JsonHelper.ReadObject<ToolOptions>(filename);
            this.Options.ApiMode = ApiModeEnum.v2JSON.ToString();
            this.RestoreOptions();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btn_ImageCrawler_Start.Enabled = false;
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
                    this.btn_ImageCrawler_Start.Enabled = true;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        public void OpenTumblrFile(string file)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    this.TumblrSaveFile = !string.IsNullOrEmpty(file) ? FileHelper.ReadTumblrFile(file) : null;

                    //this.tumblrBlog = this.tumblrSaveFile != null ? this.tumblrSaveFile.blog : null;

                    this.txt_SaveLocation.Text = !string.IsNullOrEmpty(file) ? Path.GetDirectoryName(file) : string.Empty;

                    if (this.TumblrSaveFile != null && this.TumblrSaveFile.Blog != null && !string.IsNullOrEmpty(this.TumblrSaveFile.Blog.Url))
                    {
                        this.txt_TumblrURL.Text = this.TumblrSaveFile.Blog.Url;
                    }
                    else if (this.TumblrSaveFile != null && this.TumblrSaveFile.Blog != null && string.IsNullOrEmpty(this.TumblrSaveFile.Blog.Url) && !string.IsNullOrEmpty(this.TumblrSaveFile.Blog.Cname))
                    {
                        this.txt_TumblrURL.Text = this.TumblrSaveFile.Blog.Cname;
                    }
                    else
                    {
                        this.txt_TumblrURL.Text = "Error parsing save file...";
                    }

                    UpdateStatusText(_STATUS_READY);
                    this.btn_ImageCrawler_Start.Enabled = true;
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
        public void RestoreOptions()
        {
            this.check_Options_ParseGIF.Checked = this.Options.ParseGIF;
            this.check_Options_ParseJPEG.Checked = this.Options.ParseJPEG;
            this.check_Options_ParseDownload.Checked = !this.Options.ParseOnly;
            this.check_Options_ParseOnly.Checked = this.Options.ParseOnly;
            this.check_Options_ParsePhotoSets.Checked = this.Options.ParsePhotoSets;
            this.check_Options_ParsePNG.Checked = this.Options.ParsePNG;
            this.apiMode = this.Options.ApiMode;

            this.check_Options_GenerateLog.Checked = this.Options.GenerateLog;
        }

        /// <summary>
        ///
        /// </summary>
        public void SaveLogFile()
        {
            FileHelper.SaveTumblrFile(this.SaveLocation + @"\" + this.TumblrLogFile.Filename, this.TumblrLogFile);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        public void SaveOptions(string filename)
        {
            JsonHelper.SaveObject<ToolOptions>(filename, this.Options);
        }

        public void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile saveFile = new SaveFile(this.Ripper.Blog.Name + ".tumblr", this.Ripper.Blog);
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool SaveTumblrFile(string name)
        {
            this.TumblrSaveFile = new SaveFile(name + ".tumblr", this.Ripper.Blog);

            return FileHelper.SaveTumblrFile(this.SaveLocation + @"\" + this.TumblrSaveFile.Filename, this.TumblrSaveFile);
        }

        public void SetDoubleBuffering(System.Windows.Forms.Control control, bool value)
        {
            System.Reflection.PropertyInfo controlProperty = typeof(System.Windows.Forms.Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            controlProperty.SetValue(control, value, null);
        }

        /// <summary>
        ///
        /// </summary>
        public void SetOptions()
        {
            this.Options.ParsePNG = this.check_Options_ParsePNG.Checked;
            this.Options.ParseJPEG = this.check_Options_ParseJPEG.Checked;
            this.Options.ParseGIF = this.check_Options_ParseGIF.Checked;
            this.Options.ParsePhotoSets = this.check_Options_ParsePhotoSets.Checked;
            this.Options.ApiMode = Enum.GetName(typeof(ApiModeEnum), this.select_Options_APIMode.SelectedIndex);
            this.Options.ParseOnly = this.check_Options_ParseOnly.Checked;
            this.Options.ApiMode = this.apiMode;
            this.Options.GenerateLog = this.check_Options_GenerateLog.Checked;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StartGetStats(object sender, EventArgs e)
        {
            this.lbl_PostCount.Visible = false;
            this.bar_Progress.BarColor = Color.Black;
            this.lbl_PercentBar.ForeColor = Color.Black;
            this.lbl_PostCount.ForeColor = Color.Black;
            this.lbl_PercentBar.Text = string.Empty;
            this.TumblrURL = WebHelper.RemoveTrailingBackslash(txt_Stats_TumblrURL.Text);

            UpdateStatusText(_STATUS_INIT);
            if (IsValidURL(this.TumblrURL))
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StartImageCrawl(object sender, EventArgs e)
        {
            EnableUI_Crawl(false);
            this.IsCancelled = false;
            btn_ImageCrawler_Stop.Visible = true;
            this.lbl_PercentBar.Text = string.Empty;
            this.IsCrawlingDone = false;
            this.txt_WorkStatus.Clear();
            this.SaveLocation = this.txt_SaveLocation.Text;
            this.TumblrURL = this.txt_TumblrURL.Text;

            this.lbl_PostCount.ForeColor = Color.Black;
            this.bar_Progress.BarColor = Color.Black;
            this.lbl_PercentBar.ForeColor = Color.Black;
            this.lbl_PercentBar.Text = string.Empty;
            this.txt_WorkStatus.Visible = true;

            this.bar_Progress.Value = 0;

            this.lbl_Size.Text = string.Empty;
            this.lbl_Size.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.lbl_PostCount.Text = string.Empty;
            this.lbl_PostCount.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.DownManager = new DownloadManager();
            this.Ripper = new ImageRipper();

            if (ValidateInputFields())
            {
                if (!this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateStatusText(_STATUS_INIT);
                        UpdateWorkStatusTextNewLine(_STATUS_INIT);
                    });
                }

                this.IsCrawlingDone = false;
                this.TumblrLogFile = null;

                if (!IsCancelled)
                {
                    this.crawl_Worker.RunWorkerAsync(Ripper);

                    this.crawl_UpdateUI_Worker.RunWorkerAsync(Ripper);
                }
            }
            else
            {
                this.btn_ImageCrawler_Start.Enabled = true;
                this.lbl_PostCount.Visible = false;
                this.lbl_Size.Visible = false;
                this.bar_Progress.Visible = false;
                this.img_DisplayImage.Visible = true;
                this.tab_TumblrStats.Enabled = true;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StatsTumblrURLUpdate(object sender, EventArgs e)
        {
            this.txt_Stats_TumblrURL.Text = this.txt_TumblrURL.Text;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TabControl_Main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (this.DisableOtherTabs)
            {
                KRBTabControl.KRBTabControl tabWizardControl = sender as KRBTabControl.KRBTabControl;

                int selectedTab = tabWizardControl.SelectedIndex;

                KRBTabControl.TabPageEx tabPage = (KRBTabControl.TabPageEx)tabWizardControl.SelectedTab;

                //Disable the tab selection
                if (this.CurrentSelectedTab != selectedTab)
                {
                    //If selected tab is different than the current one, re-select the current tab.
                    //This disables the navigation using the tab selection.
                    tabWizardControl.SelectTab(this.CurrentSelectedTab);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TabMainTabSelect_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TabPage_Enter(object sender, EventArgs e)
        {
            this.CurrentSelectedTab = this.tabControl_Main.SelectedIndex;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItem TSMI = sender as ToolStripMenuItem;

            AdvancedMenuRenderer renderer = TSMI.GetCurrentParent().Renderer as AdvancedMenuRenderer;

            renderer.ChangeTextForeColor(TSMI, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Txt_StatsTumblrURL_TextChanged(object sender, EventArgs e)
        {
            this.txt_TumblrURL.Text = this.txt_Stats_TumblrURL.Text;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="strToReplace"></param>
        /// <param name="strToAdd"></param>
        public void UpdateWorkStatusTextConcat(string strToReplace, string strToAdd = "")
        {
            if (this.txt_WorkStatus.Text.Contains(strToReplace) && !this.txt_WorkStatus.Text.Contains(string.Concat(strToReplace, strToAdd)))
            {
                this.txt_WorkStatus.Text = txt_WorkStatus.Text.Replace(strToReplace, string.Concat(strToReplace, strToAdd));

                this.txt_WorkStatus.Update();
                this.txt_WorkStatus.Refresh();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        public void UpdateWorkStatusTextNewLine(string text)
        {
            if (!this.txt_WorkStatus.Text.Contains(text))
            {
                this.txt_WorkStatus.Text += txt_WorkStatus.Text != "" ? "\r\n" : "";
                this.txt_WorkStatus.Text += ":: " + text;
                this.txt_WorkStatus.Update();
                this.txt_WorkStatus.Refresh();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="replaceStr"></param>
        public void UpdateWorkStatusTextReplace(string str, string replaceStr)
        {
            if (this.txt_WorkStatus.Text.Contains(str))
            {
                this.txt_WorkStatus.Text = txt_WorkStatus.Text.Replace(str, replaceStr);

                this.txt_WorkStatus.Update();
                this.txt_WorkStatus.Refresh();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool ValidateInputFields()
        {
            bool saveLocationEmpty = string.IsNullOrEmpty(this.SaveLocation);
            bool urlValid = true;

            if (saveLocationEmpty)
            {
                MsgBox.Show("Save Location cannot be left empty! \r\nSelect a valid location on disk", _STATUS_ERROR, MsgBox.Buttons.OK, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                EnableUI_Crawl(true);
                this.btn_Browse.Focus();
            }
            else
            {
                if (!IsValidURL(this.TumblrURL))
                {
                    MsgBox.Show("Please enter valid url!", _STATUS_ERROR, MsgBox.Buttons.OK, MsgBox.Icon.Error, MsgBox.AnimateStyle.FadeIn, true);
                    this.txt_TumblrURL.Focus();
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
        public void WorkStatusAutoScroll(object sender, EventArgs e)
        {
            this.txt_WorkStatus.SelectionStart = this.txt_WorkStatus.TextLength;
            this.txt_WorkStatus.ScrollToCaret();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelOperations(object sender, EventArgs e)
        {
            IsCancelled = true;
            if (this.Ripper != null)
            {
                this.Ripper.IsCancelled = true;
            }

            IsDownloadDone = true;
            EnableUI_Crawl(true);

            UpdateWorkStatusTextNewLine("Operation cancelled ...");
            this.img_DisplayImage.Image = Resources.tumblrlogo;
            MsgBox.Show("Current operation has been cancelled successfully!", "Cancel", MsgBox.Buttons.OK, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn, false);
            UpdateStatusText(_STATUS_READY);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check_ParseDownload_CheckedChanged(object sender, EventArgs e)
        {
            if (this.check_Options_ParseDownload.Checked)
            {
                this.check_Options_ParseOnly.Checked = false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check_ParseOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (this.check_Options_ParseOnly.Checked)
            {
                this.check_Options_ParseDownload.Checked = false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsAccept(object sender, EventArgs e)
        {
            this.SetOptions();
            this.SaveOptions(this.optionsFileName);
            this.UpdateStatusText("Options saved");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsUIRestore(object sender, EventArgs e)
        {
            this.RestoreOptions();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Main_SelectedIndexChanging(object sender, KRBTabControl.KRBTabControl.SelectedIndexChangingEventArgs e)
        {
            if (this.DisableOtherTabs)
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
                    tabWizardControl.SelectTab(this.CurrentSelectedTab);
                }
            }

            if (e.TabPage.Text == "IsSelectable?")
                e.Cancel = true;
        }
    }
}