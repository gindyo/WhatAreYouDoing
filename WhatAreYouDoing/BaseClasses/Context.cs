using System.Collections.Generic;
using System.Linq;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.BaseClasses
{
    public abstract class Context
    {
        protected readonly IWAYDDatasource _datasource;

        public Context(IDataSourceFactory datasourceFactory)
        {
            _datasource = datasourceFactory.GetCurrent();
        }

        public IQueryable<IEntry> GetAllEntries()
        {
            return _datasource.GetAllEntries();
        }
        protected IQueryable<ISetting> GetAllSettings()
        {
            return _datasource.GetAllSettings();
        }
    }
}