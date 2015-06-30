using System.Linq;
using WhatAreYouDoing.Interfaces;

ing WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance;

namespace WhatAreYouDoing.BaseClasses
{
    public abstract class Context
    {
        protected readonly IWAYDDatasource _datasource;
        private IModelFactory _modelFactory;

        public Context(IDataSourceFactory datasourceFactory, IModelFactory modelFactory)
        {
            _datasource = datasourceFactory.GetCurrent();
            _modelFactory = modelFactory;
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