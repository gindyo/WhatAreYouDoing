using Volante;
using WhatAreYouDoing.Interfaces;

namespace WhatAreYouDoing.Persistance.Models
{
    public class WAYDSetting : Persistent, ISetting
    {
        private int _key;
        private object _value;

        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}