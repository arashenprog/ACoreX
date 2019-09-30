

using ACoreX.Injector.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ACoreX.Injector.Core
{
    public static class ContainerBuilderExtention
    {

        public static IContainerBuilder AddBuilder(this IServiceCollection serviceCollection, IContainerBuilder builder)
        {
            serviceCollection.AddSingleton<IContainerBuilder>(c => builder);
            return builder;
        }
    }
}
