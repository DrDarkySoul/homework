using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormChooseFile : Form
    {
        static public MediaPlayer mediaPlayer = new MediaPlayer();
        static WMPLib.IWMPPlaylist playlist = mediaPlayer.WindowsMediaPlayer.playlistCollection.newPlaylist("myplaylist");
        static WMPLib.IWMPMedia media;
        static int numOfMusic = 0;

        public FormChooseFile()
        {
            InitializeComponent();
            mediaPlayer.Owner = this;
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
                    foreach (String str in dialog.FileNames)
                    {
                        listBoxPlaylist.Items.Add(str);
                    }
                    foreach (String str in listBoxPlaylist.Items)
                    {
                        media = mediaPlayer.WindowsMediaPlayer.newMedia(str);
                        playlist.appendItem(media);
                        numOfMusic++;
                    }
                    mediaPlayer.WindowsMediaPlayer.currentPlaylist = playlist;
                }
            }
            Thread mainThread = new Thread(() => mediaPlayer.Show());
            mainThread.IsBackground = true;
            mainThread.Start();
            mediaPlayer.WindowsMediaPlayer.Ctlcontrols.play();
            listBoxPlaylist.SelectedIndex = 0;
        }
        public void nextSong()
        {
            mediaPlayer.WindowsMediaPlayer.Ctlcontrols.next();
        }
        public void prevSong()
        {
            mediaPlayer.WindowsMediaPlayer.Ctlcontrols.previous();
        }
        public void playSong()
        {
            mediaPlayer.WindowsMediaPlayer.Ctlcontrols.play();
        }
        public void stopSong()
        {
            mediaPlayer.WindowsMediaPlayer.Ctlcontrols.stop();
        }
        public void voulumeUp()
        {
            int vol = mediaPlayer.WindowsMediaPlayer.settings.volume;
            if (vol <= 80)
                vol = mediaPlayer.WindowsMediaPlayer.settings.volume + 20;
            else
                vol = 100;
            mediaPlayer.WindowsMediaPlayer.settings.volume = vol;
        }
        public void voulumeDown()
        {
            int vol = mediaPlayer.WindowsMediaPlayer.settings.volume;
            if (vol > 20)
                vol = mediaPlayer.WindowsMediaPlayer.settings.volume - 20;
            else
                vol = 0;
            mediaPlayer.WindowsMediaPlayer.settings.volume = vol;
        }
    }
}
