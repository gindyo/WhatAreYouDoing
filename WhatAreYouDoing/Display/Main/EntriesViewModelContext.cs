using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WhatAreYouDoing.BaseClasses;
using WhatAreYouDoing.Interfaces;

ing WhatAreYouDoing.UIModels;

namespace WhatAreYouDoing.Display.Main
{
    public class EntriesViewModelContext : Context, IViewModelContext
    {
        private readonly IEntry _currentEntry;
        private IModelFactory _modelFactory;

        public EntriesViewModelContext(IDataSourceFactory datasourceFactory, IModelFactory modelFactory) : base(datasourceFactory, modelFactory)
        {
            _currentEntry = _datasource.GetEntry();
        }

        public IEntry GetCurrentEntry()
        {
            return _currentEntry;
        }

        public IEnumerable<IUIEntry> GetEntriesForDate(DateTime date)
        {

            var todaysEntries = GetAllEntries().Where(e => e.Time.Date == date.Date).Select(e=> NewUIEntry(e)).ToList();
            AddDuration(todaysEntries);
            return todaysEntries;
        }

        public IModelFactory ModelFactory
        {
            get { return _modelFactory; }
            set { _modelFactory = value; }
        }

        public IUIEntry NewUIEntry(IEntry entry)
        {
            return _modelFactory.NewUIEntry(entry);
        }

        private void AddDuration(List<IUIEntry> entries)
        {
            entries.Sort((entry, entry1) => entry.Time.CompareTo(entry1.Time));
            for (var i = 0; i < entries.Count; i++)
            {
                if (entries.Count == i + 1) break;
                if (entries[i + 1] != null)
                    entries[i].Duration = entries[i + 1].Time - entries[i].Time;
            }
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