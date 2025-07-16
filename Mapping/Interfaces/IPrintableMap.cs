namespace Forgotten_OOP.Mapping.Interfaces;

/// <summary>
/// Represents a map that can be printed to the console
/// </summary>
/// <typeparam name="TRoom">The type of room contained within the map. Must implement the <see cref="IRoom"/> interface</typeparam>
public interface IPrintableMap<TRoom> where TRoom : IRoom
{
    /// <summary>
    /// Prints the map to the console
    /// </summary>
    public void PrintMap();
}
