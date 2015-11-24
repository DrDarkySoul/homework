using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Player
{
    public partial class FormChooseFile : Form
    {
        public FormMedia mediaPlayer = new FormMedia();
        public List<String> strURL = new List<String>();
        public FormChooseFile()
        {
            InitializeComponent();  
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.SpecialFolder.MyMusic.ToString();
            dialog.Filter = "MP3 files |*.mp3| All files {*.*}|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileNames != null)
                {
                    strURL = dialog.FileNames.ToList<String>();
                    foreach (String str in strURL)
                    {
                        listBoxPlaylist.Items.Add(str);
                    }
                    mediaPlayer.Show();
                    labelName.Text = listBoxPlaylist.Items[0].ToString();
                    mediaPlayer.windowsMediaPlayer.URL = listBoxPlaylist.Items[0].ToString();
                    mediaPlayer.windowsMediaPlayer.Ctlcontrols.play();
                }
            }      
        }

        private void listBoxPlaylist_MouseClick(object sender, MouseEventArgs e)
        {
            int index = this.listBoxPlaylist.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                mediaPlayer.windowsMediaPlayer.URL = listBoxPlaylist.Items[index].ToString();
                mediaPlayer.windowsMediaPlayer.Ctlcontrols.play();
                labelName.Text = listBoxPlaylist.Items[0].ToString();
            }
        }
    }
}
