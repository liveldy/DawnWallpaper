using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using Sunny.UI;
using AxWMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.IO;

namespace DawnWallpaper
{
    public partial class ControlForm : UIForm
    {
        //资源目录
        public static string assetsDirectory = Path.Combine(Application.StartupPath, "assets");
        //当前选中壁纸索引
        public static string indexName = "";
        public static string indexNameVideo = "";
        private List<string> indexNameList = new List<string>();
        //正在使用壁纸索引
        private string nowPlaying = "";
        private string nowPlayingVideo = "";
        //是否播放语音、背景音乐
        public static bool sound = true;
        public static bool bgm = true;
        //壁纸窗口
        private IntPtr windowsHandle = IntPtr.Zero;
        private WallpaperForm? wallpaperform;
        private AxWindowsMediaPlayer? wallpaperplayer;

        public ControlForm()
        {
            InitializeComponent();
            InitializeBoot();
        }

        //UI更新函数：初始化窗口、调整窗口大小时调用
        private void InitialzeUI()
        {
            Font font = new Font("宋体", this.Height / 70);
            uiLabel1.Font = font;
            uiCheckBox1.Font = font;
            uiCheckBox2.Font = font;
            uiComboBox1.Font = font;
            uiButton1.Font = font;
            uiButton2.Font = font;
            uiButton3.Font = font;

            uiFlowLayoutPanel1.Location = new Point(20, 70);
            uiFlowLayoutPanel1.Size = new Size(this.Width / 3 * 2, this.Height - 70 - 20);
            uiTitlePanel1.Location = new Point(this.Width / 3 * 2 + 40, 70);
            uiTitlePanel1.Size = new Size(this.Width - (this.Width / 3 * 2 + 40) - 20, this.Height - 70 - 20);
            pictureBox1.Location = new Point(uiTitlePanel1.Width / 4, uiTitlePanel1.Height / 8);
            pictureBox1.Size = new Size(uiTitlePanel1.Width / 4 * 2, uiTitlePanel1.Width / 4 * 2);
            uiLabel1.Location = new Point(uiTitlePanel1.Width / 16, uiTitlePanel1.Height / 8 + uiTitlePanel1.Width / 8 * 5);
            uiLabel1.Size = new Size(uiTitlePanel1.Width / 8 * 7, pictureBox1.Height / 2);
            uiCheckBox1.Location = new Point(uiTitlePanel1.Width / 4, uiTitlePanel1.Height / 8 + uiTitlePanel1.Width / 8 * 7);
            uiCheckBox2.Location = new Point(uiTitlePanel1.Width / 4, uiTitlePanel1.Height / 8 + uiTitlePanel1.Width / 8 * 7 + 50);
            uiComboBox1.Location = new Point(uiTitlePanel1.Width / 4, uiTitlePanel1.Height / 8 + uiTitlePanel1.Width / 8 * 7 + 100);
            uiButton1.Location = new Point(uiTitlePanel1.Width / 10, uiTitlePanel1.Height / 10 * 8);
            uiButton1.Size = new Size(uiTitlePanel1.Width / 10 * 8, uiTitlePanel1.Height / 30 * 2);
            uiButton2.Location = new Point(uiTitlePanel1.Width / 10, uiTitlePanel1.Height / 10 * 9);
            uiButton2.Size = new Size(uiTitlePanel1.Width / 20 * 7, uiTitlePanel1.Height / 30 * 2);
            uiButton3.Location = new Point(uiTitlePanel1.Width / 20 * 11, uiTitlePanel1.Height / 10 * 9);
            uiButton3.Size = new Size(uiTitlePanel1.Width / 20 * 7, uiTitlePanel1.Height / 30 * 2);
            uiFlowLayoutPanel1.Clear();
            InitializeData();
        }

        //信息更新函数：选中壁纸时更新右侧信息和当前选中壁纸索引
        private void InitializeDescription()
        {
            IniFile configini = new IniFile(Path.Combine(assetsDirectory, indexName, "config.ini"));
            pictureBox1.ImageLocation = Path.Combine(assetsDirectory, indexName, "header.png");
            uiLabel1.Text = configini.ReadString("main", "name", "") + "\n"
                + configini.ReadString("main", "version", "") + "\n"
                + configini.ReadString("main", "author", "") + "\n"
                + configini.ReadString("main", "size", "") + "\n";

            uiComboBox1.Items.Clear();
            string videoDirectory = Path.Combine(assetsDirectory, indexName, "videos");
            if (Directory.Exists(videoDirectory))
            {
                DirectoryInfo srcPath = new DirectoryInfo(videoDirectory);
                foreach (FileInfo subFile in srcPath.GetFiles())
                {
                    uiComboBox1.Items.Add(Path.GetFileName(subFile.Name));
                    if (nowPlayingVideo != "") uiComboBox1.SelectedItem = nowPlayingVideo;
                    else uiComboBox1.SelectedIndex = 0;
                }
            }
            if (indexName == nowPlaying) uiButton1.Text = "正在应用";
            else uiButton1.Text = "应用";
        }

