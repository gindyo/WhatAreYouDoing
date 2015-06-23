using Volante;

namespace WhatAreYouDoing.Persistance
{
    public class DatabaseRoot : Persistent
    {
        public IIndex<long, Entry> EntryIndex;
    }
}