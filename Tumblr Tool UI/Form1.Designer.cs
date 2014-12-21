using System.Drawing;
namespace Tumblr_Tool
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.lbl_PercentBar = new System.Windows.Forms.Label();
            this.menuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.download_Worker = new System.ComponentModel.BackgroundWorker();
            this.menu_TopMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crawl_Worker = new System.ComponentModel.BackgroundWorker();
            this.crawl_UpdateUI_Worker = new System.ComponentModel.BackgroundWorker();
            this.status_Strip = new System.Windows.Forms.StatusStrip();
            this.lbl_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_PostCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_Size = new System.Windows.Forms.ToolStripStatusLabel();
            this.download_UIUpdate_Worker = new System.ComponentModel.BackgroundWorker();
            this.getStats_Worker = new System.ComponentModel.BackgroundWorker();
            this.getStatsUI_Worker = new System.ComponentModel.BackgroundWorker();
            this.fileBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tabControl_Main = new Dotnetrix.Controls.TabControl();
            this.tab_ImageRipper = new System.Windows.Forms.TabPage();
            this.lbl_Mode = new System.Windows.Forms.Label();
            this.txt_WorkStatus = new System.Windows.Forms.RichTextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.img_DisplayImage = new System.Windows.Forms.PictureBox();
            this.txt_TumblrURL = new System.Windows.Forms.TextBox();
            this.lbl_TumblrURL = new System.Windows.Forms.Label();
            this.txt_SaveLocation = new System.Windows.Forms.TextBox();
            this.lbl_SaveLocation = new System.Windows.Forms.Label();
            this.tab_TumblrStats = new System.Windows.Forms.TabPage();
            this.txt_Stats_BlogDescription = new System.Windows.Forms.RichTextBox();
            this.lbl_Stats_BlogTitle = new System.Windows.Forms.Label();
            this.lbl_Stats_URL = new System.Windows.Forms.Label();
            this.txt_Stats_TumblrURL = new System.Windows.Forms.TextBox();
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
            this.btn_GetStats = new System.Windows.Forms.Button();
            this.select_Mode = new Tumblr_Tool.AdvancedComboBox();
            this.img_Stats_Avatar = new Tumblr_Tool.CirclePictureBox();
            this.bar_Progress = new Tumblr_Tool.ColorProgressBar();
            this.menu_TopMenu.SuspendLayout();
            this.status_Strip.SuspendLayout();
            this.tabControl_Main.SuspendLayout();
            this.tab_ImageRipper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_DisplayImage)).BeginInit();
            this.tab_TumblrStats.SuspendLayout();
            this.box_PostStats.SuspendLayout();
            this.table_Stats_PostStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_Stats_Avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "down.ico");
            this.iconList.Images.SetKeyName(1, "info.ico");
            this.iconList.Images.SetKeyName(2, "browse.ico");
            this.iconList.Images.SetKeyName(3, "crawl.ico");
            this.iconList.Images.SetKeyName(4, "about.ico");
            this.iconList.Images.SetKeyName(5, "info.ico");
            this.iconList.Images.SetKeyName(6, "menu.ico");
            this.iconList.Images.SetKeyName(7, "options.ico");
            // 
            // lbl_PercentBar
            // 
            this.lbl_PercentBar.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PercentBar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lbl_PercentBar.Location = new System.Drawing.Point(0, 345);
            this.lbl_PercentBar.Name = "lbl_PercentBar";
            this.lbl_PercentBar.Size = new System.Drawing.Size(642, 23);
            this.lbl_PercentBar.TabIndex = 14;
            this.lbl_PercentBar.Text = "[Progress %]";
            this.lbl_PercentBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_PercentBar.Visible = false;
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
            // download_Worker
            // 
            this.download_Worker.WorkerReportsProgress = true;
            this.download_Worker.WorkerSupportsCancellation = true;
            this.download_Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.downloadWorker_DoWork);
            this.download_Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.downloadWorker_AfterDone);
            // 
            // menu_TopMenu
            // 
            this.menu_TopMenu.BackColor = System.Drawing.Color.White;
            this.menu_TopMenu.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.menu_TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
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
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Image = global::Tumblr_Tool.Properties.Resources.menu;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 21);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripMenuItem_Paint);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.ToolTipText = "Open Tumblr Tools Save file";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            this.openToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripMenuItem_Paint);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.SystemColors.Menu;
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Visible = false;
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            this.saveToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripMenuItem_Paint);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.optionsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.optionsToolStripMenuItem.Image = global::Tumblr_Tool.Properties.Resources.options;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(66, 21);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.ToolTipText = "View Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            this.optionsToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripMenuItem_Paint);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Image = global::Tumblr_Tool.Properties.Resources.help;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(27, 21);
            this.helpToolStripMenuItem.Text = "?";
            this.helpToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripMenuItem_Paint);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Image = global::Tumblr_Tool.Properties.Resources.about;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.ToolTipText = "About this product";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            this.aboutToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripMenuItem_Paint);
            // 
            // crawl_Worker
            // 
            this.crawl_Worker.WorkerReportsProgress = true;
            this.crawl_Worker.WorkerSupportsCancellation = true;
            this.crawl_Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.crawlWorker_DoWork);
            this.crawl_Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.crawlWorker_AfterDone);
            // 
            // crawl_UpdateUI_Worker
            // 
            this.crawl_UpdateUI_Worker.WorkerReportsProgress = true;
            this.crawl_UpdateUI_Worker.WorkerSupportsCancellation = true;
            this.crawl_UpdateUI_Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.crawlWorker_UI__DoWork);
            this.crawl_UpdateUI_Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.crawlWorker_UI__AfterDone);
            // 
            // status_Strip
            // 
            this.status_Strip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.status_Strip.AutoSize = false;
            this.status_Strip.BackColor = System.Drawing.Color.Transparent;
            this.status_Strip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.status_Strip.Dock = System.Windows.Forms.DockStyle.None;
            this.status_Strip.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_Strip.GripMargin = new System.Windows.Forms.Padding(0);
            this.status_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_Status,
            this.lbl_PostCount,
            this.lbl_Size});
            this.status_Strip.Location = new System.Drawing.Point(0, 366);
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
            // download_UIUpdate_Worker
            // 
            this.download_UIUpdate_Worker.WorkerReportsProgress = true;
            this.download_UIUpdate_Worker.WorkerSupportsCancellation = true;
            this.download_UIUpdate_Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.downloadWorker_UI__DoWork);
            this.download_UIUpdate_Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.downloadWorker_UI__AfterDone);
            // 
            // getStats_Worker
            // 
            this.getStats_Worker.WorkerReportsProgress = true;
            this.getStats_Worker.WorkerSupportsCancellation = true;
            this.getStats_Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getStatsWorker_DoWork);
            this.getStats_Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.getStatsWorker_AfterDone);
            // 
            // getStatsUI_Worker
            // 
            this.getStatsUI_Worker.WorkerReportsProgress = true;
            this.getStatsUI_Worker.WorkerSupportsCancellation = true;
            this.getStatsUI_Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getStatsWorker_UI__DoWork);
            this.getStatsUI_Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.getStatsWorker_UI_AfterDone);
            // 
            // fileBackgroundWorker
            // 
            this.fileBackgroundWorker.WorkerSupportsCancellation = true;
            this.fileBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.fileBW_DoWork);
            this.fileBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.fileBW_AfterDone);
            // 
            // tabControl_Main
            // 
            this.tabControl_Main.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl_Main.BackColor = System.Drawing.Color.White;
            this.tabControl_Main.Controls.Add(this.tab_ImageRipper);
            this.tabControl_Main.Controls.Add(this.tab_TumblrStats);
            this.tabControl_Main.DoubleBufferTabPages = true;
            this.tabControl_Main.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.tabControl_Main.ImageList = this.iconList;
            this.tabControl_Main.Location = new System.Drawing.Point(0, 25);
            this.tabControl_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl_Main.Name = "tabControl_Main";
            this.tabControl_Main.Padding = new System.Drawing.Point(5, 0);
            this.tabControl_Main.SelectedIndex = 0;
            this.tabControl_Main.SelectedTabColor = System.Drawing.Color.White;
            this.tabControl_Main.Size = new System.Drawing.Size(625, 275);
            this.tabControl_Main.TabColor = System.Drawing.Color.White;
            this.tabControl_Main.TabIndex = 0;
            this.tabControl_Main.UseBackColorBehindTabs = true;
            this.tabControl_Main.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Main_Selecting);
            // 
            // tab_ImageRipper
            // 
            this.tab_ImageRipper.BackColor = System.Drawing.Color.White;
            this.tab_ImageRipper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tab_ImageRipper.Controls.Add(this.lbl_Mode);
            this.tab_ImageRipper.Controls.Add(this.select_Mode);
            this.tab_ImageRipper.Controls.Add(this.txt_WorkStatus);
            this.tab_ImageRipper.Controls.Add(this.btn_Start);
            this.tab_ImageRipper.Controls.Add(this.btn_Browse);
            this.tab_ImageRipper.Controls.Add(this.img_DisplayImage);
            this.tab_ImageRipper.Controls.Add(this.txt_TumblrURL);
            this.tab_ImageRipper.Controls.Add(this.lbl_TumblrURL);
            this.tab_ImageRipper.Controls.Add(this.txt_SaveLocation);
            this.tab_ImageRipper.Controls.Add(this.lbl_SaveLocation);
            this.tab_ImageRipper.ForeColor = System.Drawing.Color.Black;
            this.tab_ImageRipper.ImageIndex = 0;
            this.tab_ImageRipper.Location = new System.Drawing.Point(0, 25);
            this.tab_ImageRipper.Margin = new System.Windows.Forms.Padding(0);
            this.tab_ImageRipper.Name = "tab_ImageRipper";
            this.tab_ImageRipper.Size = new System.Drawing.Size(625, 250);
            this.tab_ImageRipper.TabIndex = 0;
            this.tab_ImageRipper.Text = "Image Crawler";
            this.tab_ImageRipper.Enter += new System.EventHandler(this.tabPage_Enter);
            // 
            // lbl_Mode
            // 
            this.lbl_Mode.AutoSize = true;
            this.lbl_Mode.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.lbl_Mode.Location = new System.Drawing.Point(252, 38);
            this.lbl_Mode.Name = "lbl_Mode";
            this.lbl_Mode.Size = new System.Drawing.Size(44, 16);
            this.lbl_Mode.TabIndex = 17;
            this.lbl_Mode.Text = "Mode:";
            // 
            // txt_WorkStatus
            // 
            this.txt_WorkStatus.BackColor = System.Drawing.Color.White;
            this.txt_WorkStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_WorkStatus.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txt_WorkStatus.DetectUrls = false;
            this.txt_WorkStatus.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.txt_WorkStatus.ForeColor = System.Drawing.Color.Black;
            this.txt_WorkStatus.Location = new System.Drawing.Point(8, 81);
            this.txt_WorkStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_WorkStatus.Name = "txt_WorkStatus";
            this.txt_WorkStatus.ReadOnly = true;
            this.txt_WorkStatus.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txt_WorkStatus.Size = new System.Drawing.Size(425, 158);
            this.txt_WorkStatus.TabIndex = 8;
            this.txt_WorkStatus.TabStop = false;
            this.txt_WorkStatus.Text = "";
            this.txt_WorkStatus.TextChanged += new System.EventHandler(this.workStatusAutoScroll);
            // 
            // btn_Start
            // 
            this.btn_Start.FlatAppearance.BorderSize = 0;
            this.btn_Start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Start.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btn_Start.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Start.ImageIndex = 3;
            this.btn_Start.ImageList = this.iconList;
            this.btn_Start.Location = new System.Drawing.Point(346, 6);
            this.btn_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(87, 28);
            this.btn_Start.TabIndex = 7;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Crawl_Click);
            this.btn_Start.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btn_Start.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btn_Browse
            // 
            this.btn_Browse.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btn_Browse.FlatAppearance.BorderSize = 0;
            this.btn_Browse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Browse.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btn_Browse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Browse.ImageIndex = 2;
            this.btn_Browse.ImageList = this.iconList;
            this.btn_Browse.Location = new System.Drawing.Point(253, 6);
            this.btn_Browse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(87, 28);
            this.btn_Browse.TabIndex = 6;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            this.btn_Browse.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btn_Browse.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // img_DisplayImage
            // 
            this.img_DisplayImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.img_DisplayImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.img_DisplayImage.ErrorImage = global::Tumblr_Tool.Properties.Resources.tumblrlogo;
            this.img_DisplayImage.Image = global::Tumblr_Tool.Properties.Resources.tumblrlogo;
            this.img_DisplayImage.InitialImage = null;
            this.img_DisplayImage.Location = new System.Drawing.Point(442, 0);
            this.img_DisplayImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.img_DisplayImage.Name = "img_DisplayImage";
            this.img_DisplayImage.Size = new System.Drawing.Size(183, 235);
            this.img_DisplayImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.img_DisplayImage.TabIndex = 4;
            this.img_DisplayImage.TabStop = false;
            // 
            // txt_TumblrURL
            // 
            this.txt_TumblrURL.BackColor = System.Drawing.Color.White;
            this.txt_TumblrURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_TumblrURL.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.txt_TumblrURL.ForeColor = System.Drawing.Color.Black;
            this.txt_TumblrURL.Location = new System.Drawing.Point(78, 39);
            this.txt_TumblrURL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_TumblrURL.Name = "txt_TumblrURL";
            this.txt_TumblrURL.Size = new System.Drawing.Size(169, 21);
            this.txt_TumblrURL.TabIndex = 3;
            this.txt_TumblrURL.Text = "http://";
            this.txt_TumblrURL.TextChanged += new System.EventHandler(this.statsTumblrURLUpdate);
            // 
            // lbl_TumblrURL
            // 
            this.lbl_TumblrURL.AutoSize = true;
            this.lbl_TumblrURL.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.lbl_TumblrURL.Location = new System.Drawing.Point(0, 38);
            this.lbl_TumblrURL.Name = "lbl_TumblrURL";
            this.lbl_TumblrURL.Size = new System.Drawing.Size(69, 16);
            this.lbl_TumblrURL.TabIndex = 2;
            this.lbl_TumblrURL.Text = "Tumblr URL:";
            // 
            // txt_SaveLocation
            // 
            this.txt_SaveLocation.BackColor = System.Drawing.Color.White;
            this.txt_SaveLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_SaveLocation.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.txt_SaveLocation.ForeColor = System.Drawing.Color.Black;
            this.txt_SaveLocation.Location = new System.Drawing.Point(103, 9);
            this.txt_SaveLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_SaveLocation.Name = "txt_SaveLocation";
            this.txt_SaveLocation.ReadOnly = true;
            this.txt_SaveLocation.Size = new System.Drawing.Size(144, 21);
            this.txt_SaveLocation.TabIndex = 1;
            // 
            // lbl_SaveLocation
            // 
            this.lbl_SaveLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_SaveLocation.AutoSize = true;
            this.lbl_SaveLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_SaveLocation.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.lbl_SaveLocation.Location = new System.Drawing.Point(0, 8);
            this.lbl_SaveLocation.Name = "lbl_SaveLocation";
            this.lbl_SaveLocation.Size = new System.Drawing.Size(90, 16);
            this.lbl_SaveLocation.TabIndex = 0;
            this.lbl_SaveLocation.Text = "Save Location:";
            // 
            // tab_TumblrStats
            // 
            this.tab_TumblrStats.BackColor = System.Drawing.Color.White;
            this.tab_TumblrStats.Controls.Add(this.img_Stats_Avatar);
            this.tab_TumblrStats.Controls.Add(this.txt_Stats_BlogDescription);
            this.tab_TumblrStats.Controls.Add(this.lbl_Stats_BlogTitle);
            this.tab_TumblrStats.Controls.Add(this.lbl_Stats_URL);
            this.tab_TumblrStats.Controls.Add(this.txt_Stats_TumblrURL);
            this.tab_TumblrStats.Controls.Add(this.box_PostStats);
            this.tab_TumblrStats.Controls.Add(this.btn_GetStats);
            this.tab_TumblrStats.ImageIndex = 1;
            this.tab_TumblrStats.Location = new System.Drawing.Point(0, 25);
            this.tab_TumblrStats.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tab_TumblrStats.Name = "tab_TumblrStats";
            this.tab_TumblrStats.Size = new System.Drawing.Size(625, 250);
            this.tab_TumblrStats.TabIndex = 1;
            this.tab_TumblrStats.Text = "Blog Stats";
            this.tab_TumblrStats.Enter += new System.EventHandler(this.tabPage_Enter);
            // 
            // txt_Stats_BlogDescription
            // 
            this.txt_Stats_BlogDescription.BackColor = System.Drawing.Color.White;
            this.txt_Stats_BlogDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Stats_BlogDescription.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txt_Stats_BlogDescription.DetectUrls = false;
            this.txt_Stats_BlogDescription.ForeColor = System.Drawing.Color.Black;
            this.txt_Stats_BlogDescription.Location = new System.Drawing.Point(16, 83);
            this.txt_Stats_BlogDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Stats_BlogDescription.Name = "txt_Stats_BlogDescription";
            this.txt_Stats_BlogDescription.ReadOnly = true;
            this.txt_Stats_BlogDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txt_Stats_BlogDescription.Size = new System.Drawing.Size(597, 72);
            this.txt_Stats_BlogDescription.TabIndex = 4;
            this.txt_Stats_BlogDescription.TabStop = false;
            this.txt_Stats_BlogDescription.Text = "";
            // 
            // lbl_Stats_BlogTitle
            // 
            this.lbl_Stats_BlogTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Stats_BlogTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Stats_BlogTitle.Location = new System.Drawing.Point(358, 24);
            this.lbl_Stats_BlogTitle.Name = "lbl_Stats_BlogTitle";
            this.lbl_Stats_BlogTitle.Size = new System.Drawing.Size(246, 26);
            this.lbl_Stats_BlogTitle.TabIndex = 1;
            this.lbl_Stats_BlogTitle.Text = "[Blog Title Here]";
            this.lbl_Stats_BlogTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Stats_URL
            // 
            this.lbl_Stats_URL.AutoSize = true;
            this.lbl_Stats_URL.Location = new System.Drawing.Point(9, 4);
            this.lbl_Stats_URL.Name = "lbl_Stats_URL";
            this.lbl_Stats_URL.Size = new System.Drawing.Size(31, 16);
            this.lbl_Stats_URL.TabIndex = 1;
            this.lbl_Stats_URL.Text = "URL:";
            // 
            // txt_Stats_TumblrURL
            // 
            this.txt_Stats_TumblrURL.BackColor = System.Drawing.Color.White;
            this.txt_Stats_TumblrURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Stats_TumblrURL.ForeColor = System.Drawing.Color.Black;
            this.txt_Stats_TumblrURL.Location = new System.Drawing.Point(12, 24);
            this.txt_Stats_TumblrURL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Stats_TumblrURL.Name = "txt_Stats_TumblrURL";
            this.txt_Stats_TumblrURL.Size = new System.Drawing.Size(178, 21);
            this.txt_Stats_TumblrURL.TabIndex = 0;
            this.txt_Stats_TumblrURL.Text = "http://";
            this.txt_Stats_TumblrURL.TextChanged += new System.EventHandler(this.txt_StatsTumblrURL_TextChanged);
            // 
            // box_PostStats
            // 
            this.box_PostStats.Controls.Add(this.lbl_Stats_TotalCount);
            this.box_PostStats.Controls.Add(this.table_Stats_PostStats);
            this.box_PostStats.Controls.Add(this.lbl_Stats_Total);
            this.box_PostStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.box_PostStats.Location = new System.Drawing.Point(16, 163);
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
            // btn_GetStats
            // 
            this.btn_GetStats.FlatAppearance.BorderSize = 0;
            this.btn_GetStats.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_GetStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_GetStats.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_GetStats.ImageIndex = 3;
            this.btn_GetStats.ImageList = this.iconList;
            this.btn_GetStats.Location = new System.Drawing.Point(103, 51);
            this.btn_GetStats.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_GetStats.Name = "btn_GetStats";
            this.btn_GetStats.Size = new System.Drawing.Size(87, 24);
            this.btn_GetStats.TabIndex = 3;
            this.btn_GetStats.Text = "Get Stats";
            this.btn_GetStats.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_GetStats.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_GetStats.UseVisualStyleBackColor = true;
            this.btn_GetStats.Click += new System.EventHandler(this.btn_GetStats_Click);
            this.btn_GetStats.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btn_GetStats.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // select_Mode
            // 
            this.select_Mode.BackColor = System.Drawing.Color.White;
            this.select_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.select_Mode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select_Mode.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.select_Mode.ForeColor = System.Drawing.Color.Black;
            this.select_Mode.FormattingEnabled = true;
            this.select_Mode.HighlightBackColor = System.Drawing.Color.White;
            this.select_Mode.HighlightForeColor = System.Drawing.Color.Maroon;
            this.select_Mode.Location = new System.Drawing.Point(305, 39);
            this.select_Mode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.select_Mode.Name = "select_Mode";
            this.select_Mode.Size = new System.Drawing.Size(128, 22);
            this.select_Mode.Style = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.select_Mode.TabIndex = 16;
            // 
            // img_Stats_Avatar
            // 
            this.img_Stats_Avatar.ErrorImage = global::Tumblr_Tool.Properties.Resources.avatar;
            this.img_Stats_Avatar.Image = global::Tumblr_Tool.Properties.Resources.avatar;
            this.img_Stats_Avatar.InitialImage = global::Tumblr_Tool.Properties.Resources.avatar;
            this.img_Stats_Avatar.Location = new System.Drawing.Point(288, 4);
            this.img_Stats_Avatar.Name = "img_Stats_Avatar";
            this.img_Stats_Avatar.Size = new System.Drawing.Size(72, 72);
            this.img_Stats_Avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.img_Stats_Avatar.TabIndex = 19;
            this.img_Stats_Avatar.TabStop = false;
            // 
            // bar_Progress
            // 
            this.bar_Progress.BackColor = System.Drawing.Color.White;
            this.bar_Progress.BarColor = System.Drawing.Color.Black;
            this.bar_Progress.BorderColor = System.Drawing.Color.Transparent;
            this.bar_Progress.FillStyle = Tumblr_Tool.ColorProgressBar.FillStyles.Solid;
            this.bar_Progress.ForeColor = System.Drawing.Color.LightGray;
            this.bar_Progress.Location = new System.Drawing.Point(3, 311);
            this.bar_Progress.Margin = new System.Windows.Forms.Padding(0);
            this.bar_Progress.Maximum = 100;
            this.bar_Progress.Minimum = 0;
            this.bar_Progress.Name = "bar_Progress";
            this.bar_Progress.Size = new System.Drawing.Size(622, 30);
            this.bar_Progress.Step = 11;
            this.bar_Progress.TabIndex = 13;
            this.bar_Progress.Value = 60;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(626, 392);
            this.Controls.Add(this.lbl_PercentBar);
            this.Controls.Add(this.status_Strip);
            this.Controls.Add(this.tabControl_Main);
            this.Controls.Add(this.menu_TopMenu);
            this.Controls.Add(this.bar_Progress);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu_TopMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tumblr Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_Closing);
            this.menu_TopMenu.ResumeLayout(false);
            this.menu_TopMenu.PerformLayout();
            this.status_Strip.ResumeLayout(false);
            this.status_Strip.PerformLayout();
            this.tabControl_Main.ResumeLayout(false);
            this.tab_ImageRipper.ResumeLayout(false);
            this.tab_ImageRipper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_DisplayImage)).EndInit();
            this.tab_TumblrStats.ResumeLayout(false);
            this.tab_TumblrStats.PerformLayout();
            this.box_PostStats.ResumeLayout(false);
            this.box_PostStats.PerformLayout();
            this.table_Stats_PostStats.ResumeLayout(false);
            this.table_Stats_PostStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_Stats_Avatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Dotnetrix.Controls.TabControl tabControl_Main;
        private System.Windows.Forms.TabPage tab_ImageRipper;
        private System.Windows.Forms.TabPage tab_TumblrStats;
        private System.Windows.Forms.Label lbl_SaveLocation;
        private System.Windows.Forms.PictureBox img_DisplayImage;
        private System.Windows.Forms.TextBox txt_TumblrURL;
        private System.Windows.Forms.Label lbl_TumblrURL;
        private System.Windows.Forms.TextBox txt_SaveLocation;
        private System.Windows.Forms.ToolStripMenuItem menuItem_File;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Help;
        private System.Windows.Forms.ToolStripMenuItem menuItem_About;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.RichTextBox txt_WorkStatus;
        private System.ComponentModel.BackgroundWorker download_Worker;
        private ColorProgressBar bar_Progress;
        private System.Windows.Forms.MenuStrip menu_TopMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker crawl_Worker;
        private System.ComponentModel.BackgroundWorker crawl_UpdateUI_Worker;
        private System.Windows.Forms.StatusStrip status_Strip;
        private System.Windows.Forms.ToolStripStatusLabel lbl_Status;
        private System.Windows.Forms.ToolStripStatusLabel lbl_PostCount;
        private System.Windows.Forms.ToolStripStatusLabel lbl_Size;
        private System.ComponentModel.BackgroundWorker download_UIUpdate_Worker;
        private System.Windows.Forms.TextBox txt_Stats_TumblrURL;
        private System.Windows.Forms.Label lbl_Stats_Photo;
        private System.Windows.Forms.Label lbl_Stats_PhotoCount;
        private System.Windows.Forms.Label lbl_Stats_TotalCount;
        private System.Windows.Forms.Label lbl_Stats_Total;
        private System.Windows.Forms.Label lbl_Stats_URL;
        private System.ComponentModel.BackgroundWorker getStats_Worker;
        private System.ComponentModel.BackgroundWorker getStatsUI_Worker;
        private System.Windows.Forms.Button btn_GetStats;
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
        private System.ComponentModel.BackgroundWorker fileBackgroundWorker;
        private System.Windows.Forms.Label lbl_PercentBar;
        private System.Windows.Forms.Label lbl_Mode;
        private AdvancedComboBox select_Mode;
        private System.Windows.Forms.ImageList iconList;
        private CirclePictureBox img_Stats_Avatar;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel table_Stats_PostStats;
    }
}

