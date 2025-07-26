namespace Forgotten_OOP.Entities.Interfaces;

#region Using Directives

using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents the Marlo entity in the game, which is a good character that can teleport to the initial room
/// </summary>
public interface IMarlo<TRoom> : IEntity<TRoom> where TRoom : IRoom<TRoom>
{
    /// <summary>
    /// Moves the entity back to his initial room of the game
    /// </summary>
    public void ReturnToInitialRoom();
}
