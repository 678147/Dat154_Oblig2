using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SolarSystemApp
{
    public class EventController
    {
        private System.Timers.Timer timer;
        public event EventHandler DoTick;
        private double tEvent;
        private double elapsedTime;
        
        public EventController(double tEvent)
        {
            this.tEvent = tEvent;
            timer = new System.Timers.Timer(tEvent);
            timer.Elapsed += OnTimedEvent;
            elapsedTime = 0;
        }

        public void Start()
        {
            timer.Start();
        }
        public void Stop()
        {
            timer.Stop();
        }
        public void SetSpeed(double speed) 
        {
            timer.Interval = speed;
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            elapsedTime += 1;
            DoTick?.Invoke(this, EventArgs.Empty);
        }
        public double GetElapsedTime()
        {
            return elapsedTime;
        }
    }
}