        //数据更新函数：更新壁纸列表，在初始化窗口、重新激活窗口时调用
        private void InitializeData()
        {
            uiFlowLayoutPanel1.Clear();
            DirectoryInfo srcPath = new DirectoryInfo(assetsDirectory);
            if (srcPath.GetDirectories().Length == 0) return;
            List<string> types = new List<string>();
            foreach (DirectoryInfo subDirectory in srcPath.GetDirectories())
            {
                int count = 0;
                IniFile iniPackage = new IniFile(Path.Combine(subDirectory.FullName, "config.ini"));
                if (types.Count == 0) types.Add(iniPackage.ReadString("main", "type", ""));
                foreach (string type in types.ToArray())
                {
                    if (iniPackage.ReadString("main", "type", "") == type) break;
                    if (types.Count - 1 == count) types.Add(iniPackage.ReadString("main", "type", ""));
                    count++;
                }
            }
            for (int i = 0; i < types.Count; i++)
            {
                UILine typetax = new UILine();
                typetax.Text = types.ToArray()[i];
                typetax.Size = new Size(uiFlowLayoutPanel1.Width - 10, typetax.Height);
                uiFlowLayoutPanel1.Add(typetax);
                foreach (DirectoryInfo subDirectory in srcPath.GetDirectories())
                {
                    IniFile iniPackage = new IniFile(Path.Combine(subDirectory.FullName, "config.ini"));
                    if (types.ToArray()[i] == iniPackage.ReadString("main", "type", ""))
                    {
                        PictureBox header = new PictureBox();
                        header.Height = uiFlowLayoutPanel1.Width / 5 - 7;
                        header.Width = uiFlowLayoutPanel1.Width / 5 - 7;
                        header.SizeMode = PictureBoxSizeMode.StretchImage;
                        header.Name = Path.GetFileName(subDirectory.Name);
                        header.ImageLocation = Path.Combine(subDirectory.FullName, "header.png");
                        header.InitialImage = this.IconImage;
                        header.Click += Header_Click;
                        uiFlowLayoutPanel1.Add(header);
                    }
                }
            }
        }

        //启动首位处理函数：启动程序时先运行的初始化函数
        private void InitializeBoot()
        {
            if (!Directory.Exists(assetsDirectory)) Directory.CreateDirectory(assetsDirectory);
            DirectoryInfo srcPath = new DirectoryInfo(assetsDirectory);
            foreach (FileInfo subFile in srcPath.GetFiles())
            {
                File.Delete(subFile.FullName);
            }
            Wallpaper.GETHandleRun(this);

            HotKey.KeySet(this.Handle);
            if (srcPath.GetDirectories().Length != 0)
            {
                foreach (DirectoryInfo subDirectory in srcPath.GetDirectories())
                {
                    IniFile iniPackage = new IniFile(Path.Combine(subDirectory.FullName, "config.ini"));
                    indexNameList.Add(Path.GetFileName(subDirectory.Name));
                }
            }
            uiComboBox1.SelectedIndex = 0;
        }

        /*****壁纸加载函数*****/

        //核心函数
        private void wallpaperLoad()
        {
            wallpaperExitNormal();
            wallpaperLoadNormal();
            nowPlaying = indexName;
            uiButton1.Text = "正在应用";
        }

        //壁纸加载函数
        private void wallpaperLoadNormal()
        {
            if (!File.Exists(Path.Combine(assetsDirectory, indexName, "videos", uiComboBox1.SelectedText))) return;
            sound = uiCheckBox1.Checked;
            bgm = uiCheckBox2.Checked;
            wallpaperform = new WallpaperForm();
            wallpaperplayer = wallpaperform.axWindowsMediaPlayer1;
            wallpaperplayer.URL = Path.Combine(assetsDirectory, indexName, "videos", uiComboBox1.SelectedText);
            wallpaperform.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            wallpaperCorrect(wallpaperplayer);
            Wallpaper.SetFather(wallpaperform);
            wallpaperform.Show();
            windowsHandle = wallpaperform.Handle;

            this.wallpaperform.TextChangedEvent += updateAudio;
        }
        //壁纸退出函数
        private void wallpaperExitNormal()
        {
            if (windowsHandle != IntPtr.Zero)
            {
#pragma warning disable CS8600
                Form wallpaperWindows = (Form)Control.FromHandle(windowsHandle);
#pragma warning restore CS8600
                if (wallpaperWindows != null) wallpaperWindows.Close();
                Wallpaper.Refresh();
            }
        }

