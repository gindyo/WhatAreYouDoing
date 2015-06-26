namespace WhatAreYouDoing.Interfaces
{
    public interface IModelFactory
    {
        IEntry NewEntry(IEntry e = null);
        IUIEntry NewUIEntry(IEntry entry = null);
        ISetting NewSetting(ISetting setting = null);
    }
}