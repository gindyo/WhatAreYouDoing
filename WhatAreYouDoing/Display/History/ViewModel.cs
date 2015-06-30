using System;
using System.Collections.Generic;
using System.Linq;
using WhatAreYouDoing.Interfaces;


using WhatAreYouDoing.BaseClasses;
using WhatAreYouDoing.Interfaces;
usBaseClasses.ViewModelbjectFactory;
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
                Entries = Context.GetEntriesForDate(value.Date).ToList();
                OnPropertyChanged();
                OnPropertyChanged("Entries");
            }
        }

        public List<IUIEntry> Entries { get; set; }
    }
}