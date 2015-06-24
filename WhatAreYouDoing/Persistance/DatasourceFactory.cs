using WhatAreYouDoing.Factories;

namespace WhatAreYouDoing.Persistance
{
    public class DatasourceFactory : IDataSourceFactory
    {
        private const string _fileName = "database.dbs";
        private static EntryDatabase _database;
        private readonly bool _inMemory;

        public DatasourceFactory(bool inMemory)
        {
            _inMemory = inMemory;
        }

        public static string FileName
        {
            get { return _fileName; }
        }

        public IWAYDDatasource GetCurrent()
        {
            
            EntryDatabase db = _database ?? (_database = new EntryDatabase(_fileName, _inMemory));
            if (!db.IsOpened)
                if (_inMemory)
                    db.Open(_fileName, 0);
                else
                    db.Open(_fileName);
            return db;
        }

        public void Cleanup()
        {
            if (_database.IsOpened) _database.Close();
            _database = null;
        }

   }
}