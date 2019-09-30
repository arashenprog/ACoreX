using System;
using System.Security.Claims;

namespace ACoreX.Authentication.Abstractions
{
    public interface IToken
    {
        string Value { get; set; }

        string GenerateToken(DateTime expiryTime, params TokenKey[] keys);
        TokenKey[] ParseToken(string token);
        ClaimsPrincipal ValidateToken(string token);
        string GetValue(string key);
    }
    public class TokenKey
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}