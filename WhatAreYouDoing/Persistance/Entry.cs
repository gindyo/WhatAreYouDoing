using System;
using Volante;

namespace WhatAreYouDoing.Persistance
{
    public class Entry : Persistent
    {
        public string Value { get; set; }
        public DateTime Time { get; set; }
    }
}