namespace Forgotten_OOP.Mapping.Interfaces;

#region Using Directives

using Forgotten_OOP.Enums;
using Forgotten_OOP.Items.Interfaces;

#endregion

/// <summary>
/// An interface for a room
/// </summary>
public interface IRoom<TSelf> : IEquatable<TSelf> where TSelf : IRoom<TSelf>
{
    /// <summary>
    /// Gets or sets the collection of items currently on the ground
    /// </summary>
    public Stack<IItem> ItemsOnGround { get; }

    /// <summary>
    /// Gets a value indicating whether the current room is the starting room
    /// </summary>
    public bool IsStartingRoom { get; }

    /// <summary>
    /// Gets a value indicating whether the current room is designated as an enemy spawning room
    /// </summary>
    public bool IsEnemySpawningRoom { get; }

    /// <summary>
    /// Gets a value indicating whether the current room is designated as a pink room
    /// </summary>
    // Todo: define what a pink room or create a derived interface or class for pink rooms
    public bool IsPinkRoom { get; }

    /// <summary>
    /// Gets the coordinates of this room within the map
    /// </summary>
    /// <returns>A <see cref="Tuple{T1, T2}" /> containing the X and Y coordinates, or null if map is not available</returns>
    public Tuple<int, int>? GetCoordinates();

    /// <summary>
    /// Gets adjacent rooms in all directions
    /// </summary>
    /// <returns>A <see cref="Dictionary{TKey, TValue}"/> of <see cref="Direction"/> and their corresponding <see cref="TSelf"/></returns>
    public Dictionary<Direction, TSelf> GetAdjacentRooms();
}