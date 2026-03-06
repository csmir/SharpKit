#if NET6_0_OR_GREATER

using System.Threading.Channels;

namespace Nimble.Threading.Channels;

/// <summary>
///     A background channel that processes incoming values from any source thread or task on one background task.
/// </summary>
/// <remarks>
///     Integrate this type with <b>Microsoft.Extensions.Hosting.IHostedService</b> using <b>IServiceCollection.AddHostedService</b> and binding <b>Start/StopAsync</b>
///     with <see cref="StartReadingAsync(CancellationToken)"/>/<see cref="StopReadingAsync(CancellationToken)"/> respectively to have the background channel start and stop with the host application.
/// </remarks>
/// <typeparam name="TValue">The value represented in the channel throughput. This value is implemented by <see cref="ProcessAsync(TValue, CancellationToken)"/>.</typeparam>
public abstract class BackgroundChannel<TValue> : IBackgroundChannel<TValue>, IDisposable, IAsyncDisposable
    where TValue : notnull
{
    private Channel<TValue> _channel;
    private CancellationTokenSource? _cancellationSource;
    private Task? _runnerTask;
    private bool _disposed;

    /// <summary>
    ///     Creates a new bounded <see cref="BackgroundChannel{TValue}"/> with the provided options.
    /// </summary>
    /// <param name="capacity">The maximum capacity of the channel.</param>
    /// <param name="fullMode">How the channel should behave when its content exceeds <paramref name="capacity"/>.</param>
    /// <param name="itemDropped">Executed when an item is dropped from entering the channel under the scenario of <paramref name="fullMode"/>.</param>
    public BackgroundChannel(int capacity, BoundedChannelFullMode fullMode = BoundedChannelFullMode.DropWrite, Action<TValue>? itemDropped = null)
    {
        _channel = Channel.CreateBounded(new BoundedChannelOptions(capacity)
        {
            FullMode = fullMode,
            SingleReader = true,
            SingleWriter = false,
            AllowSynchronousContinuations = false
        }, itemDropped);

        _cancellationSource = new CancellationTokenSource();
    }

    /// <summary>
    ///     Creates a new unbounded <see cref="BackgroundChannel{TValue}"/> with the default options.
    /// </summary>
    public BackgroundChannel()
    {
        _channel = Channel.CreateUnbounded<TValue>(new UnboundedChannelOptions()
        {
            SingleReader = true,
            SingleWriter = false,
            AllowSynchronousContinuations = false
        });

        _cancellationSource = new CancellationTokenSource();
    }

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException" />
    /// <exception cref="ArgumentNullException" />
    public bool TryWrite(TValue value)
    {
        ObjectDisposedException.ThrowIf(_disposed, typeof(BackgroundChannel<TValue>));
        ArgumentNullException.ThrowIfNull(value);

        return TryWriteInner(value);
    }

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException" />
    /// <exception cref="ArgumentNullException" />
    public async ValueTask<bool> WriteAsync(TValue value, CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, typeof(BackgroundChannel<TValue>));
        ArgumentNullException.ThrowIfNull(value);

        try
        {
            while (await _channel.Writer.WaitToWriteAsync(cancellationToken))
                return TryWriteInner(value);
        }
        catch (ChannelClosedException)
        {
            throw;
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            throw new ChannelClosedException(ex);
        }
        // Swallow OperationCanceledException as it is expected when a cancellationtoken is canceled, and return false to indicate the write operation was not successful.
        catch (OperationCanceledException) { }

        return false;
    }

    private bool TryWriteInner(TValue value)
        => _channel.Writer.TryWrite(value);

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException" />
    protected abstract ValueTask ProcessAsync(TValue value, CancellationToken cancellationToken);

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException" />
    public void StartReading()
        => StartReadingAsync().AsTask().Wait();

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException" />
    public ValueTask StartReadingAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, typeof(BackgroundChannel<TValue>));

        var linked = CancellationTokenSource.CreateLinkedTokenSource(_cancellationSource!.Token, cancellationToken);

        _runnerTask = Task.Run(async () =>
        {
            try
            {
                while (await _channel.Reader.WaitToReadAsync(linked.Token))
                    while (_channel.Reader.TryRead(out var value))
                    {
                        await ProcessAsync(value, linked.Token);
                    }
            }
            // The thrown exception is not because of the cancellation of the background task, but because of a fault in the processing logic.
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _channel.Writer.TryComplete(ex);
            }
            // Swallow OperationCanceledException as it is expected when a cancellationtoken is canceled, and just exit the loop to end the background task.
            catch (OperationCanceledException) { }

        }, linked.Token);

        return ValueTask.CompletedTask;
    }

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException" />
    public void StopReading()
        => StopReadingAsync().AsTask().Wait();

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException" />
    public ValueTask StopReadingAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, typeof(BackgroundChannel<TValue>));

        // If the cancellation source is already canceled or null, it means the channel is already stopped, so we can just return.
        if (_cancellationSource == null || _cancellationSource.IsCancellationRequested)
            return ValueTask.CompletedTask;

        _channel.Writer.TryComplete();

        _cancellationSource.Cancel();

        try
        {
            _runnerTask?.Wait(cancellationToken);
        }
        catch (AggregateException ex) when (ex.InnerException != null)
        {
            throw ex.InnerException;
        }

        return ValueTask.CompletedTask;
    }

    /// <inheritdoc/>
    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "Implemented in DisposeAsync")]
    void IDisposable.Dispose()
        => DisposeAsync().AsTask().Wait();

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (_disposed)
            return;

        _disposed = true;

        await StopReadingAsync();

        _runnerTask?.Dispose();
        _runnerTask = null;

        _cancellationSource?.Dispose();
        _cancellationSource = null;

        _channel = null!;

        GC.SuppressFinalize(this);
    }
}

