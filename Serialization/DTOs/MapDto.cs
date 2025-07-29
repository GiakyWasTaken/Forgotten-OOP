namespace Forgotten_OOP.Serialization.DTOs;

#region Using Directives

using System.Collections.Generic;

#endregion

/// <summary>
/// Data transfer object representing the game map and its room layout
/// </summary>
public class MapDto<TRoomDto> where TRoomDto : RoomDto
{
    #region Properties

    /// <summary>
    /// Gets or sets the dimension of the map (width and height)
    /// </summary>
    public int MapDimension { get; set; }

    /// <summary>
    /// Gets or sets the collection of rooms in the map
    /// </summary>
    public List<TRoomDto> Rooms { get; set; } = [];

    /// <summary>
    /// Gets or sets the ID of the starting room
    /// </summary>
    public long StartingRoomId { get; set; }

    #endregion
}
