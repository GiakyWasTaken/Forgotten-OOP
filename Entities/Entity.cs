namespace Forgotten_OOP.Entities;

#region Using Directives

using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Enums;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents an entity in the Forgotten OOP game
/// </summary>
public class Entity(string name, Room startingRoom, Map<Room> gameMap) : IEntity<Room>
{
    #region Private Fields

    /// <summary>
    /// Reference to the game map for navigation and room queries
    /// </summary>
    private readonly Map<Room> gameMap = gameMap;

    #endregion

    #region Properties

    /// <inheritdoc />
    public string Name { get; } = name;

    /// <inheritdoc />
    public Room CurrentRoom { get; private set; } = startingRoom;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void Move(Direction dir)
    {
        if (gameMap.TryGetRoomInDirection(CurrentRoom, dir, out Room? nextRoom) && nextRoom != null)
        {
            CurrentRoom = nextRoom;
        }
    }

    /// <inheritdoc />
    public void Move(Room room)
    {
        if (CurrentRoom.GetAdjacentRooms().ContainsValue(room))
        {
            CurrentRoom = room;
        }
    }

    /// <inheritdoc />
    public void Teleport(Room room)
    {
        CurrentRoom = room;
    }

    #endregion
}
