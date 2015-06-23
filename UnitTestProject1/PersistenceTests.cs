using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhatAreYouDoing.Persistance;

namespace UnitTestProject1
{
    [TestClass]
    public class PersistenceTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            if(CurrentDb().IsOpened)
                MyDatabaseFactory.Cleanup();
            if(File.Exists(MyDatabaseFactory.FileName))
                File.Delete(MyDatabaseFactory.FileName);
        }

        [TestMethod]
        public void DatabaseCanBeOpened()
        {
            using (var _db = CurrentDb())
            {
                Assert.IsTrue(_db.Root != null); 
            }
        }

        private static EntryDatabase CurrentDb()
        {
            return MyDatabaseFactory.Current(true);
        }

        [TestMethod]
        public void CanInsertEntry()
        {
            using (var _db = CurrentDb())
            {
                var initsize = _db.UsedSize;
                var entry = new Entry();
                _db.storeObject(entry);
                var size = _db.UsedSize;
                Assert.IsTrue(size > initsize);
            }
        }
        [TestMethod]
        public void CanRetreiveEntry()
        {
            using (var _db = CurrentDb())
            {
                var initsize = _db.UsedSize;
                var entry = new Entry();
                _db.storeObject(entry);
                var persistedEntry = _db.GetObjectByOid(entry.Oid);
                Assert.AreEqual(persistedEntry, entry);
            }
        }
        [TestMethod]
        public void EntrySavesItsef()
        {
            var entry = new Entry();
            entry.Save();
            using (var _db = CurrentDb())
            {
                var persistedEntry = _db.GetObjectByOid(entry.Oid);
                Assert.AreEqual(persistedEntry, entry);
            }
        }
        [TestMethod]
        public void ReturnsAllEntries()
        {
            using (var _db = CurrentDb())
            {
                new Entry().Save();
                new Entry().Save();
                new Entry().Save();
            
                Assert.AreEqual(3, _db.GetAll().Count);
            }
        }
        [TestMethod]
        public void EntriesArePersistedToFile()
        {
            List<Entry> all;
            //write entries to database and close database
            using (var _db = MyDatabaseFactory.Current())
            {
                for (var i = 0; i < 10; i++)
                {
                    new Entry{Time = DateTime.Now}.Save();
                }
            }
            //open database again and get all entries
            using (var _db = MyDatabaseFactory.Current())
            {
                all = _db.GetAll();
            }
            //delete the file
            File.Delete(MyDatabaseFactory.FileName);

            Assert.AreEqual(10, all.Count);
        }
    }
}
