using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace File_Manager
{
    public partial class PackageCreation : Form
    {
        private DirectoryInfo Resource;

        public PackageCreation()
        {
            InitializeComponent();
        }

        private void SelectFilesbtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbrower = new FolderBrowserDialog();
            if (fbrower.ShowDialog() == DialogResult.OK)
            {
                Resource = new DirectoryInfo(fbrower.SelectedPath);
            }
        }

        private void Createbtn_Click(object sender, EventArgs e)
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

            SaveFileDialog sfDialog = new SaveFileDialog
            {
                Filter = "Package File | *.pkg",
                DefaultExt = "pkg"
            };

            if (sfDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo SaveLocation = new FileInfo(sfDialog.FileName);
                if (SaveLocation.Exists) SaveLocation.Delete();
                using (ZipArchive zip = new ZipArchive(SaveLocation.Create(), ZipArchiveMode.Update, false))
                {

                    Uri uri;
                    if (checkBox1.Checked)
                    {
                        try
                        {
                            uri = new Uri(UrlBox.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Invalid URL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        uri = null;
                        foreach (FileInfo file in Resource.GetFiles("*", SearchOption.AllDirectories))
                        {
                            ZipArchiveEntry entry = zip.CreateEntry(file.FullName.Replace(Resource.FullName, $"Resource{Path.DirectorySeparatorChar}{PathBox.Text}"));

                            using (Stream f = file.OpenRead())
                            using (Stream d = entry.Open())
                                f.CopyTo(d);
                        }
                    }

                    Package package = new Package(PkgName.Text, Auths.Text, DateTime.Now, PkgVersion.Text, Resource.EnumerateFiles("*", SearchOption.AllDirectories).Select(f => f.FullName.Replace(Resource.FullName + "\\", "")).ToArray(), uri);

                    using (StreamWriter writer = new StreamWriter(zip.CreateEntry("Package.json").Open()))
                    {
                        writer.Write(JsonConvert.SerializeObject(package));
                    }
                }
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) UrlBox.Enabled = true;
            else UrlBox.Enabled = false;
        }
    }
}
