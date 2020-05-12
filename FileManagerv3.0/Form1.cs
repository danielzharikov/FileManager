using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO.Compression;

namespace FileManagerv3._0
{
    public partial class Form1 : Form
    {
        private string FilePath = "C:/";
        private List<string> tocopy = new List<string>();
        private List<string> toarchive = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void loadingfiles()
        {
            try
            {
                fileSystemWatcher1.Path = FilePath;
                DirectoryInfo fileList = new DirectoryInfo(FilePath);
                FileInfo[] files = fileList.GetFiles(); // GET ALL THE FILES
                DirectoryInfo[] dirs = fileList.GetDirectories(); // GET ALL THE DIRECTORIES
                listView1.Items.Clear();
                for (int i = 0; i < dirs.Length; i++)
                {
                    if (!dirs[i].Exists) MessageBox.Show("sosi");
                    ListViewItem tempitem = new ListViewItem(dirs[i].Name); // FIRST COLOMN - NAME
                    tempitem.SubItems.Add("Folder"); // SECOND COLOMN - EXTENTION
                    tempitem.SubItems.Add("<DIR>"); // THIRD COLOMN - SIZE
                    tempitem.SubItems.Add(dirs[i].LastWriteTime.ToString()); // FORTH COLOMN - LAST WRITETIME
                    listView1.Items.Add(tempitem);
                }
                for (int i = 0; i < files.Length; i++)
                {
                    ListViewItem tempitem = new ListViewItem(files[i].Name); // FIRST COLOMN - NAME
                    tempitem.SubItems.Add(files[i].Extension); // SECOND COLOMN - EXTENTION
                    tempitem.SubItems.Add(files[i].Length.ToString()); // THIRD COLOMN - SIZE
                    tempitem.SubItems.Add(files[i].LastWriteTime.ToString()); // FORTH COLOMN - LAST WRITETIME
                    listView1.Items.Add(tempitem);
                }
                FilePathTextBox.Text = FilePath;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                GoBack();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadingfiles();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select your path." })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    FilePath = fbd.SelectedPath;
                }
            }
            loadingfiles();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for(int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected)
                {
                    FileAttributes fileAtr = File.GetAttributes(FilePath + "/" + listView1.Items[i].Text);
                    if ((fileAtr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        FilePath = FilePath + "/" + listView1.Items[i].Text;
                        loadingfiles();
                    }
                    else
                    {
                        string path = FilePath + "/" + listView1.Items[i].Text;
                        if (File.Exists(path))
                        {
                            try
                            {
                                // открывает файл стандартной программой операционной системы
                                Process.Start(path);
                            }
                            catch (Win32Exception)
                            {
                                MessageBox.Show("Error. Unable to open file.");
                            }
                        }
                    }
                    break;
                }
            }
        }


        private void GoBack()
        {
            if (FilePath.Length > 3)
            {
                int to = 0;
                for (int i = FilePath.Length - 1; i > 2; i--)
                {
                    if (FilePath[i] == '/')
                    {
                        to = i;
                        break;
                    }
                }
                if (to != 0)
                {
                    FilePath = FilePath.Substring(0, to);
                    loadingfiles();
                }
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            // FIRSTY CHECKING IF SOMEONE IS SELECTED
            tocopy.Clear();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected)
                {
                    tocopy.Add(FilePath + "/" + listView1.Items[i].Text);
                }
            }
        }

        private string CutName(string str)
        {
            int to = 0;
            string toreturn = "";
            for (int i = str.Length - 1; i > 2; i--)
            {
                if (str[i] == '/')
                {
                    to = i;
                    break;
                }
            }
            if (to != 0)
            {
                toreturn = str.Substring(to + 1, str.Length-1-to);
            }
            return toreturn;
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if(tocopy.Count > 0)
            {
                for(int i = 0; i < tocopy.Count; i++)
                {
                    FileAttributes fileAtr = File.GetAttributes(tocopy[i]);
                    if ((fileAtr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Directory.Move(tocopy[i], FilePath + "/" + CutName(tocopy[i]));
                    }
                    else
                    {
                        File.Move(tocopy[i], FilePath + "/" + CutName(tocopy[i]));
                    }
                }
                tocopy.Clear();
            }
            loadingfiles();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected)
                {
                    FileAttributes fileAtr = File.GetAttributes(FilePath + "/" + listView1.Items[i].Text);
                    if ((fileAtr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Directory.Delete(FilePath + "/" + listView1.Items[i].Text);
                    }
                    else
                    {
                        File.Delete(FilePath + "/" + listView1.Items[i].Text);
                    }
                }
            }
            loadingfiles();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(RenameTextBox.Text))
            {
                int counting = 0;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Selected)
                    {
                        counting++;
                    }
                }
                if (counting > 1 || counting == 0)
                    MessageBox.Show("Choose only one file/directory");
                else
                {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        if (listView1.Items[i].Selected)
                        {
                            FileAttributes fileAtr = File.GetAttributes(FilePath + "/" + listView1.Items[i].Text);
                            if ((fileAtr & FileAttributes.Directory) == FileAttributes.Directory)
                            {
                                Directory.Move(FilePath + "/" + listView1.Items[i].Text, FilePath + "/" + RenameTextBox.Text);
                                RenameTextBox.Clear();
                                loadingfiles();
                            }
                            else
                            {
                                File.Move(FilePath + "/" + listView1.Items[i].Text, FilePath + "/" + RenameTextBox.Text + listView1.Items[i].SubItems[0]);
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Input anything in Special TEXTBOX");
            }
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            toarchive.Clear();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected)
                {
                    toarchive.Add(FilePath + "/" + listView1.Items[i].Text);
                }
            }
            if(toarchive.Count > 0)
            {
                try
                {
                    var zip = ZipFile.Open(FilePath + "/" + CompressTextBox.Text + ".zip", ZipArchiveMode.Create);
                    try
                    {
                        for (int i = 0; i < toarchive.Count; i++)
                        {
                            zip.CreateEntryFromFile(toarchive[i], Path.GetFileName(toarchive[i]), CompressionLevel.Optimal);
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        zip.Dispose();
                        File.Delete(FilePath + "/" + CompressTextBox.Text + ".zip");
                    }
                    zip.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            loadingfiles();
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            loadingfiles();
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            loadingfiles();
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            loadingfiles();
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            loadingfiles();
        }

        private void btnNewFolder_Click(object sender, EventArgs e)
        {
            int counting = 0;
            if(Directory.Exists(FilePath + "/" + "New Folder"))
            {
                counting++;
                while (Directory.Exists(FilePath + "/" + "New Folder" + counting.ToString()))
                {
                    counting++;
                }
            }
            if (counting == 0)
                Directory.CreateDirectory(FilePath + "/" + "New Folder");
            else Directory.CreateDirectory(FilePath + "/" + "New Folder" + counting.ToString());
            loadingfiles();
        }
    }
}
