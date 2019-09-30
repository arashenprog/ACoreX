using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace ACoreX.Authentication.Abstractions
{
    public interface IAutentication
    {
        Task<AuthenticateResult> HandleAuthenticateAsync(HttpContext context);

    }
}
