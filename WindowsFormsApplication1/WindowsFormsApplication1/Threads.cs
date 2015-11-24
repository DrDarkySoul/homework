using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WindowsFormsApplication1
{
    public class Threads /// класс для создания дополнительных потоков
    {
        private ChangeUI _ui;
        private List<Thread> _theads;

        public Threads(ChangeUI ui)
        {
            _ui = ui;
        }

        public void Run(int countThreads)
        {
            _theads = new List<Thread>();
            var webCamOutput = new Thread(Output);
            webCamOutput.IsBackground = true;
            _theads.Add(webCamOutput);
            _theads[0].Start();
        }

        public void Abort()
        {
            foreach(Thread thread in _theads)
            {
                thread.Abort();
            }
            _theads.Clear();
        }
        public void Output()
        {
            //
        }
    }


    /*  private void setMatrixToContainer(ImageBox img, Mat matrix)
        {
            if (InvokeRequired)
                Invoke(new Action(() => setMatrixToContainer(img, matrix)));
            else
                img.Image = matrix;
        }

     * 
     * 
     * 
        private void setImageToContainer(ImageBox img, Image<Gray,byte> image)
        {
            if (InvokeRequired)
                Invoke(new Action(() => setImageToContainer(img, image)));
            else
                img.Image = image;
        }*/


    /* private void setLabelValue(Label l, string value)
        {
            if (InvokeRequired)
               Invoke(new Action(() => setLabelValue(l, value)));
            else
                l.Text = value;
       }*/

    /* labelDebagLog.Text = "Width(X coord.) - " + imageFrameGray.Width.ToString() +
                          "       Heigth(Y coord.) - " + imageFrameGray.Height.ToString();*/

    /*  private int _frames = 0;
        private long _currentTime;
        private long _fps;
        private int _dFps;      */
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
