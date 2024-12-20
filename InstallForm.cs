﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Sunny.UI;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace DawnWallpaper
{
    public partial class InstallForm : UIForm
    {
        private UIEditForm ?configEdit;

        public InstallForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        private async void Install_Load(object sender, EventArgs e)
        {
            uiTextBox1.Text = "正在检查网络...";
            string host = "www.baidu.com";
            int timeout = 3000;
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply reply = ping.Send(host, timeout);
                    if (reply.Status == IPStatus.Success)
                    {
                        uiTextBox1.Text += "\r\n网络连接可用！";
                        if (!File.Exists(Path.Combine(Application.StartupPath, "GETHandle.exe")))
                        {
                            uiTextBox1.Text += "\r\n初始化环境...";
                            try
                            {
                                await DownloadFileAsync("https://oss.agsn.site/DawnWallpaper/GETHandle.exe", Path.Combine(Application.StartupPath, "GETHandle.exe"));
                                Wallpaper.GETHandleRun(this);
                                uiTextBox1.Text += "\r\n环境初始化成功！";
                            }
                            catch
                            {
                                uiTextBox1.Text += "\r\n环境初始化失败！";
                            }
                        }
                        if (!File.Exists(Path.Combine(Application.StartupPath, "SRCConfig.ini")))
                        {
                            uiTextBox1.Text += "\r\n未进行订阅配置！";
                            return;
                        }
                        else
                        {
                            IniFile SRCConfig = new IniFile(Path.Combine(Application.StartupPath, "SRCConfig.ini"));
                            string SRCName = SRCConfig.ReadString("main", "SRCName", "");
                            string SRCUrl = SRCConfig.ReadString("main", "SRCUrl", "");
                            uiTextBox1.Text += "\r\n订阅配置读取完成！";
                            uiTextBox1.Text += "\r\n订阅主题名称：" + SRCName;
                            uiTextBox1.Text += "\r\n订阅URL：" + SRCUrl;
                            uiTextBox1.Text += "\r\n获取下载列表...";
                            string url = SRCUrl + "list.ini";
                            string savePath = Path.Combine(Application.StartupPath, "list.ini");
                            try
                            {
                                await DownloadFileAsync(url, savePath);
                                IniFile listIni = new IniFile(savePath);
                                foreach (string section in listIni.Sections)
                                {
                                    uiListBox1.Items.Add(listIni.ReadString(section, "name", ""));
                                }
                                uiTextBox1.Text += "\r\n列表获取成功！";
                            }
                            catch
                            {
                                uiTextBox1.Text += "\r\n列表获取失败！";
                            }
                        }
                    }
                    else
                    {
                        uiTextBox1.Text += "\r\n网络连接不可用！";
                    }
                }
                catch
                {
                    uiTextBox1.Text += "\r\n网络连接不可用！";
                    this.Enabled = true;
                }
            }



        }
        async Task DownloadFileAsync(string url, string destinationPath)
        {
            uiButton1.Enabled = false;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            HttpClient httpClient = new HttpClient(handler);
            using (var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                var startTime = DateTime.Now;

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                {
                    var buffer = new byte[8192];
                    long totalRead = 0;
                    int read;

                    while ((read = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, read);
                        totalRead += read;

                        if (totalBytes != -1)
                        {
                            int progressPercentage = (int)((totalRead * 100L) / totalBytes);
                            uiProcessBar1.Value = progressPercentage;
                        }

                        var elapsedTime = DateTime.Now - startTime;
                        double downloadSpeed = totalRead / elapsedTime.TotalSeconds;
                        string speedText = $"{(downloadSpeed / 1024 / 1024):F2} MB/s";

                        string downloadedSizeText = $"{totalRead / 1024 / 1024} MB";
                        string totalSizeText = totalBytes != -1 ? $"{totalBytes / 1024 / 1024} MB" : "Unknown";
                        uiLabel1.Text = $"{downloadedSizeText}/{totalSizeText}({speedText})";
                    }
                }
            }
            uiLabel1.Text = "";
            uiProcessBar1.Value = 0;
            uiButton1.Enabled = true;
        }

        private void InitializeUI()
        {
            uiListBox1.Location = new Point(20, 50);
            uiListBox1.Size = new Size(this.Width / 2, this.Height - 70);
            uiTextBox1.Location = new Point(this.Width / 2 + 40, 50);
            uiTextBox1.Size = new Size(this.Width / 2 - 60, this.Height / 5 * 3);
            uiLabel1.Location = new Point(this.Width / 2 + 40, 50 + this.Height / 5 * 3 + 10);
            uiLabel1.Size = new Size(this.Width / 2 - 60, this.Height / 3 * 2 - this.Height / 5 * 3 - 10);
            int widthOut = this.Height - 50 - this.Height / 3 * 2 - 20;
            uiProcessBar1.Location = new Point(this.Width / 2 + 40, 50 + this.Height / 3 * 2 + 20);
            uiProcessBar1.Size = new Size(this.Width / 2 - 60, widthOut / 2 - 30);
            uiButton1.Location = new Point(this.Width / 2 + 40, this.Height / 3 * 2 + 20 + widthOut / 2 + 40);
            uiButton1.Size = new Size(this.Width / 4 - 20, widthOut / 2 - 10);
            uiButton2.Location = new Point(this.Width / 4 * 3 + 40, this.Height / 3 * 2 + 20 + widthOut / 2 + 40);
            uiButton2.Size = new Size(this.Width / 4 - 60, widthOut / 2 - 10);
        }

        private void Install_SizeChanged(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private async void uiButton1_Click(object sender, EventArgs e)
        {
            if (uiListBox1.Items.Count == 0 || uiListBox1.SelectedIndex == -1) return;
            IniFile listIni = new IniFile(Path.Combine(Application.StartupPath, "list.ini"));
            foreach (string section in listIni.Sections)
            {
                if (uiListBox1.SelectedItem.ToString() == listIni.ReadString(section, "name", ""))
                {
                    uiTextBox1.Text += "\r\n" + listIni.ReadString(section, "name", "") + " 正在下载...";
                    await DownloadFileAsync(listIni.ReadString(section, "url", ""), Path.Combine(Application.StartupPath, "assets", section + ".w"));
                    uiTextBox1.Text += "\r\n" + listIni.ReadString(section, "name", "") + " 下载完成！";
                    uiTextBox1.Text += "\r\n" + listIni.ReadString(section, "name", "") + " 正在安装...";
                    ZipFile.ExtractToDirectory(Path.Combine(Application.StartupPath, "assets", section + ".w"), Path.Combine(Application.StartupPath, "assets"));
                    File.Delete(Path.Combine(Application.StartupPath, "assets", section + ".w"));
                    uiTextBox1.Text += "\r\n" + listIni.ReadString(section, "name", "") + " 安装完成！";
                }
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            uiTextBox1.Text += "\r\n正在打开文件...";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择DawnWallpaper安装包";
            openFileDialog.Filter = "wallpaper package (*.w)|*.w|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                uiTextBox1.Text += "\r\n正在安装...";
                ZipFile.ExtractToDirectory(filePath, Path.Combine(Application.StartupPath, "assets"));
                uiTextBox1.Text += "\r\n安装完成！";
            }
            else
            {
                uiTextBox1.Text += "\r\n取消选择";
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConfigEditForm();
        }

        private void ConfigEditForm()
        {
            configEdit = new UIEditForm();
            configEdit.Text = "配置编辑";
            configEdit.Size = new Size(600, 250);
            configEdit.ButtonOkClick += ConfigEdit_ButtonOkClick;
            configEdit.ButtonCancelClick += ConfigEdit_ButtonCancelClick;

            UITextBox SRCName = new UITextBox();
            SRCName.Name = "SRCName";
            SRCName.Location = new Point(30, 60);
            SRCName.Size = new Size(540, 30);
            SRCName.Watermark = "订阅主题名称";

            UITextBox SRCUrl = new UITextBox();
            SRCUrl.Name = "SRCUrl";
            SRCUrl.Location = new Point(30, 120);
            SRCUrl.Size = new Size(540, 30);
            SRCUrl.Watermark = "订阅URL";


            if (File.Exists(Path.Combine(Application.StartupPath, "SRCConfig.ini")))
            {
                IniFile SRCConfig = new IniFile(Path.Combine(Application.StartupPath, "SRCConfig.ini"));
                SRCName.Text = SRCConfig.ReadString("main", "SRCName", "");
                SRCUrl.Text = SRCConfig.ReadString("main", "SRCUrl", "");
            }

            configEdit.Controls.Add(SRCName);
            configEdit.Controls.Add(SRCUrl);

            configEdit.ShowDialog();
        }

        private void ConfigEdit_ButtonOkClick(object? sender, EventArgs e)
        {
            if (configEdit == null) return;
            string SRCName = configEdit.Controls["SRCName"].Text;
            string SRCUrl = configEdit.Controls["SRCUrl"].Text;
            if (SRCName == "" || SRCUrl == "")
            {
                UIMessageBox.Show("存在空项！");
                configEdit.Close();
                return;
            }
            IniFile SRCConfig = new IniFile(Path.Combine(Application.StartupPath, "SRCConfig.ini"));
            SRCConfig.Write("main", "SRCName", SRCName);
            SRCConfig.Write("main", "SRCUrl", SRCUrl);

            uiTextBox1.Text += "\r\n已保存配置！";
            configEdit.Close();
            this.Close();
        }
        private void ConfigEdit_ButtonCancelClick(object? sender, EventArgs e)
        {
            if (configEdit == null) return;
            uiTextBox1.Text += "\r\n取消保存配置";
            configEdit.Close();
        }
    }
}
