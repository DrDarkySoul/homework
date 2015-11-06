using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;                              //Именно это пространство имен поддерживает многопоточность

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace WindowsFormsApplication1
{
    public delegate void ChangeUI(Mat matrix);
    public delegate void ChangeUI_(Image<Gray,byte> image);  //пытаюсь выводить в новом потоке изображение  в imagebox чтобы не было задержек в связи с обработко изображения


    public partial class Form1 : Form
    {
        private Capture _capture = null;        //берет картинку из потока вебки как изображение
        private bool _captureInProgress;        //проверяет идет ли поток
        private bool isDetected = false;        // будет проверять есть ли изображение метки

        private Gray red_color_min = new Gray();        //для разбиения изображения по цветам
        private Gray red_color_max = new Gray();
        private Gray blue_color_min = new Gray();
        private Gray blue_color_max = new Gray();
        private Gray green_color_min = new Gray();
        private Gray green_color_max = new Gray();

        private int surrounding = 25;
     //   private int count_frames = 0;               // количество кадров или точек в движении


        public Form1()
        {
            InitializeComponent();
            CvInvoke.UseOpenCL = false;
            try
            {
                _capture = new Capture(0); // 0 - встроенная камера, 1 - подключенная, -1 - любая доступная
                _capture.ImageGrabbed += ProcessFrame;
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ProcessFrame(object sender, EventArgs arg)
        {
            _capture.FlipHorizontal = true;                                    // Переворачиваем изображение относительно оси У
            Mat ImageMatrix = new Mat();                                       //Матрица, которую вы забираем из потока камеры
            _capture.Retrieve(ImageMatrix, 0);
            Image<Bgr, byte> ImageFrameBGR = ImageMatrix.ToImage<Bgr, byte>();
            Image<Gray, byte> ImageFrameGray = RGBFilter(ImageFrameBGR,
                                          red_color_min, red_color_max,        //Фильтрация на пороговые значения цвета 
                                          green_color_min, green_color_max,
                                          blue_color_min, blue_color_max);
            labelDebagLog.Text = "Width(X coord.) - " + ImageFrameGray.Width.ToString() +
                          "       Heigth(Y coord.) - " + ImageFrameGray.Height.ToString();
            ImageFrameGray = MassCenter(ImageFrameGray);
            Display(ImageMatrix, ImageFrameGray);                    ///<--------------------- отображение
        }

        void Display(Mat imgMat, Image<Gray, byte> imgGray)                 //Функция потока, передаем параметр
        {
            moutionImageBox.Image = imgGray;
            imageBoxColor.Image = imgMat;                                   //отображение
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {                                 //остановка записи
                    btnStart.Text = "Start Capture";
                    _capture.Pause();
                }
                else
                {                                //запуск записи
                    btnStart.Text = "Stop";
                    _capture.Start();
                }
                _captureInProgress = !_captureInProgress;
            }
        }

        private void ReleaseData()
        {
            if (_capture != null)
                _capture.Dispose();
        }

        private Image<Gray, byte> MassCenter(Image<Gray, byte> input)  //ищем центр масс
        {
            int count = 0;
            float center_x = 0;
            float center_y = 0;
            for (int i = 0; i < input.Height; i++)       //пробегаемся по пикселям
            {
                for (int j = 0; j < input.Width; j++)
                {
                    if (input.Data[i, j, 0] == 255)    // суммируем координаты белых пикселей
                    {
                        center_x += j;
                        center_y += i;
                        count++;                     // и их количество
                    }
                }
            }
            if (count > 1000)
            {
                isDetected = true;
                center_x = center_x / count;           //находим координаты центра масс
                center_y = center_y / count;
                input = RectPaint(input, (int)center_x, (int)center_y, count);  //рисуем прямоугольник
            }
            else
                isDetected = false;
            labelCenter.Text = "X: " + center_x.ToString() +
                              " Y: " + (center_y).ToString() +
                              " count: " + count.ToString();
            return input;
        }

        private Image<Gray, byte> RectPaint(Image<Gray, byte> input, int x, int y, int count)
        {
            if (count >= 2000)
                count = (int)count / 50;
            else
                count = 80;
            input.Draw(new Rectangle((int)x - count / 2, (int)y - count / 2, count, count), new Gray(150), 2);
            return input;
        }

        private Image<Gray, byte> RGBFilter(Image<Bgr, byte> input, //разбиваем на каналы и фильтруем по цвету
                                            Gray Rmin, Gray Rmax,
                                            Gray Gmin, Gray Gmax,
                                            Gray Bmin, Gray Bmax)
        {
            Image<Gray, byte>[] channels = input.Split();
            channels[0] = channels[0].InRange(Bmin, Bmax);
            channels[1] = channels[1].InRange(Gmin, Gmax);
            channels[2] = channels[2].InRange(Rmin, Rmax);
            Image<Gray, byte> result = channels[0].And(channels[1]);
            result = result.And(channels[2]);
            return result;
        }


        public void trackBarRed_Scroll(object sender, EventArgs e)
        {
            red_color_min = new Gray((double)trackBarRed.Value - surrounding);
            red_color_max = new Gray((double)trackBarRed.Value + surrounding);
            labelRed.Text = "Red:" + red_color_min.ToString() + " - " + red_color_max.ToString();
        }

        private void trackBarGreen_Scroll(object sender, EventArgs e)
        {
            green_color_min = new Gray((double)trackBarGreen.Value - surrounding);
            green_color_max = new Gray((double)trackBarGreen.Value + surrounding);
            labelGreen.Text = "Green:" + green_color_min.ToString() + " - " + green_color_max.ToString();
        }

        private void trackBarBlue_Scroll(object sender, EventArgs e)
        {
            blue_color_min = new Gray((double)trackBarBlue.Value - surrounding);
            blue_color_max = new Gray((double)trackBarBlue.Value + surrounding);
            labelBlue.Text = "Blue:" + blue_color_min.ToString() + " - " + blue_color_max.ToString();
        }

        private void radioButtonOrange_CheckedChanged(object sender, EventArgs e)
        {
            trackBarRed.Value = 230;
            trackBarGreen.Value = 55;
            trackBarBlue.Value = 25;
            trackBarBlue_Scroll(sender, e);
            trackBarGreen_Scroll(sender, e);
            trackBarRed_Scroll(sender, e);
        }

        private void radioButtonSkin_CheckedChanged(object sender, EventArgs e)
        {
            trackBarRed.Value = 180;
            trackBarGreen.Value = 70;
            trackBarBlue.Value = 65;
            trackBarBlue_Scroll(sender, e);
            trackBarGreen_Scroll(sender, e);
            trackBarRed_Scroll(sender, e);
        }

        private void radioButtonOther_CheckedChanged(object sender, EventArgs e)
        {
            trackBarRed.Value = 25;
            trackBarGreen.Value = 25;
            trackBarBlue.Value = 25;
            trackBarBlue_Scroll(sender, e);
            trackBarGreen_Scroll(sender, e);
            trackBarRed_Scroll(sender, e);
        }

        private void textBoxRed_TextChanged(object sender, EventArgs e) //<------не забудь написать исключения
        {
            surrounding = (int)(int.Parse(textBoxRed.Text) / 2);
            trackBarRed.Minimum = surrounding;
            trackBarRed.Maximum = 255 - surrounding;
        }

        private void textBoxGreen_TextChanged(object sender, EventArgs e)
        {
            surrounding = (int)(int.Parse(textBoxGreen.Text) / 2);
            trackBarGreen.Minimum = surrounding;
            trackBarGreen.Maximum = 255 - surrounding;
        }

        private void textBoxBlue_TextChanged(object sender, EventArgs e)
        {
            surrounding = (int)(int.Parse(textBoxBlue.Text) / 2);
            trackBarBlue.Minimum = surrounding;
            trackBarBlue.Maximum = 255 - surrounding;
        }

    }
}
