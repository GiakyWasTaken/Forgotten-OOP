namespace Forgotten_OOP.GameManagers.Interfaces;

#region Using Directives

using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Interface for the game manager
/// </summary>
public interface IGameManager
{
    /// <summary>
    /// The configuration settings for the game
    /// </summary>
    public Configs GameConfigs { get; }

    /// <summary>
    /// The map of the game, containing all rooms
    /// </summary>
    public Map GameMap { get; }

    /// <summary>
    /// Starts the game loop
    /// </summary>
    public void StartGameLoop();

    /// <summary>
    /// Saves the current game state
    /// </summary>
    public void SaveGame();

    /// <summary>
    /// Loads a saved game state
    /// </summary>
    public void LoadGame();

    /// <summary>
    /// Ends the game and returns to the main menu
    /// </summary>
    public void EndGame();
}