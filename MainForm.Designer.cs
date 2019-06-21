namespace Package_Manager
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.packageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InstallBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.InstallZipBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.InstallWebBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.InstallZipWebBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.ManualLocBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoLocBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.CreatePkgBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.SdStatusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PackName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Files = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PackageView = new System.Windows.Forms.ListView();
            this.menuStrip1.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.packageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // packageToolStripMenuItem
            // 
            this.packageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InstallBtn,
            this.InstallZipBtn,
            this.InstallWebBtn,
            this.InstallZipWebBtn,
            this.ManualLocBtn,
            this.AutoLocBtn,
            this.CreatePkgBtn});
            this.packageToolStripMenuItem.Name = "packageToolStripMenuItem";
            this.packageToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.packageToolStripMenuItem.Text = "Package";
            // 
            // InstallBtn
            // 
            this.InstallBtn.Enabled = false;
            this.InstallBtn.Name = "InstallBtn";
            this.InstallBtn.Size = new System.Drawing.Size(245, 22);
            this.InstallBtn.Text = "Install";
            this.InstallBtn.Click += new System.EventHandler(this.InstallBtn_Click);
            // 
            // InstallZipBtn
            // 
            this.InstallZipBtn.Enabled = false;
            this.InstallZipBtn.Name = "InstallZipBtn";
            this.InstallZipBtn.Size = new System.Drawing.Size(245, 22);
            this.InstallZipBtn.Text = "Install from ZIP file";
            this.InstallZipBtn.Click += new System.EventHandler(this.InstallZipBtn_Click);
            // 
            // InstallWebBtn
            // 
            this.InstallWebBtn.Enabled = false;
            this.InstallWebBtn.Name = "InstallWebBtn";
            this.InstallWebBtn.Size = new System.Drawing.Size(245, 22);
            this.InstallWebBtn.Text = "Install package from the internet";
            this.InstallWebBtn.Click += new System.EventHandler(this.InstallWebBtn_Click);
            // 
            // InstallZipWebBtn
            // 
            this.InstallZipWebBtn.Enabled = false;
            this.InstallZipWebBtn.Name = "InstallZipWebBtn";
            this.InstallZipWebBtn.Size = new System.Drawing.Size(245, 22);
            this.InstallZipWebBtn.Text = "Install ZIP from the internet";
            this.InstallZipWebBtn.Click += new System.EventHandler(this.InstallZipWebBtn_Click);
            // 
            // ManualLocBtn
            // 
            this.ManualLocBtn.Name = "ManualLocBtn";
            this.ManualLocBtn.Size = new System.Drawing.Size(245, 22);
            this.ManualLocBtn.Text = "Set install location manually";
            this.ManualLocBtn.Click += new System.EventHandler(this.ManualLocBtn_Click);
            // 
            // AutoLocBtn
            // 
            this.AutoLocBtn.Enabled = false;
            this.AutoLocBtn.Name = "AutoLocBtn";
            this.AutoLocBtn.Size = new System.Drawing.Size(245, 22);
            this.AutoLocBtn.Text = "Set install location automatically";
            this.AutoLocBtn.Click += new System.EventHandler(this.AutoLocBtn_Click);
            // 
            // CreatePkgBtn
            // 
            this.CreatePkgBtn.Name = "CreatePkgBtn";
            this.CreatePkgBtn.Size = new System.Drawing.Size(245, 22);
            this.CreatePkgBtn.Text = "Create package file";
            this.CreatePkgBtn.Click += new System.EventHandler(this.CreatePkgBtn_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SdStatusStrip});
            this.StatusStrip.Location = new System.Drawing.Point(0, 428);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(800, 22);
            this.StatusStrip.TabIndex = 2;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // SdStatusStrip
            // 
            this.SdStatusStrip.Image = global::Package_Manager.Properties.Resources.red;
            this.SdStatusStrip.Name = "SdStatusStrip";
            this.SdStatusStrip.Size = new System.Drawing.Size(37, 17);
            this.SdStatusStrip.Text = "SD";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // PackName
            // 
            this.PackName.Text = "Package Name";
            this.PackName.Width = 180;
            // 
            // Author
            // 
            this.Author.Text = "Author(s)";
            this.Author.Width = 180;
            // 
            // Files
            // 
            this.Files.Text = "Files";
            this.Files.Width = 180;
            // 
            // Version
            // 
            this.Version.Text = "Package Version";
            this.Version.Width = 180;
            // 
            // PackageView
            // 
            this.PackageView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PackageView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PackName,
            this.Author,
            this.Files,
            this.Version});
            this.PackageView.HideSelection = false;
            this.PackageView.Location = new System.Drawing.Point(12, 46);
            this.PackageView.Name = "PackageView";
            this.PackageView.Size = new System.Drawing.Size(776, 359);
            this.PackageView.TabIndex = 1;
            this.PackageView.UseCompatibleStateImageBehavior = false;
            this.PackageView.View = System.Windows.Forms.View.Details;
            this.PackageView.ItemActivate += new System.EventHandler(this.ListView1_ItemActivate);
            this.PackageView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView1_RightClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.PackageView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "File Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem packageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InstallBtn;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel SdStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem ManualLocBtn;
        private System.Windows.Forms.ToolStripMenuItem AutoLocBtn;
        private System.Windows.Forms.ToolStripMenuItem CreatePkgBtn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InstallWebBtn;
        private System.Windows.Forms.ColumnHeader PackName;
        private System.Windows.Forms.ColumnHeader Author;
        private System.Windows.Forms.ColumnHeader Files;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ListView PackageView;
        private System.Windows.Forms.ToolStripMenuItem InstallZipBtn;
        private System.Windows.Forms.ToolStripMenuItem InstallZipWebBtn;
    }
}

