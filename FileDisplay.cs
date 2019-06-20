using System.Windows.Forms;

namespace Package_Manager
{
    public partial class FileDisplay : Form
    {
        public FileDisplay(string Files, string Title = "Files")
        {
            InitializeComponent();
            Text = Title;
            richTextBox1.Text = Files;
        }
    }
}
