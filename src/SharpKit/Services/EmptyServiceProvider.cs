namespace SharpKit;

/// <summary>
///     Represents an implementation of <see cref="IServiceProvider"/> that does not serve any services. 
///     Use this in place of not wanting to implement a service provider where it is ordinarily required.
/// </summary>
public sealed class EmptyServiceProvider : IServiceProvider
{
    /// <inheritdoc />
    public object? GetService(Type serviceType) => null;
}
