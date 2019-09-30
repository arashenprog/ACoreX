namespace ACoreX.Authentication.Abstractions
{
    public interface IAuthHandler
    {
        bool Check(IToken token);
    }
}

