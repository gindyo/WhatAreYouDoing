using System;

namespace WhatAreYouDoing.Interfaces
{
    public interface IUIEntry
    {
        string Value { get; set; }
        DateTime Time { get; }
        TimeSpan Duration { get; set; }
    }
}