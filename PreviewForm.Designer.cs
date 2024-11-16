using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace DawnWallpaper
{
    partial class PreviewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewForm));
            axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(111, 107);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(498, 197);
            axWindowsMediaPlayer1.TabIndex = 0;
            // 
            // PreviewForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(939, 551);
            Controls.Add(axWindowsMediaPlayer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(640, 360);
            Name = "PreviewForm";
            Padding = new Padding(2, 36, 2, 2);
            Resizable = true;
            ShowDragStretch = true;
            Text = "预览窗口";
            ZoomScaleRect = new Rectangle(22, 22, 800, 450);
            FormClosing += PreviewW_FormClosing;
            Load += PreviewW_Load;
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}
