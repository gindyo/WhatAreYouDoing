using System;

namespace WhatAreYouDoing.Persistance
{
    public interface IEntry
    {
        long Id { get; }
        string Value { get; set; }
        DateTime Time { get; }
    }
}