using Volante;
using WhatAreYouDoing.Persistance.Models;

namespace WhatAreYouDoing.Persistance
{
    public class DatabaseRoot : Persistent
    {
        public IIndex<long, Entry> EntryIndex;
        public IIndex<int, WAYDSetting> SettingsIndex;
    }

}