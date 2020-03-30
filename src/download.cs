using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace DownloadScreen_cs
{
    public partial class download : Form
    {
        public string URL = String.Empty;
        public string FileName = String.Empty;

        WebClient wc = new WebClient();

        public download()
        {
            InitializeComponent();
        }

        private void download_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadComplete);
                Uri fileurl = new Uri(URL);
                wc.DownloadFileAsync(fileurl, FileName);
            });

            thread.Start();

            progressBar1.Minimum = 0;
        }

        private void DownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download Complete!", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                progressBar1.Minimum = 0;
                double recieve = double.Parse(e.BytesReceived.ToString());
                double total = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = recieve / total * 100;
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            }));
        }
    }
}