        //宽高调整函数
        private void wallpaperCorrect(AxWindowsMediaPlayer player)
        {
            float screenAspectRatio = (float)Screen.PrimaryScreen.Bounds.Width / Screen.PrimaryScreen.Bounds.Height;
            float videoAspectRatio = 16f / 9f;

            if (screenAspectRatio > videoAspectRatio)
            {
                int newHeight = (int)(player.Width / videoAspectRatio);
                player.Height = newHeight;
            }
            else
            {
                int newWidth = (int)(player.Height * videoAspectRatio);
                player.Width = newWidth;
            }
        }

        private void AutoBootIniSet()
        {
            IniFile autoboot = new IniFile(Path.Combine(Application.StartupPath, "boot.ini"));
            autoboot.Write("main", "indexName", indexName);
            autoboot.Write("main", "indexNameVideo", indexNameVideo);
        }
        private void AutoBootIniGet()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "boot.ini")))
            {
                IniFile autoboot = new IniFile(Path.Combine(Application.StartupPath, "boot.ini"));
                indexName = autoboot.ReadString("main", "indexName", "");
                indexNameVideo = autoboot.ReadString("main", "indexNameVideo", "");
                wallpaperLoad();
            }
        }

        //更新音频进度委托函数
        private void updateAudio(string text)
        {
            notifyIcon1.Text = "破晓壁纸" + text;

            if (indexName == nowPlaying) uiButton1.Text = "正在应用" + text;
            else uiButton1.Text = "应用";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        protected override void WndProc(ref Message m)
        {
            UINotifier.CloseAll();
            const int WM_HOTKEY = 0x0312;
            if (indexName == "")
            {
                AutoBootIniGet();
                base.WndProc(ref m);
            }
            InitializeDescription();
            uiComboBox1.SelectedIndex = 0;
            indexNameVideo = uiComboBox1.SelectedText;
            if (m.Msg == WM_HOTKEY)
            {
                if (indexNameList.Count == 0)
                {
                    UINotifier.Show("无壁纸", UINotifierType.ERROR, "错误");
                    base.WndProc(ref m);
                }
                if (indexNameList.IndexOf(indexName) == -1)
                {
                    UINotifier.Show("壁纸索引错误", UINotifierType.ERROR, "错误");
                    base.WndProc(ref m);
                }
                switch (m.WParam.ToInt32())
                {
                    case HotKey.HOTKEY_ID_LEFT:
                        if (indexNameList.IndexOf(indexName) == 0)
                        {
                            UINotifier.Show("已经是第一张壁纸", UINotifierType.INFO, "提示");
                            break;
                        }
                        indexName = indexNameList.ToArray()[indexNameList.IndexOf(indexName) - 1];
                        wallpaperLoad();
                        UINotifier.Show("已切换到上一张壁纸", UINotifierType.OK, "成功");
                        break;
                    case HotKey.HOTKEY_ID_RIGHT:
                        if (indexNameList.IndexOf(indexName) == indexNameList.Count - 1) 
                        {
                            UINotifier.Show("已经是最后一张壁纸", UINotifierType.INFO, "提示");
                            break;
                        }
                        indexName = indexNameList.ToArray()[indexNameList.IndexOf(indexName) + 1];
                        wallpaperLoad();
                        UINotifier.Show("已切换到下一张壁纸", UINotifierType.OK, "成功");
                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void ControlForm_Load(object sender, EventArgs e)
        {
            InitialzeUI();
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
            wallpaperLoad();
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
            wallpaperExitNormal();
            HotKey.KeyOut(this.Handle);
            AutoBootIniSet();
            Application.ExitThread();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            UIMessageBox.Show(@"破晓壁纸
作者：哀歌殇年
Github:https://github.com/liveldy

版本：V1.0.5.0
更新公告：
1.切换壁纸快捷键并在右下方提示窗口
2.新增壁纸删除功能
3.新增屏保功能
4.启动时自动播放上一次的壁纸

QQ：2690034441
            ", "关于");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolder, "破晓壁纸.lnk");
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(assetsDirectory, indexName, "videos", uiComboBox1.SelectedText))) return;
            sound = uiCheckBox1.Checked;
            bgm = uiCheckBox2.Checked;
            Form preview = new PreviewForm();
            preview.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form install = new InstallForm();
            install.ShowDialog();
        }

        private void ControlForm_Activated(object sender, EventArgs e)
        {
            InitializeData();
        }

        private void uiComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexNameVideo = uiComboBox1.SelectedText;
        }
    }
}
