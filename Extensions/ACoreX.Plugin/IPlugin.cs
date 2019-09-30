
using ACoreX.Injector.Abstractions;
using System;

namespace ACoreX.Plugin
{
    public interface IPlugin
    {
        string Name { get; set; }
        string Description { get; set; }

        void Register(IContainerBuilder builder);
    }
}
