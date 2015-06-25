using System;
using System.Linq;
using Volante;

namespace WhatAreYouDoing.Interfaces
{
    public interface IWAYDDatasource : IDatabase, IDisposable
    {
        IQueryable<IEntry> GetAllEntries();
        IQueryable<ISetting> GetAllSettings();
        void Save(IEntry currentEntry);
        void Save(ISetting currentEntry);
        IEntry GetEntry(long id);
        IEntry GetEntry();
        ISetting GetSetting();
        ISetting GetSetting(int id);
    }
}