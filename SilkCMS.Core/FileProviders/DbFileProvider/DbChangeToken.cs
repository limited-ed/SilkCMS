using Microsoft.Extensions.Primitives;

namespace SilkCMS.Core.FileProviders;

public class DbChangeToken: IChangeToken
{
    public IDisposable RegisterChangeCallback(Action<object> callback, object state) => EmptyDisposable.Instance;
    public bool ActiveChangeCallbacks => false;
    public bool HasChanged => false;
}

internal class EmptyDisposable : IDisposable
{
    public static EmptyDisposable Instance { get; } = new EmptyDisposable();
    private EmptyDisposable() { }
    public void Dispose() { }
}