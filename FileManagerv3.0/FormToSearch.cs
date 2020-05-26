using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FileManagerv3._0
{
    public partial class FormToSearch : Form
    {
        private string fromwheresearch = "";
        private string regextosearch;
        public FormToSearch(string link, string regex)
        {
            InitializeComponent();
            fromwheresearch = link;
            regextosearch = regex;
            FindAndLoadFiles(link, regex);
        }

        private void FindAndLoadFiles(string link, string regex)
        {
            string[] filePaths = Directory.GetFiles(link, regex, SearchOption.AllDirectories);
            foreach(string filepath in filePaths)
            {
                listBox1.Items.Add(filepath);
            }
            MessageBox.Show(link + " " + regex);
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string path = listBox1.SelectedItem.ToString();
            if (File.Exists(path))
            {
                try
                {
                    Process.Start(path);
                }
                catch (Win32Exception)
                {
                    MessageBox.Show("Error. Unable to open file.");
                }
            }
        }
    }
}
