using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using HACGUI.Extensions;

namespace Package_Manager
{
    public partial class QuickPackageCreation : Form
    {
        public Package Pkg { get; set; }
        private FileInfo Zip;

        public QuickPackageCreation(FileInfo Zip)
        {
            InitializeComponent();
            this.Zip = Zip;
            PkgName.Text = Zip.GetFileNameWithoutExtension();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PkgName.Text))
            {
                MessageBox.Show("Error", "A package name must be declared!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(Auths.Text))
            {
                MessageBox.Show("Error", "An author must be declared!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(PkgVersion.Text))
            {
                MessageBox.Show("Error", "A package version must be declared!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ZipArchive archive = new ZipArchive(Zip.OpenRead(), ZipArchiveMode.Read, false);
            Pkg = new Package(PkgName.Text, Auths.Text, DateTime.Now, PkgVersion.Text, archive.Entries.Select(ent => ent.FullName).Where(f => !f.EndsWith("/")).ToArray(), null) ;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
