namespace Forgotten_OOP.GameManagers;

#region Using Directives

using Forgotten_OOP.GameManagers.Interfaces;
using Forgotten_OOP.Injectables;
using Forgotten_OOP.Injectables.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// The main class for the Forgotten OOP game
/// </summary>
public class GameManager : IGameManager
{
    #region Attributes

    /// <summary>
    /// The game logger
    /// </summary>
    private readonly ILogger logger = ServiceHelper.GetService<ILogger>();

    /// <summary>
    /// The game console
    /// </summary>
    private readonly IConsole console = ServiceHelper.GetService<IConsole>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public Configs GameConfigs { get; }

    /// <inheritdoc />
    public Map GameMap { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="GameManager"/> class with the specified game configurations
    /// </summary>
    /// <param name="gameConfigs">A <see cref="GameConfigs"/> object containing the game configurations</param>
    public GameManager(Configs gameConfigs)
    {
        GameConfigs = gameConfigs;

        GameMap = InitializeMap();

        SpawnItems();

        SpawnEntities();
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void StartGameLoop()
    {
        throw new NotImplementedException("GameManager loop logic is not implemented yet.");
    }

    /// <inheritdoc />
    public void SaveGame()
    {
        throw new NotImplementedException("Save game logic is not implemented yet.");
    }

    /// <inheritdoc />
    public void LoadGame()
    {
        throw new NotImplementedException("Load game logic is not implemented yet.");
    }

    /// <inheritdoc />
    public void EndGame()
    {
        throw new NotImplementedException("End game logic is not implemented yet.");
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Initializes the game map
    /// </summary>
    /// <returns>An <see cref="Map"/> object</returns>
    private Map InitializeMap()
    {
        return new Map();
    }

    /// <summary>
    /// Spawns items in the game world
    /// </summary>
    private void SpawnItems()
    {
        throw new NotImplementedException("Item spawning logic is not implemented yet.");
    }

    /// <summary>
    /// Spawns entities in the game world
    /// </summary>
    private void SpawnEntities()
    {
        throw new NotImplementedException("Entity spawning logic is not implemented yet.");
    }

    #endregion
}