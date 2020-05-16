using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FileManagerv3._0
{
    public partial class FormSettings : Form
    {
        UserSettings userprop = new UserSettings();
        public FormSettings()
        {
            InitializeComponent();
        }

        private void savesettings(int color, int listviewcolor)
        {
            userprop = new UserSettings(color, listviewcolor);
            XmlSerializer xml = new XmlSerializer(typeof(UserSettings));
            using(FileStream fs = new FileStream("UserProps.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, userprop);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            int background = comboBoxColor.SelectedIndex;
            int listviewcolor = comboBox1.SelectedIndex;
            savesettings(background, listviewcolor);
        }
    }
}
