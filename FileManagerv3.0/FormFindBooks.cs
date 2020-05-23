using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagerv3._0
{
    public partial class FormFindBooks : Form
    {
        string StandURL = "https://www.labirint.ru/search/";
        string prefix = "python";
        public FormFindBooks()
        {
            InitializeComponent();
        }

        private string GetHTML(string url)
        {
            string line = "";
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip,
            };
            using (var client = new HttpClient(handler))
            {
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        line = content.ReadAsStringAsync().Result;
                    }
                }
            }
            return line;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Input amount of books, you want to see");
                return;
            }
            try
            {
                if (comboBox1.SelectedItem.ToString() == "C#")
                    prefix = "c%23";
                else
                    prefix = comboBox1.SelectedItem.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            int page = 1;
            string url = StandURL + prefix + "/?page=" + page.ToString();
            string line = GetHTML(url);
            Match amount = Regex.Match(line, "<span>Товары</span>(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)</span>");
            Regex regex = new Regex("data-product-id=\"(.*?)\"(\n|\r|\r\n)data-name=\"(.*?)\"(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)data-discount-price=\"(.*?)\"(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)(\n|\r|\r\n)(.*?)title=\"(.*?)-");
            int allamount = int.Parse(amount.Groups[4].Value);
            int amountweneed = int.Parse(textBox1.Text);
            if(allamount < amountweneed)
            {
                MessageBox.Show($"There are only {allamount} books.");
                amountweneed = allamount;
            }
            int counting = 0;
            listView1.Clear();
            while(counting < amountweneed)
            {
                url = StandURL + prefix + "/?page=" + page.ToString();
                line = GetHTML(url);
                foreach (Match match in regex.Matches(line))
                {
                    if (counting == amountweneed)
                        break;
                    ListViewItem tempitem = new ListViewItem(match.Groups[3].Value); // FIRST COLOMN - NAME
                    tempitem.SubItems.Add(match.Groups[48].Value); // SECOND COLOMN - EXTENTION
                    tempitem.SubItems.Add(match.Groups[30].Value); // THIRD COLOMN - SIZE
                    tempitem.SubItems.Add(match.Groups[1].Value); // FORTH COLOMN - LAST WRITETIME
                    listView1.Items.Add(tempitem);
                    counting++;
                }
                page++;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected)
                {
                    Process.Start("https://www.labirint.ru/" + "books/" + listView1.Items[i].SubItems[3].Text);
                    break;
                }
            }
        }
    }
}
