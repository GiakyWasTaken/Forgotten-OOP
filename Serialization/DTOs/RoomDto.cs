namespace Forgotten_OOP.Serialization.DTOs;

#region Using Directives

using System.Collections.Generic;

#endregion

/// <summary>
/// Data transfer object representing a room in the game map
/// </summary>
public class RoomDto
{
    #region Properties

    /// <summary>
    /// Gets or sets the unique identifier of the room
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the X coordinate of the room in the map
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate of the room in the map
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this room is a pink room (special room type)
    /// </summary>
    public bool IsPinkRoom { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this room is the starting room
    /// </summary>
    public bool IsStartingRoom { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this room can spawn enemies
    /// </summary>
    public bool IsEnemySpawningRoom { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this room is initially closed
    /// </summary>
    public bool IsClosed { get; set; }

    /// <summary>
    /// Gets or sets the collection of item type names that are on the ground in this room
    /// </summary>
    public List<string> ItemsOnGround { get; set; } = [];

    #endregion
}
