using System;
using System.Collections.Generic;
using System.Text;

namespace ACoreX.Configurations.NetCore
{
    public class NetCoreConfiguration : ACoreX.Configurations.Abstractions.IConfiguration
    {
        public Microsoft.Extensions.Configuration.IConfiguration _configuration { get; }
        public NetCoreConfiguration(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Get(string key)
        {
            return _configuration[key];
        }
    }
}
