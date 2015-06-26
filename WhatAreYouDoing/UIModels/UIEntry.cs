using System;
using WhatAreYouDoing.Interfaces;

namespace WhatAreYouDoing.UIModels
{
    public class UIEntry : IUIEntry
    {
        private readonly IEntry entry;
        private TimeSpan _duration;

        public UIEntry()
        {
        }

        public UIEntry(IEntry entry)
        {
            this.entry = entry;
        }

        public string Value
        {
            get { return entry.Value; }
            set { entry.Value = value; }
        }

        public DateTime Time
        {
            get { return entry.Time; }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public IEntry ToInterface()
        {
            return entry;
        }

        public override string ToString()
        {
            return "value: " + Value + ", time: " + Time;
        }

        public override bool Equals(object obj)
        {
            var other = obj as UIEntry;
            if (null == other)
                return false;
            return Value == other.Value && Time == other.Time;
        }
    }
}