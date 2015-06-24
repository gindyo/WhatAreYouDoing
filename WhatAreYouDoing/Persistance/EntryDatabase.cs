using System;
using System.Linq;
using Volante;
using Volante.Impl;
using WhatAreYouDoing.Factories;

namespace WhatAreYouDoing.Persistance
{
    public class EntryDatabase : DatabaseImpl, IDisposable, IWAYDDatasource
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

        public IQueryable<IEntry> GetAll()
        {
            return GetRoot().EntryIndex.Select(e => e as IEntry).AsQueryable();
        }

        public void Save(IEntry currentEntry)
        {
            Entry entry = GetRoot().EntryIndex.Get(currentEntry.Id);
            if (null == entry)
                Insert(currentEntry);
            else
                entry.Store();
        }

        public IEntry GetEntry(int id)
        {
            return GetRoot().EntryIndex.Get(id);
        }

        public IEntry GetEntry()
        {
            return new Entry();
        }

        private DatabaseRoot GetRoot()
        {
            return Root as DatabaseRoot;
        }

        public void Insert(IEntry e)
        {
            var entry = e as Entry;
            entry.Time = DateTime.Now;
            if (null == entry)
                throw new Exception("Object must be an entry");

            storeObject(entry);
            GetRoot().EntryIndex.Put(entry.Oid, entry);
            Commit();
        }
    }
}