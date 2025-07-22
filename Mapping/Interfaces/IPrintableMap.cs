namespace Forgotten_OOP.Mapping.Interfaces;

#region Using Directives

using Forgotten_OOP.Entities.Interfaces;

#endregion

/// <summary>
/// Represents a map that can be printed to the console
/// </summary>
public interface IPrintableMap<TRoom> where TRoom : IRoom<TRoom>
{
    /// <summary>
    /// Prints the map to the console
    /// </summary>
    public void PrintMap(List<IEntity<TRoom>>? entities);
}
