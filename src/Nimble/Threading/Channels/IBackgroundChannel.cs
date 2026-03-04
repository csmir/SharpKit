#if NET6_0_OR_GREATER

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

#endif