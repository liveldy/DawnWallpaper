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
        //��ԴĿ¼
        public static string assetsDirectory = Path.Combine(Application.StartupPath, "assets");
        //��ǰѡ�б�ֽ����
        public static string indexName = "";
        public static string indexNameVideo = "";
        private List<string> indexNameList = new List<string>();
        //����ʹ�ñ�ֽ����
        private string nowPlaying = "";
        private string nowPlayingVideo = "";
        //�Ƿ񲥷���������������
        public static bool sound = true;
        public static bool bgm = true;
        //��ֽ����
        private IntPtr windowsHandle = IntPtr.Zero;
        private WallpaperForm? wallpaperform;
        private AxWindowsMediaPlayer? wallpaperplayer;

        public ControlForm()
        {
            InitializeComponent();
            InitializeBoot();
        }

        //UI���º�������ʼ�����ڡ��������ڴ�Сʱ����
        private void InitialzeUI()
        {
            Font font = new Font("����", this.Height / 70);
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

        //��Ϣ���º�����ѡ�б�ֽʱ�����Ҳ���Ϣ�͵�ǰѡ�б�ֽ����
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
            if (indexName == nowPlaying) uiButton1.Text = "����Ӧ��";
            else uiButton1.Text = "Ӧ��";
        }

        //���ݸ��º��������±�ֽ�б��ڳ�ʼ�����ڡ����¼����ʱ����
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

        //������λ����������������ʱ�����еĳ�ʼ������
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

        /*****��ֽ���غ���*****/

        //���ĺ���
        private void wallpaperLoad()
        {
            wallpaperExitNormal();
            wallpaperLoadNormal();
            nowPlaying = indexName;
            uiButton1.Text = "����Ӧ��";
        }

        //��ֽ���غ���
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
        //��ֽ�˳�����
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

        //��ߵ�������
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

        //������Ƶ����ί�к���
        private void updateAudio(string text)
        {
            notifyIcon1.Text = "������ֽ" + text;

            if (indexName == nowPlaying) uiButton1.Text = "����Ӧ��" + text;
            else uiButton1.Text = "Ӧ��";
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
                    UINotifier.Show("�ޱ�ֽ", UINotifierType.ERROR, "����");
                    base.WndProc(ref m);
                }
                if (indexNameList.IndexOf(indexName) == -1)
                {
                    UINotifier.Show("��ֽ��������", UINotifierType.ERROR, "����");
                    base.WndProc(ref m);
                }
                switch (m.WParam.ToInt32())
                {
                    case HotKey.HOTKEY_ID_LEFT:
                        if (indexNameList.IndexOf(indexName) == 0)
                        {
                            UINotifier.Show("�Ѿ��ǵ�һ�ű�ֽ", UINotifierType.INFO, "��ʾ");
                            break;
                        }
                        indexName = indexNameList.ToArray()[indexNameList.IndexOf(indexName) - 1];
                        wallpaperLoad();
                        UINotifier.Show("���л�����һ�ű�ֽ", UINotifierType.OK, "�ɹ�");
                        break;
                    case HotKey.HOTKEY_ID_RIGHT:
                        if (indexNameList.IndexOf(indexName) == indexNameList.Count - 1) 
                        {
                            UINotifier.Show("�Ѿ������һ�ű�ֽ", UINotifierType.INFO, "��ʾ");
                            break;
                        }
                        indexName = indexNameList.ToArray()[indexNameList.IndexOf(indexName) + 1];
                        wallpaperLoad();
                        UINotifier.Show("���л�����һ�ű�ֽ", UINotifierType.OK, "�ɹ�");
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
            UIMessageBox.Show(@"������ֽ
���ߣ���������
Github:https://github.com/liveldy

�汾��V1.0.5.0
���¹��棺
1.�л���ֽ��ݼ��������·���ʾ����
2.������ֽɾ������
3.������������
4.����ʱ�Զ�������һ�εı�ֽ

QQ��2690034441
            ", "����");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolder, "������ֽ.lnk");
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
