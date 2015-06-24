using System.Collections.Generic;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.Contexts
{
    public interface IMainWindowViewModelContext
    {
        IEntry GetCurrentEntry();
        List<IEntry> GetAllEntries();
        void SaveCurrentEntry();
        void Close();
        List<IEntry> GetTodaysEntries();
    }
}