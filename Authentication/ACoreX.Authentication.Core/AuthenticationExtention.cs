

using ACoreX.Authentication.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ACoreX.Authentication.Core
{
    public static class AuthenticationExtentionInstance
    {
        public static IServiceCollection AddAuthenticationInstance<T>(this IServiceCollection services, string stringKey = "ASDQWE!@#asdqwe123ASDQWE!@#asdqwe123") where T : IAuthService
        {

            object service = System.Activator.CreateInstance(typeof(T), new object[] { services, stringKey });
            return services;
        }

    }
}
