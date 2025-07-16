namespace Forgotten_OOP.Entities;

#region Using Directives

using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents an enemy character within the game, capable of interacting with rooms and the game map
/// </summary>
public class Enemy(string name, IRoom startingRoom, IMap<IRoom> gameMap, int actionDelay) : Entity(name, startingRoom, gameMap), IEnemy
{
    #region Properties

    /// <inheritdoc />
    public int ActionDelay { get; set; } = actionDelay;

    #endregion
}