using System;
using System.Collections.Generic;
using System.Windows;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing
{
    public class MainWindowViewModel
    {
        private Entry _entry;
        private string _value;
        private List<Entry> _entries;

        public MainWindowViewModel()
        {
            _entry = new Entry();
            Entries = MyDatabaseFactory.Current().GetAll();
        }

        public List<Entry> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }

        public string Value
        {
            get { return   _value; }
            set
            {
                _entry.Value = value;
                _entry.Time = DateTime.Now;
                _entry.Save();

            }
        }

        public void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }
    }
}
