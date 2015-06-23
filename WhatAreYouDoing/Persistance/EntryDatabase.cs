using System;
using System.Collections.Generic;
using System.Linq;
using Volante;
using Volante.Impl;

namespace WhatAreYouDoing.Persistance
{
    public class EntryDatabase : DatabaseImpl, IDisposable
    {
        public EntryDatabase(string filePath, bool inMemory)
        {
            if (inMemory)
                Open(filePath, 0);
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