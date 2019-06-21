using HACGUI.Extensions;
using HACGUI.Services;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Package_Manager
{
    public partial class MainForm : Form
    {
        DirectoryInfo ManualDir;
        Dictionary<Package, FileInfo> Packages;
        PackageCreation PackageCreation = new PackageCreation();
        FileDisplay FileDisplay;

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
                        SdStatusStrip.Image = Properties.Resources.green;
                        PopulateMenu();
                        InstallBtn.Enabled = true;
                        InstallWebBtn.Enabled = true;
                    };
                    Invoke(inv);
                };
                SDService.OnSDRemoved += (drive) =>
                {
                    MethodInvoker inv = () =>
                    {
                        SdStatusStrip.Image = Properties.Resources.red;
                        Packages = new Dictionary<Package, FileInfo>();
                        PackageView.Items.Clear();
                        InstallBtn.Enabled = false;
                        InstallWebBtn.Enabled = false;

                    };
                    Invoke(inv);
                };
                SDService.Start();
            };
        }

        private void InstallBtn_Click(object sender, EventArgs e)
        {
            FileInfo PackageFile;
            using (OpenFileDialog PackageDialog = new OpenFileDialog
            {
                Filter = "Package File|*.pkg|Package Index File|Package.json|All Files|*.*",
                Title = "Select a Package File!"
            })
            {
                if (PackageDialog.ShowDialog() != DialogResult.OK) return;

                PackageFile = new FileInfo(PackageDialog.FileName);
            }

            Install(PackageFile);
        }


        private int CheckPackages(Package install, out Package data)
        {
            data = null;
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
                            data = Outdated;
                            return 1;
                        }
                        else return -1;
                    }
                    else
                    {
                        MessageBox.Show("The package you are trying to install is already installed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
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
                    using (FileDisplay display = new FileDisplay($"{string.Join("\n", Conflicting)}", "Conflicting Files"))
                    {
                        display.Show();
                    }
                    return 1;
                }
            }
            return 0;
        }

        private void ManualLocBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;
            ManualDir = new DirectoryInfo(dialog.SelectedPath);
            Packages = new Dictionary<Package, FileInfo>();
            PackageView.Items.Clear();
            SDService.Stop();
            PopulateMenu();
            AutoLocBtn.Enabled = true;
            ManualLocBtn.Enabled = false;
            StatusStrip.Visible = false;
            InstallBtn.Enabled = true;
            InstallZipBtn.Enabled = true;
            InstallWebBtn.Enabled = true;
        }

        private void AutoLocBtn_Click(object sender, EventArgs e)
        {
            InstallBtn.Enabled = false;
            InstallWebBtn.Enabled = false;
            InstallZipBtn.Enabled = false;
            Packages = new Dictionary<Package, FileInfo>();
            PackageView.Items.Clear();
            SDService.Start();
            ManualDir = null;
            AutoLocBtn.Enabled = false;
            ManualLocBtn.Enabled = true;
            StatusStrip.Visible = true;

        }

        private void InstallPackage(Package pkg, DirectoryInfo Resource)
        {
            FileInfo PackageIndex = GetCurrentDrive().GetDirectory("FM").GetDirectory("Packages").GetFile($"{string.Concat(pkg.name.Without(Path.GetInvalidFileNameChars()))}.json");
            PackageIndex.WriteAllText(JsonConvert.SerializeObject(pkg));
            Resource.DirectoryCopy(GetCurrentDrive().FullName, true);

            Packages.Add(pkg, PackageIndex);
            PopulateMenu();
            MessageBox.Show("Package Installed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void InstallPackage(Package pkg, FileInfo Resource)
        {
            FileInfo PackageIndex = GetCurrentDrive().GetDirectory("FM").GetDirectory("Packages").GetFile($"{string.Concat(pkg.name.Without(Path.GetInvalidFileNameChars()))}.json");
            PackageIndex.WriteAllText(JsonConvert.SerializeObject(pkg));
            ZipFile.ExtractToDirectory(Resource.FullName, GetCurrentDrive().FullName);
            Packages.Add(pkg, PackageIndex);
            PopulateMenu();
            MessageBox.Show("Package Installed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void CreatePkgBtn_Click(object sender, EventArgs e)
        {
            if (PackageCreation.IsDisposed) PackageCreation = new PackageCreation();
            PackageCreation.Show();
            PackageCreation.Activate();
        }

        private void PopulateMenu()
        {
            PackageView.Items.Clear();
            DirectoryInfo PackagesDirectory = GetCurrentDrive().GetDirectory("FM").GetDirectory("Packages");
            Dictionary<Package, FileInfo> tmp = new Dictionary<Package, FileInfo>();
            if (!PackagesDirectory.Exists)
            {
                PackagesDirectory.Create();
                return;
            }

            foreach (FileInfo file in PackagesDirectory.EnumerateFiles("*.json"))
            {
                Package pck = JsonConvert.DeserializeObject<Package>(file.ReadAllText());
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
                PackageView.Items.Add(new ListViewItem(Item));
            }
        }

        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            if (FileDisplay == null || FileDisplay.IsDisposed) FileDisplay = new FileDisplay(PackageView.Items[PackageView.SelectedIndices[0]].SubItems[2].Text.Replace("\n ", "\n"));
            FileDisplay.Show();
            FileDisplay.Activate();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e) => RemovePackage(Packages.Single(p => p.Key.name == PackageView.Items[PackageView.SelectedIndices[0]].Text));

        private void RemovePackage(KeyValuePair<Package, FileInfo> Package)
        {
            foreach (string file in Package.Key.files)
                GetCurrentDrive().GetFile(file).Delete();

            Package.Value.Delete();
            Packages.Remove(Package.Key);
            PackageView.Items.RemoveByKey(Package.Key.name);
            MessageBox.Show("Package Removed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ListView1_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (PackageView.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void Install(FileInfo PackageFile)
        {
            if (PackageFile.Extension.ToLower() == ".pkg")
            {
                DirectoryInfo dir = new DirectoryInfo(Path.GetTempPath()).GetDirectory("FM").GetDirectory(Path.GetRandomFileName());
                ZipFile.ExtractToDirectory(PackageFile.FullName, dir.FullName);
                PackageFile = dir.GetFile("Package.json");
            }

            Package Pkg;
            try
            {
                Pkg = JsonConvert.DeserializeObject<Package>(PackageFile.ReadAllText());
            }
            catch
            {
                MessageBox.Show("Invalid package file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DirectoryInfo ResourceDirectory;
            if (Pkg.uri != null)
            {
                FileInfo ResourceDl;
                using (WebClient wc = new WebClient())
                {
                    ResourceDl = new DirectoryInfo(Path.GetTempPath()).GetDirectory("FM").GetFile(Path.GetRandomFileName());
                    if (ResourceDl.Exists) ResourceDl.Delete();
                    try
                    {
                        wc.DownloadFile(Pkg.uri, ResourceDl.FullName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Download Failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                switch (CheckPackages(Pkg, out Package Outdated))
                {
                    default:
                    case -1:
                        return;
                    case 0:
                        InstallPackage(Pkg, ResourceDl);
                        return;
                    case 1:
                        RemovePackage(Packages.Single(p => p.Key.name == Outdated.name));
                        InstallPackage(Pkg, ResourceDl);
                        return;
                }
            }
            else
            {
                ResourceDirectory = PackageFile.Directory.GetDirectory("Resource");
                if (!ResourceDirectory.Exists)
                {
                    MessageBox.Show("Resource folder does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            switch (CheckPackages(Pkg, out Package dated))
            {
                case -1:
                    return;
                case 0:
                    InstallPackage(Pkg, ResourceDirectory);
                    break;
                case 1:
                    RemovePackage(Packages.Single(p => p.Key.name == dated.name));
                    InstallPackage(Pkg, ResourceDirectory);
                    break;
            }
        }

        private DirectoryInfo GetCurrentDrive() => ManualDir ?? SDService.CurrentDrive.RootDirectory;

        private void InstallWebBtn_Click(object sender, EventArgs e)
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

            FileInfo PackageFile;
            using (WebClient wc = new WebClient())
            {
                PackageFile = new DirectoryInfo(Path.GetTempPath()).GetDirectory("FM").GetFile(uri.Segments.Last());
                if (PackageFile.Exists) PackageFile.Delete();
                try
                {
                    wc.DownloadFile(uri, PackageFile.FullName);
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Download Failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            Install(PackageFile);
        }

        private void InstallZipBtn_Click(object sender, EventArgs e)
        {
            FileInfo Zip;
            using (OpenFileDialog PackageDialog = new OpenFileDialog
            {
                Filter = "Zip File|*.zip|All Files|*.*",
                Title = "Select a Zip File!"
            })
            {
                if (PackageDialog.ShowDialog() != DialogResult.OK) return;

                Zip = new FileInfo(PackageDialog.FileName);
            }

            using (QuickPackageCreation creation = new QuickPackageCreation(Zip))
            {
                if (creation.ShowDialog() != DialogResult.OK) return;

                switch (CheckPackages(creation.Pkg, out Package Outdated))
                {
                    default:
                    case -1:
                        return;
                    case 0:
                        InstallPackage(creation.Pkg, Zip);
                        return;
                    case 1:
                        RemovePackage(Packages.Single(p => p.Key.name == Outdated.name));
                        InstallPackage(creation.Pkg, Zip);
                        return;
                }
            }
        }

        private void InstallZipWebBtn_Click(object sender, EventArgs e)
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

            FileInfo Zip;
            using (WebClient wc = new WebClient())
            {
                Zip = new DirectoryInfo(Path.GetTempPath()).GetDirectory("FM").GetFile(uri.Segments.Last());
                try
                {
                    wc.DownloadFile(uri, Zip.FullName);
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Download Failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            using (QuickPackageCreation creation = new QuickPackageCreation(Zip))
            {
                if (creation.ShowDialog() != DialogResult.OK) return;

                switch (CheckPackages(creation.Pkg, out Package Outdated))
                {
                    default:
                    case -1:
                        return;
                    case 0:
                        InstallPackage(creation.Pkg, Zip);
                        return;
                    case 1:
                        RemovePackage(Packages.Single(p => p.Key.name == Outdated.name));
                        InstallPackage(creation.Pkg, Zip);
                        return;
                }
            }
        }
    }
}
