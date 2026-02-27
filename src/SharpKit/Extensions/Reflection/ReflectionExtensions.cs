namespace SharpKit;

/// <summary>
///     Provides useful extensions for supported reflection contexts.
/// </summary>
public static class ReflectionExtensions
{
    extension(ICustomAttributeProvider attributeProvider)
    {
        /// <summary>
        ///     Returns a list of all attributes defined on this provider, excluding named attributes, in a strongly typed manner.
        /// </summary>
        /// <param name="inherit"> When true, look up the hierachy chain for inherited attributes as well. </param>
        /// <returns> An <see cref="IEnumerable{T}"/> of attributes, or an empty array. </returns>
        /// <exception cref="TypeLoadException"/>
        public IEnumerable<Attribute> GetAttributes(bool inherit = false) => attributeProvider.GetCustomAttributes(inherit).OfType<Attribute>();
    }
}
