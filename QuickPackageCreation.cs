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
        public DirectoryInfo Resources { get; set; }
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
                MessageBox.Show("A package name must be declared!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(Auths.Text))
            {
                MessageBox.Show("An author must be declared!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(PkgVersion.Text))
            {
                MessageBox.Show("A package version must be declared!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DirectoryInfo Resource = new DirectoryInfo(Path.GetTempPath()).GetDirectory("FM").GetDirectory(Path.GetRandomFileName());
            ZipFile.ExtractToDirectory(Zip.FullName, $"{Resource.FullName}{Path.DirectorySeparatorChar}{PathBox.Text}");
            Pkg = new Package(PkgName.Text, Auths.Text, DateTime.Now, PkgVersion.Text, Resource.EnumerateFiles("*", SearchOption.AllDirectories).Select(f => f.FullName.Replace(Resource.FullName + "\\", "")).ToArray(), null);
            Resources = Resource;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
