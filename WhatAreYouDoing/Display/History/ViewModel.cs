using System;
using System.Collections.Generic;
using System.Linq;
using WhatAreYouDoing.BaseClasses;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.UIModels;

namespace WhatAreYouDoing.Display.History
{
    

    public class ViewModel : BaseClasses.ViewModel, IHistoryViewModel
    {
        private IViewModelContext _context;
        private DateTime _selectedDate;

        public IViewModelContext Context
        {
            get { return _context; }
            set
            {
                _context = value;
                SelectedDate = DateTime.Now;
            }
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

        public List<UIEntry> Entries { get; set; }
    }
}