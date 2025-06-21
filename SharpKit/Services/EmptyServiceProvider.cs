namespace SharpKit;

public sealed class EmptyServiceProvider : IServiceProvider
{
    public object? GetService(Type serviceType)
    {
        return null;
    }
}
