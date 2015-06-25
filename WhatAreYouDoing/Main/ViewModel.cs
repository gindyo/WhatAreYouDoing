using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WhatAreYouDoing.BaseClasses;
using WhatAreYouDoing.History;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.Utilities;

namespace WhatAreYouDoing.Main
{
    public class ViewModel : BaseViewModel
    {
        private List<UIEntry> _entries;
        private IEntry _entry;
        private double _interval;
        private IViewModelContext _viewModelContext;

        public Scheduler Scheduler { get; set; }

        public IViewModelContext ViewModelContext
        {
            get { return _viewModelContext; }
            set
            {
                _viewModelContext = value;
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
                _viewModelContext.SaveCurrentEntry();
            }
        }

        public void CloseWindow()
        {
            _viewModelContext.Close();
        }

        public IHistoryViewModel HistoryViewModel { get; set; }
       
    }
}