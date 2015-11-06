using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WindowsFormsApplication1
{
    public class Threads
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

        private void Output(Mat matrix, Image<Gray, byte> image)
        {

        }


    }
}
