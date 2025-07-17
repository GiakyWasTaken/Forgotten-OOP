namespace Forgotten_OOP.Entities.Interfaces;

#region Using Directives

using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Interface representing an enemy in the game
/// </summary>
public interface IEnemy<TRoom> : IEntity<TRoom> where TRoom : IRoom<TRoom>
{
    /// <summary>
    /// Gets or sets the number of player actions before the enemy can act
    /// </summary>
    public int ActionDelay { get; set; }
}