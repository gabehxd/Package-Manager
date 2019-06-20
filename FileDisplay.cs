using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager
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
