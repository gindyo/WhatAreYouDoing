using System;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing
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
    }
}