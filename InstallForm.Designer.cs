namespace DawnWallpaper
{
    partial class InstallForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallForm));
            uiListBox1 = new Sunny.UI.UIListBox();
            uiTextBox1 = new Sunny.UI.UITextBox();
            uiButton1 = new Sunny.UI.UIButton();
            uiButton2 = new Sunny.UI.UIButton();
            uiProcessBar1 = new Sunny.UI.UIProcessBar();
            SuspendLayout();
            // 
            // uiListBox1
            // 
            uiListBox1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiListBox1.HoverColor = Color.FromArgb(155, 200, 255);
            uiListBox1.ItemSelectForeColor = Color.White;
            uiListBox1.Location = new Point(20, 50);
            uiListBox1.Margin = new Padding(4, 5, 4, 5);
            uiListBox1.MinimumSize = new Size(1, 1);
            uiListBox1.Name = "uiListBox1";
            uiListBox1.Padding = new Padding(2);
            uiListBox1.ShowText = false;
            uiListBox1.Size = new Size(408, 525);
            uiListBox1.TabIndex = 0;
            uiListBox1.Text = "uiListBox1";
            // 
            // uiTextBox1
            // 
            uiTextBox1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox1.Location = new Point(445, 49);
            uiTextBox1.Margin = new Padding(4, 5, 4, 5);
            uiTextBox1.MinimumSize = new Size(1, 16);
            uiTextBox1.Multiline = true;
            uiTextBox1.Name = "uiTextBox1";
            uiTextBox1.Padding = new Padding(5);
            uiTextBox1.ReadOnly = true;
            uiTextBox1.ShowText = false;
            uiTextBox1.Size = new Size(332, 415);
            uiTextBox1.TabIndex = 1;
            uiTextBox1.TextAlignment = ContentAlignment.TopLeft;
            uiTextBox1.Watermark = "";
            // 
            // uiButton1
            // 
            uiButton1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton1.Location = new Point(445, 522);
            uiButton1.MinimumSize = new Size(1, 1);
            uiButton1.Name = "uiButton1";
            uiButton1.Size = new Size(150, 52);
            uiButton1.TabIndex = 2;
            uiButton1.Text = "安装";
            uiButton1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton1.Click += uiButton1_Click;
            // 
            // uiButton2
            // 
            uiButton2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton2.Location = new Point(627, 522);
            uiButton2.MinimumSize = new Size(1, 1);
            uiButton2.Name = "uiButton2";
            uiButton2.Size = new Size(150, 52);
            uiButton2.TabIndex = 3;
            uiButton2.Text = "从本地安装";
            uiButton2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton2.Click += uiButton2_Click;
            // 
            // uiProcessBar1
            // 
            uiProcessBar1.DecimalPlaces = 0;
            uiProcessBar1.FillColor = Color.FromArgb(235, 243, 255);
            uiProcessBar1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiProcessBar1.Location = new Point(445, 472);
            uiProcessBar1.MinimumSize = new Size(3, 3);
            uiProcessBar1.Name = "uiProcessBar1";
            uiProcessBar1.Size = new Size(332, 44);
            uiProcessBar1.TabIndex = 4;
            uiProcessBar1.Text = "uiProcessBar1";
            // 
            // Install
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 600);
            Controls.Add(uiProcessBar1);
            Controls.Add(uiButton2);
            Controls.Add(uiButton1);
            Controls.Add(uiTextBox1);
            Controls.Add(uiListBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(800, 600);
            Name = "Install";
            Padding = new Padding(2, 36, 2, 2);
            Resizable = true;
            ShowDragStretch = true;
            Text = "安装";
            ZoomScaleRect = new Rectangle(22, 22, 800, 450);
            Load += Install_Load;
            SizeChanged += Install_SizeChanged;
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIListBox uiListBox1;
        private Sunny.UI.UITextBox uiTextBox1;
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIProcessBar uiProcessBar1;
    }
}