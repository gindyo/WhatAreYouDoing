using System.Collections.Generic;
using System.Linq;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.BaseClasses
{
    public abstract class BaseViewModelContext
    {
        protected readonly IWAYDDatasource _datasource;

        public BaseViewModelContext(IDataSourceFactory datasourceFactory)
        {
            _datasource = datasourceFactory.GetCurrent();
        }

        public List<IEntry> GetAllEntries()
        {
            return _datasource.GetAll().ToList();
        }
    }
}