/// <summary>
///     A background channel that processes incoming values from any source thread or task on one background task.
/// </summary>
/// <remarks>
///     Implement <see cref="BackgroundChannel{TValue}"/> in a self-defined type or call 
///     <see cref="Create{TValue}(Func{TValue, ValueTask})"/>/<see cref="Create{TValue}(Func{TValue, ValueTask}, int, BoundedChannelFullMode, Action{TValue}?)"/> 
///     to create a new <see cref="DelegateBackgroundChannel{TValue}"/> if you just want to use a delegate for processing incoming values without defining a new type.
/// </remarks>
public static class BackgroundChannel
{
    /// <summary>
    ///     Creates a new <see cref="DelegateBackgroundChannel{TValue}"/> with the provided processing delegate and channel options.
    /// </summary>
    /// <param name="processor">The delegate that processes incoming values to the channel.</param>
    /// <param name="capacity">The maximum capacity of the channel.</param>
    /// <param name="fullMode">How the channel should behave when its content exceeds <paramref name="capacity"/>.</param>
    /// <param name="itemDropped">Executed when an item is dropped from entering the channel under the scenario of <paramref name="fullMode"/>.</param>
    public static IBackgroundChannel<TValue> Create<TValue>(Func<TValue, ValueTask> processor, int capacity, BoundedChannelFullMode fullMode = BoundedChannelFullMode.DropWrite, Action<TValue>? itemDropped = null)
        where TValue : notnull
        => new DelegateBackgroundChannel<TValue>(processor, capacity, fullMode, itemDropped);

    /// <summary>
    ///     Creates a new unbounded <see cref="DelegateBackgroundChannel{TValue}"/> with the provided processing delegate and default options.
    /// </summary>
    /// <param name="processor">The delegate that processes incoming values to the channel.</param>
    public static IBackgroundChannel<TValue> Create<TValue>(Func<TValue, ValueTask> processor)
        where TValue : notnull
        => new DelegateBackgroundChannel<TValue>(processor);
}

#endif