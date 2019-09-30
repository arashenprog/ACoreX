using Microsoft.AspNetCore.Builder;

namespace ACoreX.WebAPI
{

    public static class ApiResponseMiddlewareExtension
    {
        public static IApplicationBuilder UseAPIResponseWrapperMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiResponseMiddleware>();
        }
    }

}
