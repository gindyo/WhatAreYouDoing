using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WhatAreYouDoing.Factories;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.Contexts
{
    public class MainWindowVMContext : IMainWindowContext
    {
        private readonly IEntry _currentEntry;
        private readonly IWAYDDatasource _datasource;

        public MainWindowVMContext(IDataSourceFactory datasourceFactory)
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

        public void SaveCurrentEntry()
        {
            _datasource.Save(_currentEntry);
        }

        public void Close()
        {
            Application.Current.MainWindow.Close();
        }
    }
}