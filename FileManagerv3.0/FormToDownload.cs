using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagerv3._0
{
    public partial class FormToDownload : Form
    {
        public FormToDownload()
        {
            InitializeComponent();
        }

        private void btnToOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select your path." })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = fbd.SelectedPath;
                }
            }
        }

        public void DownloadFile(String remoteFilename, String localFilename)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            int bytesProcessed = 0;
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;
            try
            {
                WebRequest request = WebRequest.Create(remoteFilename);
                if (request != null)
                {
                    response = request.GetResponse();
                    if (response != null)
                    {
                        if (token.IsCancellationRequested)
                        {
                            MessageBox.Show("The operation is canceled.");
                            return;
                        }
                        remoteStream = response.GetResponseStream();
                        localStream = File.Create(localFilename);
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        Task task1 = new Task(() =>
                            {
                                do
                                {
                                    if (token.IsCancellationRequested)
                                    {
                                        MessageBox.Show("The operation is canceled.");
                                        return;
                                    }
                                    bytesRead = remoteStream.Read(buffer, 0, buffer.Length);
                                    localStream.Write(buffer, 0, bytesRead);
                                    bytesProcessed += bytesRead;
                                } while (bytesRead > 0);
                            }); task1.Start();
                        cancelTokenSource.Cancel();
                        
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void btnToStart_Click(object sender, EventArgs e)
        {
            DownloadFile(textBox1.Text, textBox2.Text);
        }
    }
}
