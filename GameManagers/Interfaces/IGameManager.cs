namespace Forgotten_OOP.GameManagers.Interfaces;

#region Using Directives

using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Interface for the game manager
/// </summary>
public interface IGameManager<out TPlayer, TEntity, out TMap, TRoom>
    where TPlayer : IPlayer<TRoom>
    where TEntity : IEntity<TRoom>
    where TMap : IMap<TRoom>
    where TRoom : IRoom<TRoom>
{
    /// <summary>
    /// Gets a value indicating whether the game is currently running
    /// </summary>
    public bool IsGameRunning { get; }

    /// <summary>
    /// The configuration settings for the game
    /// </summary>
    public Configs GameConfigs { get; }

    /// <summary>
    /// The player in the game
    /// </summary>
    public TPlayer Player { get; }

    /// <summary>
    /// Gets the collection of entities managed by this instance
    /// </summary>
    public List<TEntity> Entities { get; }

    /// <summary>
    /// The map of the game, containing all rooms
    /// </summary>
    public TMap GameMap { get; }

    /// <summary>
    /// Gets the total number of actions performed
    /// </summary>
    public long ActionsCount { get; }

    /// <summary>
    /// Starts the game loop
    /// </summary>
    public void StartGameLoop();

    /// <summary>
    /// Increments the count of actions performed in the game
    /// </summary>
    public void IncrementActionsCount();

    /// <summary>
    /// Pauses the game and returns to the main menu
    /// </summary>
    public void PauseGame();

    /// <summary>
    /// Ends the game and returns to the main menu
    /// </summary>
    public void EndGame();
}