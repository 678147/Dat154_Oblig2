using System;
using System.Timers;

namespace SolarSystemApp
{
    public class EventController
    {
        private System.Timers.Timer timer;
        public event EventHandler DoTick;
        private double elapsedTime;
        private double currentSpeed; 
        private const double minSpeed = 10;  
        private const double maxSpeed = 5000; 
        private const double speedStep = 10;  

        public EventController(double initialSpeed)
        {
            this.currentSpeed = initialSpeed;
            timer = new System.Timers.Timer(currentSpeed);
            timer.Elapsed += OnTimedEvent;
            elapsedTime = 0;
        }

        public void Start() => timer.Start();
        public void Stop() => timer.Stop();

        public void SetSpeed(bool increase)
        {
            if (increase)
                currentSpeed = Math.Max(minSpeed, currentSpeed - speedStep); 
            else
                currentSpeed = Math.Min(maxSpeed, currentSpeed + speedStep); 

            timer.Interval = currentSpeed;
            Console.WriteLine($"New Speed: {currentSpeed}ms per tick"); 
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            elapsedTime += 1;
            DoTick?.Invoke(this, EventArgs.Empty);
        }

        public double GetElapsedTime() => elapsedTime;
    }
}
