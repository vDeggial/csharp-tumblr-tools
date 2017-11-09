using System.Drawing;
namespace Tumblr_Tool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lbl_PercentBar = new System.Windows.Forms.Label();
            this.menuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.imageDownloadWorker = new System.ComponentModel.BackgroundWorker();
            this.menu_TopMenu = new System.Windows.Forms.MenuStrip();
            this.menuItem_LoadFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.imageCrawlWorker = new System.ComponentModel.BackgroundWorker();
            this.imageCrawlWorkerUI = new System.ComponentModel.BackgroundWorker();
            this.status_Strip = new System.Windows.Forms.StatusStrip();
            this.lbl_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_PostCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_Size = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageDownloadWorkerUI = new System.ComponentModel.BackgroundWorker();
            this.blogGetStatsWorker = new System.ComponentModel.BackgroundWorker();
            this.blogGetStatsWorkerUI = new System.ComponentModel.BackgroundWorker();
            this.fileOpenWorker = new System.ComponentModel.BackgroundWorker();
            this.tabControl_Main = new KRBTabControl.KRBTabControl();
            this.tab_PhotoPostsParser = new KRBTabControl.TabPageEx();
            this.select_Crawler_ImagesSize = new Tumblr_Tool.AdvancedComboBox();
            this.btn_Crawler_Stop = new System.Windows.Forms.Button();
            this.iconsList = new System.Windows.Forms.ImageList(this.components);
            this.lbl_Crawler_ImageSize = new System.Windows.Forms.Label();
            this.lbl_Crawler_Mode = new System.Windows.Forms.Label();
            this.select_Crawler_Mode = new Tumblr_Tool.AdvancedComboBox();
            this.txt_Crawler_WorkStatus = new System.Windows.Forms.RichTextBox();
            this.btn_Crawler_Browse = new System.Windows.Forms.Button();
            this.img_Crawler_DisplayImage = new System.Windows.Forms.PictureBox();
            this.btn_Crawler_Start = new System.Windows.Forms.Button();
            this.txt_Crawler_SaveLocation = new System.Windows.Forms.TextBox();
            this.lbl_Crawler_SaveLocation = new System.Windows.Forms.Label();
            this.tab_TumblrStats = new KRBTabControl.TabPageEx();
            this.img_Stats_Avatar = new Tumblr_Tool.CirclePictureBox();
            this.txt_Stats_BlogDescription = new System.Windows.Forms.RichTextBox();
            this.lbl_Stats_BlogTitle = new System.Windows.Forms.Label();
            this.box_PostStats = new System.Windows.Forms.GroupBox();
            this.lbl_Stats_TotalCount = new System.Windows.Forms.Label();
            this.table_Stats_PostStats = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Stats_QuoteStats = new System.Windows.Forms.Label();
            this.lbl_Stats_Photo = new System.Windows.Forms.Label();
            this.lbl_Stats_Quote = new System.Windows.Forms.Label();
            this.lbl_Stats_LinkCount = new System.Windows.Forms.Label();
            this.lbl_Stats_ChatCount = new System.Windows.Forms.Label();
            this.lbl_Stats_PhotoCount = new System.Windows.Forms.Label();
            this.lbl_Stats_AudioCount = new System.Windows.Forms.Label();
            this.lbl_Stats_Text = new System.Windows.Forms.Label();
            this.lbl_Stats_Link = new System.Windows.Forms.Label();
            this.lbl_Stats_Audio = new System.Windows.Forms.Label();
            this.lbl_Stats_TextCount = new System.Windows.Forms.Label();
            this.lbl_Stats_Video = new System.Windows.Forms.Label();
            this.lbl_Stats_VideoCount = new System.Windows.Forms.Label();
            this.lbl_Stats_Answer = new System.Windows.Forms.Label();
            this.lbl_Stats_AnswerCount = new System.Windows.Forms.Label();
            this.lbl_Stats_Chat = new System.Windows.Forms.Label();
            this.lbl_Stats_Total = new System.Windows.Forms.Label();
            this.btn_Stats_Start = new System.Windows.Forms.Button();
            this.tab_TagScanner = new KRBTabControl.TabPageEx();
            this.check_Tags_PhotoOnly = new System.Windows.Forms.CheckBox();
            this.btn_TagScanner_SaveAsFile = new System.Windows.Forms.Button();
            this.list_TagScanner_TagList = new System.Windows.Forms.ListBox();
            this.lbl_TagScanner_TagCount = new System.Windows.Forms.Label();
            this.lbl_TagScanner_NumberOfTags = new System.Windows.Forms.Label();
            this.btn_TagScanner_Stop = new System.Windows.Forms.Button();
            this.btn_TagScanner_Start = new System.Windows.Forms.Button();
            this.tab_Options = new KRBTabControl.TabPageEx();
            this.section_Options = new System.Windows.Forms.GroupBox();
            this.section_Options_LogOptions = new System.Windows.Forms.GroupBox();
            this.check_Options_GenerateUncompressedLog = new System.Windows.Forms.CheckBox();
            this.check_Options_GenerateLog = new System.Windows.Forms.CheckBox();
            this.section_Options_BackupOptions = new System.Windows.Forms.GroupBox();
            this.check_Options_OldToNewDownloadOrder = new System.Windows.Forms.CheckBox();
            this.check_Options_ParseOnly = new System.Windows.Forms.CheckBox();
            this.btn_Options_Reset = new System.Windows.Forms.Button();
            this.section_Options_ImageTypes = new System.Windows.Forms.GroupBox();
            this.check_Options_ParseGIF = new System.Windows.Forms.CheckBox();
            this.check_Options_ParsePNG = new System.Windows.Forms.CheckBox();
            this.check_Options_ParseJPEG = new System.Windows.Forms.CheckBox();
            this.check_Options_ParsePhotoSets = new System.Windows.Forms.CheckBox();
            this.btn_Options_Save = new System.Windows.Forms.Button();
            this.tab_About = new KRBTabControl.TabPageEx();
            this.lbl_About_Copyright = new System.Windows.Forms.Label();
            this.lbl_About_Info = new System.Windows.Forms.Label();
            this.lbl_About_Version = new System.Windows.Forms.Label();
            this.lbl_About_Title = new System.Windows.Forms.Label();
            this.txt_TumblrURL = new System.Windows.Forms.TextBox();
            this.lbl_Crawler_TumblrURL = new System.Windows.Forms.Label();
            this.bar_Progress = new Tumblr_Tool.ColorProgressBar();
            this.blogTagListWorker = new System.ComponentModel.BackgroundWorker();
            this.blogTagLIstWorkerUI = new System.ComponentModel.BackgroundWorker();
            this.tagListSaveWorker = new System.ComponentModel.BackgroundWorker();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayIcon_MenuItem_Restore = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIcon_MenuItem_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_TopMenu.SuspendLayout();
            this.status_Strip.SuspendLayout();
            this.tabControl_Main.SuspendLayout();
            this.tab_PhotoPostsParser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_Crawler_DisplayImage)).BeginInit();
            this.tab_TumblrStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_Stats_Avatar)).BeginInit();
            this.box_PostStats.SuspendLayout();
            this.table_Stats_PostStats.SuspendLayout();
            this.tab_TagScanner.SuspendLayout();
            this.tab_Options.SuspendLayout();
            this.section_Options.SuspendLayout();
            this.section_Options_LogOptions.SuspendLayout();
            this.section_Options_BackupOptions.SuspendLayout();
            this.section_Options_ImageTypes.SuspendLayout();
            this.tab_About.SuspendLayout();
            this.trayIconContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_PercentBar
            // 
            this.lbl_PercentBar.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PercentBar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lbl_PercentBar.Location = new System.Drawing.Point(0, 351);
            this.lbl_PercentBar.Name = "lbl_PercentBar";
            this.lbl_PercentBar.Size = new System.Drawing.Size(642, 23);
            this.lbl_PercentBar.TabIndex = 14;
            this.lbl_PercentBar.Text = "[%]";
            this.lbl_PercentBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuItem_File
            // 
            this.menuItem_File.Name = "menuItem_File";
            this.menuItem_File.Size = new System.Drawing.Size(37, 20);
            this.menuItem_File.Text = "File";
            // 
            // menuItem_Help
            // 
            this.menuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_About});
            this.menuItem_Help.Name = "menuItem_Help";
            this.menuItem_Help.Size = new System.Drawing.Size(44, 20);
            this.menuItem_Help.Text = "Help";
            // 
            // menuItem_About
            // 
            this.menuItem_About.Name = "menuItem_About";
            this.menuItem_About.Size = new System.Drawing.Size(107, 22);
            this.menuItem_About.Text = "About";
            // 
            // imageDownloadWorker
            // 
            this.imageDownloadWorker.WorkerReportsProgress = true;
            this.imageDownloadWorker.WorkerSupportsCancellation = true;
            this.imageDownloadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DownloadWorker_Work);
            this.imageDownloadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DownloadWorker_Completed);
            // 
            // menu_TopMenu
            // 
            this.menu_TopMenu.BackColor = System.Drawing.Color.White;
            this.menu_TopMenu.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.menu_TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_LoadFromFile});
            this.menu_TopMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menu_TopMenu.Location = new System.Drawing.Point(0, 0);
            this.menu_TopMenu.Name = "menu_TopMenu";
            this.menu_TopMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menu_TopMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menu_TopMenu.ShowItemToolTips = true;
            this.menu_TopMenu.Size = new System.Drawing.Size(626, 25);
            this.menu_TopMenu.TabIndex = 6;
            this.menu_TopMenu.Text = "topMenu";
            // 
            // menuItem_LoadFromFile
            // 
            this.menuItem_LoadFromFile.BackColor = System.Drawing.Color.White;
            this.menuItem_LoadFromFile.Image = global::Tumblr_Tool.Properties.Resources.open;
            this.menuItem_LoadFromFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuItem_LoadFromFile.Name = "menuItem_LoadFromFile";
            this.menuItem_LoadFromFile.Size = new System.Drawing.Size(89, 21);
            this.menuItem_LoadFromFile.Text = "Load File";
            this.menuItem_LoadFromFile.ToolTipText = "Open Tumblr Tools Save file";
            this.menuItem_LoadFromFile.Click += new System.EventHandler(this.ShowDialog_OpenFile);
            // 
            // imageCrawlWorker
            // 
            this.imageCrawlWorker.WorkerReportsProgress = true;
            this.imageCrawlWorker.WorkerSupportsCancellation = true;
            this.imageCrawlWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CrawlWorker_Work);
            this.imageCrawlWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CrawlWorker_Completed);
            // 
            // imageCrawlWorkerUI
            // 
            this.imageCrawlWorkerUI.WorkerReportsProgress = true;
            this.imageCrawlWorkerUI.WorkerSupportsCancellation = true;
            this.imageCrawlWorkerUI.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CrawlWorkerUI_Work);
            this.imageCrawlWorkerUI.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CrawlWorkerUI_Completed);
            // 
            // status_Strip
            // 
            this.status_Strip.AutoSize = false;
            this.status_Strip.BackColor = System.Drawing.Color.Transparent;
            this.status_Strip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.status_Strip.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_Strip.GripMargin = new System.Windows.Forms.Padding(0);
            this.status_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_Status,
            this.lbl_PostCount,
            this.lbl_Size});
            this.status_Strip.Location = new System.Drawing.Point(0, 375);
            this.status_Strip.Name = "status_Strip";
            this.status_Strip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.status_Strip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.status_Strip.Size = new System.Drawing.Size(626, 29);
            this.status_Strip.SizingGrip = false;
            this.status_Strip.Stretch = false;
            this.status_Strip.TabIndex = 7;
            this.status_Strip.Text = "Status";
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = false;
            this.lbl_Status.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.lbl_Status.Image = global::Tumblr_Tool.Properties.Resources.home;
            this.lbl_Status.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(208, 24);
            this.lbl_Status.Text = "[Status]";
            this.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_PostCount
            // 
            this.lbl_PostCount.AutoSize = false;
            this.lbl_PostCount.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.lbl_PostCount.Image = global::Tumblr_Tool.Properties.Resources.image;
            this.lbl_PostCount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_PostCount.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_PostCount.Name = "lbl_PostCount";
            this.lbl_PostCount.Size = new System.Drawing.Size(208, 29);
            this.lbl_PostCount.Text = "[Post Count]";
            this.lbl_PostCount.Visible = false;
            // 
            // lbl_Size
            // 
            this.lbl_Size.AutoSize = false;
            this.lbl_Size.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.lbl_Size.Image = global::Tumblr_Tool.Properties.Resources.filesize;
            this.lbl_Size.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Size.LinkVisited = true;
            this.lbl_Size.Name = "lbl_Size";
            this.lbl_Size.Size = new System.Drawing.Size(209, 24);
            this.lbl_Size.Text = "[Size]";
            this.lbl_Size.Visible = false;
            // 
            // imageDownloadWorkerUI
            // 
            this.imageDownloadWorkerUI.WorkerReportsProgress = true;
            this.imageDownloadWorkerUI.WorkerSupportsCancellation = true;
            this.imageDownloadWorkerUI.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DownloadWorkerUI_Work);
            this.imageDownloadWorkerUI.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DownloadWorkerUI_Completed);
            // 
            // blogGetStatsWorker
            // 
            this.blogGetStatsWorker.WorkerReportsProgress = true;
            this.blogGetStatsWorker.WorkerSupportsCancellation = true;
            this.blogGetStatsWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetStatsWorker_Work);
            this.blogGetStatsWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GetStatsWorker_Completed);
            // 
            // blogGetStatsWorkerUI
            // 
            this.blogGetStatsWorkerUI.WorkerReportsProgress = true;
            this.blogGetStatsWorkerUI.WorkerSupportsCancellation = true;
            this.blogGetStatsWorkerUI.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetStatsWorkerUI_Work);
            this.blogGetStatsWorkerUI.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GetStatsWorkerUI_Completed);
            // 
            // fileOpenWorker
            // 
            this.fileOpenWorker.WorkerSupportsCancellation = true;
            this.fileOpenWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.FileOpenWorker_Work);
            this.fileOpenWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.FileOpenWorker_Completed);
            // 
            // tabControl_Main
            // 
            this.tabControl_Main.AllowDrop = true;
            this.tabControl_Main.BackgroundHatcher.HatchType = System.Drawing.Drawing2D.HatchStyle.DashedVertical;
            this.tabControl_Main.BorderColor = System.Drawing.Color.Transparent;
            this.tabControl_Main.Controls.Add(this.tab_PhotoPostsParser);
            this.tabControl_Main.Controls.Add(this.tab_TumblrStats);
            this.tabControl_Main.Controls.Add(this.tab_TagScanner);
            this.tabControl_Main.Controls.Add(this.tab_Options);
            this.tabControl_Main.Controls.Add(this.tab_About);
            this.tabControl_Main.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.tabControl_Main.GradientCaption.ActiveCaptionColorEnd = System.Drawing.SystemColors.ActiveBorder;
            this.tabControl_Main.HeaderStyle = KRBTabControl.KRBTabControl.TabHeaderStyle.Texture;
            this.tabControl_Main.ImageList = this.iconsList;
            this.tabControl_Main.IsCaptionVisible = false;
            this.tabControl_Main.IsDocumentTabStyle = true;
            this.tabControl_Main.IsDrawHeader = false;
            this.tabControl_Main.IsUserInteraction = false;
            this.tabControl_Main.ItemSize = new System.Drawing.Size(0, 26);
            this.tabControl_Main.Location = new System.Drawing.Point(0, 57);
            this.tabControl_Main.Name = "tabControl_Main";
            this.tabControl_Main.SelectedIndex = 3;
            this.tabControl_Main.Size = new System.Drawing.Size(625, 256);
            this.tabControl_Main.TabBorderColor = System.Drawing.Color.Transparent;
            this.tabControl_Main.TabGradient.ColorEnd = System.Drawing.Color.Transparent;
            this.tabControl_Main.TabGradient.ColorStart = System.Drawing.Color.Transparent;
            this.tabControl_Main.TabGradient.GradientStyle = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.tabControl_Main.TabGradient.TabPageSelectedTextColor = System.Drawing.Color.Maroon;
            this.tabControl_Main.TabIndex = 0;
            this.tabControl_Main.TabPageCloseIconColor = System.Drawing.Color.Transparent;
            this.tabControl_Main.TabStyles = KRBTabControl.KRBTabControl.TabStyle.VS2010;
            this.tabControl_Main.UpDownStyle = KRBTabControl.KRBTabControl.UpDown32Style.Default;
            this.tabControl_Main.SelectedIndexChanging += new System.EventHandler<KRBTabControl.KRBTabControl.SelectedIndexChangingEventArgs>(this.TabPage_Change);
            // 
            // tab_PhotoPostsParser
            // 
            this.tab_PhotoPostsParser.BackColor = System.Drawing.Color.White;
            this.tab_PhotoPostsParser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tab_PhotoPostsParser.Controls.Add(this.select_Crawler_ImagesSize);
            this.tab_PhotoPostsParser.Controls.Add(this.btn_Crawler_Stop);
            this.tab_PhotoPostsParser.Controls.Add(this.lbl_Crawler_ImageSize);
            this.tab_PhotoPostsParser.Controls.Add(this.lbl_Crawler_Mode);
            this.tab_PhotoPostsParser.Controls.Add(this.select_Crawler_Mode);
            this.tab_PhotoPostsParser.Controls.Add(this.txt_Crawler_WorkStatus);
            this.tab_PhotoPostsParser.Controls.Add(this.btn_Crawler_Browse);
            this.tab_PhotoPostsParser.Controls.Add(this.img_Crawler_DisplayImage);
            this.tab_PhotoPostsParser.Controls.Add(this.btn_Crawler_Start);
            this.tab_PhotoPostsParser.Controls.Add(this.txt_Crawler_SaveLocation);
            this.tab_PhotoPostsParser.Controls.Add(this.lbl_Crawler_SaveLocation);
            this.tab_PhotoPostsParser.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_PhotoPostsParser.ForeColor = System.Drawing.Color.Black;
            this.tab_PhotoPostsParser.ImageIndex = 1;
            this.tab_PhotoPostsParser.IsClosable = false;
            this.tab_PhotoPostsParser.Location = new System.Drawing.Point(1, 32);
            this.tab_PhotoPostsParser.Margin = new System.Windows.Forms.Padding(0);
            this.tab_PhotoPostsParser.Name = "tab_PhotoPostsParser";
            this.tab_PhotoPostsParser.Size = new System.Drawing.Size(623, 219);
            this.tab_PhotoPostsParser.TabIndex = 0;
            this.tab_PhotoPostsParser.Text = "Images Backup";
            this.tab_PhotoPostsParser.ToolTipText = "Download Tumblr photo posts images to local location for backup ... ";
            this.tab_PhotoPostsParser.Enter += new System.EventHandler(this.TabPage_Enter);
            // 
            // select_Crawler_ImagesSize
            // 
            this.select_Crawler_ImagesSize.ArrowBackColor = System.Drawing.Color.White;
            this.select_Crawler_ImagesSize.ArrowForeColor = System.Drawing.Color.Black;
            this.select_Crawler_ImagesSize.BackColor = System.Drawing.Color.White;
            this.select_Crawler_ImagesSize.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.select_Crawler_ImagesSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.select_Crawler_ImagesSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select_Crawler_ImagesSize.ForeColor = System.Drawing.Color.Black;
            this.select_Crawler_ImagesSize.HighlightBackColor = System.Drawing.Color.White;
            this.select_Crawler_ImagesSize.HighlightForeColor = System.Drawing.Color.Maroon;
            this.select_Crawler_ImagesSize.Items.AddRange(new object[] {
            "[Image Sizes]"});
            this.select_Crawler_ImagesSize.Location = new System.Drawing.Point(505, 123);
            this.select_Crawler_ImagesSize.Margin = new System.Windows.Forms.Padding(0);
            this.select_Crawler_ImagesSize.Name = "select_Crawler_ImagesSize";
            this.select_Crawler_ImagesSize.ShowArrow = true;
            this.select_Crawler_ImagesSize.Size = new System.Drawing.Size(98, 22);
            this.select_Crawler_ImagesSize.Style = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.select_Crawler_ImagesSize.TabIndex = 21;
            // 
            // btn_Crawler_Stop
            // 
            this.btn_Crawler_Stop.FlatAppearance.BorderSize = 0;
            this.btn_Crawler_Stop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Crawler_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Crawler_Stop.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btn_Crawler_Stop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Crawler_Stop.ImageIndex = 0;
            this.btn_Crawler_Stop.ImageList = this.iconsList;
            this.btn_Crawler_Stop.Location = new System.Drawing.Point(539, 186);
            this.btn_Crawler_Stop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Crawler_Stop.Name = "btn_Crawler_Stop";
            this.btn_Crawler_Stop.Size = new System.Drawing.Size(61, 28);
            this.btn_Crawler_Stop.TabIndex = 20;
            this.btn_Crawler_Stop.Text = "Stop";
            this.btn_Crawler_Stop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Crawler_Stop.UseVisualStyleBackColor = true;
            this.btn_Crawler_Stop.Visible = false;
            this.btn_Crawler_Stop.Click += new System.EventHandler(this.Cancel_PhotoPostParse);
            this.btn_Crawler_Stop.MouseEnter += new System.EventHandler(this.Button_OnMouseEnter);
            this.btn_Crawler_Stop.MouseLeave += new System.EventHandler(this.Button_OnMouseLeave);
            // 
            // iconsList
            // 
            this.iconsList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconsList.ImageStream")));
            this.iconsList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconsList.Images.SetKeyName(0, "cancel.ico");
            this.iconsList.Images.SetKeyName(1, "down.ico");
            this.iconsList.Images.SetKeyName(2, "info.ico");
            this.iconsList.Images.SetKeyName(3, "browse.ico");
            this.iconsList.Images.SetKeyName(4, "crawl.ico");
            this.iconsList.Images.SetKeyName(5, "about.ico");
            this.iconsList.Images.SetKeyName(6, "menu.ico");
            this.iconsList.Images.SetKeyName(7, "options.ico");
            // 
            // lbl_Crawler_ImageSize
            // 
            this.lbl_Crawler_ImageSize.AutoSize = true;
            this.lbl_Crawler_ImageSize.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.lbl_Crawler_ImageSize.Location = new System.Drawing.Point(430, 126);
            this.lbl_Crawler_ImageSize.Name = "lbl_Crawler_ImageSize";
            this.lbl_Crawler_ImageSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_Crawler_ImageSize.Size = new System.Drawing.Size(69, 16);
            this.lbl_Crawler_ImageSize.TabIndex = 19;
            this.lbl_Crawler_ImageSize.Text = "Image Size:";
            this.lbl_Crawler_ImageSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl_Crawler_Mode
            // 
            this.lbl_Crawler_Mode.AutoSize = true;
            this.lbl_Crawler_Mode.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.lbl_Crawler_Mode.Location = new System.Drawing.Point(430, 90);
            this.lbl_Crawler_Mode.Name = "lbl_Crawler_Mode";
            this.lbl_Crawler_Mode.Size = new System.Drawing.Size(44, 16);
            this.lbl_Crawler_Mode.TabIndex = 17;
            this.lbl_Crawler_Mode.Text = "Mode:";
            // 
            // select_Crawler_Mode
            // 
            this.select_Crawler_Mode.ArrowBackColor = System.Drawing.Color.White;
            this.select_Crawler_Mode.ArrowForeColor = System.Drawing.Color.Black;
            this.select_Crawler_Mode.BackColor = System.Drawing.Color.White;
            this.select_Crawler_Mode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.select_Crawler_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.select_Crawler_Mode.DropDownWidth = 48;
            this.select_Crawler_Mode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select_Crawler_Mode.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.select_Crawler_Mode.ForeColor = System.Drawing.Color.Black;
            this.select_Crawler_Mode.HighlightBackColor = System.Drawing.Color.White;
            this.select_Crawler_Mode.HighlightForeColor = System.Drawing.Color.Maroon;
            this.select_Crawler_Mode.ItemHeight = 16;
            this.select_Crawler_Mode.Items.AddRange(new object[] {
            "[Mode]"});
            this.select_Crawler_Mode.Location = new System.Drawing.Point(504, 87);
            this.select_Crawler_Mode.Margin = new System.Windows.Forms.Padding(0);
            this.select_Crawler_Mode.Name = "select_Crawler_Mode";
            this.select_Crawler_Mode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.select_Crawler_Mode.ShowArrow = true;
            this.select_Crawler_Mode.Size = new System.Drawing.Size(98, 22);
            this.select_Crawler_Mode.Style = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.select_Crawler_Mode.TabIndex = 16;
            this.select_Crawler_Mode.TabStop = false;
            // 
            // txt_Crawler_WorkStatus
            // 
            this.txt_Crawler_WorkStatus.BackColor = System.Drawing.Color.White;
            this.txt_Crawler_WorkStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Crawler_WorkStatus.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txt_Crawler_WorkStatus.DetectUrls = false;
            this.txt_Crawler_WorkStatus.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.txt_Crawler_WorkStatus.ForeColor = System.Drawing.Color.Black;
            this.txt_Crawler_WorkStatus.Location = new System.Drawing.Point(24, 20);
            this.txt_Crawler_WorkStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Crawler_WorkStatus.Name = "txt_Crawler_WorkStatus";
            this.txt_Crawler_WorkStatus.ReadOnly = true;
            this.txt_Crawler_WorkStatus.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txt_Crawler_WorkStatus.Size = new System.Drawing.Size(162, 195);
            this.txt_Crawler_WorkStatus.TabIndex = 8;
            this.txt_Crawler_WorkStatus.TabStop = false;
            this.txt_Crawler_WorkStatus.Text = "Welcome to Tumblr Tools!";
            this.txt_Crawler_WorkStatus.TextChanged += new System.EventHandler(this.WorkStatusAutoScroll);
            // 
            // btn_Crawler_Browse
            // 
            this.btn_Crawler_Browse.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btn_Crawler_Browse.FlatAppearance.BorderSize = 0;
            this.btn_Crawler_Browse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Crawler_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Crawler_Browse.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btn_Crawler_Browse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Crawler_Browse.ImageIndex = 3;
            this.btn_Crawler_Browse.ImageList = this.iconsList;
            this.btn_Crawler_Browse.Location = new System.Drawing.Point(515, 49);
            this.btn_Crawler_Browse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Crawler_Browse.Name = "btn_Crawler_Browse";
            this.btn_Crawler_Browse.Size = new System.Drawing.Size(87, 28);
            this.btn_Crawler_Browse.TabIndex = 6;
            this.btn_Crawler_Browse.Text = "Browse";
            this.btn_Crawler_Browse.UseVisualStyleBackColor = true;
            this.btn_Crawler_Browse.Click += new System.EventHandler(this.BrowseLocalPath);
            this.btn_Crawler_Browse.MouseEnter += new System.EventHandler(this.Button_OnMouseEnter);
            this.btn_Crawler_Browse.MouseLeave += new System.EventHandler(this.Button_OnMouseLeave);
            // 
            // img_Crawler_DisplayImage
            // 
            this.img_Crawler_DisplayImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.img_Crawler_DisplayImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.img_Crawler_DisplayImage.ErrorImage = global::Tumblr_Tool.Properties.Resources.tumblrlogo;
            this.img_Crawler_DisplayImage.Image = global::Tumblr_Tool.Properties.Resources.tumblrlogo;
            this.img_Crawler_DisplayImage.InitialImage = null;
            this.img_Crawler_DisplayImage.Location = new System.Drawing.Point(215, 20);
            this.img_Crawler_DisplayImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.img_Crawler_DisplayImage.Name = "img_Crawler_DisplayImage";
            this.img_Crawler_DisplayImage.Size = new System.Drawing.Size(183, 198);
            this.img_Crawler_DisplayImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.img_Crawler_DisplayImage.TabIndex = 4;
            this.img_Crawler_DisplayImage.TabStop = false;
            // 
            // btn_Crawler_Start
            // 
            this.btn_Crawler_Start.FlatAppearance.BorderSize = 0;
            this.btn_Crawler_Start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Crawler_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Crawler_Start.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btn_Crawler_Start.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Crawler_Start.ImageIndex = 4;
            this.btn_Crawler_Start.ImageList = this.iconsList;
            this.btn_Crawler_Start.Location = new System.Drawing.Point(433, 187);
            this.btn_Crawler_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Crawler_Start.Name = "btn_Crawler_Start";
            this.btn_Crawler_Start.Size = new System.Drawing.Size(61, 28);
            this.btn_Crawler_Start.TabIndex = 7;
            this.btn_Crawler_Start.Text = "Start";
            this.btn_Crawler_Start.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Crawler_Start.UseVisualStyleBackColor = true;
            this.btn_Crawler_Start.Click += new System.EventHandler(this.Start_PhotoPostParse);
            this.btn_Crawler_Start.MouseEnter += new System.EventHandler(this.Button_OnMouseEnter);
            this.btn_Crawler_Start.MouseLeave += new System.EventHandler(this.Button_OnMouseLeave);
            // 
            // txt_Crawler_SaveLocation
            // 
            this.txt_Crawler_SaveLocation.BackColor = System.Drawing.Color.White;
            this.txt_Crawler_SaveLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Crawler_SaveLocation.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.txt_Crawler_SaveLocation.ForeColor = System.Drawing.Color.Black;
            this.txt_Crawler_SaveLocation.Location = new System.Drawing.Point(433, 20);
            this.txt_Crawler_SaveLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Crawler_SaveLocation.Name = "txt_Crawler_SaveLocation";
            this.txt_Crawler_SaveLocation.ReadOnly = true;
            this.txt_Crawler_SaveLocation.Size = new System.Drawing.Size(169, 21);
            this.txt_Crawler_SaveLocation.TabIndex = 1;
            // 
            // lbl_Crawler_SaveLocation
            // 
            this.lbl_Crawler_SaveLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Crawler_SaveLocation.AutoSize = true;
            this.lbl_Crawler_SaveLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Crawler_SaveLocation.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.lbl_Crawler_SaveLocation.Location = new System.Drawing.Point(430, 0);
            this.lbl_Crawler_SaveLocation.Name = "lbl_Crawler_SaveLocation";
            this.lbl_Crawler_SaveLocation.Size = new System.Drawing.Size(102, 16);
            this.lbl_Crawler_SaveLocation.TabIndex = 0;
            this.lbl_Crawler_SaveLocation.Text = "Backup Location:";
            // 
            // tab_TumblrStats
            // 
            this.tab_TumblrStats.BackColor = System.Drawing.Color.White;
            this.tab_TumblrStats.Controls.Add(this.img_Stats_Avatar);
            this.tab_TumblrStats.Controls.Add(this.txt_Stats_BlogDescription);
            this.tab_TumblrStats.Controls.Add(this.lbl_Stats_BlogTitle);
            this.tab_TumblrStats.Controls.Add(this.box_PostStats);
            this.tab_TumblrStats.Controls.Add(this.btn_Stats_Start);
            this.tab_TumblrStats.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_TumblrStats.ImageIndex = 2;
            this.tab_TumblrStats.IsClosable = false;
            this.tab_TumblrStats.Location = new System.Drawing.Point(1, 32);
            this.tab_TumblrStats.Margin = new System.Windows.Forms.Padding(0);
            this.tab_TumblrStats.Name = "tab_TumblrStats";
            this.tab_TumblrStats.Size = new System.Drawing.Size(623, 219);
            this.tab_TumblrStats.TabIndex = 1;
            this.tab_TumblrStats.Text = "Blog Stats";
            this.tab_TumblrStats.ToolTipText = "Get Tumblr blog stats such as post count per type etc ...";
            this.tab_TumblrStats.Enter += new System.EventHandler(this.TabPage_Enter);
            // 
            // img_Stats_Avatar
            // 
            this.img_Stats_Avatar.ErrorImage = global::Tumblr_Tool.Properties.Resources.avatar;
            this.img_Stats_Avatar.Image = global::Tumblr_Tool.Properties.Resources.avatar;
            this.img_Stats_Avatar.InitialImage = global::Tumblr_Tool.Properties.Resources.avatar;
            this.img_Stats_Avatar.Location = new System.Drawing.Point(11, 31);
            this.img_Stats_Avatar.Name = "img_Stats_Avatar";
            this.img_Stats_Avatar.Size = new System.Drawing.Size(72, 72);
            this.img_Stats_Avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.img_Stats_Avatar.TabIndex = 19;
            this.img_Stats_Avatar.TabStop = false;
            // 
            // txt_Stats_BlogDescription
            // 
            this.txt_Stats_BlogDescription.BackColor = System.Drawing.Color.White;
            this.txt_Stats_BlogDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Stats_BlogDescription.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txt_Stats_BlogDescription.DetectUrls = false;
            this.txt_Stats_BlogDescription.ForeColor = System.Drawing.Color.Black;
            this.txt_Stats_BlogDescription.Location = new System.Drawing.Point(312, 4);
            this.txt_Stats_BlogDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Stats_BlogDescription.Name = "txt_Stats_BlogDescription";
            this.txt_Stats_BlogDescription.ReadOnly = true;
            this.txt_Stats_BlogDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txt_Stats_BlogDescription.Size = new System.Drawing.Size(301, 72);
            this.txt_Stats_BlogDescription.TabIndex = 4;
            this.txt_Stats_BlogDescription.TabStop = false;
            this.txt_Stats_BlogDescription.Text = "Description";
            // 
            // lbl_Stats_BlogTitle
            // 
            this.lbl_Stats_BlogTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Stats_BlogTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Stats_BlogTitle.Location = new System.Drawing.Point(89, 50);
            this.lbl_Stats_BlogTitle.Name = "lbl_Stats_BlogTitle";
            this.lbl_Stats_BlogTitle.Size = new System.Drawing.Size(165, 26);
            this.lbl_Stats_BlogTitle.TabIndex = 1;
            this.lbl_Stats_BlogTitle.Text = "[Tumblr Stats]";
            this.lbl_Stats_BlogTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // box_PostStats
            // 
            this.box_PostStats.Controls.Add(this.lbl_Stats_TotalCount);
            this.box_PostStats.Controls.Add(this.table_Stats_PostStats);
            this.box_PostStats.Controls.Add(this.lbl_Stats_Total);
            this.box_PostStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.box_PostStats.Location = new System.Drawing.Point(16, 131);
            this.box_PostStats.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.box_PostStats.Name = "box_PostStats";
            this.box_PostStats.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.box_PostStats.Size = new System.Drawing.Size(597, 83);
            this.box_PostStats.TabIndex = 18;
            this.box_PostStats.TabStop = false;
            // 
            // lbl_Stats_TotalCount
            // 
            this.lbl_Stats_TotalCount.AutoSize = true;
            this.lbl_Stats_TotalCount.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Stats_TotalCount.Location = new System.Drawing.Point(322, 13);
            this.lbl_Stats_TotalCount.Name = "lbl_Stats_TotalCount";
            this.lbl_Stats_TotalCount.Size = new System.Drawing.Size(13, 15);
            this.lbl_Stats_TotalCount.TabIndex = 1;
            this.lbl_Stats_TotalCount.Text = "0";
            // 
            // table_Stats_PostStats
            // 
            this.table_Stats_PostStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.table_Stats_PostStats.AutoSize = true;
            this.table_Stats_PostStats.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.table_Stats_PostStats.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.table_Stats_PostStats.ColumnCount = 12;
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table_Stats_PostStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_QuoteStats, 7, 1);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_Photo, 0, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_Quote, 6, 1);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_LinkCount, 1, 1);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_ChatCount, 9, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_PhotoCount, 1, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_AudioCount, 11, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_Text, 2, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_Link, 0, 1);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_Audio, 10, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_TextCount, 3, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_Video, 4, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_VideoCount, 5, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_Answer, 6, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_AnswerCount, 7, 0);
            this.table_Stats_PostStats.Controls.Add(this.lbl_Stats_Chat, 8, 0);
            this.table_Stats_PostStats.Location = new System.Drawing.Point(4, 39);
            this.table_Stats_PostStats.Margin = new System.Windows.Forms.Padding(0);
            this.table_Stats_PostStats.Name = "table_Stats_PostStats";
            this.table_Stats_PostStats.RowCount = 2;
            this.table_Stats_PostStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_Stats_PostStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table_Stats_PostStats.Size = new System.Drawing.Size(584, 36);
            this.table_Stats_PostStats.TabIndex = 15;
            // 
            // lbl_Stats_QuoteStats
            // 
            this.lbl_Stats_QuoteStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_QuoteStats.AutoSize = true;
            this.lbl_Stats_QuoteStats.Location = new System.Drawing.Point(344, 18);
            this.lbl_Stats_QuoteStats.Name = "lbl_Stats_QuoteStats";
            this.lbl_Stats_QuoteStats.Size = new System.Drawing.Size(44, 16);
            this.lbl_Stats_QuoteStats.TabIndex = 9;
            this.lbl_Stats_QuoteStats.Text = "0";
            this.lbl_Stats_QuoteStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_Photo
            // 
            this.lbl_Stats_Photo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_Photo.AutoSize = true;
            this.lbl_Stats_Photo.Location = new System.Drawing.Point(3, 0);
            this.lbl_Stats_Photo.Name = "lbl_Stats_Photo";
            this.lbl_Stats_Photo.Size = new System.Drawing.Size(43, 16);
            this.lbl_Stats_Photo.TabIndex = 3;
            this.lbl_Stats_Photo.Text = "Photo:";
            this.lbl_Stats_Photo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_Quote
            // 
            this.lbl_Stats_Quote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_Quote.AutoSize = true;
            this.lbl_Stats_Quote.Location = new System.Drawing.Point(289, 18);
            this.lbl_Stats_Quote.Name = "lbl_Stats_Quote";
            this.lbl_Stats_Quote.Size = new System.Drawing.Size(49, 16);
            this.lbl_Stats_Quote.TabIndex = 6;
            this.lbl_Stats_Quote.Text = "Quote:";
            this.lbl_Stats_Quote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Stats_LinkCount
            // 
            this.lbl_Stats_LinkCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_LinkCount.AutoSize = true;
            this.lbl_Stats_LinkCount.Location = new System.Drawing.Point(239, 18);
            this.lbl_Stats_LinkCount.Name = "lbl_Stats_LinkCount";
            this.lbl_Stats_LinkCount.Size = new System.Drawing.Size(44, 16);
            this.lbl_Stats_LinkCount.TabIndex = 8;
            this.lbl_Stats_LinkCount.Text = "0";
            this.lbl_Stats_LinkCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_ChatCount
            // 
            this.lbl_Stats_ChatCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_ChatCount.AutoSize = true;
            this.lbl_Stats_ChatCount.Location = new System.Drawing.Point(439, 0);
            this.lbl_Stats_ChatCount.Name = "lbl_Stats_ChatCount";
            this.lbl_Stats_ChatCount.Size = new System.Drawing.Size(44, 16);
            this.lbl_Stats_ChatCount.TabIndex = 15;
            this.lbl_Stats_ChatCount.Text = "0";
            this.lbl_Stats_ChatCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_PhotoCount
            // 
            this.lbl_Stats_PhotoCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_PhotoCount.AutoSize = true;
            this.lbl_Stats_PhotoCount.Location = new System.Drawing.Point(52, 0);
            this.lbl_Stats_PhotoCount.Name = "lbl_Stats_PhotoCount";
            this.lbl_Stats_PhotoCount.Size = new System.Drawing.Size(44, 16);
            this.lbl_Stats_PhotoCount.TabIndex = 2;
            this.lbl_Stats_PhotoCount.Text = "0";
            this.lbl_Stats_PhotoCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_AudioCount
            // 
            this.lbl_Stats_AudioCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_AudioCount.AutoSize = true;
            this.lbl_Stats_AudioCount.Location = new System.Drawing.Point(537, 0);
            this.lbl_Stats_AudioCount.Name = "lbl_Stats_AudioCount";
            this.lbl_Stats_AudioCount.Size = new System.Drawing.Size(44, 16);
            this.lbl_Stats_AudioCount.TabIndex = 13;
            this.lbl_Stats_AudioCount.Text = "0";
            this.lbl_Stats_AudioCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_Text
            // 
            this.lbl_Stats_Text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_Text.AutoSize = true;
            this.lbl_Stats_Text.Location = new System.Drawing.Point(102, 0);
            this.lbl_Stats_Text.Name = "lbl_Stats_Text";
            this.lbl_Stats_Text.Size = new System.Drawing.Size(33, 16);
            this.lbl_Stats_Text.TabIndex = 4;
            this.lbl_Stats_Text.Text = "Text:";
            this.lbl_Stats_Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_Link
            // 
            this.lbl_Stats_Link.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_Link.AutoSize = true;
            this.table_Stats_PostStats.SetColumnSpan(this.lbl_Stats_Link, 5);
            this.lbl_Stats_Link.Location = new System.Drawing.Point(3, 18);
            this.lbl_Stats_Link.Name = "lbl_Stats_Link";
            this.lbl_Stats_Link.Size = new System.Drawing.Size(230, 16);
            this.lbl_Stats_Link.TabIndex = 5;
            this.lbl_Stats_Link.Text = "Link:";
            this.lbl_Stats_Link.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Stats_Audio
            // 
            this.lbl_Stats_Audio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_Audio.AutoSize = true;
            this.lbl_Stats_Audio.Location = new System.Drawing.Point(489, 0);
            this.lbl_Stats_Audio.Name = "lbl_Stats_Audio";
            this.lbl_Stats_Audio.Size = new System.Drawing.Size(42, 16);
            this.lbl_Stats_Audio.TabIndex = 12;
            this.lbl_Stats_Audio.Text = "Audio:";
            this.lbl_Stats_Audio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_TextCount
            // 
            this.lbl_Stats_TextCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_TextCount.AutoSize = true;
            this.lbl_Stats_TextCount.Location = new System.Drawing.Point(141, 0);
            this.lbl_Stats_TextCount.Name = "lbl_Stats_TextCount";
            this.lbl_Stats_TextCount.Size = new System.Drawing.Size(44, 16);
            this.lbl_Stats_TextCount.TabIndex = 7;
            this.lbl_Stats_TextCount.Text = "0";
            this.lbl_Stats_TextCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_Video
            // 
            this.lbl_Stats_Video.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_Video.AutoSize = true;
            this.lbl_Stats_Video.Location = new System.Drawing.Point(191, 0);
            this.lbl_Stats_Video.Name = "lbl_Stats_Video";
            this.lbl_Stats_Video.Size = new System.Drawing.Size(42, 16);
            this.lbl_Stats_Video.TabIndex = 10;
            this.lbl_Stats_Video.Text = "Video:";
            this.lbl_Stats_Video.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_VideoCount
            // 
            this.lbl_Stats_VideoCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_VideoCount.AutoSize = true;
            this.lbl_Stats_VideoCount.Location = new System.Drawing.Point(239, 0);
            this.lbl_Stats_VideoCount.Name = "lbl_Stats_VideoCount";
            this.lbl_Stats_VideoCount.Size = new System.Drawing.Size(44, 16);
            this.lbl_Stats_VideoCount.TabIndex = 11;
            this.lbl_Stats_VideoCount.Text = "0";
            this.lbl_Stats_VideoCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_Answer
            // 
            this.lbl_Stats_Answer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_Answer.AutoSize = true;
            this.lbl_Stats_Answer.Location = new System.Drawing.Point(289, 0);
            this.lbl_Stats_Answer.Name = "lbl_Stats_Answer";
            this.lbl_Stats_Answer.Size = new System.Drawing.Size(49, 16);
            this.lbl_Stats_Answer.TabIndex = 16;
            this.lbl_Stats_Answer.Text = "Answer:";
            this.lbl_Stats_Answer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_AnswerCount
            // 
            this.lbl_Stats_AnswerCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_AnswerCount.AutoSize = true;
            this.lbl_Stats_AnswerCount.Location = new System.Drawing.Point(344, 0);
            this.lbl_Stats_AnswerCount.Name = "lbl_Stats_AnswerCount";
            this.lbl_Stats_AnswerCount.Size = new System.Drawing.Size(44, 16);
            this.lbl_Stats_AnswerCount.TabIndex = 17;
            this.lbl_Stats_AnswerCount.Text = "0";
            this.lbl_Stats_AnswerCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_Chat
            // 
            this.lbl_Stats_Chat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Stats_Chat.AutoSize = true;
            this.lbl_Stats_Chat.Location = new System.Drawing.Point(394, 0);
            this.lbl_Stats_Chat.Name = "lbl_Stats_Chat";
            this.lbl_Stats_Chat.Size = new System.Drawing.Size(39, 16);
            this.lbl_Stats_Chat.TabIndex = 14;
            this.lbl_Stats_Chat.Text = "Chat:";
            this.lbl_Stats_Chat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_Total
            // 
            this.lbl_Stats_Total.AutoSize = true;
            this.lbl_Stats_Total.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Stats_Total.Location = new System.Drawing.Point(255, 13);
            this.lbl_Stats_Total.Name = "lbl_Stats_Total";
            this.lbl_Stats_Total.Size = new System.Drawing.Size(65, 15);
            this.lbl_Stats_Total.TabIndex = 0;
            this.lbl_Stats_Total.Text = "Total Posts:";
            // 
            // btn_Stats_Start
            // 
            this.btn_Stats_Start.FlatAppearance.BorderSize = 0;
            this.btn_Stats_Start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Stats_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Stats_Start.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Stats_Start.ImageIndex = 3;
            this.btn_Stats_Start.ImageList = this.iconsList;
            this.btn_Stats_Start.Location = new System.Drawing.Point(264, 99);
            this.btn_Stats_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Stats_Start.Name = "btn_Stats_Start";
            this.btn_Stats_Start.Size = new System.Drawing.Size(87, 24);
            this.btn_Stats_Start.TabIndex = 3;
            this.btn_Stats_Start.Text = "Get Stats";
            this.btn_Stats_Start.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Stats_Start.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Stats_Start.UseVisualStyleBackColor = true;
            this.btn_Stats_Start.Click += new System.EventHandler(this.Start_GetStats);
            this.btn_Stats_Start.MouseEnter += new System.EventHandler(this.Button_OnMouseEnter);
            this.btn_Stats_Start.MouseLeave += new System.EventHandler(this.Button_OnMouseLeave);
            // 
            // tab_TagScanner
            // 
            this.tab_TagScanner.BackColor = System.Drawing.Color.White;
            this.tab_TagScanner.Controls.Add(this.check_Tags_PhotoOnly);
            this.tab_TagScanner.Controls.Add(this.btn_TagScanner_SaveAsFile);
            this.tab_TagScanner.Controls.Add(this.list_TagScanner_TagList);
            this.tab_TagScanner.Controls.Add(this.lbl_TagScanner_TagCount);
            this.tab_TagScanner.Controls.Add(this.lbl_TagScanner_NumberOfTags);
            this.tab_TagScanner.Controls.Add(this.btn_TagScanner_Stop);
            this.tab_TagScanner.Controls.Add(this.btn_TagScanner_Start);
            this.tab_TagScanner.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_TagScanner.ImageIndex = 6;
            this.tab_TagScanner.IsClosable = false;
            this.tab_TagScanner.Location = new System.Drawing.Point(1, 32);
            this.tab_TagScanner.Name = "tab_TagScanner";
            this.tab_TagScanner.Size = new System.Drawing.Size(623, 219);
            this.tab_TagScanner.TabIndex = 4;
            this.tab_TagScanner.Text = "Tag List";
            this.tab_TagScanner.ToolTipText = "Generate a list of all tags from Tumblr blog ...";
            this.tab_TagScanner.Enter += new System.EventHandler(this.TabPage_Enter);
            // 
            // check_Tags_PhotoOnly
            // 
            this.check_Tags_PhotoOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_Tags_PhotoOnly.Location = new System.Drawing.Point(94, 155);
            this.check_Tags_PhotoOnly.Name = "check_Tags_PhotoOnly";
            this.check_Tags_PhotoOnly.Size = new System.Drawing.Size(85, 20);
            this.check_Tags_PhotoOnly.TabIndex = 10;
            this.check_Tags_PhotoOnly.Text = "Photo Only";
            this.check_Tags_PhotoOnly.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.check_Tags_PhotoOnly.UseVisualStyleBackColor = true;
            // 
            // btn_TagScanner_SaveAsFile
            // 
            this.btn_TagScanner_SaveAsFile.FlatAppearance.BorderSize = 0;
            this.btn_TagScanner_SaveAsFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TagScanner_SaveAsFile.ImageKey = "browse.ico";
            this.btn_TagScanner_SaveAsFile.ImageList = this.iconsList;
            this.btn_TagScanner_SaveAsFile.Location = new System.Drawing.Point(188, 192);
            this.btn_TagScanner_SaveAsFile.Name = "btn_TagScanner_SaveAsFile";
            this.btn_TagScanner_SaveAsFile.Size = new System.Drawing.Size(112, 23);
            this.btn_TagScanner_SaveAsFile.TabIndex = 9;
            this.btn_TagScanner_SaveAsFile.Text = "Save To File";
            this.btn_TagScanner_SaveAsFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_TagScanner_SaveAsFile.UseVisualStyleBackColor = true;
            this.btn_TagScanner_SaveAsFile.Click += new System.EventHandler(this.TagList_SaveAasFile);
            this.btn_TagScanner_SaveAsFile.MouseEnter += new System.EventHandler(this.Button_OnMouseEnter);
            this.btn_TagScanner_SaveAsFile.MouseLeave += new System.EventHandler(this.Button_OnMouseLeave);
            // 
            // list_TagScanner_TagList
            // 
            this.list_TagScanner_TagList.BackColor = System.Drawing.Color.White;
            this.list_TagScanner_TagList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list_TagScanner_TagList.Cursor = System.Windows.Forms.Cursors.Default;
            this.list_TagScanner_TagList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.list_TagScanner_TagList.FormattingEnabled = true;
            this.list_TagScanner_TagList.ItemHeight = 16;
            this.list_TagScanner_TagList.Items.AddRange(new object[] {
            "Click Get Tags to start ..."});
            this.list_TagScanner_TagList.Location = new System.Drawing.Point(306, 9);
            this.list_TagScanner_TagList.Name = "list_TagScanner_TagList";
            this.list_TagScanner_TagList.Size = new System.Drawing.Size(296, 208);
            this.list_TagScanner_TagList.Sorted = true;
            this.list_TagScanner_TagList.TabIndex = 8;
            this.list_TagScanner_TagList.TabStop = false;
            this.list_TagScanner_TagList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox_DrawItem);
            // 
            // lbl_TagScanner_TagCount
            // 
            this.lbl_TagScanner_TagCount.Font = new System.Drawing.Font("Century Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TagScanner_TagCount.Location = new System.Drawing.Point(47, 34);
            this.lbl_TagScanner_TagCount.Name = "lbl_TagScanner_TagCount";
            this.lbl_TagScanner_TagCount.Size = new System.Drawing.Size(189, 46);
            this.lbl_TagScanner_TagCount.TabIndex = 7;
            this.lbl_TagScanner_TagCount.Text = "0";
            this.lbl_TagScanner_TagCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TagScanner_NumberOfTags
            // 
            this.lbl_TagScanner_NumberOfTags.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TagScanner_NumberOfTags.Location = new System.Drawing.Point(47, 80);
            this.lbl_TagScanner_NumberOfTags.Name = "lbl_TagScanner_NumberOfTags";
            this.lbl_TagScanner_NumberOfTags.Size = new System.Drawing.Size(189, 29);
            this.lbl_TagScanner_NumberOfTags.TabIndex = 6;
            this.lbl_TagScanner_NumberOfTags.Text = "Tags";
            this.lbl_TagScanner_NumberOfTags.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_TagScanner_Stop
            // 
            this.btn_TagScanner_Stop.FlatAppearance.BorderSize = 0;
            this.btn_TagScanner_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TagScanner_Stop.ImageKey = "browse.ico";
            this.btn_TagScanner_Stop.ImageList = this.iconsList;
            this.btn_TagScanner_Stop.Location = new System.Drawing.Point(147, 123);
            this.btn_TagScanner_Stop.Name = "btn_TagScanner_Stop";
            this.btn_TagScanner_Stop.Size = new System.Drawing.Size(92, 26);
            this.btn_TagScanner_Stop.TabIndex = 5;
            this.btn_TagScanner_Stop.Text = "Stop";
            this.btn_TagScanner_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_TagScanner_Stop.UseVisualStyleBackColor = true;
            this.btn_TagScanner_Stop.Click += new System.EventHandler(this.Cancel_TagScanner);
            this.btn_TagScanner_Stop.MouseEnter += new System.EventHandler(this.Button_OnMouseEnter);
            this.btn_TagScanner_Stop.MouseLeave += new System.EventHandler(this.Button_OnMouseLeave);
            // 
            // btn_TagScanner_Start
            // 
            this.btn_TagScanner_Start.FlatAppearance.BorderSize = 0;
            this.btn_TagScanner_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TagScanner_Start.ImageKey = "browse.ico";
            this.btn_TagScanner_Start.ImageList = this.iconsList;
            this.btn_TagScanner_Start.Location = new System.Drawing.Point(39, 123);
            this.btn_TagScanner_Start.Name = "btn_TagScanner_Start";
            this.btn_TagScanner_Start.Size = new System.Drawing.Size(92, 26);
            this.btn_TagScanner_Start.TabIndex = 3;
            this.btn_TagScanner_Start.Text = "Get Tags";
            this.btn_TagScanner_Start.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_TagScanner_Start.UseVisualStyleBackColor = true;
            this.btn_TagScanner_Start.Click += new System.EventHandler(this.Start_TagScan);
            this.btn_TagScanner_Start.MouseEnter += new System.EventHandler(this.Button_OnMouseEnter);
            this.btn_TagScanner_Start.MouseLeave += new System.EventHandler(this.Button_OnMouseLeave);
            // 
            // tab_Options
            // 
            this.tab_Options.BackColor = System.Drawing.Color.White;
            this.tab_Options.Controls.Add(this.section_Options);
            this.tab_Options.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_Options.ImageIndex = 7;
            this.tab_Options.Location = new System.Drawing.Point(1, 32);
            this.tab_Options.Name = "tab_Options";
            this.tab_Options.Size = new System.Drawing.Size(623, 219);
            this.tab_Options.TabIndex = 2;
            this.tab_Options.Text = "Tool Options";
            this.tab_Options.ToolTipText = "General tools options  ...";
            // 
            // section_Options
            // 
            this.section_Options.Controls.Add(this.section_Options_LogOptions);
            this.section_Options.Controls.Add(this.section_Options_BackupOptions);
            this.section_Options.Controls.Add(this.btn_Options_Reset);
            this.section_Options.Controls.Add(this.section_Options_ImageTypes);
            this.section_Options.Controls.Add(this.btn_Options_Save);
            this.section_Options.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.section_Options.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.section_Options.Location = new System.Drawing.Point(11, 14);
            this.section_Options.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.section_Options.Name = "section_Options";
            this.section_Options.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.section_Options.Size = new System.Drawing.Size(612, 185);
            this.section_Options.TabIndex = 7;
            this.section_Options.TabStop = false;
            this.section_Options.Text = "Options";
            // 
            // section_Options_LogOptions
            // 
            this.section_Options_LogOptions.Controls.Add(this.check_Options_GenerateUncompressedLog);
            this.section_Options_LogOptions.Controls.Add(this.check_Options_GenerateLog);
            this.section_Options_LogOptions.Location = new System.Drawing.Point(357, 23);
            this.section_Options_LogOptions.Name = "section_Options_LogOptions";
            this.section_Options_LogOptions.Size = new System.Drawing.Size(205, 107);
            this.section_Options_LogOptions.TabIndex = 6;
            this.section_Options_LogOptions.TabStop = false;
            this.section_Options_LogOptions.Text = "Backup Log Options";
            // 
            // check_Options_GenerateUncompressedLog
            // 
            this.check_Options_GenerateUncompressedLog.Checked = true;
            this.check_Options_GenerateUncompressedLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_Options_GenerateUncompressedLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_Options_GenerateUncompressedLog.Location = new System.Drawing.Point(8, 57);
            this.check_Options_GenerateUncompressedLog.Name = "check_Options_GenerateUncompressedLog";
            this.check_Options_GenerateUncompressedLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.check_Options_GenerateUncompressedLog.Size = new System.Drawing.Size(143, 34);
            this.check_Options_GenerateUncompressedLog.TabIndex = 1;
            this.check_Options_GenerateUncompressedLog.Text = "Generate Text Log File";
            this.check_Options_GenerateUncompressedLog.UseVisualStyleBackColor = true;
            // 
            // check_Options_GenerateLog
            // 
            this.check_Options_GenerateLog.Checked = true;
            this.check_Options_GenerateLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_Options_GenerateLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_Options_GenerateLog.Location = new System.Drawing.Point(8, 36);
            this.check_Options_GenerateLog.Name = "check_Options_GenerateLog";
            this.check_Options_GenerateLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.check_Options_GenerateLog.Size = new System.Drawing.Size(119, 18);
            this.check_Options_GenerateLog.TabIndex = 0;
            this.check_Options_GenerateLog.Text = "Generate Log File";
            this.check_Options_GenerateLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.check_Options_GenerateLog.UseVisualStyleBackColor = true;
            this.check_Options_GenerateLog.CheckedChanged += new System.EventHandler(this.GenerateLog_CheckedChange);
            // 
            // section_Options_BackupOptions
            // 
            this.section_Options_BackupOptions.Controls.Add(this.check_Options_OldToNewDownloadOrder);
            this.section_Options_BackupOptions.Controls.Add(this.check_Options_ParseOnly);
            this.section_Options_BackupOptions.Location = new System.Drawing.Point(153, 23);
            this.section_Options_BackupOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.section_Options_BackupOptions.Name = "section_Options_BackupOptions";
            this.section_Options_BackupOptions.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.section_Options_BackupOptions.Size = new System.Drawing.Size(198, 143);
            this.section_Options_BackupOptions.TabIndex = 1;
            this.section_Options_BackupOptions.TabStop = false;
            this.section_Options_BackupOptions.Text = "Backup Download Options";
            // 
            // check_Options_OldToNewDownloadOrder
            // 
            this.check_Options_OldToNewDownloadOrder.Enabled = false;
            this.check_Options_OldToNewDownloadOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_Options_OldToNewDownloadOrder.Location = new System.Drawing.Point(8, 57);
            this.check_Options_OldToNewDownloadOrder.Name = "check_Options_OldToNewDownloadOrder";
            this.check_Options_OldToNewDownloadOrder.Size = new System.Drawing.Size(164, 20);
            this.check_Options_OldToNewDownloadOrder.TabIndex = 2;
            this.check_Options_OldToNewDownloadOrder.Text = "Reverse Download Order";
            this.check_Options_OldToNewDownloadOrder.UseVisualStyleBackColor = true;
            // 
            // check_Options_ParseOnly
            // 
            this.check_Options_ParseOnly.AutoSize = true;
            this.check_Options_ParseOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_Options_ParseOnly.Location = new System.Drawing.Point(8, 36);
            this.check_Options_ParseOnly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_Options_ParseOnly.Name = "check_Options_ParseOnly";
            this.check_Options_ParseOnly.Size = new System.Drawing.Size(117, 20);
            this.check_Options_ParseOnly.TabIndex = 0;
            this.check_Options_ParseOnly.Text = "Parse Only Mode";
            this.check_Options_ParseOnly.UseVisualStyleBackColor = true;
            // 
            // btn_Options_Reset
            // 
            this.btn_Options_Reset.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Options_Reset.FlatAppearance.BorderSize = 0;
            this.btn_Options_Reset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Options_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Options_Reset.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Options_Reset.Location = new System.Drawing.Point(475, 137);
            this.btn_Options_Reset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Options_Reset.Name = "btn_Options_Reset";
            this.btn_Options_Reset.Size = new System.Drawing.Size(87, 29);
            this.btn_Options_Reset.TabIndex = 9;
            this.btn_Options_Reset.Text = "Reset";
            this.btn_Options_Reset.UseVisualStyleBackColor = true;
            this.btn_Options_Reset.Click += new System.EventHandler(this.Options_Reset);
            this.btn_Options_Reset.MouseEnter += new System.EventHandler(this.Button_OnMouseEnter);
            this.btn_Options_Reset.MouseLeave += new System.EventHandler(this.Button_OnMouseLeave);
            // 
            // section_Options_ImageTypes
            // 
            this.section_Options_ImageTypes.Controls.Add(this.check_Options_ParseGIF);
            this.section_Options_ImageTypes.Controls.Add(this.check_Options_ParsePNG);
            this.section_Options_ImageTypes.Controls.Add(this.check_Options_ParseJPEG);
            this.section_Options_ImageTypes.Controls.Add(this.check_Options_ParsePhotoSets);
            this.section_Options_ImageTypes.Location = new System.Drawing.Point(7, 23);
            this.section_Options_ImageTypes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.section_Options_ImageTypes.Name = "section_Options_ImageTypes";
            this.section_Options_ImageTypes.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.section_Options_ImageTypes.Size = new System.Drawing.Size(138, 158);
            this.section_Options_ImageTypes.TabIndex = 4;
            this.section_Options_ImageTypes.TabStop = false;
            this.section_Options_ImageTypes.Text = "Backup Image Types";
            // 
            // check_Options_ParseGIF
            // 
            this.check_Options_ParseGIF.AutoSize = true;
            this.check_Options_ParseGIF.Checked = true;
            this.check_Options_ParseGIF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_Options_ParseGIF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_Options_ParseGIF.Location = new System.Drawing.Point(8, 95);
            this.check_Options_ParseGIF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_Options_ParseGIF.Name = "check_Options_ParseGIF";
            this.check_Options_ParseGIF.Size = new System.Drawing.Size(42, 20);
            this.check_Options_ParseGIF.TabIndex = 5;
            this.check_Options_ParseGIF.Text = "GIF";
            this.check_Options_ParseGIF.UseVisualStyleBackColor = true;
            // 
            // check_Options_ParsePNG
            // 
            this.check_Options_ParsePNG.AutoSize = true;
            this.check_Options_ParsePNG.Checked = true;
            this.check_Options_ParsePNG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_Options_ParsePNG.Enabled = false;
            this.check_Options_ParsePNG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_Options_ParsePNG.Location = new System.Drawing.Point(8, 65);
            this.check_Options_ParsePNG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_Options_ParsePNG.Name = "check_Options_ParsePNG";
            this.check_Options_ParsePNG.Size = new System.Drawing.Size(49, 20);
            this.check_Options_ParsePNG.TabIndex = 4;
            this.check_Options_ParsePNG.Text = "PNG";
            this.check_Options_ParsePNG.UseVisualStyleBackColor = true;
            // 
            // check_Options_ParseJPEG
            // 
            this.check_Options_ParseJPEG.AutoSize = true;
            this.check_Options_ParseJPEG.Checked = true;
            this.check_Options_ParseJPEG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_Options_ParseJPEG.Enabled = false;
            this.check_Options_ParseJPEG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_Options_ParseJPEG.Location = new System.Drawing.Point(7, 36);
            this.check_Options_ParseJPEG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_Options_ParseJPEG.Name = "check_Options_ParseJPEG";
            this.check_Options_ParseJPEG.Size = new System.Drawing.Size(79, 20);
            this.check_Options_ParseJPEG.TabIndex = 2;
            this.check_Options_ParseJPEG.Text = "JPG/JPEG";
            this.check_Options_ParseJPEG.UseVisualStyleBackColor = true;
            // 
            // check_Options_ParsePhotoSets
            // 
            this.check_Options_ParsePhotoSets.AutoSize = true;
            this.check_Options_ParsePhotoSets.Checked = true;
            this.check_Options_ParsePhotoSets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_Options_ParsePhotoSets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_Options_ParsePhotoSets.Location = new System.Drawing.Point(7, 123);
            this.check_Options_ParsePhotoSets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_Options_ParsePhotoSets.Name = "check_Options_ParsePhotoSets";
            this.check_Options_ParsePhotoSets.Size = new System.Drawing.Size(77, 20);
            this.check_Options_ParsePhotoSets.TabIndex = 3;
            this.check_Options_ParsePhotoSets.Text = "PhotoSets";
            this.check_Options_ParsePhotoSets.UseVisualStyleBackColor = true;
            // 
            // btn_Options_Save
            // 
            this.btn_Options_Save.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Options_Save.FlatAppearance.BorderSize = 0;
            this.btn_Options_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Options_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Options_Save.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Options_Save.Location = new System.Drawing.Point(357, 137);
            this.btn_Options_Save.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Options_Save.Name = "btn_Options_Save";
            this.btn_Options_Save.Size = new System.Drawing.Size(87, 29);
            this.btn_Options_Save.TabIndex = 8;
            this.btn_Options_Save.Text = "Save";
            this.btn_Options_Save.UseVisualStyleBackColor = true;
            this.btn_Options_Save.Click += new System.EventHandler(this.Options_Save);
            this.btn_Options_Save.MouseEnter += new System.EventHandler(this.Button_OnMouseEnter);
            this.btn_Options_Save.MouseLeave += new System.EventHandler(this.Button_OnMouseLeave);
            // 
            // tab_About
            // 
            this.tab_About.BackColor = System.Drawing.Color.White;
            this.tab_About.Controls.Add(this.lbl_About_Copyright);
            this.tab_About.Controls.Add(this.lbl_About_Info);
            this.tab_About.Controls.Add(this.lbl_About_Version);
            this.tab_About.Controls.Add(this.lbl_About_Title);
            this.tab_About.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_About.ImageIndex = 5;
            this.tab_About.Location = new System.Drawing.Point(1, 32);
            this.tab_About.Name = "tab_About";
            this.tab_About.Size = new System.Drawing.Size(623, 219);
            this.tab_About.TabIndex = 3;
            this.tab_About.Text = "About";
            this.tab_About.ToolTipText = "About this piece of ... software ...";
            // 
            // lbl_About_Copyright
            // 
            this.lbl_About_Copyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_About_Copyright.Location = new System.Drawing.Point(167, 171);
            this.lbl_About_Copyright.Name = "lbl_About_Copyright";
            this.lbl_About_Copyright.Size = new System.Drawing.Size(279, 47);
            this.lbl_About_Copyright.TabIndex = 7;
            this.lbl_About_Copyright.Text = "© 2013 - 2017 Shino Amakusa tumblrtools.codeplex.com";
            this.lbl_About_Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_About_Info
            // 
            this.lbl_About_Info.Location = new System.Drawing.Point(167, 100);
            this.lbl_About_Info.Name = "lbl_About_Info";
            this.lbl_About_Info.Size = new System.Drawing.Size(279, 60);
            this.lbl_About_Info.TabIndex = 6;
            this.lbl_About_Info.Text = "Tumblr Tools is a simple application for parsing and downloading photo posts from" +
    " any Tumblr blog as well as getting basic stats for those.";
            this.lbl_About_Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_About_Version
            // 
            this.lbl_About_Version.AutoSize = true;
            this.lbl_About_Version.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_About_Version.Location = new System.Drawing.Point(548, 0);
            this.lbl_About_Version.Name = "lbl_About_Version";
            this.lbl_About_Version.Size = new System.Drawing.Size(75, 16);
            this.lbl_About_Version.TabIndex = 5;
            this.lbl_About_Version.Text = "Version: 0.0.0";
            // 
            // lbl_About_Title
            // 
            this.lbl_About_Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_About_Title.AutoSize = true;
            this.lbl_About_Title.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_About_Title.Location = new System.Drawing.Point(235, 68);
            this.lbl_About_Title.Name = "lbl_About_Title";
            this.lbl_About_Title.Size = new System.Drawing.Size(134, 32);
            this.lbl_About_Title.TabIndex = 4;
            this.lbl_About_Title.Text = "Tumblr Tools";
            this.lbl_About_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_About_Title.UseCompatibleTextRendering = true;
            // 
            // txt_TumblrURL
            // 
            this.txt_TumblrURL.BackColor = System.Drawing.Color.White;
            this.txt_TumblrURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_TumblrURL.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.txt_TumblrURL.ForeColor = System.Drawing.Color.Black;
            this.txt_TumblrURL.Location = new System.Drawing.Point(261, 29);
            this.txt_TumblrURL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_TumblrURL.Name = "txt_TumblrURL";
            this.txt_TumblrURL.Size = new System.Drawing.Size(169, 21);
            this.txt_TumblrURL.TabIndex = 0;
            this.txt_TumblrURL.TabStop = false;
            this.txt_TumblrURL.Text = "http://";
            // 
            // lbl_Crawler_TumblrURL
            // 
            this.lbl_Crawler_TumblrURL.AutoSize = true;
            this.lbl_Crawler_TumblrURL.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.lbl_Crawler_TumblrURL.Location = new System.Drawing.Point(186, 31);
            this.lbl_Crawler_TumblrURL.Name = "lbl_Crawler_TumblrURL";
            this.lbl_Crawler_TumblrURL.Size = new System.Drawing.Size(69, 16);
            this.lbl_Crawler_TumblrURL.TabIndex = 2;
            this.lbl_Crawler_TumblrURL.Text = "Tumblr URL:";
            // 
            // bar_Progress
            // 
            this.bar_Progress.BackColor = System.Drawing.Color.White;
            this.bar_Progress.BarColor = System.Drawing.Color.Black;
            this.bar_Progress.BorderColor = System.Drawing.Color.Transparent;
            this.bar_Progress.FillStyle = Tumblr_Tool.ColorProgressBar.FillStyles.Solid;
            this.bar_Progress.ForeColor = System.Drawing.Color.Black;
            this.bar_Progress.Location = new System.Drawing.Point(25, 317);
            this.bar_Progress.Margin = new System.Windows.Forms.Padding(0);
            this.bar_Progress.Maximum = 100;
            this.bar_Progress.Minimum = 0;
            this.bar_Progress.Name = "bar_Progress";
            this.bar_Progress.Size = new System.Drawing.Size(578, 30);
            this.bar_Progress.Step = 11;
            this.bar_Progress.TabIndex = 13;
            this.bar_Progress.Value = 50;
            // 
            // blogTagListWorker
            // 
            this.blogTagListWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TagListWorker_Work);
            this.blogTagListWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TagListWorker_Completed);
            // 
            // blogTagLIstWorkerUI
            // 
            this.blogTagLIstWorkerUI.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TagListWorkerUI_Work);
            this.blogTagLIstWorkerUI.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TagListWorkerUI_Completed);
            // 
            // tagListSaveWorker
            // 
            this.tagListSaveWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TagListSaveWorker_Work);
            this.tagListSaveWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TagListSaveWorker_Completed);
            // 
            // trayIcon
            // 
            this.trayIcon.Text = "trayIcon";
            this.trayIcon.Visible = true;
            this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseDoubleClick);
            // 
            // trayIconContextMenu
            // 
            this.trayIconContextMenu.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trayIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayIcon_MenuItem_Restore,
            this.trayIcon_MenuItem_Close});
            this.trayIconContextMenu.Name = "trayIconContextMenu";
            this.trayIconContextMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // trayIcon_MenuItem_Restore
            // 
            this.trayIcon_MenuItem_Restore.Image = global::Tumblr_Tool.Properties.Resources.home;
            this.trayIcon_MenuItem_Restore.Name = "trayIcon_MenuItem_Restore";
            this.trayIcon_MenuItem_Restore.Size = new System.Drawing.Size(152, 22);
            this.trayIcon_MenuItem_Restore.Text = "Restore";
            this.trayIcon_MenuItem_Restore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.trayIcon_MenuItem_Restore.Click += new System.EventHandler(this.trayIcon_MenuItem_Restore_Click);
            // 
            // trayIcon_MenuItem_Close
            // 
            this.trayIcon_MenuItem_Close.Image = global::Tumblr_Tool.Properties.Resources.menu;
            this.trayIcon_MenuItem_Close.Name = "trayIcon_MenuItem_Close";
            this.trayIcon_MenuItem_Close.Size = new System.Drawing.Size(152, 22);
            this.trayIcon_MenuItem_Close.Text = "Close";
            this.trayIcon_MenuItem_Close.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.trayIcon_MenuItem_Close.Click += new System.EventHandler(this.trayIcon_MenuItem_Close_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(626, 404);
            this.Controls.Add(this.lbl_PercentBar);
            this.Controls.Add(this.status_Strip);
            this.Controls.Add(this.tabControl_Main);
            this.Controls.Add(this.txt_TumblrURL);
            this.Controls.Add(this.menu_TopMenu);
            this.Controls.Add(this.bar_Progress);
            this.Controls.Add(this.lbl_Crawler_TumblrURL);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu_TopMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tumblr Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExitApplication);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menu_TopMenu.ResumeLayout(false);
            this.menu_TopMenu.PerformLayout();
            this.status_Strip.ResumeLayout(false);
            this.status_Strip.PerformLayout();
            this.tabControl_Main.ResumeLayout(false);
            this.tab_PhotoPostsParser.ResumeLayout(false);
            this.tab_PhotoPostsParser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_Crawler_DisplayImage)).EndInit();
            this.tab_TumblrStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.img_Stats_Avatar)).EndInit();
            this.box_PostStats.ResumeLayout(false);
            this.box_PostStats.PerformLayout();
            this.table_Stats_PostStats.ResumeLayout(false);
            this.table_Stats_PostStats.PerformLayout();
            this.tab_TagScanner.ResumeLayout(false);
            this.tab_Options.ResumeLayout(false);
            this.section_Options.ResumeLayout(false);
            this.section_Options_LogOptions.ResumeLayout(false);
            this.section_Options_BackupOptions.ResumeLayout(false);
            this.section_Options_BackupOptions.PerformLayout();
            this.section_Options_ImageTypes.ResumeLayout(false);
            this.section_Options_ImageTypes.PerformLayout();
            this.tab_About.ResumeLayout(false);
            this.tab_About.PerformLayout();
            this.trayIconContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KRBTabControl.KRBTabControl tabControl_Main;
        private System.Windows.Forms.Label lbl_Crawler_SaveLocation;
        private System.Windows.Forms.PictureBox img_Crawler_DisplayImage;
        private System.Windows.Forms.TextBox txt_TumblrURL;
        private System.Windows.Forms.Label lbl_Crawler_TumblrURL;
        private System.Windows.Forms.TextBox txt_Crawler_SaveLocation;
        private System.Windows.Forms.ToolStripMenuItem menuItem_File;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Help;
        private System.Windows.Forms.ToolStripMenuItem menuItem_About;
        private System.Windows.Forms.Button btn_Crawler_Browse;
        private System.Windows.Forms.Button btn_Crawler_Start;
        private System.Windows.Forms.RichTextBox txt_Crawler_WorkStatus;
        private System.ComponentModel.BackgroundWorker imageDownloadWorker;
        private ColorProgressBar bar_Progress;
        private System.Windows.Forms.MenuStrip menu_TopMenu;
        private System.ComponentModel.BackgroundWorker imageCrawlWorker;
        private System.ComponentModel.BackgroundWorker imageCrawlWorkerUI;
        private System.Windows.Forms.StatusStrip status_Strip;
        private System.Windows.Forms.ToolStripStatusLabel lbl_Status;
        private System.Windows.Forms.ToolStripStatusLabel lbl_PostCount;
        private System.Windows.Forms.ToolStripStatusLabel lbl_Size;
        private System.ComponentModel.BackgroundWorker imageDownloadWorkerUI;
        private System.Windows.Forms.Label lbl_Stats_Photo;
        private System.Windows.Forms.Label lbl_Stats_PhotoCount;
        private System.Windows.Forms.Label lbl_Stats_TotalCount;
        private System.Windows.Forms.Label lbl_Stats_Total;
        private System.ComponentModel.BackgroundWorker blogGetStatsWorker;
        private System.ComponentModel.BackgroundWorker blogGetStatsWorkerUI;
        private System.Windows.Forms.Button btn_Stats_Start;
        private System.Windows.Forms.Label lbl_Stats_QuoteStats;
        private System.Windows.Forms.Label lbl_Stats_LinkCount;
        private System.Windows.Forms.Label lbl_Stats_TextCount;
        private System.Windows.Forms.Label lbl_Stats_Quote;
        private System.Windows.Forms.Label lbl_Stats_Link;
        private System.Windows.Forms.Label lbl_Stats_Text;
        private System.Windows.Forms.Label lbl_Stats_ChatCount;
        private System.Windows.Forms.Label lbl_Stats_Chat;
        private System.Windows.Forms.Label lbl_Stats_AudioCount;
        private System.Windows.Forms.Label lbl_Stats_Audio;
        private System.Windows.Forms.Label lbl_Stats_VideoCount;
        private System.Windows.Forms.Label lbl_Stats_Video;
        private System.Windows.Forms.Label lbl_Stats_AnswerCount;
        private System.Windows.Forms.Label lbl_Stats_Answer;
        private System.Windows.Forms.GroupBox box_PostStats;
        private System.Windows.Forms.Label lbl_Stats_BlogTitle;
        private System.Windows.Forms.RichTextBox txt_Stats_BlogDescription;
        private System.ComponentModel.BackgroundWorker fileOpenWorker;
        private System.Windows.Forms.Label lbl_PercentBar;
        private System.Windows.Forms.Label lbl_Crawler_Mode;
        private AdvancedComboBox select_Crawler_Mode;
        private CirclePictureBox img_Stats_Avatar;
        private System.Windows.Forms.TableLayoutPanel table_Stats_PostStats;
        private KRBTabControl.TabPageEx tab_PhotoPostsParser;
        private KRBTabControl.TabPageEx tab_TumblrStats;
        private KRBTabControl.TabPageEx tab_Options;
        private KRBTabControl.TabPageEx tab_About;
        private System.Windows.Forms.GroupBox section_Options_LogOptions;
        private System.Windows.Forms.CheckBox check_Options_GenerateLog;
        private System.Windows.Forms.Button btn_Options_Reset;
        private System.Windows.Forms.GroupBox section_Options;
        private System.Windows.Forms.GroupBox section_Options_BackupOptions;
        private System.Windows.Forms.CheckBox check_Options_ParseOnly;
        private System.Windows.Forms.GroupBox section_Options_ImageTypes;
        private System.Windows.Forms.CheckBox check_Options_ParseGIF;
        private System.Windows.Forms.CheckBox check_Options_ParsePNG;
        private System.Windows.Forms.CheckBox check_Options_ParseJPEG;
        private System.Windows.Forms.CheckBox check_Options_ParsePhotoSets;
        private System.Windows.Forms.Button btn_Options_Save;
        private System.Windows.Forms.Label lbl_About_Copyright;
        private System.Windows.Forms.Label lbl_About_Info;
        private System.Windows.Forms.Label lbl_About_Version;
        private System.Windows.Forms.Label lbl_About_Title;
        private System.Windows.Forms.Label lbl_Crawler_ImageSize;
        private System.Windows.Forms.ImageList iconsList;
        private System.Windows.Forms.Button btn_Crawler_Stop;
        private AdvancedComboBox select_Crawler_ImagesSize;
        private System.Windows.Forms.CheckBox check_Options_OldToNewDownloadOrder;
        private KRBTabControl.TabPageEx tab_TagScanner;
        private System.Windows.Forms.Button btn_TagScanner_Start;
        private System.ComponentModel.BackgroundWorker blogTagListWorker;
        private System.ComponentModel.BackgroundWorker blogTagLIstWorkerUI;
        private System.Windows.Forms.Button btn_TagScanner_Stop;
        private System.Windows.Forms.Label lbl_TagScanner_TagCount;
        private System.Windows.Forms.Label lbl_TagScanner_NumberOfTags;
        private System.Windows.Forms.ListBox list_TagScanner_TagList;
        private System.Windows.Forms.Button btn_TagScanner_SaveAsFile;
        private System.ComponentModel.BackgroundWorker tagListSaveWorker;
        private System.Windows.Forms.CheckBox check_Tags_PhotoOnly;
        private System.Windows.Forms.ToolStripMenuItem menuItem_LoadFromFile;
        private System.Windows.Forms.CheckBox check_Options_GenerateUncompressedLog;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem trayIcon_MenuItem_Restore;
        private System.Windows.Forms.ToolStripMenuItem trayIcon_MenuItem_Close;
    }
}

