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
        private const string _fileName = "database.dbs";

        public static string FileName
        {
            get { return _fileName; }
        }

        public static EntryDatabase Current(bool inMemory = false)
        {
            var db =  _database ?? (_database = new EntryDatabase(_fileName, inMemory));
            if(!db.IsOpened)
                if(inMemory)
                    db.Open(_fileName, 0);
                else
                    db.Open(_fileName);
            return db;
        }

        public static void Cleanup()
        {
            if(_database.IsOpened) _database.Close();
            _database = null;
        }
    }

    public class DatabaseRoot : Persistent
    {

        public IIndex<long, Entry> EntryIndex;
    }

    public class EntryDatabase : DatabaseImpl, IDisposable
    {
        public EntryDatabase(string filePath, bool inMemory)
        {
            if(inMemory)
                Open(filePath,0);
            else
                Open(filePath);
            DatabaseRoot dbRoot = null;
            if (Root == null)
            {
                dbRoot = new DatabaseRoot();
                dbRoot.EntryIndex = CreateIndex<Int64, Entry>(IndexType.Unique);
                Root = dbRoot;
                Commit();
            }
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
            Commit();
        }
    }
}