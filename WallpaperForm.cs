using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace DawnWallpaper
{
    public partial class WallpaperForm : Form
    {
        private string WallpaperLocation;
        private bool WallpaperBGM;

        public WallpaperForm(string wallpaperlocation, bool wallpaperbgm)
        {
            InitializeComponent();
            WallpaperLocation = wallpaperlocation;
            WallpaperBGM = wallpaperbgm;
            InitializeData();
        }

        private void InitializeData()
        {
            this.Location = new Point(0, 0);
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            WallpaperWindowsMediaPlayer.Location = new Point(0, 0);
            WallpaperWindowsMediaPlayer.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            WallpaperWindowsMediaPlayer.enableContextMenu = false;
            WallpaperWindowsMediaPlayer.uiMode = "none";
            WallpaperWindowsMediaPlayer.stretchToFit = true;
            WallpaperWindowsMediaPlayer.settings.setMode("loop", true);
            WallpaperWindowsMediaPlayer.settings.mute = !WallpaperBGM;
            if (!WallpaperBGM) WallpaperWindowsMediaPlayer.settings.volume = 0;
            WallpaperWindowsMediaPlayer.URL = WallpaperLocation;
        }
    }
}
