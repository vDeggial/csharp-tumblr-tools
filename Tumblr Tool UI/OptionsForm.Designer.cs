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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.parseOptionSection = new System.Windows.Forms.GroupBox();
            this.box_Log = new System.Windows.Forms.GroupBox();
            this.check_GenerateLog = new System.Windows.Forms.CheckBox();
            this.box_APIOptions = new System.Windows.Forms.GroupBox();
            this.select_APIMode = new Tumblr_Tool.AdvancedComboBox();
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
            this.box_Log.SuspendLayout();
            this.box_APIOptions.SuspendLayout();
            this.box_General.SuspendLayout();
            this.box_ImageTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // parseOptionSection
            // 
            this.parseOptionSection.Controls.Add(this.box_Log);
            this.parseOptionSection.Controls.Add(this.box_APIOptions);
            this.parseOptionSection.Controls.Add(this.box_General);
            this.parseOptionSection.Controls.Add(this.box_ImageTypes);
            this.parseOptionSection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.parseOptionSection.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parseOptionSection.Location = new System.Drawing.Point(5, 4);
            this.parseOptionSection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.parseOptionSection.Name = "parseOptionSection";
            this.parseOptionSection.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.parseOptionSection.Size = new System.Drawing.Size(304, 228);
            this.parseOptionSection.TabIndex = 0;
            this.parseOptionSection.TabStop = false;
            this.parseOptionSection.Text = "Parse Options";
            // 
            // box_Log
            // 
            this.box_Log.Controls.Add(this.check_GenerateLog);
            this.box_Log.Location = new System.Drawing.Point(7, 184);
            this.box_Log.Name = "box_Log";
            this.box_Log.Size = new System.Drawing.Size(291, 72);
            this.box_Log.TabIndex = 6;
            this.box_Log.TabStop = false;
            this.box_Log.Text = "Log Options";
            // 
            // check_GenerateLog
            // 
            this.check_GenerateLog.AutoSize = true;
            this.check_GenerateLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_GenerateLog.Location = new System.Drawing.Point(8, 26);
            this.check_GenerateLog.Name = "check_GenerateLog";
            this.check_GenerateLog.Size = new System.Drawing.Size(124, 20);
            this.check_GenerateLog.TabIndex = 0;
            this.check_GenerateLog.Text = "Generate Post Log";
            this.check_GenerateLog.UseVisualStyleBackColor = true;
            // 
            // box_APIOptions
            // 
            this.box_APIOptions.Controls.Add(this.select_APIMode);
            this.box_APIOptions.Location = new System.Drawing.Point(147, 130);
            this.box_APIOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.box_APIOptions.Name = "box_APIOptions";
            this.box_APIOptions.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.box_APIOptions.Size = new System.Drawing.Size(157, 51);
            this.box_APIOptions.TabIndex = 5;
            this.box_APIOptions.TabStop = false;
            this.box_APIOptions.Text = "API Options";
            this.box_APIOptions.Visible = false;
            // 
            // select_APIMode
            // 
            this.select_APIMode.BackColor = System.Drawing.Color.White;
            this.select_APIMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.select_APIMode.Enabled = false;
            this.select_APIMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select_APIMode.ForeColor = System.Drawing.Color.Black;
            this.select_APIMode.FormattingEnabled = true;
            this.select_APIMode.HighlightBackColor = System.Drawing.Color.White;
            this.select_APIMode.HighlightForeColor = System.Drawing.Color.Maroon;
            this.select_APIMode.Items.AddRange(new object[] {
            "API v.1 (XML)",
            "API v.2 (JSON)"});
            this.select_APIMode.Location = new System.Drawing.Point(8, 23);
            this.select_APIMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.select_APIMode.Name = "select_APIMode";
            this.select_APIMode.Size = new System.Drawing.Size(140, 22);
            this.select_APIMode.Style = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.box_General.Size = new System.Drawing.Size(158, 98);
            this.box_General.TabIndex = 1;
            this.box_General.TabStop = false;
            this.box_General.Text = "Method Options";
            // 
            // check_ParseDownload
            // 
            this.check_ParseDownload.AutoSize = true;
            this.check_ParseDownload.Checked = true;
            this.check_ParseDownload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_ParseDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_ParseDownload.Location = new System.Drawing.Point(8, 65);
            this.check_ParseDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_ParseDownload.Name = "check_ParseDownload";
            this.check_ParseDownload.Size = new System.Drawing.Size(124, 20);
            this.check_ParseDownload.TabIndex = 1;
            this.check_ParseDownload.Text = "Parse && Download";
            this.check_ParseDownload.UseVisualStyleBackColor = true;
            this.check_ParseDownload.CheckedChanged += new System.EventHandler(this.check_ParseDownload_CheckedChanged);
            // 
            // check_ParseOnly
            // 
            this.check_ParseOnly.AutoSize = true;
            this.check_ParseOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_ParseOnly.Location = new System.Drawing.Point(8, 36);
            this.check_ParseOnly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_ParseOnly.Name = "check_ParseOnly";
            this.check_ParseOnly.Size = new System.Drawing.Size(81, 20);
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
            this.check_GIF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_GIF.Location = new System.Drawing.Point(8, 95);
            this.check_GIF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_GIF.Name = "check_GIF";
            this.check_GIF.Size = new System.Drawing.Size(42, 20);
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
            this.check_PNG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_PNG.Location = new System.Drawing.Point(8, 65);
            this.check_PNG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_PNG.Name = "check_PNG";
            this.check_PNG.Size = new System.Drawing.Size(49, 20);
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
            this.check_JPEG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_JPEG.Location = new System.Drawing.Point(7, 36);
            this.check_JPEG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_JPEG.Name = "check_JPEG";
            this.check_JPEG.Size = new System.Drawing.Size(79, 20);
            this.check_JPEG.TabIndex = 2;
            this.check_JPEG.Text = "JPG/JPEG";
            this.check_JPEG.UseVisualStyleBackColor = true;
            // 
            // check_PhotoSets
            // 
            this.check_PhotoSets.AutoSize = true;
            this.check_PhotoSets.Checked = true;
            this.check_PhotoSets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_PhotoSets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_PhotoSets.Location = new System.Drawing.Point(7, 123);
            this.check_PhotoSets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check_PhotoSets.Name = "check_PhotoSets";
            this.check_PhotoSets.Size = new System.Drawing.Size(77, 20);
            this.check_PhotoSets.TabIndex = 3;
            this.check_PhotoSets.Text = "PhotoSets";
            this.check_PhotoSets.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(157, 277);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(87, 28);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            this.btn_Cancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btn_Cancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btn_Accept
            // 
            this.btn_Accept.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Accept.FlatAppearance.BorderSize = 0;
            this.btn_Accept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Accept.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Accept.Location = new System.Drawing.Point(58, 277);
            this.btn_Accept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Accept.Name = "btn_Accept";
            this.btn_Accept.Size = new System.Drawing.Size(87, 28);
            this.btn_Accept.TabIndex = 5;
            this.btn_Accept.Text = "Accept";
            this.btn_Accept.UseVisualStyleBackColor = true;
            this.btn_Accept.Click += new System.EventHandler(this.btn_Accept_Click);
            this.btn_Accept.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btn_Accept.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(307, 306);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.parseOptionSection);
            this.Controls.Add(this.btn_Accept);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.setOptions);
            this.parseOptionSection.ResumeLayout(false);
            this.box_Log.ResumeLayout(false);
            this.box_Log.PerformLayout();
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
        private AdvancedComboBox select_APIMode;
        private System.Windows.Forms.GroupBox box_Log;
        private System.Windows.Forms.CheckBox check_GenerateLog;

    }
}