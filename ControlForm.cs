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
        
        /*****公共变量*****/
        
        //资源目录
        public static string assetsDirectory = Path.Combine(Application.StartupPath, "assets");
        //当前选中壁纸索引
        public static string indexName = "";
        //正在使用壁纸索引
        private string nowPlaying = "";
        //是否播放语音、背景音乐（仅非静谧模式有效）
        public static bool sound = true;
        public static bool bgm = true;
        //是否静谧模式
        public static bool quietMode = false;

        /*****正常模式窗口*****/
        private IntPtr windowsHandle = IntPtr.Zero;
        private WallpaperForm? wallpaperform;
        private AxWMPLib.AxWindowsMediaPlayer? wallpaperplayer;

        /*****静谧模式窗口*****/
        private QuietWallpaperForm main;
        private AxWMPLib.AxWindowsMediaPlayer player;


        public ControlForm()
        {
            InitializeComponent();
        }

        //UI更新函数：初始化窗口、调整窗口大小时调用
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

            if (indexName == nowPlaying) uiButton1.Text = "正在应用";
            else uiButton1.Text = "应用";
        }

        //数据更新函数：更新壁纸列表，在初始化窗口、重新激活窗口时调用
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

        //启动首位处理函数：启动程序时先运行的初始化函数
        private void InitializeBoot()
        {
            if (!Directory.Exists(assetsDirectory)) Directory.CreateDirectory(assetsDirectory);
        }

        /*****壁纸加载函数*****/

        //核心函数
        private void wallpaperLoad()
        {
            wallpaperExitNormal();
            if (quietMode)wallpaperLoadQuiet();
            else wallpaperLoadNormal();
            nowPlaying = indexName;
            uiButton1.Text = "正在应用";
        }

        //简要说明：正常模式切换壁纸时窗口加载一次退出一次，程序结束也退出；
        //          静谧模式窗口只有首次加载，切换壁纸只需要切换URL即可，只有程序结束时退出
       
        //壁纸加载函数：加载正常模式
        private void wallpaperLoadNormal()
        {
            if (!File.Exists(Path.Combine(assetsDirectory, indexName, "video.mp4"))) return;

            sound = uiCheckBox1.Checked;
            bgm = uiCheckBox2.Checked;
            wallpaperform = new WallpaperForm();
            wallpaperplayer = wallpaperform.axWindowsMediaPlayer1;
            wallpaperplayer.URL = Path.Combine(assetsDirectory, indexName, "video.mp4");
            Wallpaper.SetFather(wallpaperform);
            wallpaperform.Show();
            windowsHandle = wallpaperform.Handle;

            this.wallpaperform.TextChangedEvent += updateAudio;
        }
        //壁纸加载函数：退出正常模式
        private void wallpaperExitNormal()
        {
            if (windowsHandle != IntPtr.Zero)
            {
                Form wallpaperWindows = (Form)Control.FromHandle(windowsHandle);
                wallpaperWindows.Close();
                Wallpaper.Refresh();
            }
        }
        //壁纸加载函数：加载静谧模式
        private void wallpaperLoadQuiet()
        {
            if (main == null)
            {
                main = new QuietWallpaperForm();
                player = main.player;
                player.URL = Path.Combine(assetsDirectory, indexName, "video.mp4");
                Wallpaper.SetFather(main);
                main.Show();
            }
            player.URL = Path.Combine(assetsDirectory, indexName, "video.mp4");
        }
        //壁纸加载函数：退出静谧模式
        private void wallpaperExitQuiet()
        {
            if (quietMode && main != null)
            {
                main.Hide();
                this.Hide();
                Wallpaper.Refresh();
                main.Dispose();
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

        private void ControlForm_Load(object sender, EventArgs e)
        {
            InitializeBoot();
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
            wallpaperExitQuiet();
            Application.ExitThread();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("破晓壁纸\n作者：哀歌殇年\n版本：V1.0.1.0\nQQ：2690034441\n本软件完全免费，严禁用于商用", "关于");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolder, "破晓壁纸.lnk");
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

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (nowPlaying != "")
            {
                UIMessageTip.Show("请在未启用壁纸前选择\n如果已经启用壁纸，可以重启程序");
                return;
            }
            UIMessageTip.Delay = 1000;
            quietMode = !quietMode;
            if (quietMode)
            {
                UIMessageTip.ShowOk("已开启");
            }
            else
            {
                UIMessageTip.ShowError("已关闭");
            }
        }
    }
}
