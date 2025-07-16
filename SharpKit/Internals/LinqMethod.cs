namespace SharpKit.Internals;
public enum LinqMethod
{
    #region Aggregation

    /// <inheritdoc cref="Enumerable.Aggregate"/>
    Aggregate,

    /// <inheritdoc cref="Enumerable.Average"/>
    Average,

    /// <inheritdoc cref="Enumerable.Count"/>
    Count,

    /// <inheritdoc cref="Enumerable.LongCount"/>
    LongCount,

    /// <inheritdoc cref="Enumerable.Sum"/>
    Sum,

    /// <inheritdoc cref="Enumerable.Min"/>
    Min,

    /// <inheritdoc cref="Enumerable.MinBy"/>
    MinBy,

    /// <inheritdoc cref="Enumerable.Max"/>
    Max,

    /// <inheritdoc cref="Enumerable.MaxBy"/>
    MaxBy,

    #endregion

    #region As... Wrappers

    /// <inheritdoc cref="Enumerable.AsEnumerable"/>
    AsEnumerable,

    /// <inheritdoc cref="Queryable.AsQueryable"/>
    AsQueryable,

    /// <inheritdoc cref="ParallelEnumerable.AsParallel"/>
    AsParallel,

    #endregion

    #region Conversion

    /// <inheritdoc cref="Enumerable.ToArray"/>
    ToArray,

    /// <inheritdoc cref="Enumerable.ToList"/>
    ToList,

    /// <inheritdoc cref="Enumerable.ToHashSet"/>
    ToHashSet,

    /// <inheritdoc cref="Enumerable.ToDictionary"/>
    ToDictionary,

    /// <inheritdoc cref="Enumerable.ToLookup"/>
    ToLookup,

    #endregion

    #region Element Retrieval

    /// <inheritdoc cref="Enumerable.ElementAt"/>
    ElementAt,

    /// <inheritdoc cref="Enumerable.ElementAtOrDefault"/>
    ElementAtOrDefault,

    /// <inheritdoc cref="Enumerable.First"/>
    First,

    /// <inheritdoc cref="Enumerable.FirstOrDefault"/>
    FirstOrDefault,

    /// <inheritdoc cref="Enumerable.Last"/>
    Last,

    /// <inheritdoc cref="Enumerable.LastOrDefault"/>
    LastOrDefault,

    /// <inheritdoc cref="Enumerable.Single"/>
    Single,

    /// <inheritdoc cref="Enumerable.SingleOrDefault"/>
    SingleOrDefault,

    #endregion

    #region Filtering & Projection

    /// <inheritdoc cref="Enumerable.Where"/>
    Where,

    /// <inheritdoc cref="Enumerable.Select"/>
    Select,

    /// <inheritdoc cref="Enumerable.SelectMany"/>
    SelectMany,

    /// <inheritdoc cref="Enumerable.OfType"/>
    OfType,

    /// <inheritdoc cref="Enumerable.Cast"/>
    Cast,

    /// <inheritdoc cref="Enumerable.Skip"/>
    Skip,

    /// <inheritdoc cref="Enumerable.SkipLast"/>
    SkipLast,

    /// <inheritdoc cref="Enumerable.SkipWhile"/>
    SkipWhile,

    /// <inheritdoc cref="Enumerable.Take"/>
    Take,

    /// <inheritdoc cref="Enumerable.TakeLast"/>
    TakeLast,

    /// <inheritdoc cref="Enumerable.TakeWhile"/>
    TakeWhile,

    /// <inheritdoc cref="Enumerable.DefaultIfEmpty"/>
    DefaultIfEmpty,

    /// <inheritdoc cref="Enumerable.Append"/>
    Append,

    /// <inheritdoc cref="Enumerable.Prepend"/>
    Prepend,

    #endregion

    #region Join Operations

    /// <inheritdoc cref="Enumerable.Join"/>
    Join,

    /// <inheritdoc cref="Enumerable.GroupJoin"/>
    GroupJoin,

    #endregion

    #region Ordering

    /// <inheritdoc cref="Enumerable.OrderBy"/>
    OrderBy,

    /// <inheritdoc cref="Enumerable.OrderByDescending"/>
    OrderByDescending,

    #endregion

    #region Set Operations

    /// <inheritdoc cref="Enumerable.Concat"/>
    Concat,

    /// <inheritdoc cref="Enumerable.Distinct"/>
    Distinct,

    /// <inheritdoc cref="Enumerable.DistinctBy"/>
    DistinctBy,

    /// <inheritdoc cref="Enumerable.Except"/>
    Except,

    /// <inheritdoc cref="Enumerable.ExceptBy"/>
    ExceptBy,

    /// <inheritdoc cref="Enumerable.GroupBy"/>
    GroupBy,

    /// <inheritdoc cref="Enumerable.Intersect"/>
    Intersect,

    /// <inheritdoc cref="Enumerable.IntersectBy"/>
    IntersectBy,

    /// <inheritdoc cref="Enumerable.Union"/>
    Union,

    /// <inheritdoc cref="Enumerable.UnionBy"/>
    UnionBy,

    /// <inheritdoc cref="Enumerable.SequenceEqual"/>
    SequenceEqual,

    /// <inheritdoc cref="Enumerable.Zip"/>
    Zip,

    #endregion

    #region Utility

    /// <inheritdoc cref="Enumerable.Any"/>
    Any,

    /// <inheritdoc cref="Enumerable.All"/>
    All,

    /// <inheritdoc cref="Enumerable.TryGetNonEnumeratedCount"/>
    TryGetNonEnumeratedCount,

    /// <inheritdoc cref="System.Array.CopyTo"/>
    CopyTo,

    /// <inheritdoc cref="Enumerable.Chunk"/>
    Chunk,

    /// <inheritdoc cref="Enumerable.Repeat"/>
    Repeat,

    #endregion
}
