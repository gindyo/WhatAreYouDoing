using System;
using System.Timers;

namespace WhatAreYouDoing
{
    public class Scheduler
    {
        private readonly Timer _timer;
        public Scheduler()
        {
            _timer = new Timer();
            _timer.AutoReset = true;
        }

        public double Interval
        {
            get { return _timer.Interval / 1000 / 60; }
            set { _timer.Interval = value * 1000 * 60; }
        }
        public void Repeat(Action act, int i)
        {
            _timer.Interval = i;
            ElapsedEventHandler eh = (sender, args) => act();
            _timer.Elapsed += eh;
            _timer.Start();
        }
    }
}