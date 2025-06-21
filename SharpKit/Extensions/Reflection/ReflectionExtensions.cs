namespace SharpKit;

public static class ReflectionExtensions
{
    public static IEnumerable<Attribute> GetAttributes(this ICustomAttributeProvider attributeProvider, bool inherit = false)
        => attributeProvider.GetCustomAttributes(inherit).OfType<Attribute>();
}
