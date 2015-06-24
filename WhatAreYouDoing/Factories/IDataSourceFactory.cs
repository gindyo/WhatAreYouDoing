using System;
using System.Linq;
using System.Windows.Documents;
using Volante;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.Factories
{
    public interface IDataSourceFactory
    {
        IWAYDDatasource GetCurrent();
        void Cleanup();
    }

    public interface IWAYDDatasource : IDatabase, IDisposable
    {
        IQueryable<IEntry> GetAll();
        void Save(IEntry currentEntry);
        IEntry GetEntry(int id);
        IEntry GetEntry();
    }
}