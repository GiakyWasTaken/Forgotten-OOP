namespace Forgotten_OOP.Entities;

#region Using Directives

using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Enums;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents an entity in the Forgotten OOP game
/// </summary>
public class Entity(string name, IRoom startingRoom, IMap<IRoom> gameMap) : IEntity
{
    #region Private Fields

    /// <summary>
    /// Reference to the game map for navigation and room queries
    /// </summary>
    private readonly IMap<IRoom> gameMap = gameMap;

    #endregion

    #region Properties

    /// <inheritdoc />
    public string Name { get; } = name;

    /// <inheritdoc />
    public IRoom CurrentRoom { get; private set; } = startingRoom;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void Move(Direction dir)
    {
        if (gameMap.TryGetRoomInDirection(CurrentRoom, dir, out IRoom? nextRoom) && nextRoom != null)
        {
            CurrentRoom = nextRoom;
        }
    }

    /// <inheritdoc />
    public void Teleport(IRoom room)
    {
        CurrentRoom = room;
    }

    #endregion
}
