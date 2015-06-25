using System;
using System.Linq;
using Volante;

namespace WhatAreYouDoing.Interfaces
{
    public interface IWAYDDatasource : IDatabase, IDisposable
    {
        IQueryable<IEntry> GetAll();
        void Save(IEntry currentEntry);
        IEntry GetEntry(long id);
        IEntry GetEntry();
    }
}