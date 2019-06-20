using HACGUI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HACGUI.Extensions;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Net;
using Microsoft.VisualBasic;

namespace Package_Manager
{
    public partial class MainForm : Form
    {
        DirectoryInfo ManualDir;
        Dictionary<Package, FileInfo> Packages;
        PackageCreation PackageCreation = new PackageCreation();

        public MainForm()
        {
            InitializeComponent();

            Load += (_, __) =>
            {
                SDService.OnSDPluggedIn += (drive) =>
                {
                    //item 0 is always the SD status
                    MethodInvoker inv = () =>
                    {
                        statusStrip1.Items[0].Image = Properties.Resources.green;
                        PopulateMenu();
                        installToolStripMenuItem.Enabled = true;
                        installFromTheInternetToolStripMenuItem.Enabled = true;
                        lockAllPackageFilesToolStripMenuItem.Enabled = true;
                        unlockAllPackageFilesToolStripMenuItem.Enabled = true;
                    };
                    Invoke(inv);
                };
                SDService.OnSDRemoved += (drive) =>
                {
                    MethodInvoker inv = () =>
                    {
                        statusStrip1.Items[0].Image = Properties.Resources.red;
                        Packages = null;
                        listView1.Items.Clear();
                        installToolStripMenuItem.Enabled = false;
                        installFromTheInternetToolStripMenuItem.Enabled = false;
                        lockAllPackageFilesToolStripMenuItem.Enabled = false;
                        unlockAllPackageFilesToolStripMenuItem.Enabled = false;

                    };
                    Invoke(inv);
                };
                SDService.Start();
            };
        }

        private void InstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Install();
        }

