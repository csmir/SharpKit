#if NET6_0_OR_GREATER

using System.Threading.Channels;

namespace Nimble.Threading.Channels;

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
    /// <remarks>
    ///     If the provided <paramref name="cancellationToken"/> is cancelled before the background task has completed, this method will throw an <see cref="OperationCanceledException"/>.
    ///     <br />
    ///     If the background task produces or produced an exception during execution, this method will throw an <see cref="AggregateException"/> with the produced exception inside.
    /// </remarks>
    /// <param name="cancellationToken">A cancellation token that can be notified to end the stopping operation early if the running task does not cancel on wait.</param>
    /// <returns>A <see cref="ValueTask"/> representing the reader shutdown operation.</returns>
    /// <exception cref="AggregateException" />
    /// <exception cref="OperationCanceledException" />
    public ValueTask StopReadingAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Attempts to write to the channel synchronously.
    /// </summary>
    /// <remarks>
    ///     This method will attempt to write the value to the channel without wait and return whether the write succeeded.
    ///     To understand why the write failed, consider calling <see cref="WriteAsync(TValue, CancellationToken)"/> instead.
    /// </remarks>
    /// <param name="value">The value to write.</param>
    /// <returns><see langword="true"/> if the value was written. When the channel has been closed or full and the fullmode is wait, <see langword="false"/>.</returns>
    public bool TryWrite(TValue value);

    /// <summary>
    ///     Writes a value to the channel asynchronously, which will be processed by the background task. 
    ///     This call may be delayed if the channel is full.
    /// </summary>
    /// <remarks>
    ///     This task will wait until the value is written to the channel, which may be delayed if the channel is full. 
    ///     If the channel is closed or the provided cancellation token is cancelled, this method will return <see langword="false"/>.
    ///     <br />
    ///     <br />
    ///     When the channel is closed with an exception, this method will throw a <see cref="ChannelClosedException"/> with the provided exception as inner exception.
    /// </remarks>
    /// <param name="value">The value that should be written.</param>
    /// <param name="cancellationToken">A cancellation token that can be notified to end the write operation early in case it hangs due to the reader being blocked.</param>
    /// <returns>A <see cref="ValueTask"/> that returns <see langword="true"/> if the value was written. When the channel has been closed or the write operation cancelled, <see langword="false"/>.</returns>
    /// <exception cref="ChannelClosedException">Thrown when the channel was closed with an exception.</exception>
    public ValueTask<bool> WriteAsync(TValue value, CancellationToken cancellationToken = default);
}

#endif