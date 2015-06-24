using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WhatAreYouDoing.Factories;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.Contexts
{
    public class MainWindowViewModelContext : IMainWindowViewModelContext
    {
        private readonly IEntry _currentEntry;
        private readonly IWAYDDatasource _datasource;

        public MainWindowViewModelContext(IDataSourceFactory datasourceFactory)
        {
            _datasource = datasourceFactory.GetCurrent();
            _currentEntry = _datasource.GetEntry();
        }

        public IEntry GetCurrentEntry()
        {
            return _currentEntry;
        }

        public List<IEntry> GetAllEntries()
        {
            return _datasource.GetAll().ToList();
        }

        public List<IEntry> GetTodaysEntries()
        {
            return _datasource.GetAll().Where(e => e.Time > DateTime.Today).ToList();
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