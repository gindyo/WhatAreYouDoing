using System;
using WhatAreYouDoing.Interfaces;

namespace WhatAreYouDoing.Display.Settings
{
    public class ViewModel : BaseClasses.ViewModel
    {
        private Context _context;
        private ISetting _intervalSetting;

        public Context Context
        {
            get { return _context; }
            set
            {
                _context = value;
                _intervalSetting = _context.GetInterval();
                OnPropertyChanged("Interval");
            }
        }

        public double Interval
        {
            get { return Convert.ToDouble(_intervalSetting.Value); }
            set
            {
                _intervalSetting.Value = value;
                Context.SaveInterval(_intervalSetting);
            }
        }
    }
}