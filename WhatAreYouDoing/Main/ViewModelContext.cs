using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WhatAreYouDoing.BaseClasses;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.Main
{
    public class ViewModelContext : BaseViewModelContext, IViewModelContext
    {
        private readonly IEntry _currentEntry;

        public ViewModelContext(IDataSourceFactory datasourceFactory) : base(datasourceFactory)
        {
            _currentEntry = _datasource.GetEntry();
        }

        public IEntry GetCurrentEntry()
        {
            return _currentEntry;
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