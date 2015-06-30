using System;
using System.Collections.Generic;
using System.Linq;
using WhatAreYouDoing.Interfaces;

sing WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.UIModels;
usingBaseClasses.ViewModelities;

namespace WhatAreYouDoing.Display.Main
{
    public class ViewModel : BaseClasses.ViewModel
    {
        private List<IUIEntry> _entries;
        private 

        #region Windsor injected

        public Settings.ViewModel SettingsViewModel { get; set; }
        public History.ViewModel HistoryViewModel { get; set; }

        #endregionel { get; set; }
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
                SetLastEntryDuration();
            }
        }

        private void SetLastEntryDuration()
        {
            if (Entries.Any())
            {
                IUIEntry lastEntry = Entries.Last();
                lastEntry.Duration = DateTime.Now - lastEntry.Time;
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