using DawnWallpaper.Common;
using Newtonsoft.Json;
using Sunny.UI;
using System.Diagnostics;
using System.IO;
using static DawnWallpaper.Common.FormCtrl;
using static DawnWallpaper.Common.JsonModel;
namespace DawnWallpaper
{
    public partial class MainForm : UIForm
    {
        public static string? VideoLocation;
        public static bool VideoBGM = true;

        private WallpaperForm? wallpaperform;

        private int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
        private int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;
        private bool videocombo = false;

        private string? AssetsPath, ItemPath;

        public MainForm()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeUI()
        {
            MainFlowLayoutPanel.Location = new Point(4, 40);
            MainFlowLayoutPanel.Size = new Size(this.Width / 3 * 2, this.Height - 45);
            MainTitlePanel.Location = new Point(4 + this.Width / 3 * 2 + 10, 40);
            MainTitlePanel.Size = new Size(this.Width / 3 - 20, this.Height - 45);
            MainPictureBox.Location = new Point(MainTitlePanel.Width / 3, MainTitlePanel.Height / 6);
            MainPictureBox.Size = new Size(MainTitlePanel.Width / 3, MainTitlePanel.Width / 3);
            MainLabel.Location = new Point(10, MainTitlePanel.Height / 6 + MainTitlePanel.Width / 3 + MainTitlePanel.Height / 12);
            MainLabel.Size = new Size(MainTitlePanel.Width - 20, MainTitlePanel.Height / 6);
            MainComboBox.Location = new Point(MainTitlePanel.Width / 5, MainTitlePanel.Height / 3 + MainTitlePanel.Width / 3 + MainTitlePanel.Height / 6);
            MainComboBox.Size = new Size(MainTitlePanel.Width / 5 * 3, MainComboBox.Height);
            MainCheckBox.Location = new Point(MainTitlePanel.Width / 5, MainTitlePanel.Height / 3 + MainTitlePanel.Width / 3 + MainTitlePanel.Height / 6 + MainComboBox.Height);
            MainCheckBox.Size = new Size(MainTitlePanel.Width / 5 * 3, MainCheckBox.Height);
            MainViewButton.Location = new Point(MainTitlePanel.Width / 21, MainTitlePanel.Height - MainViewButton.Height - 10);
            MainViewButton.Size = new Size(MainTitlePanel.Width / 21 * 9, MainViewButton.Height);
            MainOpenButton.Location = new Point(MainTitlePanel.Width / 21 * 11, MainTitlePanel.Height - MainViewButton.Height - 10);
            MainOpenButton.Size = new Size(MainTitlePanel.Width / 21 * 9, MainViewButton.Height);
            MainApplyButton.Location = new Point(MainTitlePanel.Width / 21, MainTitlePanel.Height - MainViewButton.Height - MainApplyButton.Height - 20);
            MainApplyButton.Size = new Size(MainTitlePanel.Width / 21 * 19, MainApplyButton.Height);
        }

        private void InitializeData()
        {
            if(!Directory.Exists(Path.Combine(Application.StartupPath, "assets")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "assets"));
            }
            DirectoryInfo assetsdir = new DirectoryInfo(Path.Combine(Application.StartupPath, "assets"));
            foreach (DirectoryInfo wallpaperdir in assetsdir.GetDirectories())
            {
                IniFile Info = new IniFile(Path.Combine(wallpaperdir.FullName, "data.ini"));
                if (Info == null || Info.ReadString("main", "name", "") == "") continue;
                STAStripComboBox.Items.Add(Info.ReadString("main", "name", ""));
                STAStripComboBox.SelectedIndex = 0;
            }
            FormCtrl.SendMsgToProgman();
        }

        private void InitializeItem()
        {
            MainFlowLayoutPanel.Clear();
            int ItemCountWidth = MainFlowLayoutPanel.Width / (ScreenWidth / 15);
            if (ItemCountWidth == 0) return;
            DirectoryInfo ItemInfo = new DirectoryInfo(Path.Combine(AssetsPath));
            foreach(DirectoryInfo ItemData in ItemInfo.GetDirectories())
            {
                PictureBox Item = new PictureBox();
                Item.Text = ItemData.Name;
                Item.Size = new Size(MainFlowLayoutPanel.Width / ItemCountWidth - ItemCountWidth * 3, MainFlowLayoutPanel.Width / ItemCountWidth - ItemCountWidth * 3);
                Item.SizeMode = PictureBoxSizeMode.StretchImage;
                Item.ImageLocation = Path.Combine(ItemData.FullName, "icon.jpg");
                Item.Click += Item_Click;
                MainFlowLayoutPanel.Add(Item);
            }
        }

