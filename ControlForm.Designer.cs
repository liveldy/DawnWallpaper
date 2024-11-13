namespace DawnWallpaper
{
    partial class ControlForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlForm));
            uiFlowLayoutPanel1 = new Sunny.UI.UIFlowLayoutPanel();
            uiTitlePanel1 = new Sunny.UI.UITitlePanel();
            uiButton3 = new Sunny.UI.UIButton();
            uiButton2 = new Sunny.UI.UIButton();
            uiCheckBox2 = new Sunny.UI.UICheckBox();
            uiCheckBox1 = new Sunny.UI.UICheckBox();
            uiButton1 = new Sunny.UI.UIButton();
            uiLabel1 = new Sunny.UI.UILabel();
            pictureBox1 = new PictureBox();
            uiContextMenuStrip1 = new Sunny.UI.UIContextMenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            notifyIcon1 = new NotifyIcon(components);
            uiTitlePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            uiContextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // uiFlowLayoutPanel1
            // 
            uiFlowLayoutPanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiFlowLayoutPanel1.Location = new Point(20, 70);
            uiFlowLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
            uiFlowLayoutPanel1.MinimumSize = new Size(1, 1);
            uiFlowLayoutPanel1.Name = "uiFlowLayoutPanel1";
            uiFlowLayoutPanel1.Padding = new Padding(2);
            uiFlowLayoutPanel1.ShowText = false;
            uiFlowLayoutPanel1.Size = new Size(905, 738);
            uiFlowLayoutPanel1.TabIndex = 0;
            uiFlowLayoutPanel1.Text = "uiFlowLayoutPanel1";
            uiFlowLayoutPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiTitlePanel1
            // 
            uiTitlePanel1.Controls.Add(uiButton3);
            uiTitlePanel1.Controls.Add(uiButton2);
            uiTitlePanel1.Controls.Add(uiCheckBox2);
            uiTitlePanel1.Controls.Add(uiCheckBox1);
            uiTitlePanel1.Controls.Add(uiButton1);
            uiTitlePanel1.Controls.Add(uiLabel1);
            uiTitlePanel1.Controls.Add(pictureBox1);
            uiTitlePanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTitlePanel1.Location = new Point(945, 70);
            uiTitlePanel1.Margin = new Padding(4, 5, 4, 5);
            uiTitlePanel1.MinimumSize = new Size(1, 1);
            uiTitlePanel1.Name = "uiTitlePanel1";
            uiTitlePanel1.ShowText = false;
            uiTitlePanel1.Size = new Size(376, 738);
            uiTitlePanel1.TabIndex = 1;
            uiTitlePanel1.Text = "属性";
            uiTitlePanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiButton3
            // 
            uiButton3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton3.Location = new Point(19, 672);
            uiButton3.MinimumSize = new Size(1, 1);
            uiButton3.Name = "uiButton3";
            uiButton3.Size = new Size(150, 52);
            uiButton3.TabIndex = 6;
            uiButton3.Text = "预览";
            uiButton3.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton3.Click += uiButton3_Click;
            // 
            // uiButton2
            // 
            uiButton2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton2.Location = new Point(196, 672);
            uiButton2.MinimumSize = new Size(1, 1);
            uiButton2.Name = "uiButton2";
            uiButton2.Size = new Size(165, 52);
            uiButton2.TabIndex = 5;
            uiButton2.Text = "打开文件夹";
            uiButton2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton2.Click += uiButton2_Click;
            // 
            // uiCheckBox2
            // 
            uiCheckBox2.Checked = true;
            uiCheckBox2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiCheckBox2.ForeColor = Color.FromArgb(48, 48, 48);
            uiCheckBox2.Location = new Point(50, 525);
            uiCheckBox2.MinimumSize = new Size(1, 1);
            uiCheckBox2.Name = "uiCheckBox2";
            uiCheckBox2.Size = new Size(290, 44);
            uiCheckBox2.TabIndex = 4;
            uiCheckBox2.Text = "背景音乐";
            // 
            // uiCheckBox1
            // 
            uiCheckBox1.Checked = true;
            uiCheckBox1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiCheckBox1.ForeColor = Color.FromArgb(48, 48, 48);
            uiCheckBox1.Location = new Point(50, 475);
            uiCheckBox1.MinimumSize = new Size(1, 1);
            uiCheckBox1.Name = "uiCheckBox1";
            uiCheckBox1.Size = new Size(290, 45);
            uiCheckBox1.TabIndex = 3;
            uiCheckBox1.Text = "语音";
            // 
            // uiButton1
            // 
            uiButton1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton1.Location = new Point(19, 612);
            uiButton1.MinimumSize = new Size(1, 1);
            uiButton1.Name = "uiButton1";
            uiButton1.Size = new Size(342, 52);
            uiButton1.TabIndex = 2;
            uiButton1.Text = "应用";
            uiButton1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton1.Click += uiButton1_Click;
            // 
            // uiLabel1
            // 
            uiLabel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(50, 346);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(290, 100);
            uiLabel1.TabIndex = 1;
            uiLabel1.Text = "名称\r\n版本\r\n作者\r\n大小";
            uiLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(88, 90);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 200);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // uiContextMenuStrip1
            // 
            uiContextMenuStrip1.BackColor = Color.FromArgb(243, 249, 255);
            uiContextMenuStrip1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiContextMenuStrip1.ImageScalingSize = new Size(24, 24);
            uiContextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem6, toolStripSeparator1, toolStripMenuItem5, toolStripSeparator2, toolStripMenuItem4, toolStripMenuItem3 });
            uiContextMenuStrip1.Name = "uiContextMenuStrip1";
            uiContextMenuStrip1.Size = new Size(201, 196);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(200, 30);
            toolStripMenuItem1.Text = "全屏";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(200, 30);
            toolStripMenuItem2.Text = "开机自启动";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(200, 30);
            toolStripMenuItem6.Text = "静默模式";
            toolStripMenuItem6.Click += toolStripMenuItem6_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(197, 6);
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(200, 30);
            toolStripMenuItem5.Text = "安装";
            toolStripMenuItem5.Click += toolStripMenuItem5_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(197, 6);
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(200, 30);
            toolStripMenuItem4.Text = "关于";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(200, 30);
            toolStripMenuItem3.Text = "退出";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "破晓壁纸";
            notifyIcon1.Visible = true;
            notifyIcon1.Click += notifyIcon1_Click;
            // 
            // ControlForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1347, 831);
            CloseAskString = "";
            Controls.Add(uiTitlePanel1);
            Controls.Add(uiFlowLayoutPanel1);
            ExtendBox = true;
            ExtendMenu = uiContextMenuStrip1;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(800, 600);
            Name = "ControlForm";
            Padding = new Padding(2, 50, 2, 2);
            Resizable = true;
            ShowDragStretch = true;
            Text = "破晓壁纸";
            TitleHeight = 50;
            ZoomScaleRect = new Rectangle(22, 22, 800, 450);
            Activated += ControlForm_Activated;
            Load += ControlForm_Load;
            SizeChanged += ControlForm_SizeChanged;
            uiTitlePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            uiContextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIFlowLayoutPanel uiFlowLayoutPanel1;
        private Sunny.UI.UITitlePanel uiTitlePanel1;
        private Sunny.UI.UICheckBox uiCheckBox1;
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UILabel uiLabel1;
        private PictureBox pictureBox1;
        private Sunny.UI.UICheckBox uiCheckBox2;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIContextMenuStrip uiContextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItem3;
        private NotifyIcon notifyIcon1;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripSeparator toolStripSeparator2;
        private Sunny.UI.UIButton uiButton3;
        private ToolStripMenuItem toolStripMenuItem6;
    }
}