        private void SetInstallLocationManuallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;
            ManualDir = new DirectoryInfo(dialog.SelectedPath);
            Packages = new Dictionary<Package, FileInfo>();
            listView1.Items.Clear();
            SDService.Stop();
            PopulateMenu();
            setInstallLocationToolStripMenuItem.Enabled = true;
            setInstallLocationManuallyToolStripMenuItem.Enabled = false;
            statusStrip1.Visible = false;
            installToolStripMenuItem.Enabled = true;
            installFromTheInternetToolStripMenuItem.Enabled = true;
            lockAllPackageFilesToolStripMenuItem.Enabled = true;
            unlockAllPackageFilesToolStripMenuItem.Enabled = true;
        }

        private void SetInstallLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            installToolStripMenuItem.Enabled = false;
            installFromTheInternetToolStripMenuItem.Enabled = false;
            lockAllPackageFilesToolStripMenuItem.Enabled = false;
            unlockAllPackageFilesToolStripMenuItem.Enabled = false;
            Packages = new Dictionary<Package, FileInfo>();
            listView1.Items.Clear();
            SDService.Start();
            ManualDir = null;
            setInstallLocationToolStripMenuItem.Enabled = false;
            setInstallLocationManuallyToolStripMenuItem.Enabled = true;
            statusStrip1.Visible = true;

        }

        private void InstallPackage(FileInfo pkg, DirectoryInfo Resource, DirectoryInfo InstallLocation)
        {
            Package info = JsonConvert.DeserializeObject<Package>(File.ReadAllText(pkg.FullName));
            FileInfo PackageIndex = InstallLocation.GetDirectory("FM").GetDirectory("Packages").GetFile($"{string.Concat(info.name.Without(Path.GetInvalidFileNameChars()))}.json");
            pkg.CopyTo(PackageIndex.FullName, true);
            Resource.DirectoryCopy(InstallLocation.FullName, true);

            Packages.Add(info, PackageIndex);
            PopulateMenu();
            MessageBox.Show("Package Installed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreatePackageFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PackageCreation.IsDisposed) PackageCreation = new PackageCreation();
            PackageCreation.Show();
            PackageCreation.Activate();
        }

        private void PopulateMenu()
        {
            DirectoryInfo PackagesDirectory = GetCurrentDrive().GetDirectory("FM").GetDirectory("Packages");
            Dictionary<Package, FileInfo> tmp = new Dictionary<Package, FileInfo>();
            if (!PackagesDirectory.Exists)
            {
                PackagesDirectory.Create();
                return;
            }
            foreach (FileInfo file in PackagesDirectory.EnumerateFiles("*.json"))
            {
                Package pck = JsonConvert.DeserializeObject<Package>(File.ReadAllText(file.FullName));
                if (pck != null) tmp.Add(pck, file);
            }
            Packages = tmp;

            foreach (Package pkg in Packages.Keys)
            {
                string[] Item = new string[4];
                Item[0] = pkg.name;
                Item[1] = pkg.authors;
                Item[2] = string.Join("\n ", pkg.files);
                Item[3] = pkg.packageVersion.ToString();
                listView1.Items.Add(new ListViewItem(Item));
            }
        }

        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            FileDisplay display = new FileDisplay(listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text);
            display.Show();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyValuePair<Package, FileInfo> Package = Packages.Single(p => p.Key.name == listView1.Items[listView1.SelectedIndices[0]].Text);
            foreach (string file in Package.Key.files)
                GetCurrentDrive().GetFile(file).Delete();

            Package.Value.Delete();
            Packages.Remove(Package.Key);
            listView1.Items.RemoveAt(listView1.SelectedIndices[0]);
            MessageBox.Show("Package Removed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ListView1_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private DirectoryInfo GetCurrentDrive() => ManualDir ?? SDService.CurrentDrive.RootDirectory;

        private void Install(Uri URI = null)
        {
            //Important Directories and Files
            FileInfo pck;
            if (URI == null)
            {
                OpenFileDialog PackageDialog = new OpenFileDialog
                {
                    Filter = "Package File|*.pkg|Package Index File|Package.json|All Files|*.*",
                    Title = "Select a Package File!"
                };

                if (PackageDialog.ShowDialog() != DialogResult.OK) return;

                pck = new FileInfo(PackageDialog.FileName);
            }
            else
            {
                WebClient wc = new WebClient();
                pck = new DirectoryInfo(Path.GetTempPath()).GetFile(URI.Segments.Last());
                if (pck.Exists) pck.Delete();
                try
                {
                    wc.DownloadFile(URI, pck.FullName);
                }

                catch (Exception e)
                {
                    MessageBox.Show($"Download Failed: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (pck.Extension.ToLower() == ".pkg")
            {
                DirectoryInfo dir = new DirectoryInfo(Path.GetTempPath()).GetDirectory("FM").GetDirectory(Path.GetRandomFileName());
                ZipFile.ExtractToDirectory(pck.FullName, dir.FullName);
                pck = dir.GetFile("Package.json");
            }

            Package install;
            try
            {
                install = JsonConvert.DeserializeObject<Package>(File.ReadAllText(pck.FullName));
            }
            catch
            {
                MessageBox.Show("Invalid package file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (install == null)
            {
                MessageBox.Show("Invalid package file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DirectoryInfo ResourceDirectory;
            if (install.uri != null)
            {
                WebClient wc = new WebClient();
                FileInfo ResourceDl = new DirectoryInfo(Path.GetTempPath()).GetDirectory("FM").GetFile(Path.GetRandomFileName());
                if (ResourceDl.Exists) ResourceDl.Delete();
                try
                {
                    wc.DownloadFile(install.uri, ResourceDl.FullName);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Download Failed: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ResourceDirectory = new DirectoryInfo(Path.GetTempPath()).GetDirectory("FM").GetDirectory(Path.GetRandomFileName());
                ZipFile.ExtractToDirectory(ResourceDl.FullName, ResourceDirectory.FullName);
            }
            else
            {
                ResourceDirectory = pck.Directory.GetDirectory("Resource");
                if (!ResourceDirectory.Exists)
                {
                    MessageBox.Show("Resource folder does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DirectoryInfo PackagesFolder = GetCurrentDrive().GetDirectory("FM").GetDirectory("Packages");

            if (PackagesFolder.Exists)
            {
                Package Outdated = Packages.Keys.SingleOrDefault(p => p.name.ToLower() == install.name.ToLower());

                if (Outdated != null)
                {
                    if (Outdated.packageVersion != install.packageVersion || !Outdated.files.SequenceEqual(install.files) || install.created != Outdated.created)
                    {
                        if (MessageBox.Show("The package you are trying to install is different from one already installed, would you like to use the new package?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            IEnumerable<FileInfo> PackageFiles = Outdated.files.Select(f => new FileInfo(Path.Combine(GetCurrentDrive().FullName, f)));

                            foreach (FileInfo file in PackageFiles)
                                file.Delete();

                            InstallPackage(pck, ResourceDirectory, GetCurrentDrive());
                            Packages.Remove(Outdated);

                        }
                        else return;
                    }
                    else
                    {
                        MessageBox.Show("The package you are trying to install is already installed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }

                List<string> Conflicting = new List<string>();
                foreach (string[] Files in Packages.Keys.Select(p => p.files))
                {
                    foreach (string file in install.files)
                    {
                        if (Files.Contains(file))
                        {
                            Conflicting.Add(file);
                        }
                    }
                }

                if (Conflicting.Count != 0)
                {
                    FileDisplay display = new FileDisplay($"{string.Join("\n", Conflicting)}", "Conflicting Files");
                    display.Show();
                    return;
                }
            }

            InstallPackage(pck, ResourceDirectory, GetCurrentDrive());
        }

        private void InstallFromTheInternetToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string input = Interaction.InputBox("Input URL");
            if (input == string.Empty) return;
            Uri uri;
            try
            {
                uri = new Uri(input);
            }
            catch
            {
                MessageBox.Show("Invalid URL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Install(uri);
        }

        private void LockAllPackageFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Package p in Packages.Keys)
                foreach (string file in p.files)
                    GetCurrentDrive().GetFile(file).IsReadOnly = true;

            foreach (FileInfo file in Packages.Values)
                file.IsReadOnly = true;
        }

        private void UnlockAllPackageFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Package p in Packages.Keys)
                foreach (string file in p.files)
                    GetCurrentDrive().GetFile(file).IsReadOnly = false;

            foreach (FileInfo file in Packages.Values)
                file.IsReadOnly = false;
        }
    }
}
