using System;
using System.Collections.Generic;
using System.Linq;
using WhatAreYouDoing.BaseClasses;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.UIModels;
using WhatAreYouDoing.Utilities;

namespace WhatAreYouDoing.Display.Main
{
    public class ViewModel : BaseClasses.ViewModel
    {
        private List<IUIEntry> _entries;
        private IEntry _entry;
        private double _interval;
        private IViewModelContext _viewModelContext;

        #region Windsor injected

        public Settings.ViewModel SettingsViewModel { get; set; }
        public History.ViewModel HistoryViewModel { get; set; }

        #endregion

        public IViewModelContext ViewModelContext
        {
            get { return _viewModelContext; }
            set
            {
                _viewModelContext = value;
                _entry = value.GetCurrentEntry();
                Entries = value.GetEntriesForDate(DateTime.Now).ToList();
            }
        }

        public List<IUIEntry> Entries
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
    }
}