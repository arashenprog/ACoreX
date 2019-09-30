using Microsoft.Extensions.DependencyInjection;
using System;

namespace ACoreX.Injector.Abstractions
{
    public interface IContainerBuilder
    {
        IServiceCollection Services
        {
            get;
        }
        void AddScope<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        void AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        void AddTransient<TService, TImplementation>()
           where TService : class
           where TImplementation : class, TService;

        void Register<T>() where T : class, IModule;

        T Create<T>();

    }
}
