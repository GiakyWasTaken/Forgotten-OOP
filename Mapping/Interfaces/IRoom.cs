namespace Forgotten_OOP.Mapping.Interfaces;

#region Using Directives

using Forgotten_OOP.Items.Interfaces;

#endregion

/// <summary>
/// An interface for a room
/// </summary>
public interface IRoom : IEquatable<IRoom>
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
    /// Gets a value indicating whether the current room is designated as a pink room
    /// </summary>
    // Todo: define what a pink room is or change the name to something more descriptive
    public bool IsPinkRoom { get; }
}