using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Volante;
using Volante.Impl;

namespace WhatAreYouDoing.Persistance
{

    public static class MyDatabaseFactory
    {
        private static EntryDatabase _database;
        private static string dbFileName = "database.dbs";

        public static EntryDatabase Current(bool inMemory = false)
        {
            var db =  _database ?? (_database = new EntryDatabase());
            if(!db.IsOpened)
                if(inMemory)
                    db.Open(dbFileName, 0);
                else
                    db.Open(dbFileName);
            return db;
        }

        public static void Cleanup()
        {
            _database = null;
        }
    }

    public class DatabaseRoot : Persistent
    {

        public IIndex<long, Entry> EntryIndex;
    }

    public class EntryDatabase : DatabaseImpl, IDisposable
    {
        public EntryDatabase()
        {
            Open("database.dbs",0);
            if (Root == null)
            {
                Root = new DatabaseRoot();
                (Root as DatabaseRoot).EntryIndex = CreateIndex<Int64, Entry>(IndexType.Unique);
                Commit();
            }
            if((Root as DatabaseRoot).EntryIndex == null)
                (Root as DatabaseRoot).EntryIndex = CreateIndex<Int64, Entry>(IndexType.Unique);
            Commit();
        }
        public void Dispose()
        {
            Close();
        }

        public List<Entry> GetAll()
        {
            return GetRoot().EntryIndex.ToList();
        }

        private DatabaseRoot GetRoot()
        {
            return Root as DatabaseRoot;
        }

        public void Insert(Entry entry)
        {
            storeObject(entry);
            GetRoot().EntryIndex.Put(entry.Oid, entry);
        }
    }
}