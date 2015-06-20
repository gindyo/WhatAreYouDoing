using System;
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
            MyDatabaseFactory.Cleanup();
        }

        [TestMethod]
        public void DatabaseCanBeOpened()
        {
            using (var _db = MyDatabaseFactory.Current())
            {
                Assert.IsTrue(_db.Root != null); 
            }
        }
        [TestMethod]
        public void CanInsertEntry()
        {
            using (var _db = MyDatabaseFactory.Current())
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
            using (var _db = MyDatabaseFactory.Current(true))
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
            using (var _db = MyDatabaseFactory.Current(true))
            {
                var persistedEntry = _db.GetObjectByOid(entry.Oid);
                Assert.AreEqual(persistedEntry, entry);
            }
        }
        [TestMethod]
        public void ReturnsAllEntries()
        {
            new Entry().Save();
            new Entry().Save();
            new Entry().Save();
            
            using (var _db = MyDatabaseFactory.Current(true))
            {
                Assert.AreEqual(_db.GetAll().Count, 3);
            }
        }
    }
}
