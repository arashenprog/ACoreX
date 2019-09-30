using System;

namespace ACoreX.WebAPI
{
    public enum WebApiMethod
    {
        Get,
        Post,
        Put,
        Delete,
        Head,
        Patch
    }
    public class WebApiAttribute : Attribute
    {
        public string Route { get; set; }

        public WebApiMethod Method { get; set; } = WebApiMethod.Get;

        public bool Authorized { get; set; } = false;

        public int CacheDuration { get; set; } = 0;
    }

}
