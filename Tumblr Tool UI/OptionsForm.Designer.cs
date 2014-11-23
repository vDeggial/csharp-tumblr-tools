namespace Tumblr_Tool
{
    partial class OptionsForm
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
            this.parseOptionSection = new System.Windows.Forms.GroupBox();
            this.box_APIOptions = new System.Windows.Forms.GroupBox();
            this.select_APIMode = new System.Windows.Forms.ComboBox();
            this.box_General = new System.Windows.Forms.GroupBox();
            this.check_ParseDownload = new System.Windows.Forms.CheckBox();
            this.check_ParseOnly = new System.Windows.Forms.CheckBox();
            this.box_ImageTypes = new System.Windows.Forms.GroupBox();
            this.check_GIF = new System.Windows.Forms.CheckBox();
            this.check_PNG = new System.Windows.Forms.CheckBox();
            this.check_JPEG = new System.Windows.Forms.CheckBox();
            this.check_PhotoSets = new System.Windows.Forms.CheckBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Accept = new System.Windows.Forms.Button();
            this.parseOptionSection.SuspendLayout();
            this.box_APIOptions.SuspendLayout();
            this.box_General.SuspendLayout();
            this.box_ImageTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // parseOptionSection
            // 
            this.parseOptionSection.Controls.Add(this.box_APIOptions);
            this.parseOptionSection.Controls.Add(this.box_General);
            this.parseOptionSection.Controls.Add(this.box_ImageTypes);
            this.parseOptionSection.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parseOptionSection.Location = new System.Drawing.Point(5, 4);
            this.parseOptionSection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.parseOptionSection.Name = "parseOptionSection";
            this.parseOptionSection.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.parseOptionSection.Size = new System.Drawing.Size(324, 228);
            this.parseOptionSection.TabIndex = 0;
            this.parseOptionSection.TabStop = false;
            this.parseOptionSection.Text = "Parse Options";
            // 
            // box_APIOptions
            // 
            this.box_APIOptions.Controls.Add(this.select_APIMode);
            this.box_APIOptions.Location = new System.Drawing.Point(147, 130);
            this.box_APIOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.box_APIOptions.Name = "box_APIOptions";
            this.box_APIOptions.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.box_APIOptions.Size = new System.Drawing.Size(177, 123);
            this.box_APIOptions.TabIndex = 5;
            this.box_APIOptions.TabStop = false;
            this.box_APIOptions.Text = "API Options";
            // 
            // select_APIMode
            // 
            this.select_APIMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.select_APIMode.FormattingEnabled = true;
            this.select_APIMode.Items.AddRange(new object[] {
            "API v.1 (XML)",
            "API v.2 (JSON)"});
            this.select_APIMode.Location = new System.Drawing.Point(8, 23);
            this.select_APIMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.select_APIMode.Name = "select_APIMode";
            this.select_APIMode.Size = new System.Drawing.Size(140, 24);
            this.select_APIMode.TabIndex = 0;
            this.select_APIMode.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // box_General
            // 
            this.box_General.Controls.Add(this.check_ParseDownload);
            this.box_General.Controls.Add(this.check_ParseOnly);
            this.box_General.Location = new System.Drawing.Point(147, 23);
            this.box_General.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.box_General.Name = "box_General";
            this.box_General.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.box_General.Size = new System.Drawing.Size(176, 98);
            this.box_General.TabIndex = 1;
            this.box_General.TabStop = false;
            this.box_General.Text = "Method Options";
            // 
            // check_ParseDownload
            // 
            this.check_ParseDownload.AutoSize = true;
            this.check_ParseDownload.Checked = true;
            this.check_ParseDownload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_ParseDownload.Location = new System.Drawing.Point(8, 65);
            this.check_ParseDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_ParseDownload.Name = "check_ParseDownload";
            this.check_ParseDownload.Size = new System.Drawing.Size(148, 25);
            this.check_ParseDownload.TabIndex = 1;
            this.check_ParseDownload.Text = "Parse && Download";
            this.check_ParseDownload.UseVisualStyleBackColor = true;
            this.check_ParseDownload.CheckedChanged += new System.EventHandler(this.check_ParseDownload_CheckedChanged);
            // 
            // check_ParseOnly
            // 
            this.check_ParseOnly.AutoSize = true;
            this.check_ParseOnly.Location = new System.Drawing.Point(8, 36);
            this.check_ParseOnly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_ParseOnly.Name = "check_ParseOnly";
            this.check_ParseOnly.Size = new System.Drawing.Size(98, 25);
            this.check_ParseOnly.TabIndex = 0;
            this.check_ParseOnly.Text = "Parse Only";
            this.check_ParseOnly.UseVisualStyleBackColor = true;
            this.check_ParseOnly.CheckedChanged += new System.EventHandler(this.check_ParseOnly_CheckedChanged);
            // 
            // box_ImageTypes
            // 
            this.box_ImageTypes.Controls.Add(this.check_GIF);
            this.box_ImageTypes.Controls.Add(this.check_PNG);
            this.box_ImageTypes.Controls.Add(this.check_JPEG);
            this.box_ImageTypes.Controls.Add(this.check_PhotoSets);
            this.box_ImageTypes.Location = new System.Drawing.Point(7, 23);
            this.box_ImageTypes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.box_ImageTypes.Name = "box_ImageTypes";
            this.box_ImageTypes.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.box_ImageTypes.Size = new System.Drawing.Size(133, 158);
            this.box_ImageTypes.TabIndex = 4;
            this.box_ImageTypes.TabStop = false;
            this.box_ImageTypes.Text = "Image Types";
            // 
            // check_GIF
            // 
            this.check_GIF.AutoSize = true;
            this.check_GIF.Checked = true;
            this.check_GIF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_GIF.Location = new System.Drawing.Point(8, 95);
            this.check_GIF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_GIF.Name = "check_GIF";
            this.check_GIF.Size = new System.Drawing.Size(52, 25);
            this.check_GIF.TabIndex = 5;
            this.check_GIF.Text = "GIF";
            this.check_GIF.UseVisualStyleBackColor = true;
            // 
            // check_PNG
            // 
            this.check_PNG.AutoSize = true;
            this.check_PNG.Checked = true;
            this.check_PNG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_PNG.Enabled = false;
            this.check_PNG.Location = new System.Drawing.Point(8, 65);
            this.check_PNG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_PNG.Name = "check_PNG";
            this.check_PNG.Size = new System.Drawing.Size(61, 25);
            this.check_PNG.TabIndex = 4;
            this.check_PNG.Text = "PNG";
            this.check_PNG.UseVisualStyleBackColor = true;
            // 
            // check_JPEG
            // 
            this.check_JPEG.AutoSize = true;
            this.check_JPEG.Checked = true;
            this.check_JPEG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_JPEG.Enabled = false;
            this.check_JPEG.Location = new System.Drawing.Point(7, 36);
            this.check_JPEG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_JPEG.Name = "check_JPEG";
            this.check_JPEG.Size = new System.Drawing.Size(96, 25);
            this.check_JPEG.TabIndex = 2;
            this.check_JPEG.Text = "JPG/JPEG";
            this.check_JPEG.UseVisualStyleBackColor = true;
            // 
            // check_PhotoSets
            // 
            this.check_PhotoSets.AutoSize = true;
            this.check_PhotoSets.Checked = true;
            this.check_PhotoSets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_PhotoSets.Location = new System.Drawing.Point(7, 123);
            this.check_PhotoSets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_PhotoSets.Name = "check_PhotoSets";
            this.check_PhotoSets.Size = new System.Drawing.Size(93, 25);
            this.check_PhotoSets.TabIndex = 3;
            this.check_PhotoSets.Text = "PhotoSets";
            this.check_PhotoSets.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(159, 239);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(87, 28);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Accept
            // 
            this.btn_Accept.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Accept.Location = new System.Drawing.Point(68, 239);
            this.btn_Accept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Accept.Name = "btn_Accept";
            this.btn_Accept.Size = new System.Drawing.Size(87, 28);
            this.btn_Accept.TabIndex = 5;
            this.btn_Accept.Text = "Accept";
            this.btn_Accept.UseVisualStyleBackColor = true;
            this.btn_Accept.Click += new System.EventHandler(this.btn_Accept_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 273);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.parseOptionSection);
            this.Controls.Add(this.btn_Accept);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.setOptions);
            this.parseOptionSection.ResumeLayout(false);
            this.box_APIOptions.ResumeLayout(false);
            this.box_General.ResumeLayout(false);
            this.box_General.PerformLayout();
            this.box_ImageTypes.ResumeLayout(false);
            this.box_ImageTypes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox parseOptionSection;
        private System.Windows.Forms.CheckBox check_PhotoSets;
        private System.Windows.Forms.CheckBox check_JPEG;
        private System.Windows.Forms.GroupBox box_ImageTypes;
        private System.Windows.Forms.CheckBox check_GIF;
        private System.Windows.Forms.CheckBox check_PNG;
        private System.Windows.Forms.GroupBox box_General;
        private System.Windows.Forms.CheckBox check_ParseDownload;
        private System.Windows.Forms.CheckBox check_ParseOnly;
        private System.Windows.Forms.Button btn_Accept;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox box_APIOptions;
        private System.Windows.Forms.ComboBox select_APIMode;

    }
}