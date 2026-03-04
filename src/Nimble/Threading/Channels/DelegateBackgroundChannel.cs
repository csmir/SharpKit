#if NET6_0_OR_GREATER

using System.ComponentModel;
using System.Threading.Channels;

namespace Nimble.Threading.Channels;

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

#endif