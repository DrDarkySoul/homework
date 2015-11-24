using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MediaPlayer : Form
    {
        public String nameOfSong = "";
        public MediaPlayer()
        {
            InitializeComponent();
        }

        private void WindowsMediaPlayer_MediaChange(object sender, AxWMPLib._WMPOCXEvents_MediaChangeEvent e)
        {
            nameOfSong = WindowsMediaPlayer.currentMedia.name;
        }
    }
}
