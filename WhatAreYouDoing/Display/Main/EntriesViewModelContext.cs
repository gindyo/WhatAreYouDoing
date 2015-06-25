using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WhatAreYouDoing.BaseClasses;
using WhatAreYouDoing.Interfaces;

namespace WhatAreYouDoing.Display.Main
{
    public class EntriesViewModelContext : Context, IViewModelContext
    {
        private readonly IEntry _currentEntry;

        public EntriesViewModelContext(IDataSourceFactory datasourceFactory) : base(datasourceFactory)
        {
            _currentEntry = _datasource.GetEntry();
        }

        public IEntry GetCurrentEntry()
        {
            return _currentEntry;
        }

        public IEnumerable<IEntry> GetTodaysEntries()
        {
            return GetAllEntries().Where(e => e.Time > DateTime.Today).ToList();
        }


        public void SaveCurrentEntry()
        {
            _datasource.Save(_currentEntry);
        }

        public void Close()
        {
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = null;
        }
    }
}