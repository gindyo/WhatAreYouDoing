using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Volante;
using WhatAreYouDoing.Persistance;

namespace UnitTestProject1
{
    [TestClass]
    public class PersistenceTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            if (CurrentDb().IsOpened)
                MyDatabaseFactory.Cleanup();
            if (File.Exists(MyDatabaseFactory.FileName))
                File.Delete(MyDatabaseFactory.FileName);
        }

        [TestMethod]
        public void DatabaseCanBeOpened()
        {
            using (EntryDatabase _db = CurrentDb())
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
            using (EntryDatabase _db = CurrentDb())
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
            using (EntryDatabase _db = CurrentDb())
            {
                long initsize = _db.UsedSize;
                var entry = new Entry();
                _db.storeObject(entry);
                IPersistent persistedEntry = _db.GetObjectByOid(entry.Oid);
                Assert.AreEqual(persistedEntry, entry);
            }
        }

        [TestMethod]
        public void EntrySavesItsef()
        {
            var entry = new Entry();
            entry.Save();
            using (EntryDatabase _db = CurrentDb())
            {
                IPersistent persistedEntry = _db.GetObjectByOid(entry.Oid);
                Assert.AreEqual(persistedEntry, entry);
            }
        }

        [TestMethod]
        public void ReturnsAllEntries()
        {
            using (EntryDatabase _db = CurrentDb())
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
            using (EntryDatabase _db = MyDatabaseFactory.Current())
            {
                for (int i = 0; i < 10; i++)
                {
                    new Entry {Time = DateTime.Now}.Save();
                }
            }
            //open database again and get all entries
            using (EntryDatabase _db = MyDatabaseFactory.Current())
            {
                all = _db.GetAll();
            }
            //delete the file
            File.Delete(MyDatabaseFactory.FileName);

            Assert.AreEqual(10, all.Count);
        }
    }
}