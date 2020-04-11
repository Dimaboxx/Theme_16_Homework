using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Example_1219Wpf2
{
    public class TimeProvider: INotifyPropertyChanged
    {
        DispatcherTimer timer;

        public event PropertyChangedEventHandler PropertyChanged;

        string sTime;

        public string Time
        {
            get { return sTime; }
            set
            {
                sTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Time)));
            }
        }

        public TimeProvider()
        {
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 1)
            };

            sTime = String.Empty;

            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Time = DateTime.Now.ToLongTimeString();
        }
    }
}
