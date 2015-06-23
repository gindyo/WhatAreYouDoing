namespace WhatAreYouDoing.Persistance
{
    public static class Extentions
    {
        public static void Save(this Entry p)
        {
            MyDatabaseFactory.Current().Insert(p);
        }
    }
}