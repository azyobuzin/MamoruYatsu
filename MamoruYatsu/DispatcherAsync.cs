using System;
using System.Windows.Threading;

namespace MamoruYatsu
{
    static class DispatcherAsync
    {
        public static void Delay(TimeSpan delay, Action action)
        {
            var timer = new DispatcherTimer(DispatcherPriority.Normal, App.Current.Dispatcher);
            timer.Interval = delay;
            timer.Tick += (_, __) =>
            {
                timer.Stop();
                action();
            };
            timer.Start();
        }
    }
}
