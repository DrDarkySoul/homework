using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP3_Player
{
    public partial class Form1 : Form
    {
        private string _command;
        private bool isOpen;
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, 
                                                 int iReturnLength, IntPtr hwndCallback);
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.SpecialFolder.MyMusic.ToString();
            dialog.Filter = "MP3 files |*.mp3| All files {*.*}|*.*";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = dialog.FileName.ToString();
            }
        }

        public void Play(bool loop)
        {
            if(isOpen)
            {
                _command = "play MediaFile";
                if (loop)
                {
                    _command += " REPEAT";
                }
                mciSendString(_command, null, 0, IntPtr.Zero);
            }
        }

        public void OpenPlayer(string strFileName)
        {
            _command = "open \"" + strFileName + "\" type mpegvideo alias MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
            isOpen = true;
        }

        public void ClosePlayer()
        {
            _command = "close MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
            isOpen = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.OpenPlayer(this.textBox1.Text);
                this.Play(false);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClosePlayer();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
