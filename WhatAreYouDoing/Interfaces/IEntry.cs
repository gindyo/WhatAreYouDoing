using System;

namespace WhatAreYouDoing.Interfaces
{
    public interface IEntry
    {
        long Id { get; }
        string Value { get; set; }
        DateTime Time { get; }
    }

    public interface IUIEntry 
    {
        string Value { get; set; }
        DateTime Time { get; }
       TimeSpan Duration { get; set; } 
    }
}