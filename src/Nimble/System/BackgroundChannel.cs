#if NET6_0_OR_GREATER

using System.ComponentModel;

namespace System.Threading.Channels;

/// <summary>
///     A background channel that processes incoming values from any source thread or task on one background task.
/// </summary>
/// <typeparam name="TValue">The value represented in the channel throughput.</typeparam>
public interface IBackgroundChannel<TValue> : IAsyncDisposable, IDisposable
    where TValue : notnull
{
    /// <summary>
    ///     Starts the background processing of the channel, which will continue until <see cref="StopReading"/> or <see cref="StopReadingAsync(CancellationToken)"/> are called, or the disposable escapes its scope.
    /// </summary>
    /// <remarks>
    ///     This method blocks the calling thread until the background task has succesfully started.
    /// </remarks>
    public void StartReading();

    /// <summary>
    ///     Starts the background process that reads from the channel and processes incoming values.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token linked to the task lifetime. Cancelling this token will mean cancelling the read operation as a whole.</param>
    /// <returns>A <see cref="ValueTask"/> representing the reader startup operation.</returns>
    public ValueTask StartReadingAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Stops the background processing of the channel, and waits for the processing task to complete before returning.
    /// </summary>
    public void StopReading();

    /// <summary>
    ///     Shuts the reader down, which will stop the background processing of the channel and wait for the processing task to complete before returning.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be notified to end the stopping operation early if the running task does not cancel on wait.</param>
    /// <returns>A <see cref="ValueTask"/> representing the reader shutdown operation.</returns>
    public ValueTask StopReadingAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Attempts to write to the channel synchronously.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <returns><see langword="true"/> if the value was written; otherwise <see langword="false"/>.</returns>
    public bool TryWrite(TValue value);

    /// <summary>
    ///     Writes a value to the channel asynchronously, which will be processed by the background task. 
    ///     This method will not block the calling thread or task, but may be delayed if the channel is full.
    /// </summary>
    /// <param name="value">The value that should be written.</param>
    /// <param name="cancellationToken">A cancellation token that can be notified to end the write operation early in case it hangs due to the reader being blocked.</param>
    /// <returns>A <see cref="ValueTask"/> representing the write operation.</returns>
    public ValueTask WriteAsync(TValue value, CancellationToken cancellationToken = default);
}

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

        return _channel.Writer.TryWrite(value);
    }

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException" />
    /// <exception cref="ArgumentNullException" />
    public ValueTask WriteAsync(TValue value, CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, typeof(BackgroundChannel<TValue>));
        ArgumentNullException.ThrowIfNull(value);
        
        return _channel.Writer.WriteAsync(value, cancellationToken);
    }

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
                {
                    while (_channel.Reader.TryRead(out var value))
                        await ProcessAsync(value, linked.Token);
                }
            }
            catch (OperationCanceledException)
            {
                // Ignore cancellation exceptions. We'll just exit the loop and end the task.
            }

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

        if (_cancellationSource == null || _cancellationSource.IsCancellationRequested)
            return ValueTask.CompletedTask;

        _channel.Writer.TryComplete();

        _cancellationSource.Cancel();

        _runnerTask?.Wait(cancellationToken);

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
///     A <see cref="BackgroundChannel{TValue}"/> implementations that implements a delegate for processing incoming values.
/// </summary>
/// <typeparam name="TValue">The value represented in the channel throughput. This value is implemented by the delegate.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class DelegateBackgroundChannel<TValue> : BackgroundChannel<TValue>
    where TValue : notnull
{
    private readonly Func<TValue, ValueTask> _processor;

    internal DelegateBackgroundChannel(Func<TValue, ValueTask> processor, int capacity = 100, BoundedChannelFullMode fullMode = BoundedChannelFullMode.DropWrite, Action<TValue>? itemDropped = null)
        : base(capacity, fullMode, itemDropped) 
        => _processor = processor ?? throw new ArgumentNullException(nameof(processor));

    internal DelegateBackgroundChannel(Func<TValue, ValueTask> processor)
        : base() 
        => _processor = processor ?? throw new ArgumentNullException(nameof(processor));

    /// <inheritdoc />
    protected override ValueTask ProcessAsync(TValue value, CancellationToken _)
        => _processor(value);
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