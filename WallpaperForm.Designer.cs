namespace DawnWallpaper
{
    partial class WallpaperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WallpaperForm));
            WallpaperWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)WallpaperWindowsMediaPlayer).BeginInit();
            SuspendLayout();
            // 
            // WallpaperWindowsMediaPlayer
            // 
            WallpaperWindowsMediaPlayer.Enabled = true;
            WallpaperWindowsMediaPlayer.Location = new Point(255, 134);
            WallpaperWindowsMediaPlayer.Name = "WallpaperWindowsMediaPlayer";
            WallpaperWindowsMediaPlayer.OcxState = (AxHost.State)resources.GetObject("WallpaperWindowsMediaPlayer.OcxState");
            WallpaperWindowsMediaPlayer.Size = new Size(224, 101);
            WallpaperWindowsMediaPlayer.TabIndex = 0;
            // 
            // WallpaperForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(WallpaperWindowsMediaPlayer);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WallpaperForm";
            Text = "破晓壁纸";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)WallpaperWindowsMediaPlayer).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer WallpaperWindowsMediaPlayer;
    }
}