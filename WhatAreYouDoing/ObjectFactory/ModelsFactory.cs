using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance.Models;
using WhatAreYouDoing.UIModels;

Tasks;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance.Models;
using WhatAreYouDoing.UIModels;

namespace WhatAreYouDoing.ObjectFactory
{
    public class ModelsFactory : IModelFactory
    {
        public IEntry NewEntry(IEntry e = null)
        {
            return e == null ? new Entry() : new Entry {Time = e.Time, Value = e.Value};
        }

        public IUIEntry NewUIEntry(IEntry entry = null)
        {
            return new UIEntry(entry);
        }

        public ISetting NewSetting(ISetting setting = null)
        {
            return setting == null ? new WAYDSetting() : new WAYDSetting {Key = setting.Key, Value = setting.Value};
        }
    }
}
