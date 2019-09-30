using ACoreX.Authentication.Abstractions;
using ACoreX.Injector.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ACoreX.WebAPI
{

    public static class AuthenticationMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthenticationWrapperMiddleware<T>(this IApplicationBuilder app, IContainerBuilder container) where T : IAutentication
        {
            T z = Activator.CreateInstance<T>();
            return app.UseMiddleware<AuthenticationAPIMiddleware<T>>(z, app, container);
        }
    }

    public class AuthenticationAPIMiddleware<T> where T : IAutentication
    {
        private readonly RequestDelegate _next;
        private readonly IAutentication _auth;
        private readonly IContainerBuilder _container;
        private readonly IApplicationBuilder _app;


        public AuthenticationAPIMiddleware(RequestDelegate next, T autenticate, IApplicationBuilder app, IContainerBuilder container)
        {
            _next = next;
            _auth = autenticate;
            _container = container;
            _app = app;

            //_container.Services.AddJWTAuth();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Sign in user if this auth cookie doesn't exist

            //if (context.Request.Headers == null)
            //{
            //    // Get user from db - not done

            //    // Set claims from user object - put in dummy test name for now
            //    var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, "TEST"),

            //};

            //    var claimsIdentity = new ClaimsIdentity(claims, "MyAuthenticationCookie");

            //    context.SignInAsync("MyAuthenticationCookie", new ClaimsPrincipal(claimsIdentity));
            //}

            //_app.UseAuthentication();
            AuthenticateResult result = await _auth.HandleAuthenticateAsync(context);

            await _next(context);
        }
    }

    //public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    //{

    //    public BasicAuthenticationHandler(
    //        IOptionsMonitor<AuthenticationSchemeOptions> options,
    //        ILoggerFactory logger,
    //        UrlEncoder encoder,
    //        ISystemClock clock)
    //        : base(options, logger, encoder, clock)
    //    {

    //    }

    //    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    //    {
    //        //if (!Request.Headers.ContainsKey("Authorization"))
    //        //    return AuthenticateResult.Fail("Missing Authorization Header");

    //        ////User user = null;
    //        //try
    //        //{
    //        //    var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
    //        //    var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
    //        //    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
    //        //    var username = credentials[0];
    //        //    var password = credentials[1];
    //        //    //user = await _userService.Authenticate(username, password);
    //        //}
    //        //catch
    //        //{
    //        //    return AuthenticateResult.Fail("Invalid Authorization Header");
    //        //}

    //        //if (user == null)
    //        //    return AuthenticateResult.Fail("Invalid Username or Password");

    //        //var claims = new[] {
    //        //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    //        //    new Claim(ClaimTypes.Name, user.Username),
    //        //};
    //        Claim[] claims = new[] {
    //            new Claim(ClaimTypes.NameIdentifier, "user.Id.ToString()"),
    //            new Claim(ClaimTypes.Name, "user.Username"),
    //        };
    //        ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
    //        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
    //        AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

    //        return AuthenticateResult.Success(ticket);
    //    }
    //}
}