using System;
using System.Collections.Generic;
using System.Linq;
using WhatAreYouDoing.BaseClasses;
using WhatAreYouDoing.Main;

namespace WhatAreYouDoing.History
{
    public interface IHistoryViewModel
    {
        DateTime SelectedDate { get; set; }
        List<UIEntry> Entries { get; set; }
    }
    public class ViewModel : BaseViewModel, IHistoryViewModel
    {
        private DateTime _selectedDate;
        private List<UIEntry> _entries;
        private IViewModelContext _context;

        public IViewModelContext Context
        {
            get { return _context; }
            set
            {
                _context = value;
                SelectedDate = DateTime.Now;
            }
        }

        public ViewModel()
        {
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                Entries =
                    Context.GetAllEntries().Where(e => e.Time.Date == value.Date).Select(e => new UIEntry(e)).ToList();
                OnPropertyChanged();
                OnPropertyChanged("Entries");
            }
        }

        public List<UIEntry> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }
    }
}