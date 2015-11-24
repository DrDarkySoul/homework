using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;                                      //Именно это пространство имен поддерживает многопоточность
using System.Diagnostics;

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
        private bool _correction = false;

        private Gray red_color_min = new Gray();              //для разбиения изображения по цветам
        private Gray red_color_max = new Gray();
        private Gray blue_color_min = new Gray();
        private Gray blue_color_max = new Gray();
        private Gray green_color_min = new Gray();
        private Gray green_color_max = new Gray();

        private int _surrounding = 25;
        private int _count_frames = 0;                        // количество кадров или точек в движении

        List<Double> x_coord = new List<Double>();
        List<Double> y_coord = new List<Double>();

        private Double center_x = 0;
        private Double center_y = 0;

        private Stopwatch my_timer = new Stopwatch();

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
            Image<Bgr, byte> imageFrameBGR = imageMatrix.ToImage<Bgr, byte>(); //Преобразование
            Image<Gray, byte> imageFrameGray = RGBFilter(imageFrameBGR,
                                          red_color_min, red_color_max,//Фильтрация на пороговые значения цвета 
                                          green_color_min, green_color_max,
                                          blue_color_min, blue_color_max);

           

            imageFrameGray = MassCenter(imageFrameGray);
            Display(imageMatrix, imageFrameGray);                //<--------------------- отображение

            setLabelValue(labelTimer, my_timer.ElapsedMilliseconds.ToString());

            if (_isDetected)                                     // Ищем движения, если нашли объект
            {
                x_coord.Add(center_x);                           //добавляем в массивы координаты его центра масс
                y_coord.Add(center_y);
                _count_frames++;                                 // увеличиваем счетчик кадров
            }

            else if(x_coord != null)                                    //Если объект не видно
            {
                moutionType(x_coord, y_coord);
            }
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
                my_timer.Reset();
                center_x = center_x / count;           //находим координаты центра масс
                center_y = center_y / count;
                input = RectPaint(input, (int)center_x, (int)center_y, count);  //рисуем прямоугольник
            }
            else
            {
                _isDetected = false;
                my_timer.Start();       //    запуск таймера
            }

            setLabelValue(labelCenter, "X: " + center_x.ToString() +
                                      " Y: " + (center_y).ToString() +
                                      " count: " + count.ToString());
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

        void Display(Mat imgMat, Image<Gray, byte> imgGray)                 //Функция потока, передаем параметр
        {
            moutionImageBox.Image = imgGray;
            imageBoxColor.Image = imgMat;                                   //отображение
        }

        private string moutionType(List<Double> x_c, List<Double> y_c)
        {
            string type = "";
            Double x_beg = x_c[0];
            Double y_beg = y_c[0];
            Double x_end = x_c.Last();
            Double y_end = y_c.Last();
            Double x_max = x_c.Max();
            Double y_max = y_c.Max();
            Double x_min = x_c.Min();
            Double y_min = y_c.Min();
            if (my_timer.ElapsedMilliseconds > 1000)
            {
                my_timer.Reset();

                if (_count_frames >= 20)                        // если он был в кадре не долго 
                {                                               //или не прошло время на корректировку
                    //выключаем таймер
                    _correction = false;                        //отключаем корректировку
                    Line_func line = new Line_func(x_coord, y_coord);
                    labelFunc.Text = line.coord_a_.ToString() + "X + " + line.coord_b_.ToString();

                    if (Math.Abs(x_end - x_beg) > 200 && Math.Abs(line.coord_a_) < 1)
                    {
                        if (x_beg < 215)
                            type = " Горизонтальная линия из левого края";
                        else if (x_beg > 430)
                            type = " Горизонтальная линия из правого края";
                        else
                        {
                            if (x_end - x_beg > 0)
                                type = " Горизонтальная линия из середины вправо";
                            else if (x_end - x_beg < 0)
                                type = " Горизонтальная линия из середины влево";
                        }
                    }
                    else if (Math.Abs(y_end - y_beg) > 350 && Math.Abs(line.coord_a_) > 1)
                    {
                        if (y_end - y_beg > 0)
                            type = " Вертикальная линия сверху вниз";
                        else if (y_end - y_beg < 0)
                            type = " Вертикальная линия снизу вверх";
                    }
                    else
                        type = "";
                }
                else
                {
                    _correction = true;
                }
                if (!_correction)
                {
                    labelDebagLog.Text = "(" + x_beg.ToString() + " ; " + y_beg.ToString() + ") - (" + x_end + " ; " + y_end + ")";
                    _count_frames = 0;
                    my_timer.Reset();
                    x_coord.Clear();
                    y_coord.Clear();
                }
            }
            return type;
        }

        void setLabelValue(Label l, String value)
        {
            if (InvokeRequired)
                Invoke(new Action(() => setLabelValue(l, value)));
            else
                l.Text = value;
        }
        
        private void trackBarRed_Scroll(object sender, EventArgs e)
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
            trackBarRed.Value = trackBarRed.Minimum;
            trackBarGreen.Value = trackBarGreen.Minimum;
            trackBarBlue.Value = trackBarBlue.Minimum;
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



