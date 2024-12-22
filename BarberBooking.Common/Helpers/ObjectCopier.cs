using System.Reflection;

namespace BarberBooking.Common.Helpers;

public static class ObjectCopier
{
    public static void CopyProperties<TSource, TDestination>(TSource source, TDestination destination)
        where TSource : class
        where TDestination : class
    {
        if (source == null || destination == null)
            throw new ArgumentNullException($"Source or Destination cannot be null");

        // Get all properties of source and destination objects
        var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Create a dictionary for quick lookup of destination properties by name
        var destPropertiesDict = new Dictionary<string, PropertyInfo>();
        foreach (var destProp in destinationProperties)
        {
            if (destProp.CanWrite) // Only writable properties
                destPropertiesDict[destProp.Name] = destProp;
        }

        // Copy values from source to destination
        foreach (var sourceProp in sourceProperties)
        {
            if (sourceProp.CanRead && destPropertiesDict.TryGetValue(sourceProp.Name, out var destProp))
            {
                if (destProp.PropertyType == sourceProp.PropertyType) // Match only same type properties
                {
                    var value = sourceProp.GetValue(source);
                    destProp.SetValue(destination, value);
                }
            }
        }
    }

    // Overload that takes a source object and returns a new destination object with copied properties
    public static TDestination CopyProperties<TSource, TDestination>(TSource source)
        where TSource : class
        where TDestination : class, new()
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        // Create a new instance of the destination type
        var destination = new TDestination();

        CopyProperties(source, destination);

        return destination;
    }
}
