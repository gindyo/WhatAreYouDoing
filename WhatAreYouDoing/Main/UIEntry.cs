using System;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.Main
{
    public class UIEntry
    {
        private readonly IEntry entry;

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
            return  Value == other.Value && Time == other.Time;
        }
    }
}