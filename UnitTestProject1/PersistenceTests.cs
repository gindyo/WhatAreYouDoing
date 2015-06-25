using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Volante;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.Persistance.Models;

namespace WhatAreYouDoingTests
{
    [TestClass]
    public class PersistenceTests
    {
        public IDataSourceFactory DbFactory { get; set; }

        [TestInitialize]
        public void Init()
        {
            DbFactory = new DatasourceFactory(true);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (CurrentDb().IsOpened)
                DbFactory.Cleanup();
            if (File.Exists(DatasourceFactory.FileName))
                File.Delete(DatasourceFactory.FileName);
        }

        [TestMethod]
        public void DatabaseCanBeOpened()
        {
            using (IWAYDDatasource _db = CurrentDb())
            {
                Assert.IsTrue(_db.Root != null);
            }
        }

        private IWAYDDatasource CurrentDb()
        {
            return DbFactory.GetCurrent();
        }

        [TestMethod]
        public void CanInsertEntry()
        {
            using (IWAYDDatasource _db = CurrentDb())
            {
                long initsize = _db.UsedSize;
                var entry = new Entry();
                _db.storeObject(entry);
                long size = _db.UsedSize;
                Assert.IsTrue(size > initsize);
            }
        }

        [TestMethod]
        public void CanRetreiveEntry()
        {
            using (IWAYDDatasource _db = CurrentDb())
            {
                long initsize = _db.UsedSize;
                var entry = new Entry();
                _db.storeObject(entry);
                IPersistent persistedEntry = _db.GetObjectByOid(entry.Oid);
                Assert.AreEqual(persistedEntry, entry);
            }
        }


        [TestMethod]
        public void ReturnsAllEntries()
        {
            using (IWAYDDatasource _db = CurrentDb())
            {
                _db.Save(new Entry());
                _db.Save(new Entry());
                _db.Save(new Entry());

                Assert.AreEqual(3, _db.GetAllEntries().Count());
            }
        }

        [TestMethod]
        public void EntriesArePersistedToFile()
        {
            List<IEntry> all;
            //write entries to database and close database
            using (IWAYDDatasource _db = new DatasourceFactory(false).GetCurrent())
            {
                for (int i = 0; i < 10; i++)
                {
                    _db.Save(new Entry {Time = DateTime.Now});
                }
            }
            //open database again and get all entries
            using (IWAYDDatasource _db = new DatasourceFactory(false).GetCurrent())
            {
                all = _db.GetAllEntries().ToList();
            }
            //delete the file
            File.Delete(DatasourceFactory.FileName);

            Assert.AreEqual(10, all.Count);
        }

        [TestMethod]
        public void SettingIsPersistedToFile()
        {
            List<ISetting> all;
            //write entries to database and close database
            using (IWAYDDatasource _db = new DatasourceFactory(false).GetCurrent())
            {
               _db.Save(new WAYDSetting{Key = 0, Value = 0.1}); ;
            }
            //open database again and get all entries
            using (IWAYDDatasource _db = new DatasourceFactory(false).GetCurrent())
            {
                all = _db.GetAllSettings().ToList();
            }
            //delete the file
            File.Delete(DatasourceFactory.FileName);

            Assert.AreEqual(all.First().Value, 0.1 );
        }
        [TestMethod]
        public void ExistingSettingIsPersistedToFile()
        {
            List<ISetting> all;
            //write entries to database and close database
            using (IWAYDDatasource _db = new DatasourceFactory(false).GetCurrent())
            {
               _db.Save(new WAYDSetting{Key = 0, Value = 30}); ;
            }
            using (IWAYDDatasource _db = new DatasourceFactory(false).GetCurrent())
            {
                all = _db.GetAllSettings().ToList();
            }
            Assert.AreEqual(all.First().Value, 30 );

            //open database agiain and edit value
            using (IWAYDDatasource _db = new DatasourceFactory(false).GetCurrent())
            {
               _db.Save(new WAYDSetting{Key = 0, Value = 0.1}); ;
            }
            //open database again and get all entries
            using (IWAYDDatasource _db = new DatasourceFactory(false).GetCurrent())
            {
                all = _db.GetAllSettings().ToList();
            }
            //delete the file
            File.Delete(DatasourceFactory.FileName);

            Assert.AreEqual(all.First().Value, 0.1 );
        }
    }
}