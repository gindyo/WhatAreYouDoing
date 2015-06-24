namespace WhatAreYouDoing.Factories
{
    public interface IDataSourceFactory
    {
        IWAYDDatasource GetCurrent();
        void Cleanup();
    }
}