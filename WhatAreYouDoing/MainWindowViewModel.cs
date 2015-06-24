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
        private IMainWindowContext _context;
        private List<UIEntry> _entries;
        private IEntry _entry;
        private double _interval;

        public Scheduler Scheduler { get; set; }

        public IMainWindowContext Context
        {
            get { return _context; }
            set
            {
                _context = value;
                _entry = value.GetCurrentEntry();
                Entries = value.GetTodaysEntries().Select(e => new UIEntry(e)).ToList();
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void CloseWindow()
        {
            _context.Close();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}