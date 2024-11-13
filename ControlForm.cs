using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using Sunny.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DawnWallpaper
{
    public partial class ControlForm : UIForm
    {
        public static string assetsDirectory = Path.Combine(Application.StartupPath, "assets");
        public static string indexName = "";
        public static bool sound = true;
        public static bool bgm = true;

        private IntPtr windowsHandle = IntPtr.Zero;
        private WallpaperForm ?wallpaperform;
        private AxWMPLib.AxWindowsMediaPlayer ?wallpaperplayer;

        public ControlForm()
        {
            InitializeComponent();
        }

        private void InitialzeUI()
        {
            Font font = new Font("宋体", this.Height / 70);
            uiLabel1.Font = font;
            uiCheckBox1.Font = font;
            uiCheckBox2.Font = font;
            uiButton1.Font = font;
            uiButton2.Font = font;
            uiButton3.Font = font;

            uiFlowLayoutPanel1.Location = new Point(20, 70);
            uiFlowLayoutPanel1.Size = new Size(this.Width / 3 * 2, this.Height - 70 - 20);
            uiTitlePanel1.Location = new Point(this.Width / 3 * 2 + 40, 70);
            uiTitlePanel1.Size = new Size(this.Width - (this.Width / 3 * 2 + 40) - 20, this.Height - 70 - 20);
            pictureBox1.Location = new Point(uiTitlePanel1.Width / 4, uiTitlePanel1.Height / 8);
            pictureBox1.Size = new Size(uiTitlePanel1.Width / 4 * 2, uiTitlePanel1.Width / 4 * 2);
            uiLabel1.Location = new Point(uiTitlePanel1.Width / 8, uiTitlePanel1.Height / 8 + uiTitlePanel1.Width / 8 * 5);
            uiLabel1.Size = new Size(uiTitlePanel1.Width / 8 * 6, pictureBox1.Height / 2);
            uiCheckBox1.Location = new Point(uiTitlePanel1.Width / 4, uiTitlePanel1.Height / 8 + uiTitlePanel1.Width / 8 * 7);
            uiCheckBox2.Location = new Point(uiTitlePanel1.Width / 4, uiTitlePanel1.Height / 8 + uiTitlePanel1.Width / 8 * 7 + 50);
            uiButton1.Location = new Point(uiTitlePanel1.Width / 10, uiTitlePanel1.Height / 10 * 9);
            uiButton1.Size = new Size(uiTitlePanel1.Width / 20 * 7, uiTitlePanel1.Height / 30 * 2);
            uiButton2.Location = new Point(uiTitlePanel1.Width / 10, uiTitlePanel1.Height / 10 * 8);
            uiButton2.Size = new Size(uiTitlePanel1.Width / 10 * 8, uiTitlePanel1.Height / 30 * 2);
            uiButton3.Location = new Point(uiTitlePanel1.Width / 20 * 11, uiTitlePanel1.Height / 10 * 9);
            uiButton3.Size = new Size(uiTitlePanel1.Width / 20 * 7, uiTitlePanel1.Height / 30 * 2);
            uiFlowLayoutPanel1.Clear();
            InitializeData();
        }

        private void InitializeDescription()
        {
            IniFile configini = new IniFile(Path.Combine(assetsDirectory, indexName, "config.ini"));
            pictureBox1.ImageLocation = Path.Combine(assetsDirectory, indexName, "header.png");
            uiLabel1.Text = configini.ReadString("main", "name", "") + "\n"
                + configini.ReadString("main", "version", "") + "\n"
                + configini.ReadString("main", "author", "") + "\n"
                + configini.ReadString("main", "size", "") + "\n";
        }

        private void InitializeData()
        {
            uiFlowLayoutPanel1.Clear();
            DirectoryInfo srcPath = new DirectoryInfo(assetsDirectory);
            foreach (DirectoryInfo subDirectory in srcPath.GetDirectories())
            {
                PictureBox header = new PictureBox();
                header.Height = pictureBox1.Height;
                header.Width = pictureBox1.Width;
                header.SizeMode = PictureBoxSizeMode.StretchImage;
                header.Name = Path.GetFileName(subDirectory.Name);
                header.ImageLocation = Path.Combine(subDirectory.FullName, "header.png");
                header.Click += Header_Click;
                uiFlowLayoutPanel1.Add(header);
            }
        }

        private void InitializeIcon()
        {
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.Text = "破晓壁纸";
            notifyIcon1.Visible = true;
        }

        private void InitializeBoot()
        {
            if(!Directory.Exists(assetsDirectory)) Directory.CreateDirectory(assetsDirectory);
        }

        private void wallpaperLoad(string Name)
        {
            if (!File.Exists(Path.Combine(assetsDirectory, Name, "video.mp4"))) return;

            if (windowsHandle != IntPtr.Zero)
            {
                Form wallpaperWindows = (Form)Control.FromHandle(windowsHandle);
                wallpaperWindows.Close();
                Wallpaper.Refresh();
            }
            sound = uiCheckBox1.Checked;
            bgm = uiCheckBox2.Checked;
            wallpaperform = new WallpaperForm();
            wallpaperplayer = wallpaperform.axWindowsMediaPlayer1;
            wallpaperplayer.URL = Path.Combine(assetsDirectory, Name, "video.mp4");
            Wallpaper.SetFather(wallpaperform);
            wallpaperform.Show();
            windowsHandle = wallpaperform.Handle;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }


        private void ControlForm_Load(object sender, EventArgs e)
        {
            InitializeBoot();
            InitialzeUI();
            InitializeIcon();
        }

        private void ControlForm_SizeChanged(object sender, EventArgs e)
        {
            InitialzeUI();
        }


        private void Header_Click(object? sender, EventArgs e)
        {
            if (sender is PictureBox header)
            {
                indexName = header.Name;
            }
            InitializeDescription();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            wallpaperLoad(indexName);
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            if (indexName == "") return;
            if (!Directory.Exists(Path.Combine(assetsDirectory, indexName))) return;
            Process.Start("explorer", Path.Combine(assetsDirectory, indexName));
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized) this.WindowState = FormWindowState.Maximized;
            else this.WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            if (windowsHandle != IntPtr.Zero)
            {
                Form wallpaperWindows = (Form)Control.FromHandle(windowsHandle);
                wallpaperWindows.Close();
                Wallpaper.Refresh();
            }
            Application.ExitThread();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("破晓壁纸\n作者：哀歌殇年\n版本：V1.0.0.0\nQQ：2690034441\n本软件完全免费，严禁用于商用", "关于");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolder, "英雄联盟壁纸.lnk");
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(assetsDirectory, indexName, "video.mp4"))) return;
            sound = uiCheckBox1.Checked;
            bgm = uiCheckBox2.Checked;
            Form preview = new PreviewForm();
            preview.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form install = new InstallForm();
            install.Show();
        }

        private void ControlForm_Activated(object sender, EventArgs e)
        {
            InitializeData();
        }

    }
}
