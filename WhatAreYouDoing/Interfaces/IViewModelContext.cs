using System;
using System.Collections.Generic;
using System.Linq;

namespace WhatAreYouDoing.Interfaces
{
    public interface IViewModelContext
    {
        IEntry GetCurrentEntry();
        IQueryable<IEntry> GetAllEntries();
        void SaveCurrentEntry();
        void Close();
        IEnumerable<IUIEntry> GetEntriesForDate(DateTime date);
        IModelFactory ModelFactory { get; set; }
        IUIEntry NewUIEntry(IEntry entry);
    }
}