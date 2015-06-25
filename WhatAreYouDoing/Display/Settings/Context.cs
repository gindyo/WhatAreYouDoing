using System;
using System.Linq;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Utilities;

namespace WhatAreYouDoing.Display.Settings
{
    public class Context : BaseClasses.Context
    {
        private Scheduler _scheduler;
        public Context(IDataSourceFactory datasourceFactory, Scheduler scheduler) : base(datasourceFactory)
        {
            _scheduler = scheduler;
        }

        public ISetting GetInterval()
        {
            var interval = _datasource.GetSetting((int) Setting.Interval);
            if (interval != null)
                return interval ;
            interval =  _datasource.GetSetting();
            interval.Key = (int)Setting.Interval;
            interval.Value = 15;
            return interval;

        }

        public void SaveInterval(ISetting interval)
        {
            _scheduler.Interval = Convert.ToDouble(interval.Value);
            base._datasource.Save(interval);
        }
    }
}