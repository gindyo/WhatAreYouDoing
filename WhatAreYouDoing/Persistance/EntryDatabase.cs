using System;
using System.Linq;
using Volante;
using Volante.Impl;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance.Models;

namespace WhatAreYouDoing.Persistance
{
    public class EntryDatabase : DatabaseImpl, IWAYDDatasource
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
                dbRoot.EntryIndex = CreateIndex<Int64, Models.Entry>(IndexType.Unique);
                dbRoot.SettingsIndex = CreateIndex<int, Models.WAYDSetting>(IndexType.Unique);
                Root = dbRoot;
                Commit();
            }
        }

        public void Dispose()
        {
            Close();
        }

        public IQueryable<IEntry> GetAllEntries()
        {
            return GetRoot().EntryIndex.Select(e => e as IEntry).AsQueryable();
        }

        public IQueryable<ISetting> GetAllSettings()
        {
            return GetRoot().SettingsIndex.AsQueryable().Select(s => s as ISetting);
        }

        public IEntry GetEntry(long id)
        {
            return GetRoot().EntryIndex.Get(id);
        }

        public IEntry GetEntry()
        {
            return new Entry();
        }

        public ISetting GetSetting()
        {
            return  new WAYDSetting();
        }

        public ISetting GetSetting(int id)
        {
            var setting =  GetAllSettings().FirstOrDefault(s => s.Key == id);
            return setting;
        }

        private DatabaseRoot GetRoot()
        {
            return Root as DatabaseRoot;
        }


        public void Save(ISetting setting)
        {
            WAYDSetting s = GetRoot().SettingsIndex.Get(setting.Key);
            if (null == s)
                Insert(setting);
            else
            {
                s.Value = setting.Value;
                s.Store();
            }
            Commit();
        }

        private void Insert(ISetting s)
        {
            WAYDSetting setting = s as WAYDSetting;
            storeObject(setting);
            GetRoot().SettingsIndex.Put(setting.Key, setting);
            Commit();
        }

        public void Save(IEntry currentEntry)
        {
            if(!(currentEntry is Entry))
                throw new Exception("Can't save. it is not an entry");
            Entry entry = GetRoot().EntryIndex.Get(currentEntry.Id);
            if (null == entry)
            {
                Insert(currentEntry);
            }
            else
            {
                entry.Time = DateTime.Now;
                entry.Store();
            }
        }

        public void Insert(IEntry e)
        {
            var entry = e as Entry;
            if(entry == null)
                throw new Exception("Can't Insert. it is not an entry");
            entry.Time = DateTime.Now; 
            storeObject(entry as Entry);
            GetRoot().EntryIndex.Put(entry.Oid, entry);
            Commit();
        }
    }

}