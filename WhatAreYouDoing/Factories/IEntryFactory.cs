using System.Linq;
using WhatAreYouDoing.Main;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.Factories
{
    public interface IEntryFactory
    {
        IEntry GetEntry();
        IEntry GetEntry(long id);
        IQueryable<IEntry> GetAll();
    }
}