        private void Item_Click(object? sender, EventArgs e)
        {
            if (sender is PictureBox Item)
            {
                videocombo = false;
                MainPictureBox.ImageLocation = Item.ImageLocation;
                DirectoryInfo ItemInfo = new DirectoryInfo(Path.Combine(AssetsPath));
                foreach (DirectoryInfo ItemData in ItemInfo.GetDirectories())
                {
                    if (ItemData.Name == Item.Text)
                    {
                        JsonModel.Root InfoJson = JsonConvert.DeserializeObject<JsonModel.Root>(File.ReadAllText(Path.Combine(ItemData.FullName, "data.json")));
                        MainLabel.Text = InfoJson.Data.Name;
                        MainComboBox.DataSource = InfoJson.Data.Video;
                        ItemPath = ItemData.FullName;
                        MainComboBox.DisplayMember = "Name";
                        MainComboBox.ValueMember = "Id";
                    }
                }
                videocombo = true;
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            InitializeUI();
            if (AssetsPath != null) InitializeItem();
        }

        private void MainApplyButton_Click(object sender, EventArgs e)
        {
            if (MainComboBox.Items.Count == 0) return;
            if (wallpaperform != null)
            {
                wallpaperform.Close();
                wallpaperform.Dispose();
                wallpaperform = null;
            }
            wallpaperform = new WallpaperForm(Path.Combine(ItemPath, MainComboBox.SelectedValue + ".mp4"), MainCheckBox.Checked);
            wallpaperform.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            wallpaperform.Show();
            Win32Func.SetParent(wallpaperform.Handle, programHandle);
            FormCtrl.SendMsgToProgman();
        }

        private void MainComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!videocombo) return;
            FileInfo videofile = new FileInfo(Path.Combine(ItemPath, MainComboBox.SelectedValue + ".mp4"));
            if (videofile.Exists)
            {
                MainLabel.Text = MainComboBox.SelectedText + "\n" + Math.Round(((decimal)videofile.Length / 1024 / 1024), 2, MidpointRounding.AwayFromZero) + " MB";
            }
            else
            {
                MainLabel.Text = MainComboBox.SelectedText;
            }
        }

        private void MainViewButton_Click(object sender, EventArgs e)
        {
            if (MainComboBox.Items.Count == 0) return;
            WallpaperForm viewform = new WallpaperForm(Path.Combine(ItemPath, MainComboBox.SelectedValue + ".mp4"), MainCheckBox.Checked);
            viewform.FormBorderStyle = FormBorderStyle.Sizable;
            viewform.Text = MainComboBox.Text;
            viewform.Show();
        }

        private void MainOpenButton_Click(object sender, EventArgs e)
        {
            if (MainComboBox.Items.Count == 0) return;
            Process.Start("explorer.exe", $"/select, \"{Path.Combine(ItemPath, MainComboBox.SelectedValue + ".mp4")}\"");
        }

        private void STAStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DirectoryInfo assetsdir = new DirectoryInfo(Path.Combine(Application.StartupPath, "assets"));
            foreach (DirectoryInfo wallpaperdir in assetsdir.GetDirectories())
            {
                IniFile Info = new IniFile(Path.Combine(wallpaperdir.FullName, "data.ini"));
                if (Info == null || Info.ReadString("main", "name", "") == "") continue;
                if (Info.ReadString("main", "name", "") == STAStripComboBox.SelectedItem.ToString()) 
                {
                    AssetsPath = wallpaperdir.FullName;
                    InitializeItem();
                    MainPictureBox.Image = this.IconImage;
                    MainLabel.Text = "信息";
                    videocombo = false;
                    MainComboBox.Clear();
                    videocombo = true;
                }
            }
        }

        private void STAStripMenuItem_Click(object sender, EventArgs e)
        {
            string dawnserverpath = Path.Combine(Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName, "DawnServer.exe");
            if (File.Exists(dawnserverpath))
            {
                IniFile Info = new IniFile(Path.Combine(AssetsPath, "data.ini"));
                Process.Start(dawnserverpath, $"-sta DawnWallpaper {Info.ReadString("main", "url", "")}");
            }
            else
            {
                UIMessageBox.ShowInfo("当前应用没有STA服务，如需获取，请前往agsn.site下载破晓应用中心");
            }
        }

        private void AboutStripMenuItem_Click(object sender, EventArgs e)
        {
            UIMessageBox.Show("DawnWallpaper 破晓壁纸 V1.1.0.0 20250216\n" +
                "Copyright 2025 All Rights Reserved. 哀歌殇年 版权所有\n" +
                "官网: https://agsn.site/\n" +
                "作者QQ：2690034441\n" +
                "更新内容：\n" +
                "1.全新构建，移除了多余的功能\n" +
                "2.开始支持STA服务", "关于");
        }
    }
}
