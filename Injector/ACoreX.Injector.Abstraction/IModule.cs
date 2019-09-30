namespace ACoreX.Injector.Abstractions
{
    public interface IModule
    {
        void Register(IContainerBuilder builder);
    }
}
