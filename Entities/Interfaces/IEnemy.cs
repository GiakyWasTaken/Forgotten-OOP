namespace Forgotten_OOP.Entities.Interfaces;

/// <summary>
/// Interface representing an enemy in the game
/// </summary>
public interface IEnemy : IEntity
{
    /// <summary>
    /// Gets or sets the number of player actions before the enemy can act
    /// </summary>
    public int ActionDelay { get; set; }
}