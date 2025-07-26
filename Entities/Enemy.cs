namespace Forgotten_OOP.Entities;

#region Using Directives

using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents an enemy character within the game, capable of interacting with rooms and the game map
/// </summary>
public class Enemy(string name, Room startingRoom, Map<Room> gameMap, int actionDelay) : Entity(name, startingRoom, gameMap), IEnemy<Room>
{
    #region Properties

    /// <inheritdoc />
    public int ActionDelay { get; set; } = actionDelay;

    /// <inheritdoc />
    public Room PreviousRoom { get; protected set; } = startingRoom;

    /// <inheritdoc />
    public override Room CurrentRoom
    {
        get => base.CurrentRoom;
        protected set
        {
            PreviousRoom = base.CurrentRoom;
            base.CurrentRoom = value;
        }
    }

    #endregion
}
