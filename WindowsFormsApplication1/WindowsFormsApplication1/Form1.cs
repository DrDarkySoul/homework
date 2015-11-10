﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;                                      //Именно это пространство имен поддерживает многопоточность

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
        private Capture _capture = null;                      //берет картинку из потока вебки как изображение
        private bool _captureInProgress;                      //проверяет идет ли поток
        private bool _isDetected = false;                     // будет проверять есть ли изображение метки

        private Gray red_color_min = new Gray();              //для разбиения изображения по цветам
        private Gray red_color_max = new Gray();
        private Gray blue_color_min = new Gray();
        private Gray blue_color_max = new Gray();
        private Gray green_color_min = new Gray();
        private Gray green_color_max = new Gray();

        private int _surrounding = 25;
        private int _count_frames = 0;                        // количество кадров или точек в движении

    /*  private int _frames = 0;
        private long _currentTime;
        private long _fps;
        private int _dFps;      */

        List<Double> x_coord = new List<Double>();
        List<Double> y_coord = new List<Double>();
        private Double center_x = 0;
        private Double center_y = 0;

        private Double x_begining = 0;
        private Double y_begining = 0;
        private Double x_ending = 0;
        private Double y_ending = 0;


        public Form1()
        {
            InitializeComponent();
            CvInvoke.UseOpenCL = false;
            radioButtonOrange.Checked = true;
            try
            {
                _capture = new Capture(0);                      // 0 - встроенная камера,  
                _capture.ImageGrabbed += ProcessFrame;          // 1 - подключенная,
            }                                                   // -1 - любая доступная
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ProcessFrame(object sender, EventArgs arg)
        {
            _capture.FlipHorizontal = true;                      // Переворачиваем изображение относительно оси У
            Mat imageMatrix = new Mat();                         //Матрица, которую мы забираем из потока камеры
            _capture.Retrieve(imageMatrix, 0);
            Image<Bgr, byte> imageFrameBGR = imageMatrix.ToImage<Bgr, byte>();
            Image<Gray, byte> imageFrameGray = RGBFilter(imageFrameBGR,
                                          red_color_min, red_color_max,//Фильтрация на пороговые значения цвета 
                                          green_color_min, green_color_max,
                                          blue_color_min, blue_color_max);
           /* labelDebagLog.Text = "Width(X coord.) - " + imageFrameGray.Width.ToString() +
                          "       Heigth(Y coord.) - " + imageFrameGray.Height.ToString();*/
            imageFrameGray = MassCenter(imageFrameGray);
            Display(imageMatrix, imageFrameGray);                //<--------------------- отображение
            
            if (_isDetected)                                     // Ищем движения
            {
                x_coord.Add(center_x);
                y_coord.Add(center_y);
                if (_count_frames == 0)
                {
                    x_begining = center_x;
                    y_begining = center_y;
                }
                x_ending = center_x;
                y_ending = center_y;
                _count_frames++;
            }
            else
            {
                if (_count_frames>=20)
                {
                    Line_func line = new Line_func(x_coord, y_coord);
                    labelFunc.Text = line.coord_a_.ToString() + "X + " + line.coord_b_.ToString();

                    if (Math.Abs(x_ending - x_begining) > 200 && Math.Abs(line.coord_a_) < 1)
                    {
                        if (x_begining < 215)
                            labelType.Text = " Горизонтальная линия из левого края";
                        else if (x_begining > 430)
                            labelType.Text = " Горизонтальная линия из правого края";
                        else
                        {
                            if (x_ending - x_begining > 0)
                                labelType.Text = " Горизонтальная линия из середины вправо";
                            else if (x_ending - x_begining < 0)
                                labelType.Text = " Горизонтальная линия из середины влево";
                        }
                    }
                    else if (Math.Abs(y_ending - y_begining) > 350 && Math.Abs(line.coord_a_) > 1)
                    {
                        if (y_ending - y_begining > 0)
                            labelType.Text = " Вертикальная линия сверху вниз";
                        else if (y_ending - y_begining < 0)
                            labelType.Text = " Вертикальная линия снизу вверх";
                    }
                    else
                        labelType.Text = "";
                }
                labelDebagLog.Text = "("+x_begining.ToString()+" ; "+y_begining.ToString()+") - ("+x_ending+" ; "+y_ending+")";
                _count_frames = 0;
                x_coord.Clear();
                y_coord.Clear();
            }



            // Подсчёт FPS
         /*   _frames++;
            long _thisTime;
            _thisTime = DateTime.Now.Ticks;
            if ((_thisTime - _currentTime) > 300 * 10000)
            {
                _fps = (_dFps * 10000000 / (_thisTime - _currentTime));
                labelFPS.Text = "FPS: " + _fps.ToString();
                setLabelValue(labelFPS, "FPS: " + _fps.ToString());
                _dFps = 1;
                _currentTime = _thisTime;
                labelFrames.Text ="Frames: " + _frames.ToString();
                setLabelValue(labelFrames, "Frames: " + _frames.ToString());
            }
            else
            {
                _dFps++;
            }
            setImageToContainer(moutionImageBox, imageFrameGray);
            setMatrixToContainer(imageBoxColor, imageMatrix);*/
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
            center_x = 0;
            center_y = 0;
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
            if (count > 600)
            {
                _isDetected = true;
                center_x = center_x / count;           //находим координаты центра масс
                center_y = center_y / count;
                input = RectPaint(input, (int)center_x, (int)center_y, count);  //рисуем прямоугольник
            }
            else
                _isDetected = false;
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

       /* private void setLabelValue(Label l, string value)
        {
            if (InvokeRequired)
               Invoke(new Action(() => setLabelValue(l, value)));
            else
                l.Text = value;
       }*/

        void Display(Mat imgMat, Image<Gray, byte> imgGray)                 //Функция потока, передаем параметр
        {
            moutionImageBox.Image = imgGray;
            imageBoxColor.Image = imgMat;                                   //отображение
            
        }

      /*  private void setMatrixToContainer(ImageBox img, Mat matrix)
        {
            if (InvokeRequired)
                Invoke(new Action(() => setMatrixToContainer(img, matrix)));
            else
                img.Image = matrix;
        }

        private void setImageToContainer(ImageBox img, Image<Gray,byte> image)
        {
            if (InvokeRequired)
                Invoke(new Action(() => setImageToContainer(img, image)));
            else
                img.Image = image;
        }*/


        public void trackBarRed_Scroll(object sender, EventArgs e)
        {
            red_color_min = new Gray((double)trackBarRed.Value - _surrounding);
            red_color_max = new Gray((double)trackBarRed.Value + _surrounding);
            labelRed.Text = "Red:" + red_color_min.ToString() + " - " + red_color_max.ToString();
        }

        private void trackBarGreen_Scroll(object sender, EventArgs e)
        {
            green_color_min = new Gray((double)trackBarGreen.Value - _surrounding);
            green_color_max = new Gray((double)trackBarGreen.Value + _surrounding);
            labelGreen.Text = "Green:" + green_color_min.ToString() + " - " + green_color_max.ToString();
        }

        private void trackBarBlue_Scroll(object sender, EventArgs e)
        {
            blue_color_min = new Gray((double)trackBarBlue.Value - _surrounding);
            blue_color_max = new Gray((double)trackBarBlue.Value + _surrounding);
            labelBlue.Text = "Blue:" + blue_color_min.ToString() + " - " + blue_color_max.ToString();
        }

        private void radioButtonOrange_CheckedChanged(object sender, EventArgs e)
        {
            trackBarRed.Value = 230;
            trackBarGreen.Value = 65;
            trackBarBlue.Value = 25;
            trackBarBlue_Scroll(sender, e);
            trackBarRed_Scroll(sender, e);
            textBoxGreen.Text = "100";
            trackBarGreen_Scroll(sender, e);
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
            try
            {
                _surrounding = (int)(int.Parse(textBoxRed.Text) / 2);
                if (_surrounding > 255 || _surrounding < 0)
                    throw new FormatException();
            }
            catch(FormatException exept)
            {
                labelDebagLog.Text = exept.ToString();
                _surrounding = 25;
            }
            trackBarRed.Minimum = _surrounding;
            trackBarRed.Maximum = 255 - _surrounding;
        }

        private void textBoxGreen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _surrounding = (int)(int.Parse(textBoxGreen.Text) / 2);
                if (_surrounding > 255 || _surrounding < 0)
                    throw new FormatException();
            }
            catch (FormatException exept)
            {
                labelDebagLog.Text = exept.ToString();
                _surrounding = 25;
            }
            trackBarGreen.Minimum = _surrounding;
            trackBarGreen.Maximum = 255 - _surrounding;
        }

        private void textBoxBlue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _surrounding = (int)(int.Parse(textBoxBlue.Text) / 2);
                if (_surrounding > 255 || _surrounding < 0)
                    throw new FormatException();
            }
            catch (FormatException exept)
            {
                labelDebagLog.Text = exept.ToString();
                _surrounding = 25;
            }
            trackBarBlue.Minimum = _surrounding;
            trackBarBlue.Maximum = 255 - _surrounding;
        }

    }
}
