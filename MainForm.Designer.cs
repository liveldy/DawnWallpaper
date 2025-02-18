namespace DawnWallpaper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MainFlowLayoutPanel = new Sunny.UI.UIFlowLayoutPanel();
            MainTitlePanel = new Sunny.UI.UITitlePanel();
            MainOpenButton = new Sunny.UI.UIButton();
            MainViewButton = new Sunny.UI.UIButton();
            MainApplyButton = new Sunny.UI.UIButton();
            MainCheckBox = new Sunny.UI.UICheckBox();
            MainComboBox = new Sunny.UI.UIComboBox();
            MainLabel = new Sunny.UI.UILabel();
            MainPictureBox = new PictureBox();
            MainContextMenuStrip = new Sunny.UI.UIContextMenuStrip();
            STAStripMenuItem = new ToolStripMenuItem();
            STAStripComboBox = new ToolStripComboBox();
            AboutStripMenuItem = new ToolStripMenuItem();
            MainTitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MainPictureBox).BeginInit();
            MainContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MainFlowLayoutPanel
            // 
            MainFlowLayoutPanel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainFlowLayoutPanel.Location = new Point(4, 40);
            MainFlowLayoutPanel.Margin = new Padding(4, 5, 4, 5);
            MainFlowLayoutPanel.MinimumSize = new Size(1, 1);
            MainFlowLayoutPanel.Name = "MainFlowLayoutPanel";
            MainFlowLayoutPanel.Padding = new Padding(2);
            MainFlowLayoutPanel.ShowText = false;
            MainFlowLayoutPanel.Size = new Size(958, 755);
            MainFlowLayoutPanel.TabIndex = 0;
            MainFlowLayoutPanel.Text = "uiFlowLayoutPanel1";
            MainFlowLayoutPanel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // MainTitlePanel
            // 
            MainTitlePanel.Controls.Add(MainOpenButton);
            MainTitlePanel.Controls.Add(MainViewButton);
            MainTitlePanel.Controls.Add(MainApplyButton);
            MainTitlePanel.Controls.Add(MainCheckBox);
            MainTitlePanel.Controls.Add(MainComboBox);
            MainTitlePanel.Controls.Add(MainLabel);
            MainTitlePanel.Controls.Add(MainPictureBox);
            MainTitlePanel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainTitlePanel.Location = new Point(980, 40);
            MainTitlePanel.Margin = new Padding(4, 5, 4, 5);
            MainTitlePanel.MinimumSize = new Size(1, 1);
            MainTitlePanel.Name = "MainTitlePanel";
            MainTitlePanel.Padding = new Padding(1, 35, 1, 1);
            MainTitlePanel.ShowText = false;
            MainTitlePanel.Size = new Size(405, 755);
            MainTitlePanel.TabIndex = 1;
            MainTitlePanel.Text = "属性";
            MainTitlePanel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // MainOpenButton
            // 
            MainOpenButton.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainOpenButton.Location = new Point(233, 699);
            MainOpenButton.MinimumSize = new Size(1, 1);
            MainOpenButton.Name = "MainOpenButton";
            MainOpenButton.Size = new Size(150, 52);
            MainOpenButton.TabIndex = 6;
            MainOpenButton.Text = "打开文件夹";
            MainOpenButton.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainOpenButton.Click += MainOpenButton_Click;
            // 
            // MainViewButton
            // 
            MainViewButton.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainViewButton.Location = new Point(19, 699);
            MainViewButton.MinimumSize = new Size(1, 1);
            MainViewButton.Name = "MainViewButton";
            MainViewButton.Size = new Size(150, 52);
            MainViewButton.TabIndex = 5;
            MainViewButton.Text = "预览";
            MainViewButton.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainViewButton.Click += MainViewButton_Click;
            // 
            // MainApplyButton
            // 
            MainApplyButton.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainApplyButton.Location = new Point(19, 625);
            MainApplyButton.MinimumSize = new Size(1, 1);
            MainApplyButton.Name = "MainApplyButton";
            MainApplyButton.Size = new Size(364, 52);
            MainApplyButton.TabIndex = 4;
            MainApplyButton.Text = "应用壁纸";
            MainApplyButton.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainApplyButton.Click += MainApplyButton_Click;
            // 
            // MainCheckBox
            // 
            MainCheckBox.Checked = true;
            MainCheckBox.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainCheckBox.ForeColor = Color.FromArgb(48, 48, 48);
            MainCheckBox.Location = new Point(103, 575);
            MainCheckBox.MinimumSize = new Size(1, 1);
            MainCheckBox.Name = "MainCheckBox";
            MainCheckBox.Size = new Size(225, 44);
            MainCheckBox.TabIndex = 3;
            MainCheckBox.Text = "背景音乐";
            // 
            // MainComboBox
            // 
            MainComboBox.DataSource = null;
            MainComboBox.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            MainComboBox.FillColor = Color.White;
            MainComboBox.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainComboBox.ItemHoverColor = Color.FromArgb(155, 200, 255);
            MainComboBox.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            MainComboBox.Location = new Point(103, 505);
            MainComboBox.Margin = new Padding(4, 5, 4, 5);
            MainComboBox.MinimumSize = new Size(63, 0);
            MainComboBox.Name = "MainComboBox";
            MainComboBox.Padding = new Padding(0, 0, 30, 2);
            MainComboBox.Size = new Size(225, 44);
            MainComboBox.SymbolSize = 24;
            MainComboBox.TabIndex = 2;
            MainComboBox.Text = "壁纸";
            MainComboBox.TextAlignment = ContentAlignment.MiddleLeft;
            MainComboBox.Watermark = "";
            MainComboBox.SelectedIndexChanged += MainComboBox_SelectedIndexChanged;
            // 
            // MainLabel
            // 
            MainLabel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainLabel.ForeColor = Color.FromArgb(48, 48, 48);
            MainLabel.Location = new Point(19, 384);
            MainLabel.Name = "MainLabel";
            MainLabel.Size = new Size(364, 83);
            MainLabel.TabIndex = 1;
            MainLabel.Text = "信息";
            MainLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainPictureBox
            // 
            MainPictureBox.Image = Properties.Resources.logo;
            MainPictureBox.Location = new Point(123, 139);
            MainPictureBox.Name = "MainPictureBox";
            MainPictureBox.Size = new Size(150, 150);
            MainPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            MainPictureBox.TabIndex = 0;
            MainPictureBox.TabStop = false;
            // 
            // MainContextMenuStrip
            // 
            MainContextMenuStrip.BackColor = Color.FromArgb(243, 249, 255);
            MainContextMenuStrip.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MainContextMenuStrip.ImageScalingSize = new Size(24, 24);
            MainContextMenuStrip.Items.AddRange(new ToolStripItem[] { STAStripMenuItem, STAStripComboBox, AboutStripMenuItem });
            MainContextMenuStrip.Name = "MainContextMenuStrip";
            MainContextMenuStrip.Size = new Size(182, 102);
            // 
            // STAStripMenuItem
            // 
            STAStripMenuItem.Name = "STAStripMenuItem";
            STAStripMenuItem.Size = new Size(181, 30);
            STAStripMenuItem.Text = "STA订阅";
            STAStripMenuItem.Click += STAStripMenuItem_Click;
            // 
            // STAStripComboBox
            // 
            STAStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            STAStripComboBox.Name = "STAStripComboBox";
            STAStripComboBox.Size = new Size(121, 32);
            STAStripComboBox.SelectedIndexChanged += STAStripComboBox_SelectedIndexChanged;
            // 
            // AboutStripMenuItem
            // 
            AboutStripMenuItem.Name = "AboutStripMenuItem";
            AboutStripMenuItem.Size = new Size(181, 30);
            AboutStripMenuItem.Text = "关于";
            AboutStripMenuItem.Click += AboutStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1400, 800);
            Controls.Add(MainTitlePanel);
            Controls.Add(MainFlowLayoutPanel);
            ExtendBox = true;
            ExtendMenu = MainContextMenuStrip;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "破晓壁纸";
            ZoomScaleRect = new Rectangle(22, 22, 800, 450);
            SizeChanged += MainForm_SizeChanged;
            MainTitlePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MainPictureBox).EndInit();
            MainContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIFlowLayoutPanel MainFlowLayoutPanel;
        private Sunny.UI.UITitlePanel MainTitlePanel;
        private Sunny.UI.UILabel MainLabel;
        private PictureBox MainPictureBox;
        private Sunny.UI.UIButton MainOpenButton;
        private Sunny.UI.UIButton MainViewButton;
        private Sunny.UI.UIButton MainApplyButton;
        private Sunny.UI.UIComboBox MainComboBox;
        private Sunny.UI.UICheckBox MainCheckBox;
        private Sunny.UI.UIContextMenuStrip MainContextMenuStrip;
        private ToolStripMenuItem STAStripMenuItem;
        private ToolStripComboBox STAStripComboBox;
        private ToolStripMenuItem AboutStripMenuItem;
    }
}
