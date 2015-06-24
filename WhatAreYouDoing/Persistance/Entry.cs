using System;
using Volante;

namespace WhatAreYouDoing.Persistance
{
    public class Entry : Persistent, IEntry
    {
        private string _value;

        public long Id
        {
            get { return Oid; }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                this.Store();
            }
        }

        public DateTime Time { get; set; }
    }
}