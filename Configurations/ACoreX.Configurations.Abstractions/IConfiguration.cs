using System;
using System.Collections.Generic;
using System.Text;

namespace ACoreX.Configurations.Abstractions
{
    public interface IConfiguration
    {
        string Get(string key);
    }
}
