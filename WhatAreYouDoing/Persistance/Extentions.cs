using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Volante;

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
