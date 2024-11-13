using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DawnWallpaper
{
    public partial class QuietWallpaperForm : Form
    {
        public QuietWallpaperForm()
        {
            InitializeComponent();
        }

        private void Control_Load(object sender, EventArgs e)
        {
            player.enableContextMenu = false;
            player.uiMode = "none";
            player.stretchToFit = true;
            player.settings.setMode("loop", true);
            player.settings.mute = true;
            player.settings.volume = 0;
        }
    }
}
