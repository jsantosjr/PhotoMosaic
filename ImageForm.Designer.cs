namespace PhotoMosaic
{
    partial class ImageForm
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
            this._imageArea = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._imageArea)).BeginInit();
            this.SuspendLayout();
            // 
            // _imageArea
            // 
            this._imageArea.Location = new System.Drawing.Point(0, 0);
            this._imageArea.Name = "_imageArea";
            this._imageArea.Size = new System.Drawing.Size(268, 247);
            this._imageArea.TabIndex = 0;
            this._imageArea.TabStop = false;
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 261);
            this.Controls.Add(this._imageArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ImageForm";
            this.Text = "ImageForm";
            ((System.ComponentModel.ISupportInitialize)(this._imageArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _imageArea;
    }
}