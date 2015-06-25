namespace WhatAreYouDoing.Interfaces
{
    public interface IDataSourceFactory
    {
        IWAYDDatasource GetCurrent();
        void Cleanup();
    }
}