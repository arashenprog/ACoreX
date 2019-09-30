using ACoreX.Authentication.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Formatting = Newtonsoft.Json.Formatting;

namespace ACoreX.Authentication.JWT
{
    public class JWTToken : IToken
    {

        public string Value { get; set; }

        public JWTToken(string token)
        {
            Value = token;
        }

        public JWTToken()
        {
        }


        public string GetValue(string key)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            bool readableToken = jwtHandler.CanReadToken(Value);
            if (readableToken != true)
            {
                return "The token doesn't seem to be in a proper JWT format.";
            }
            else if (readableToken == true)
            {
                JwtSecurityToken token = jwtHandler.ReadJwtToken(Value);

                //Extract the payload of the JWT
                global::System.Collections.Generic.IEnumerable<Claim> claims = token.Claims;

                foreach (Claim c in claims)
                {
                    if (c.Type == key)
                    {
                        return c.Value;
                    }
                }
                return "not Found";
            }
            return "Token is not present";
        }

        public string GenerateToken(DateTime expiryTime, params TokenKey[] keys)
        {

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("ASDQWE!@#asdqwe123ASDQWE!@#asdqwe123");
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = expiryTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            foreach (TokenKey item in keys)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(item.Key, item.Value));
            }

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public TokenKey[] ParseToken(string token)
        {
            string stream = token;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken jsonToken = handler.ReadToken(stream);
            string getID = jsonToken.Id;
            return new TokenKey[0];
        }

        public ClaimsPrincipal ValidateToken(string Token)
        {
            string secret = "ASDQWE!@#asdqwe123ASDQWE!@#asdqwe123";
            byte[] key = Encoding.ASCII.GetBytes(secret);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            ClaimsPrincipal claims = handler.ValidateToken(Token, validations, out SecurityToken tokenSecure);
            return claims;
        }

        public string ReadToken(string Token)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            bool readableToken = jwtHandler.CanReadToken(Token);
            string result = "";
            if (readableToken != true)
            {
                return "The token doesn't seem to be in a proper JWT format.";
            }
            if (readableToken == true)
            {
                JwtSecurityToken token = jwtHandler.ReadJwtToken(Token);

                //Extract the headers of the JWT
                JwtHeader headers = token.Header;
                string jwtHeader = "{";
                foreach (global::System.Collections.Generic.KeyValuePair<string, object> h in headers)
                {
                    jwtHeader += '"' + h.Key + "\":\"" + h.Value + "\",";
                }
                jwtHeader += "}";
                string headerToken = "Header:\r\n" + JToken.Parse(jwtHeader).ToString(Formatting.Indented);

                //Extract the payload of the JWT
                global::System.Collections.Generic.IEnumerable<Claim> claims = token.Claims;
                string jwtPayload = "{";
                foreach (Claim c in claims)
                {
                    jwtPayload += '"' + c.Type + "\":\"" + c.Value + "\",";
                }
                jwtPayload += "}";
                string PayloadToken = "\r\nPayload:\r\n" + JToken.Parse(jwtPayload).ToString(Formatting.Indented);

                result = headerToken + PayloadToken;
            }
            return result;

        }

    }
}

