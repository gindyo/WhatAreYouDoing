using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WhatAreYouDoing.Contexts;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IEntry _entry;
        private IMainWindowContext _context;
        private List<UIEntry> _entries;

        public IMainWindowContext Context
        {
            get { return _context; }
            set
            {
                _context = value; 
                _entry = value.GetCurrentEntry();
                Entries = value.GetAllEntries().Select(e=> new UIEntry(e)).ToList();
            }
        }

        public MainWindowViewModel()
        {
        }

        public List<UIEntry> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged();
            }
        }

        public string Value
        {
            get { return _entry.Value; }
            set
            {
                _entry.Value = value;
                _context.SaveCurrentEntry();
            }
        }

        public void CloseWindow()
        {
            _context.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class UIEntry
    {
        private IEntry entry;
        public UIEntry(IEntry entry)
        {
            this.entry = entry;
        }

        public string Value
        {
            get { return entry.Value; }
            set { entry.Value = value; }
        }

        public DateTime Time
        {
            get { return entry.Time; }
        }
    }
}