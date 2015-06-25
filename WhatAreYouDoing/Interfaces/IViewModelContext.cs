using System.Collections.Generic;

namespace WhatAreYouDoing.Interfaces
{
    public interface IViewModelContext
    {
        IEntry GetCurrentEntry();
        List<IEntry> GetAllEntries();
        void SaveCurrentEntry();
        void Close();
        List<IEntry> GetTodaysEntries();
    }
}