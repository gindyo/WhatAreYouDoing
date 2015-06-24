using System.Collections.Generic;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.Contexts
{
    public interface IMainWindowContext
    {
        IEntry GetCurrentEntry();
        List<IEntry> GetAllEntries();
        void SaveCurrentEntry();
        void Close();
    }
}