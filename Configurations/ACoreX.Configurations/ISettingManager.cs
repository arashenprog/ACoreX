using System;
using System.Collections.Generic;
using System.Text;

namespace ACoreX.Configurations
{
    public interface ISettingManager
    {
        T Get<T>(string key, T defaultValue);
        T Get<T>(string key);
        void Set<T>(string key, T value);
    }
}
