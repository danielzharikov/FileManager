using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerv3._0
{
    [Serializable]
    public class UserSettings
    {
        public int background = 0;
        public int listviewcolor = 0;

        public UserSettings(int Background, int Fontsize1)
        {
            background = Background;
            listviewcolor = Fontsize1;
        }

        public UserSettings()
        {
            background = 0;
            listviewcolor = 0;
        }
    }
}
