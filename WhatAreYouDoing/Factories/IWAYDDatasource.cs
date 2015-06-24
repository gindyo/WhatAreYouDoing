using System;
using System.Linq;
using Volante;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.Factories
{
    public interface IWAYDDatasource : IDatabase, IDisposable
    {
        IQueryable<IEntry> GetAll();
        void Save(IEntry currentEntry);
        IEntry GetEntry(int id);
        IEntry GetEntry();
    }
}