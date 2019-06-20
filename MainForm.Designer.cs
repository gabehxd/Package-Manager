namespace File_Manager
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
            this.installToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installFromTheInternetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setInstallLocationManuallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setInstallLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createPackageFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockAllPackageFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlockAllPackageFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PackName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Files = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView1 = new System.Windows.Forms.ListView();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.packageToolStripMenuItem,
            this.miscToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // packageToolStripMenuItem
            // 
            this.packageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installToolStripMenuItem,
            this.installFromTheInternetToolStripMenuItem,
            this.setInstallLocationManuallyToolStripMenuItem,
            this.setInstallLocationToolStripMenuItem,
            this.createPackageFileToolStripMenuItem});
            this.packageToolStripMenuItem.Name = "packageToolStripMenuItem";
            this.packageToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.packageToolStripMenuItem.Text = "Package";
            // 
            // installToolStripMenuItem
            // 
            this.installToolStripMenuItem.Enabled = false;
            this.installToolStripMenuItem.Name = "installToolStripMenuItem";
            this.installToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.installToolStripMenuItem.Text = "Install";
            this.installToolStripMenuItem.Click += new System.EventHandler(this.InstallToolStripMenuItem_Click);
            // 
            // installFromTheInternetToolStripMenuItem
            // 
            this.installFromTheInternetToolStripMenuItem.Enabled = false;
            this.installFromTheInternetToolStripMenuItem.Name = "installFromTheInternetToolStripMenuItem";
            this.installFromTheInternetToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.installFromTheInternetToolStripMenuItem.Text = "Install from the internet";
            this.installFromTheInternetToolStripMenuItem.Click += new System.EventHandler(this.InstallFromTheInternetToolStripMenuItem_Click);
            // 
            // setInstallLocationManuallyToolStripMenuItem
            // 
            this.setInstallLocationManuallyToolStripMenuItem.Name = "setInstallLocationManuallyToolStripMenuItem";
            this.setInstallLocationManuallyToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.setInstallLocationManuallyToolStripMenuItem.Text = "Set install location manually";
            this.setInstallLocationManuallyToolStripMenuItem.Click += new System.EventHandler(this.SetInstallLocationManuallyToolStripMenuItem_Click);
            // 
            // setInstallLocationToolStripMenuItem
            // 
            this.setInstallLocationToolStripMenuItem.Enabled = false;
            this.setInstallLocationToolStripMenuItem.Name = "setInstallLocationToolStripMenuItem";
            this.setInstallLocationToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.setInstallLocationToolStripMenuItem.Text = "Set install location automatically";
            this.setInstallLocationToolStripMenuItem.Click += new System.EventHandler(this.SetInstallLocationToolStripMenuItem_Click);
            // 
            // createPackageFileToolStripMenuItem
            // 
            this.createPackageFileToolStripMenuItem.Name = "createPackageFileToolStripMenuItem";
            this.createPackageFileToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.createPackageFileToolStripMenuItem.Text = "Create package file";
            this.createPackageFileToolStripMenuItem.Click += new System.EventHandler(this.CreatePackageFileToolStripMenuItem_Click);
            // 
            // miscToolStripMenuItem
            // 
            this.miscToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lockAllPackageFilesToolStripMenuItem,
            this.unlockAllPackageFilesToolStripMenuItem});
            this.miscToolStripMenuItem.Name = "miscToolStripMenuItem";
            this.miscToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.miscToolStripMenuItem.Text = "Misc.";
            // 
            // lockAllPackageFilesToolStripMenuItem
            // 
            this.lockAllPackageFilesToolStripMenuItem.Enabled = false;
            this.lockAllPackageFilesToolStripMenuItem.Name = "lockAllPackageFilesToolStripMenuItem";
            this.lockAllPackageFilesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.lockAllPackageFilesToolStripMenuItem.Text = "Lock all package files";
            this.lockAllPackageFilesToolStripMenuItem.Click += new System.EventHandler(this.LockAllPackageFilesToolStripMenuItem_Click);
            // 
            // unlockAllPackageFilesToolStripMenuItem
            // 
            this.unlockAllPackageFilesToolStripMenuItem.Enabled = false;
            this.unlockAllPackageFilesToolStripMenuItem.Name = "unlockAllPackageFilesToolStripMenuItem";
            this.unlockAllPackageFilesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.unlockAllPackageFilesToolStripMenuItem.Text = "Unlock all package files";
            this.unlockAllPackageFilesToolStripMenuItem.Click += new System.EventHandler(this.UnlockAllPackageFilesToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Image = global::File_Manager.Properties.Resources.red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(37, 17);
            this.toolStripStatusLabel1.Text = "SD";
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
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PackName,
            this.Author,
            this.Files,
            this.Version});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 46);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(776, 359);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.ListView1_ItemActivate);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView1_RightClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "File Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem packageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem setInstallLocationManuallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setInstallLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createPackageFileToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installFromTheInternetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miscToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockAllPackageFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlockAllPackageFilesToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader PackName;
        private System.Windows.Forms.ColumnHeader Author;
        private System.Windows.Forms.ColumnHeader Files;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ListView listView1;
    }
}

