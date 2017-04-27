namespace PhotoMosaic
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
            this._displayImageBtn = new System.Windows.Forms.Button();
            this._imageNameTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._mosaicPathTxt = new System.Windows.Forms.TextBox();
            this._processImageBtn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._progressBarDescription = new System.Windows.Forms.ToolStripStatusLabel();
            this._progressBarPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this._progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this._selectImageBtn = new System.Windows.Forms.Button();
            this._selectMosaicImagePath = new System.Windows.Forms.Button();
            this._adjustColorsCkb = new System.Windows.Forms.CheckBox();
            this._adjustColorsTtp = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _displayImageBtn
            // 
            this._displayImageBtn.Location = new System.Drawing.Point(93, 76);
            this._displayImageBtn.Name = "_displayImageBtn";
            this._displayImageBtn.Size = new System.Drawing.Size(85, 29);
            this._displayImageBtn.TabIndex = 1;
            this._displayImageBtn.Text = "Display Image";
            this._displayImageBtn.UseVisualStyleBackColor = true;
            this._displayImageBtn.Click += new System.EventHandler(this.OnDisplayImage);
            // 
            // _imageNameTxt
            // 
            this._imageNameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._imageNameTxt.Location = new System.Drawing.Point(93, 14);
            this._imageNameTxt.Name = "_imageNameTxt";
            this._imageNameTxt.ReadOnly = true;
            this._imageNameTxt.Size = new System.Drawing.Size(247, 23);
            this._imageNameTxt.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Image:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mosaic Path:";
            // 
            // _mosaicPathTxt
            // 
            this._mosaicPathTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._mosaicPathTxt.Location = new System.Drawing.Point(93, 47);
            this._mosaicPathTxt.Name = "_mosaicPathTxt";
            this._mosaicPathTxt.ReadOnly = true;
            this._mosaicPathTxt.Size = new System.Drawing.Size(247, 23);
            this._mosaicPathTxt.TabIndex = 5;
            // 
            // _processImageBtn
            // 
            this._processImageBtn.Location = new System.Drawing.Point(184, 76);
            this._processImageBtn.Name = "_processImageBtn";
            this._processImageBtn.Size = new System.Drawing.Size(85, 29);
            this._processImageBtn.TabIndex = 6;
            this._processImageBtn.Text = "Process Image";
            this._processImageBtn.UseVisualStyleBackColor = true;
            this._processImageBtn.Click += new System.EventHandler(this.OnProcessImage);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._progressBarDescription,
            this._progressBarPercent,
            this._progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 112);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(384, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _progressBarDescription
            // 
            this._progressBarDescription.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._progressBarDescription.Name = "_progressBarDescription";
            this._progressBarDescription.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._progressBarDescription.Size = new System.Drawing.Size(232, 17);
            this._progressBarDescription.Spring = true;
            this._progressBarDescription.Text = "Status";
            this._progressBarDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._progressBarDescription.Visible = false;
            // 
            // _progressBarPercent
            // 
            this._progressBarPercent.AutoSize = false;
            this._progressBarPercent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._progressBarPercent.Name = "_progressBarPercent";
            this._progressBarPercent.Size = new System.Drawing.Size(35, 17);
            this._progressBarPercent.Text = "0%";
            this._progressBarPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._progressBarPercent.Visible = false;
            // 
            // _progressBar
            // 
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(100, 16);
            this._progressBar.Visible = false;
            // 
            // _selectImageBtn
            // 
            this._selectImageBtn.Location = new System.Drawing.Point(346, 14);
            this._selectImageBtn.Name = "_selectImageBtn";
            this._selectImageBtn.Size = new System.Drawing.Size(25, 23);
            this._selectImageBtn.TabIndex = 8;
            this._selectImageBtn.Text = "...";
            this._selectImageBtn.UseVisualStyleBackColor = true;
            this._selectImageBtn.Click += new System.EventHandler(this.OnSelectImage);
            // 
            // _selectMosaicImagePath
            // 
            this._selectMosaicImagePath.Location = new System.Drawing.Point(346, 47);
            this._selectMosaicImagePath.Name = "_selectMosaicImagePath";
            this._selectMosaicImagePath.Size = new System.Drawing.Size(25, 23);
            this._selectMosaicImagePath.TabIndex = 9;
            this._selectMosaicImagePath.Text = "...";
            this._selectMosaicImagePath.UseVisualStyleBackColor = true;
            this._selectMosaicImagePath.Click += new System.EventHandler(this.OnSelectMosaicPath);
            // 
            // _adjustColorsCkb
            // 
            this._adjustColorsCkb.AutoSize = true;
            this._adjustColorsCkb.Location = new System.Drawing.Point(284, 83);
            this._adjustColorsCkb.Name = "_adjustColorsCkb";
            this._adjustColorsCkb.Size = new System.Drawing.Size(87, 17);
            this._adjustColorsCkb.TabIndex = 10;
            this._adjustColorsCkb.Text = "Adjust Colors";
            this._adjustColorsCkb.UseVisualStyleBackColor = true;
            // 
            // _adjustColorsTtp
            // 
            this._adjustColorsTtp.AutoPopDelay = 10000;
            this._adjustColorsTtp.InitialDelay = 500;
            this._adjustColorsTtp.ReshowDelay = 100;
            this._adjustColorsTtp.Tag = "";
            this._adjustColorsTtp.ToolTipTitle = "Adjust Colors";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 134);
            this.Controls.Add(this._adjustColorsCkb);
            this.Controls.Add(this._selectMosaicImagePath);
            this.Controls.Add(this._selectImageBtn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._processImageBtn);
            this.Controls.Add(this._mosaicPathTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._imageNameTxt);
            this.Controls.Add(this._displayImageBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Photo Mosaic";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _displayImageBtn;
        private System.Windows.Forms.TextBox _imageNameTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _mosaicPathTxt;
        private System.Windows.Forms.Button _processImageBtn;
        private System.Windows.Forms.ToolStripProgressBar _progressBar;
        private System.Windows.Forms.ToolStripStatusLabel _progressBarPercent;
        private System.Windows.Forms.ToolStripStatusLabel _progressBarDescription;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button _selectImageBtn;
        private System.Windows.Forms.Button _selectMosaicImagePath;
        private System.Windows.Forms.CheckBox _adjustColorsCkb;
        private System.Windows.Forms.ToolTip _adjustColorsTtp;
    }
}

