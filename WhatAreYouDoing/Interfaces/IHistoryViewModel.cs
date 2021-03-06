﻿using System;
using System.Collections.Generic;
using WhatAreYouDoing.UIModels;

namespace WhatAreYouDoing.Interfaces
{
    public interface IHistoryViewModel
    {
        DateTime SelectedDate { get; set; }
        List<IUIEntry> Entries { get; set; }
        IViewModelContext Context { get; set; }
    }
}